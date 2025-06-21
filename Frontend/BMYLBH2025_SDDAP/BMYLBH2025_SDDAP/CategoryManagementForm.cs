using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using BMYLBH2025_SDDAP.Models;
using BMYLBH2025_SDDAP.Services;

namespace BMYLBH2025_SDDAP
{
    public partial class CategoryManagementForm : Form
    {
        #region Fields

        private readonly ApiService _apiService;
        private List<Category> _categories;
        private List<Category> _filteredCategories;
        private Category _selectedCategory;
        private bool _isEditing;
        private bool _isAdding;

        #endregion

        #region Constructor

        public CategoryManagementForm(ApiService apiService)
        {
            _apiService = apiService ?? throw new ArgumentNullException(nameof(apiService));
            InitializeComponent();
            InitializeFormSettings();
        }

        #endregion

        #region Initialization

        private void InitializeFormSettings()
        {
            // Set form properties
            this.WindowState = FormWindowState.Normal;
            
            // Style the DataGridView
            StyleDataGridView();
            
            // Set initial state
            SetViewMode();
            
            // Set placeholder text for search
            txtSearch.Text = "🔍 Search categories...";
            txtSearch.ForeColor = Color.Gray;
            txtSearch.Enter += (s, e) => {
                if (txtSearch.Text == "🔍 Search categories...")
                {
                    txtSearch.Text = "";
                    txtSearch.ForeColor = Color.Black;
                }
            };
            txtSearch.Leave += (s, e) => {
                if (string.IsNullOrWhiteSpace(txtSearch.Text))
                {
                    txtSearch.Text = "🔍 Search categories...";
                    txtSearch.ForeColor = Color.Gray;
                }
            };
        }

        private void StyleDataGridView()
        {
            // Configure DataGridView appearance
            dgvCategories.BackgroundColor = Color.White;
            dgvCategories.BorderStyle = BorderStyle.None;
            dgvCategories.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvCategories.DefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 123, 255);
            dgvCategories.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvCategories.DefaultCellStyle.BackColor = Color.White;
            dgvCategories.DefaultCellStyle.ForeColor = Color.FromArgb(33, 37, 41);
            dgvCategories.DefaultCellStyle.Font = new Font("Segoe UI", 9F);
            dgvCategories.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(248, 249, 250);
            dgvCategories.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(73, 80, 87);
            dgvCategories.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dgvCategories.ColumnHeadersHeight = 35;
            dgvCategories.RowTemplate.Height = 30;
            dgvCategories.EnableHeadersVisualStyles = false;
            dgvCategories.GridColor = Color.FromArgb(222, 226, 230);
        }

        #endregion

        #region Form Events

        private async void CategoryManagementForm_Load(object sender, EventArgs e)
        {
            await LoadCategoriesAsync();
        }

        #endregion

        #region Data Loading

        private async Task LoadCategoriesAsync()
        {
            try
            {
                // Show loading state
                this.Cursor = Cursors.WaitCursor;
                btnRefresh.Text = "Loading...";
                btnRefresh.Enabled = false;

                // Load categories from API
                _categories = await _apiService.GetAllCategoriesAsync();
                _filteredCategories = new List<Category>(_categories);

                // Update the grid
                UpdateCategoriesGrid();

                // Show success feedback
                this.Text = $"Category Management - {_categories.Count} categories loaded";
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Error loading categories: {ex.Message}");
            }
            finally
            {
                // Reset loading state
                this.Cursor = Cursors.Default;
                btnRefresh.Text = "🔄 Refresh";
                btnRefresh.Enabled = true;
            }
        }

        private void UpdateCategoriesGrid()
        {
            if (dgvCategories == null || _filteredCategories == null) return;

            var displayData = _filteredCategories.Select(c => new
            {
                ID = c.CategoryID,
                Name = c.Name,
                Description = c.Description,
                CreatedAt = c.CreatedAt.ToString("yyyy-MM-dd")
            }).ToList();

            dgvCategories.DataSource = displayData;

            // Configure column headers and widths
            if (dgvCategories.Columns.Count > 0)
            {
                dgvCategories.Columns["ID"].HeaderText = "ID";
                dgvCategories.Columns["ID"].Width = 60;
                dgvCategories.Columns["Name"].HeaderText = "Category Name";
                dgvCategories.Columns["Name"].Width = 200;
                dgvCategories.Columns["Description"].HeaderText = "Description";
                dgvCategories.Columns["Description"].Width = 300;
                dgvCategories.Columns["CreatedAt"].HeaderText = "Created";
                dgvCategories.Columns["CreatedAt"].Width = 100;
            }
        }

