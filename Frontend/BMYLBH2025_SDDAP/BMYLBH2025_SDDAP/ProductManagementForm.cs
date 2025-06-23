using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using BMYLBH2025_SDDAP.Models;
using BMYLBH2025_SDDAP.Services;

namespace BMYLBH2025_SDDAP
{
    public partial class ProductManagementForm : Form
    {
        #region Fields

        private readonly ApiService _apiService;
        private List<Product> _products;
        private List<Product> _filteredProducts;
        private List<Category> _categories;
        private Product _selectedProduct;
        private bool _isEditing;
        private bool _isAdding;

        #endregion

        #region Constructor

        public ProductManagementForm(ApiService apiService)
        {
            _apiService = apiService ?? throw new ArgumentNullException(nameof(apiService));
            InitializeComponent();
            InitializeFormSettings();
        }

        #endregion

        #region Initialization

        private void InitializeFormSettings()
        {
            try
            {
                // Set form properties
                this.WindowState = FormWindowState.Maximized;
                this.ShowInTaskbar = false;

                // Initialize data
                _products = new List<Product>();
                _filteredProducts = new List<Product>();
                _categories = new List<Category>();

                // Initialize ComboBox with default item to prevent SelectedIndex errors
                cmbCategory.Items.Clear();
                cmbCategory.Items.Add(new { CategoryID = 0, Name = "-- Loading Categories --" });
                cmbCategory.DisplayMember = "Name";
                cmbCategory.ValueMember = "CategoryID";
                if (cmbCategory.Items.Count > 0)
                {
                    cmbCategory.SelectedIndex = 0;
                }

                // Style the DataGridView
                StyleDataGridView();

                // Set initial state
                SetViewMode();
                ClearProductDetails();
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Error initializing form settings: {ex.Message}");
            }
        }

        private void StyleDataGridView()
        {
            dgvProducts.EnableHeadersVisualStyles = false;
            dgvProducts.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(52, 152, 219);
            dgvProducts.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvProducts.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgvProducts.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(250, 250, 250);
            dgvProducts.DefaultCellStyle.SelectionBackColor = Color.FromArgb(52, 152, 219);
            dgvProducts.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvProducts.ColumnHeadersHeight = 35;
            dgvProducts.RowTemplate.Height = 30;
            dgvProducts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        #endregion

        #region Form Events

        private async void ProductManagementForm_Load(object sender, EventArgs e)
        {
            await LoadDataAsync();
        }

        #endregion

        #region Data Loading

        private async Task LoadDataAsync()
        {
            await LoadProductsAsync();
            await LoadCategoriesAsync();
        }

        private async Task LoadProductsAsync()
        {
            try
            {
                // Show loading state
                this.Cursor = Cursors.WaitCursor;
                btnRefresh.Text = "Loading...";
                btnRefresh.Enabled = false;

                // Load products from API
                _products = await _apiService.GetAllProductsAsync() ?? new List<Product>();
                _filteredProducts = new List<Product>(_products);

                // Update the grid
                UpdateProductsGrid();

                // Show success feedback
                this.Text = $"Product Management - {_products.Count} products loaded";
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Error loading products: {ex.Message}");
            }
            finally
            {
                // Reset loading state
                this.Cursor = Cursors.Default;
                btnRefresh.Text = "🔄 Refresh";
                btnRefresh.Enabled = true;
            }
        }

        private async Task LoadCategoriesAsync()
        {
            try
            {
                _categories = await _apiService.GetAllCategoriesAsync() ?? new List<Category>();
                UpdateCategoryComboBox();
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Error loading categories: {ex.Message}");
            }
        }

        private void UpdateCategoryComboBox()
        {
            cmbCategory.Items.Clear();
            cmbCategory.Items.Add(new { CategoryID = 0, Name = "-- Select Category --" });
            
            foreach (var category in _categories)
            {
                cmbCategory.Items.Add(new { CategoryID = category.CategoryID, Name = category.Name });
            }
            
            cmbCategory.DisplayMember = "Name";
            cmbCategory.ValueMember = "CategoryID";
            
            // Safely set selected index
            if (cmbCategory.Items.Count > 0)
            {
                cmbCategory.SelectedIndex = 0;
            }
        }

        private void UpdateProductsGrid()
        {
            if (dgvProducts == null || _filteredProducts == null) return;

            var displayData = _filteredProducts.Select(p => new
            {
                ID = p.ProductID,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price.ToString("C"),
                Category = p.Category?.Name ?? "Uncategorized",
                MinStock = p.MinimumStockLevel
            }).ToList();

            dgvProducts.DataSource = displayData;

            // Configure column headers and widths
            if (dgvProducts.Columns.Count > 0)
            {
                dgvProducts.Columns["ID"].HeaderText = "ID";
                dgvProducts.Columns["ID"].Width = 60;
                dgvProducts.Columns["Name"].HeaderText = "Product Name";
                dgvProducts.Columns["Name"].Width = 250;
                dgvProducts.Columns["Description"].HeaderText = "Description";
                dgvProducts.Columns["Description"].Width = 300;
                dgvProducts.Columns["Price"].HeaderText = "Price";
                dgvProducts.Columns["Price"].Width = 100;
                dgvProducts.Columns["Category"].HeaderText = "Category";
                dgvProducts.Columns["Category"].Width = 150;
                dgvProducts.Columns["MinStock"].HeaderText = "Min Stock";
                dgvProducts.Columns["MinStock"].Width = 100;
            }
        }

        #endregion

        #region Search and Filter

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            FilterProducts();
        }