        #endregion

        #region Search and Filter

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            FilterCategories();
        }

        private void FilterCategories()
        {
            if (_categories == null) return;

            string searchTerm = txtSearch.Text.Trim();
            if (string.IsNullOrWhiteSpace(searchTerm) || searchTerm == "🔍 Search categories...")
            {
                _filteredCategories = new List<Category>(_categories);
            }
            else
            {
                _filteredCategories = _categories.Where(c =>
                    (c.Name?.ToLower().Contains(searchTerm.ToLower()) ?? false) ||
                    (c.Description?.ToLower().Contains(searchTerm.ToLower()) ?? false)
                ).ToList();
            }

            UpdateCategoriesGrid();
        }

        #endregion

        #region Category Selection

        private async Task ReselectCategoryById(int categoryId)
        {
            try
            {
                // Find the category in the updated list
                var categoryToSelect = _categories?.FirstOrDefault(c => c.CategoryID == categoryId);
                if (categoryToSelect == null) return;

                // Find the corresponding row in the grid
                for (int i = 0; i < dgvCategories.Rows.Count; i++)
                {
                    var row = dgvCategories.Rows[i];
                    if (row.Cells["ID"].Value == null || (int)row.Cells["ID"].Value != categoryId) continue;
                    // Clear selection first
                    dgvCategories.ClearSelection();
                        
                    // Select the row
                    row.Selected = true;
                    dgvCategories.CurrentCell = row.Cells[0];
                        
                    // Update the selected category
                    _selectedCategory = categoryToSelect;
                    await DisplayCategoryDetailsAsync(_selectedCategory);
                        
                    break;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error reselecting category: {ex.Message}");
                // If reselection fails, just clear the selection
                ClearCategoryDetails();
            }
        }

        private async void dgvCategories_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                // Skip if we're in edit/add mode to prevent interference
                if (_isEditing || _isAdding) return;
                
                if (dgvCategories.SelectedRows.Count > 0)
                {
                    var selectedRow = dgvCategories.SelectedRows[0];
                    
                    // Safely get the category ID
                    if (selectedRow.Cells["ID"].Value != null)
                    {
                        var categoryId = (int)selectedRow.Cells["ID"].Value;
                        
                        // Find the category in our list
                        _selectedCategory = _categories?.FirstOrDefault(c => c.CategoryID == categoryId);
                        
                        if (_selectedCategory != null)
                        {
                            await DisplayCategoryDetailsAsync(_selectedCategory);
                        }
                        else
                        {
                            // Category not found, clear details
                            ClearCategoryDetails();
                        }
                    }
                    else
                    {
                        ClearCategoryDetails();
                    }
                }
                else
                {
                    _selectedCategory = null;
                    ClearCategoryDetails();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in selection changed: {ex.Message}");
                ClearCategoryDetails();
            }
        }

        private async Task DisplayCategoryDetailsAsync(Category category)
        {
            if (category == null) return;

            // Display basic info
            txtCategoryId.Text = category.CategoryID.ToString();
            txtName.Text = category.Name ?? string.Empty;
            txtDescription.Text = category.Description ?? string.Empty;

            // Load and display statistics
            await LoadCategoryStatisticsAsync(category.CategoryID);

            // Enable action buttons
            btnEdit.Enabled = true;
            btnDelete.Enabled = true;
        }

        private async Task LoadCategoryStatisticsAsync(int categoryId)
        {
            try
            {
                // Load product count and total value in parallel
                var productCountTask = _apiService.GetCategoryProductCountAsync(categoryId);
                var totalValueTask = _apiService.GetCategoryTotalValueAsync(categoryId);

                await Task.WhenAll(productCountTask, totalValueTask);

                var productCount = await productCountTask;
                var totalValue = await totalValueTask;

                // Update UI
                lblProductCount.Text = productCount.ToString();
                lblTotalValue.Text = totalValue.ToString("C");
            }
            catch (Exception ex)
            {
                // Handle error gracefully
                lblProductCount.Text = "Error";
                lblTotalValue.Text = "Error";
                System.Diagnostics.Debug.WriteLine($"Error loading category statistics: {ex.Message}");
            }
        }

        private void ClearCategoryDetails()
        {
            txtCategoryId.Text = string.Empty;
            txtName.Text = string.Empty;
            txtDescription.Text = string.Empty;
            lblProductCount.Text = "0";
            lblTotalValue.Text = "$0.00";
            
            btnEdit.Enabled = false;
            btnDelete.Enabled = false;
        }

        #endregion

        #region CRUD Operations

        private void btnAdd_Click(object sender, EventArgs e)
        {
            StartAddMode();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (_selectedCategory != null && !_isEditing && !_isAdding)
            {
                StartEditMode();
            }
            else if (_selectedCategory == null)
            {
                ShowInfoMessage("Please select a category to edit.");
            }
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (_isAdding)
            {
                await SaveNewCategoryAsync();
            }
            else if (_isEditing)
            {
                await SaveCategoryChangesAsync();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            CancelEdit();
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (_selectedCategory != null)
            {
                await DeleteCategoryAsync(_selectedCategory);
            }
        }

        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            await LoadCategoriesAsync();
        }

        #endregion

        #region Add/Edit Mode Management

        private void StartAddMode()
        {
            _isAdding = true;
            _isEditing = false;
            _selectedCategory = null;

            ClearCategoryDetails();
            SetEditMode();
            
            txtName.Focus();
            lblDetailsTitle.Text = "Add New Category";
        }

        private void StartEditMode()
        {
            if (_selectedCategory == null)
            {
                ShowErrorMessage("No category selected for editing.");
                return;
            }
            
            _isEditing = true;
            _isAdding = false;
            
            SetEditMode();
            lblDetailsTitle.Text = $"Edit Category: {_selectedCategory.Name}";
            
            // Ensure the form fields are populated with the selected category data
            txtName.Text = _selectedCategory.Name ?? string.Empty;
            txtDescription.Text = _selectedCategory.Description ?? string.Empty;
            txtCategoryId.Text = _selectedCategory.CategoryID.ToString();
        }

        private void SetEditMode()
        {
            // Enable form fields
            txtName.ReadOnly = false;
            txtDescription.ReadOnly = false;
            
            // Show/hide buttons
            btnEdit.Visible = false;
            btnDelete.Visible = false;
            btnSave.Visible = true;
            btnCancel.Visible = true;
            
            // Disable grid interaction
            dgvCategories.Enabled = false;
            btnAdd.Enabled = false;
            btnRefresh.Enabled = false;
        }

        private void SetViewMode()
        {
            _isEditing = false;
            _isAdding = false;
            
            // Disable form fields
            txtName.ReadOnly = true;
            txtDescription.ReadOnly = true;
            
            // Show/hide buttons
            btnEdit.Visible = true;
            btnDelete.Visible = true;
            btnSave.Visible = false;
            btnCancel.Visible = false;
            
            // Enable grid interaction
            dgvCategories.Enabled = true;
            btnAdd.Enabled = true;
            btnRefresh.Enabled = true;
            
            lblDetailsTitle.Text = "Category Details";
        }

        private async void CancelEdit()
        {
            SetViewMode();
            
            if (_selectedCategory != null)
            {
                await DisplayCategoryDetailsAsync(_selectedCategory);
            }
            else
            {
                ClearCategoryDetails();
            }
        }

        #endregion

        #region Save Operations

        private async Task SaveNewCategoryAsync()
        {
            try
            {
                // Validate input
                if (!ValidateInput())
                    return;

                // Show loading state
                btnSave.Text = "Saving...";
                btnSave.Enabled = false;

                // Create new category
                var newCategory = new Category
                {
                    Name = txtName.Text.Trim(),
                    Description = txtDescription.Text.Trim()
                };

                // Save to API
                var result = await _apiService.CreateCategoryAsync(newCategory);

                if (result != null)
                {
                    ShowSuccessMessage("Category created successfully!");
                    await LoadCategoriesAsync(); // Refresh the list
                    
                    // Select the newly created category
                    await ReselectCategoryById(result.CategoryID);
                    
                    SetViewMode();
                }
                else
                {
                    ShowErrorMessage("Failed to create category. Please try again.");
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Error creating category: {ex.Message}");
            }
            finally
            {
                btnSave.Text = "💾 Save";
                btnSave.Enabled = true;
            }
        }

        private async Task SaveCategoryChangesAsync()
        {
            try
            {
                // Validate input
                if (!ValidateInput())
                    return;

                // Show loading state
                btnSave.Text = "Saving...";
                btnSave.Enabled = false;

                // Store the category ID for reselection after refresh
                var categoryIdToReselect = _selectedCategory.CategoryID;

                // Update category
                _selectedCategory.Name = txtName.Text.Trim();
                _selectedCategory.Description = txtDescription.Text.Trim();

                // Save to API
                var result = await _apiService.UpdateCategoryAsync(_selectedCategory.CategoryID, _selectedCategory);

                if (result != null)
                {
                    ShowSuccessMessage("Category updated successfully!");
                    await LoadCategoriesAsync(); // Refresh the list
                    
                    // Reselect the updated category
                    await ReselectCategoryById(categoryIdToReselect);
                    
                    SetViewMode();
                }
                else
                {
                    ShowErrorMessage("Failed to update category. Please try again.");
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Error updating category: {ex.Message}");
            }
            finally
            {
                btnSave.Text = "💾 Save";
                btnSave.Enabled = true;
            }
        }

        #endregion

        #region Delete Operation

        private async Task DeleteCategoryAsync(Category category)
        {
            try
            {
                // Check if category has products
                var productCount = await _apiService.GetCategoryProductCountAsync(category.CategoryID);
                
                string message = productCount > 0 
                    ? $"This category contains {productCount} product(s). Are you sure you want to delete it?\n\nCategory: {category.Name}"
                    : $"Are you sure you want to delete this category?\n\nCategory: {category.Name}";

                var result = MessageBox.Show(
                    message,
                    "Confirm Delete",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

                if (result == DialogResult.Yes)
                {
                    // Show loading state
                    btnDelete.Text = "Deleting...";
                    btnDelete.Enabled = false;

                    // Delete from API
                    var success = await _apiService.DeleteCategoryAsync(category.CategoryID);

                    if (success)
                    {
                        ShowSuccessMessage("Category deleted successfully!");
                        await LoadCategoriesAsync(); // Refresh the list
                        ClearCategoryDetails();
                    }
                    else
                    {
                        ShowErrorMessage("Failed to delete category. Please try again.");
                    }
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Error deleting category: {ex.Message}");
            }
            finally
            {
                btnDelete.Text = "🗑️ Delete";
                btnDelete.Enabled = true;
            }
        }

        #endregion

        #region Validation

        private bool ValidateInput()
        {
            // Check category name
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                ShowErrorMessage("Category name is required.");
                txtName.Focus();
                return false;
            }

            if (txtName.Text.Trim().Length < 2)
            {
                ShowErrorMessage("Category name must be at least 2 characters long.");
                txtName.Focus();
                return false;
            }

            if (txtName.Text.Trim().Length > 100)
            {
                ShowErrorMessage("Category name cannot exceed 100 characters.");
                txtName.Focus();
                return false;
            }

            // Check for duplicate names (only when adding or changing name)
            string newName = txtName.Text.Trim();
            bool isNameChanged = _selectedCategory == null || _selectedCategory.Name != newName;
            
            if (isNameChanged && _categories != null)
            {
                bool nameExists = _categories.Any(c => 
                    c.Name.Equals(newName, StringComparison.OrdinalIgnoreCase) && 
                    (_selectedCategory == null || c.CategoryID != _selectedCategory.CategoryID));
                
                if (nameExists)
                {
                    ShowErrorMessage("A category with this name already exists.");
                    txtName.Focus();
                    return false;
                }
            }

            // Check description length
            if (!string.IsNullOrWhiteSpace(txtDescription.Text) && txtDescription.Text.Trim().Length > 500)
            {
                ShowErrorMessage("Description cannot exceed 500 characters.");
                txtDescription.Focus();
                return false;
            }

            return true;
        }

        #endregion

        #region UI Helper Methods

        private void ShowSuccessMessage(string message)
        {
            MessageBox.Show(message, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ShowErrorMessage(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void ShowInfoMessage(string message)
        {
            MessageBox.Show(message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #endregion
    }
} 