        private void txtSearch_Enter(object sender, EventArgs e)
        {
            if (txtSearch.Text == "🔍 Search products...")
            {
                txtSearch.Text = "";
                txtSearch.ForeColor = Color.Black;
            }
        }

        private void txtSearch_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                txtSearch.Text = "🔍 Search products...";
                txtSearch.ForeColor = Color.Gray;
                FilterProducts();
            }
        }

        private void FilterProducts()
        {
            if (_products == null) return;

            string searchTerm = txtSearch.Text.Trim();
            if (string.IsNullOrWhiteSpace(searchTerm) || searchTerm == "🔍 Search products...")
            {
                _filteredProducts = new List<Product>(_products);
            }
            else
            {
                _filteredProducts = _products.Where(p =>
                    (p.Name?.ToLower().Contains(searchTerm.ToLower()) ?? false) ||
                    (p.Description?.ToLower().Contains(searchTerm.ToLower()) ?? false) ||
                    (p.Category?.Name?.ToLower().Contains(searchTerm.ToLower()) ?? false)
                ).ToList();
            }

            UpdateProductsGrid();
        }

        #endregion

        #region Product Selection

        private async void dgvProducts_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                // Skip if we're in edit/add mode to prevent interference
                if (_isEditing || _isAdding) return;
                
                if (dgvProducts.SelectedRows.Count > 0)
                {
                    var selectedRow = dgvProducts.SelectedRows[0];
                    
                    // Safely get the product ID
                    if (selectedRow.Cells["ID"].Value != null)
                    {
                        var productId = (int)selectedRow.Cells["ID"].Value;
                        
                        // Find the product in our list
                        _selectedProduct = _products?.FirstOrDefault(p => p.ProductID == productId);
                        
                        if (_selectedProduct != null)
                        {
                            await DisplayProductDetailsAsync(_selectedProduct);
                        }
                        else
                        {
                            // Product not found, clear details
                            ClearProductDetails();
                        }
                    }
                    else
                    {
                        ClearProductDetails();
                    }
                }
                else
                {
                    _selectedProduct = null;
                    ClearProductDetails();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in selection changed: {ex.Message}");
                ClearProductDetails();
            }
        }

        private async Task DisplayProductDetailsAsync(Product product)
        {
            if (product == null) return;

            // Display basic info
            txtProductId.Text = product.ProductID.ToString();
            txtName.Text = product.Name ?? string.Empty;
            txtDescription.Text = product.Description ?? string.Empty;
            txtPrice.Text = product.Price.ToString("F2");
            txtMinimumStock.Text = product.MinimumStockLevel.ToString();
            
            // Load current inventory quantity
            var inventory = await _apiService.GetInventoryByProductIdAsync(product.ProductID);
            txtInventoryQuantity.Text = inventory?.Quantity.ToString() ?? "0";

            // Set category selection
            if (product.CategoryID > 0 && cmbCategory.Items.Count > 0)
            {
                for (int i = 0; i < cmbCategory.Items.Count; i++)
                {
                    var item = cmbCategory.Items[i] as dynamic;
                    if (item?.CategoryID == product.CategoryID)
                    {
                        cmbCategory.SelectedIndex = i;
                        break;
                    }
                }
            }
            else if (cmbCategory.Items.Count > 0)
            {
                cmbCategory.SelectedIndex = 0;
            }

            // Load and display inventory statistics
            await LoadProductStatisticsAsync(product.ProductID);

            // Enable action buttons
            btnEdit.Enabled = true;
            btnDelete.Enabled = true;
        }

        private async Task LoadProductStatisticsAsync(int productId)
        {
            try
            {
                // Load inventory information
                var inventory = await _apiService.GetInventoryByProductIdAsync(productId);

                if (inventory != null)
                {
                    lblInventoryQuantity.Text = inventory.Quantity.ToString();
                    
                    // Determine stock status
                    var minStock = _selectedProduct?.MinimumStockLevel ?? 0;
                    if (inventory.Quantity <= 0)
                    {
                        lblStockStatus.Text = "🔴 Out of Stock";
                        lblStockStatus.ForeColor = Color.FromArgb(220, 53, 69);
                    }
                    else if (inventory.Quantity <= minStock)
                    {
                        lblStockStatus.Text = "⚠️ Low Stock";
                        lblStockStatus.ForeColor = Color.FromArgb(255, 193, 7);
                    }
                    else
                    {
                        lblStockStatus.Text = "✅ In Stock";
                        lblStockStatus.ForeColor = Color.FromArgb(40, 167, 69);
                    }
                }
                else
                {
                    lblInventoryQuantity.Text = "0";
                    lblStockStatus.Text = "❌ No Inventory";
                    lblStockStatus.ForeColor = Color.FromArgb(108, 117, 125);
                }
            }
            catch (Exception ex)
            {
                // Handle error gracefully
                lblInventoryQuantity.Text = "Error";
                lblStockStatus.Text = "Error";
                lblStockStatus.ForeColor = Color.FromArgb(220, 53, 69);
                System.Diagnostics.Debug.WriteLine($"Error loading product statistics: {ex.Message}");
            }
        }

        private void ClearProductDetails()
        {
            txtProductId.Text = string.Empty;
            txtName.Text = string.Empty;
            txtDescription.Text = string.Empty;
            txtPrice.Text = string.Empty;
            txtMinimumStock.Text = string.Empty;
            txtInventoryQuantity.Text = "0";
            
            // Safely reset category selection
            if (cmbCategory.Items.Count > 0)
            {
                cmbCategory.SelectedIndex = 0;
            }
            
            lblInventoryQuantity.Text = "0";
            lblStockStatus.Text = "No Data";
            lblStockStatus.ForeColor = Color.FromArgb(108, 117, 125);
            
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
            if (_selectedProduct != null && !_isEditing && !_isAdding)
            {
                StartEditMode();
            }
            else if (_selectedProduct == null)
            {
                ShowInfoMessage("Please select a product to edit.");
            }
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (_isAdding)
            {
                await SaveNewProductAsync();
            }
            else if (_isEditing)
            {
                await SaveProductChangesAsync();
            }
        }

        private async void btnCancel_Click(object sender, EventArgs e)
        {
            await CancelEditAsync();
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (_selectedProduct != null)
            {
                await DeleteProductAsync(_selectedProduct);
            }
        }

        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            await LoadDataAsync();
        }

        #endregion

        #region Add/Edit Mode Management

        private void StartAddMode()
        {
            _isAdding = true;
            _isEditing = false;
            _selectedProduct = null;

            ClearProductDetails();
            SetEditMode();
            
            txtName.Focus();
            lblDetailsTitle.Text = "Add New Product";
        }

        private void StartEditMode()
        {
            if (_selectedProduct == null)
            {
                ShowErrorMessage("No product selected for editing.");
                return;
            }
            
            _isEditing = true;
            _isAdding = false;
            
            SetEditMode();
            lblDetailsTitle.Text = $"Edit Product: {_selectedProduct.Name}";
            
            // Ensure the form fields are populated with the selected product data
            txtName.Text = _selectedProduct.Name ?? string.Empty;
            txtDescription.Text = _selectedProduct.Description ?? string.Empty;
            txtPrice.Text = _selectedProduct.Price.ToString("F2");
            txtMinimumStock.Text = _selectedProduct.MinimumStockLevel.ToString();
            txtProductId.Text = _selectedProduct.ProductID.ToString();
            
            // Load current inventory quantity for editing
            Task.Run(async () =>
            {
                try
                {
                    var inventory = await _apiService.GetInventoryByProductIdAsync(_selectedProduct.ProductID);
                    this.Invoke(new Action(() =>
                    {
                        txtInventoryQuantity.Text = inventory?.Quantity.ToString() ?? "0";
                    }));
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Error loading inventory for edit: {ex.Message}");
                    this.Invoke(new Action(() =>
                    {
                        txtInventoryQuantity.Text = "0";
                    }));
                }
            });
        }

        private void SetEditMode()
        {
            // Enable form fields
            txtName.ReadOnly = false;
            txtDescription.ReadOnly = false;
            txtPrice.ReadOnly = false;
            txtMinimumStock.ReadOnly = false;
            txtInventoryQuantity.ReadOnly = false;
            cmbCategory.Enabled = true;
            
            // Show/hide buttons
            btnEdit.Visible = false;
            btnDelete.Visible = false;
            btnSave.Visible = true;
            btnCancel.Visible = true;
            
            // Disable grid interaction
            dgvProducts.Enabled = false;
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
            txtPrice.ReadOnly = true;
            txtMinimumStock.ReadOnly = true;
            txtInventoryQuantity.ReadOnly = true;
            cmbCategory.Enabled = false;
            
            // Show/hide buttons
            btnEdit.Visible = true;
            btnDelete.Visible = true;
            btnSave.Visible = false;
            btnCancel.Visible = false;
            
            // Enable grid interaction
            dgvProducts.Enabled = true;
            btnAdd.Enabled = true;
            btnRefresh.Enabled = true;
            
            lblDetailsTitle.Text = "Product Details";
        }

        private async Task CancelEditAsync()
        {
            SetViewMode();
            
            if (_selectedProduct != null)
            {
                await DisplayProductDetailsAsync(_selectedProduct);
            }
            else
            {
                ClearProductDetails();
            }
        }

        #endregion

        #region Save Operations

        private async Task SaveNewProductAsync()
        {
            try
            {
                // Validate input
                if (!ValidateInput())
                    return;

                // Show loading state
                btnSave.Text = "Saving...";
                btnSave.Enabled = false;

                // Create new product
                var newProduct = new Product
                {
                    Name = txtName.Text.Trim(),
                    Description = txtDescription.Text.Trim(),
                    Price = decimal.Parse(txtPrice.Text.Replace(",","."), System.Globalization.CultureInfo.InvariantCulture),
                    MinimumStockLevel = int.Parse(txtMinimumStock.Text),
                    CategoryID = GetSelectedCategoryId()
                };

                // Save to API
                var result = await _apiService.CreateProductAsync(newProduct);

                if (result != null)
                {
                    // Create initial inventory record if quantity is specified
                    if (int.TryParse(txtInventoryQuantity.Text, out int initialQuantity) && initialQuantity > 0)
                    {
                        try
                        {
                            var inventory = new Inventory
                            {
                                ProductID = result.ProductID,
                                Quantity = initialQuantity,
                                LastUpdated = DateTime.Now
                            };
                            await _apiService.CreateInventoryAsync(inventory);
                        }
                        catch (Exception invEx)
                        {
                            // Log but don't fail the product creation
                            System.Diagnostics.Debug.WriteLine($"Warning: Failed to create initial inventory: {invEx.Message}");
                        }
                    }

                    ShowSuccessMessage("Product created successfully!");
                    await LoadProductsAsync(); // Refresh the list
                    SetViewMode();
                }
                else
                {
                    ShowErrorMessage("Failed to create product. Please try again.");
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Error creating product: {ex.Message}");
            }
            finally
            {
                btnSave.Text = "💾 Save";
                btnSave.Enabled = true;
            }
        }

        private async Task SaveProductChangesAsync()
        {
            try
            {
                // Validate input
                if (!ValidateInput())
                    return;

                // Show loading state
                btnSave.Text = "Saving...";
                btnSave.Enabled = false;

                // Update product
                _selectedProduct.Name = txtName.Text.Trim();
                _selectedProduct.Description = txtDescription.Text.Trim();
                _selectedProduct.Price = decimal.Parse(txtPrice.Text.Replace(",", "."), System.Globalization.CultureInfo.InvariantCulture);
                _selectedProduct.MinimumStockLevel = int.Parse(txtMinimumStock.Text);
                _selectedProduct.CategoryID = GetSelectedCategoryId();

                // Save to API
                var result = await _apiService.UpdateProductAsync(_selectedProduct.ProductID, _selectedProduct);

                if (result != null)
                {
                    // Update inventory quantity if changed
                    if (int.TryParse(txtInventoryQuantity.Text, out int newQuantity))
                    {
                        try
                        {
                            var currentInventory = await _apiService.GetInventoryByProductIdAsync(_selectedProduct.ProductID);
                            
                            if (currentInventory != null)
                            {
                                // Update existing inventory
                                if (currentInventory.Quantity != newQuantity)
                                {
                                    currentInventory.Quantity = newQuantity;
                                    currentInventory.LastUpdated = DateTime.Now;
                                    await _apiService.UpdateInventoryAsync(currentInventory.InventoryID, currentInventory);
                                }
                            }
                            else if (newQuantity > 0)
                            {
                                // Create new inventory record
                                var inventory = new Inventory
                                {
                                    ProductID = _selectedProduct.ProductID,
                                    Quantity = newQuantity,
                                    LastUpdated = DateTime.Now
                                };
                                await _apiService.CreateInventoryAsync(inventory);
                            }
                        }
                        catch (Exception invEx)
                        {
                            // Log but don't fail the product update
                            System.Diagnostics.Debug.WriteLine($"Warning: Failed to update inventory: {invEx.Message}");
                        }
                    }

                    ShowSuccessMessage("Product updated successfully!");
                    await LoadProductsAsync(); // Refresh the list
                    SetViewMode();
                }
                else
                {
                    ShowErrorMessage("Failed to update product. Please try again.");
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Error updating product: {ex.Message}");
            }
            finally
            {
                btnSave.Text = "💾 Save";
                btnSave.Enabled = true;
            }
        }

        #endregion

        #region Delete Operation

        private async Task DeleteProductAsync(Product product)
        {
            try
            {
                // First check if product has orders
                var hasOrders = await _apiService.CheckProductHasOrdersAsync(product.ProductID);
                
                if (hasOrders)
                {
                    ShowErrorMessage($"Cannot delete product '{product.Name}'.\n\nThis product has associated orders and cannot be deleted. Please remove all orders related to this product first.");
                    return;
                }

                // Check if product has inventory
                var inventory = await _apiService.GetInventoryByProductIdAsync(product.ProductID);
                
                string message = inventory != null && inventory.Quantity > 0
                    ? $"This product has {inventory.Quantity} items in inventory. Are you sure you want to delete it?\n\nProduct: {product.Name}"
                    : $"Are you sure you want to delete this product?\n\nProduct: {product.Name}";

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
                    var success = await _apiService.DeleteProductAsync(product.ProductID);

                    if (success)
                    {
                        ShowSuccessMessage("Product deleted successfully!");
                        await LoadProductsAsync(); // Refresh the list
                        ClearProductDetails();
                    }
                    else
                    {
                        ShowErrorMessage("Failed to delete product. Please try again.");
                    }
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Error deleting product: {ex.Message}");
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
            // Check product name
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                ShowErrorMessage("Product name is required.");
                txtName.Focus();
                return false;
            }

            if (txtName.Text.Trim().Length < 2)
            {
                ShowErrorMessage("Product name must be at least 2 characters long.");
                txtName.Focus();
                return false;
            }

            if (txtName.Text.Trim().Length > 200)
            {
                ShowErrorMessage("Product name cannot exceed 200 characters.");
                txtName.Focus();
                return false;
            }

            // Check price
            if (string.IsNullOrWhiteSpace(txtPrice.Text))
            {
                ShowErrorMessage("Price is required.");
                txtPrice.Focus();
                return false;
            }

            if (!decimal.TryParse(txtPrice.Text.Replace(",", "."), System.Globalization.NumberStyles.Number, System.Globalization.CultureInfo.InvariantCulture, out decimal price) || price < 0)
            {
                ShowErrorMessage("Please enter a valid price (0 or greater).");
                txtPrice.Focus();
                return false;
            }

            if (price > 999999999)
            {
                ShowErrorMessage("Price cannot exceed 999,999,999.");
                txtPrice.Focus();
                return false;
            }

            // Check minimum stock
            if (string.IsNullOrWhiteSpace(txtMinimumStock.Text))
            {
                ShowErrorMessage("Minimum stock level is required.");
                txtMinimumStock.Focus();
                return false;
            }

            if (!int.TryParse(txtMinimumStock.Text, out int minStock) || minStock < 0)
            {
                ShowErrorMessage("Please enter a valid minimum stock level (0 or greater).");
                txtMinimumStock.Focus();
                return false;
            }

            // Check inventory quantity
            if (!string.IsNullOrWhiteSpace(txtInventoryQuantity.Text))
            {
                if (!int.TryParse(txtInventoryQuantity.Text, out int inventoryQty) || inventoryQty < 0)
                {
                    ShowErrorMessage("Please enter a valid inventory quantity (0 or greater).");
                    txtInventoryQuantity.Focus();
                    return false;
                }

                if (inventoryQty > 999999)
                {
                    ShowErrorMessage("Inventory quantity cannot exceed 999,999.");
                    txtInventoryQuantity.Focus();
                    return false;
                }
            }

            // Check category
            var categoryId = GetSelectedCategoryId();
            if (categoryId <= 0)
            {
                ShowErrorMessage("Please select a category.");
                cmbCategory.Focus();
                return false;
            }

            // Check for duplicate names (only when adding or changing name)
            string newName = txtName.Text.Trim();
            bool isNameChanged = _selectedProduct == null || _selectedProduct.Name != newName;
            
            if (isNameChanged && _products != null)
            {
                bool nameExists = _products.Any(p => 
                    p.Name.Equals(newName, StringComparison.OrdinalIgnoreCase) && 
                    (_selectedProduct == null || p.ProductID != _selectedProduct.ProductID));
                
                if (nameExists)
                {
                    ShowErrorMessage("A product with this name already exists. Please choose a different name.");
                    txtName.Focus();
                    return false;
                }
            }

            return true;
        }

        private int GetSelectedCategoryId()
        {
            if (cmbCategory.SelectedItem is { } selectedItem)
            {
                var item = selectedItem as dynamic;
                return item?.CategoryID ?? 0;
            }
            return 0;
        }

        #endregion

        #region Helper Methods

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
