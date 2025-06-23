using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using BMYLBH2025_SDDAP.Services;
using System.Drawing;
using BMYLBH2025_SDDAP.Models;

namespace BMYLBH2025_SDDAP
{
    public partial class MainDashboard : Form
    {
        #region Fields

        private readonly ApiService _apiService;
        private List<Inventory> _currentInventory;
        private List<Product> _currentProducts;
        private List<Category> _currentCategories;
        private List<Category> _filteredCategories;
        private List<Order> _currentOrders;
        private List<Order> _filteredOrders;

        #endregion

        #region Constructor

        public MainDashboard(ApiService apiService)
        {
            _apiService = apiService ?? throw new ArgumentNullException(nameof(apiService));
            InitializeComponent();
            InitializeFormSettings();
            _ = LoadDataAsync(); // Fire and forget initial load
        }

        #endregion

        #region Initialization

        private void InitializeFormSettings()
        {
            try
            {
                // Set placeholder text for search (since Designer doesn't support this in older .NET)
                txtSearchInventory.Text = "🔍 Search inventory...";
                txtSearchInventory.ForeColor = Color.Gray;
                
                txtSearchCategories.Text = "🔍 Search categories...";
                txtSearchCategories.ForeColor = Color.Gray;
                
                txtSearchOrders.Text = "🔍 Search orders...";
                txtSearchOrders.ForeColor = Color.Gray;

                // Apply modern styling to data grids
                StyleDataGridView(dgvInventory);
                StyleDataGridView(dgvProducts);
                StyleDataGridView(dgvCategories);
                StyleDataGridView(dgvOrders);
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Error initializing form settings: {ex.Message}");
            }
        }

        private void StyleDataGridView(DataGridView dgv)
        {
            try
            {
                dgv.EnableHeadersVisualStyles = false;
                dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(52, 152, 219);
                dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
                dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(250, 250, 250);
                dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(52, 152, 219);
                dgv.DefaultCellStyle.SelectionForeColor = Color.White;
                dgv.ColumnHeadersHeight = 35;
                dgv.RowTemplate.Height = 30;
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Error styling DataGridView: {ex.Message}");
            }
        }

        #endregion

        #region Data Loading

        private async Task LoadDataAsync()
        {
            try
            {
                // Show loading state
                this.Cursor = Cursors.WaitCursor;
                btnRefresh.Text = "Loading...";
                btnRefresh.Enabled = false;

                // Load data concurrently for better performance
                var inventoryTask = _apiService.GetAllInventoryAsync();
                var productsTask = _apiService.GetAllProductsAsync();
                var categoriesTask = _apiService.GetAllCategoriesAsync();
                var ordersTask = _apiService.GetAllOrdersAsync();
                var totalValueTask = _apiService.GetTotalInventoryValueAsync();
                var lowStockTask = _apiService.GetLowStockItemsAsync();

                await Task.WhenAll(inventoryTask, productsTask, categoriesTask, ordersTask, totalValueTask, lowStockTask);

                // Store results
                _currentInventory = inventoryTask.Result;
                _currentProducts = productsTask.Result;
                _currentCategories = categoriesTask.Result;
                _currentOrders = ordersTask.Result;
                _filteredCategories = new List<Category>(_currentCategories ?? new List<Category>());
                _filteredOrders = new List<Order>(_currentOrders ?? new List<Order>());

                // Update UI
                UpdateInventoryGrid();
                UpdateProductsGrid();
                UpdateCategoriesGrid();
                UpdateOrdersGrid();
                UpdateDashboardSummary(totalValueTask.Result, lowStockTask.Result.Count);

                // Show success feedback
                this.Text = $"Inventory Management System - Dashboard (Last updated: {DateTime.Now:HH:mm:ss})";
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Error loading data: {ex.Message}");
            }
            finally
            {
                // Reset loading state
                this.Cursor = Cursors.Default;
                btnRefresh.Text = "🔄 Refresh Data";
                btnRefresh.Enabled = true;
            }
        }

        private void UpdateInventoryGrid()
        {
            try
            {
                if (dgvInventory == null || _currentInventory == null) return;

                var displayData = _currentInventory.Select(i => new
                {
                    ID = i.InventoryID,
                    Product = i.Product?.Name ?? "Unknown",
                    Category = i.Product?.Category?.Name ?? "Uncategorized",
                    Quantity = i.Quantity,
                    MinStock = i.Product?.MinimumStockLevel ?? 0,
                    Status = GetStockStatus(i.Quantity, i.Product?.MinimumStockLevel ?? 0),
                    LastUpdated = i.LastUpdated.ToString("yyyy-MM-dd HH:mm")
                }).ToList();

                dgvInventory.DataSource = displayData;
                
                // Apply conditional formatting for status column
                ApplyInventoryStatusFormatting();
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Error updating inventory grid: {ex.Message}");
            }
        }

        private void UpdateProductsGrid()
        {
            try
            {
                if (dgvProducts == null || _currentProducts == null) return;

                var displayData = _currentProducts.Select(p => new
                {
                    ID = p.ProductID,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price.ToString("C"),
                    Category = p.Category?.Name ?? "Uncategorized",
                    MinStock = p.MinimumStockLevel
                }).ToList();

                dgvProducts.DataSource = displayData;
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Error updating products grid: {ex.Message}");
            }
        }

        private void UpdateCategoriesGrid()
        {
            try
            {
                if (dgvCategories == null || _filteredCategories == null) return;

                var displayData = _filteredCategories.Select(c => new
                {
                    ID = c.CategoryID,
                    Name = c.Name,
                    Description = c.Description,
                    ProductCount = _currentProducts?.Count(p => p.CategoryID == c.CategoryID) ?? 0,
                    CreatedAt = c.CreatedAt.ToString("yyyy-MM-dd"),
                    LastUpdated = c.UpdatedAt.ToString("yyyy-MM-dd")
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
                    dgvCategories.Columns["ProductCount"].HeaderText = "Products";
                    dgvCategories.Columns["ProductCount"].Width = 100;
                    dgvCategories.Columns["CreatedAt"].HeaderText = "Created";
                    dgvCategories.Columns["CreatedAt"].Width = 120;
                    dgvCategories.Columns["LastUpdated"].HeaderText = "Updated";
                    dgvCategories.Columns["LastUpdated"].Width = 120;
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Error updating categories grid: {ex.Message}");
            }
        }

        private void UpdateOrdersGrid()
        {
            try
            {
                if (dgvOrders == null || _filteredOrders == null) return;

                var displayData = _filteredOrders.Select(o => new
                {
                    ID = o.OrderID,
                    OrderDate = o.OrderDate.ToString("yyyy-MM-dd"),
                    Supplier = o.Supplier?.Name ?? "Unknown",
                    Status = o.Status,
                    TotalAmount = o.TotalAmount.ToString("C"),
                    ItemCount = o.GetTotalItems(),
                    CreatedAt = o.CreatedAt.ToString("yyyy-MM-dd HH:mm")
                }).ToList();

                dgvOrders.DataSource = displayData;

                // Configure column headers and widths
                if (dgvOrders.Columns.Count > 0)
                {
                    dgvOrders.Columns["ID"].HeaderText = "Order #";
                    dgvOrders.Columns["ID"].Width = 80;
                    dgvOrders.Columns["OrderDate"].HeaderText = "Order Date";
                    dgvOrders.Columns["OrderDate"].Width = 120;
                    dgvOrders.Columns["Supplier"].HeaderText = "Supplier";
                    dgvOrders.Columns["Supplier"].Width = 200;
                    dgvOrders.Columns["Status"].HeaderText = "Status";
                    dgvOrders.Columns["Status"].Width = 120;
                    dgvOrders.Columns["TotalAmount"].HeaderText = "Total Amount";
                    dgvOrders.Columns["TotalAmount"].Width = 120;
                    dgvOrders.Columns["ItemCount"].HeaderText = "Items";
                    dgvOrders.Columns["ItemCount"].Width = 80;
                    dgvOrders.Columns["CreatedAt"].HeaderText = "Created";
                    dgvOrders.Columns["CreatedAt"].Width = 140;
                }

                // Apply status-based row coloring
                ApplyOrderStatusFormatting();
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Error updating orders grid: {ex.Message}");
            }
        }

        private void ApplyOrderStatusFormatting()
        {
            try
            {
                if (dgvOrders.Columns["Status"] == null) return;
                
                foreach (DataGridViewRow row in dgvOrders.Rows)
                {
                    var status = row.Cells["Status"].Value?.ToString()?.ToLower();
                    if (status == null) continue;

                    switch (status)
                    {
                        case "pending":
                            row.DefaultCellStyle.BackColor = Color.FromArgb(255, 248, 225); // Light yellow
                            break;
                        case "confirmed":
                            row.DefaultCellStyle.BackColor = Color.FromArgb(217, 237, 247); // Light blue
                            break;
                        case "processing":
                            row.DefaultCellStyle.BackColor = Color.FromArgb(230, 244, 255); // Lighter blue
                            break;
                        case "completed":
                            row.DefaultCellStyle.BackColor = Color.FromArgb(223, 240, 216); // Light green
                            break;
                        case "cancelled":
                            row.DefaultCellStyle.BackColor = Color.FromArgb(248, 215, 218); // Light red
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Error applying order status formatting: {ex.Message}");
            }
        }

        private void UpdateDashboardSummary(decimal totalValue, int lowStockCount)
        {
            try
            {
                lblTotalValue.Text = totalValue.ToString("C");
                lblLowStockCount.Text = lowStockCount.ToString();
                lblProductsCount.Text = _currentProducts?.Count.ToString() ?? "0";
                
                // Add order count to summary
                if (lblOrdersCount != null)
                {
                    lblOrdersCount.Text = _currentOrders?.Count.ToString() ?? "0";
                }

                // Update card colors based on status
                if (lowStockCount > 0)
                {
                    pnlLowStockCard.BackColor = Color.FromArgb(231, 76, 60); // Red for alerts
                }
                else
                {
                    pnlLowStockCard.BackColor = Color.FromArgb(46, 204, 113); // Green for good status
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Error updating dashboard summary: {ex.Message}");
            }
        }

        #endregion

        #region Helper Methods

        private string GetStockStatus(int currentStock, int minStock)
        {
            if (currentStock <= 0)
                return "🔴 Out of Stock";
            if (currentStock <= minStock)
                return "⚠️ Low Stock";
            return "✅ In Stock";
        }

        private void ApplyInventoryStatusFormatting()
        {
            try
            {
                if (dgvInventory.Columns["Status"] == null) return;
                foreach (DataGridViewRow row in dgvInventory.Rows)
                {
                    var status = row.Cells["Status"].Value?.ToString();
                    if (status == null) continue;
                    if (status.Contains("Out of Stock"))
                        row.DefaultCellStyle.BackColor = Color.FromArgb(255, 240, 240);
                    else if (status.Contains("Low Stock"))
                        row.DefaultCellStyle.BackColor = Color.FromArgb(255, 248, 220);
                    else
                        row.DefaultCellStyle.BackColor = Color.White;
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Error applying inventory status formatting: {ex.Message}");
            }
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

        #region Search Functionality

        private void FilterInventoryData(string searchTerm)
        {
            try
            {
                if (_currentInventory == null) return;

                if (string.IsNullOrWhiteSpace(searchTerm) || searchTerm == "🔍 Search inventory...")
                {
                    UpdateInventoryGrid();
                    return;
                }

                var filteredData = _currentInventory
                    .Where(i => 
                        (i.Product?.Name?.ToLower().Contains(searchTerm.ToLower()) ?? false) ||
                        (i.Product?.Category?.Name?.ToLower().Contains(searchTerm.ToLower()) ?? false))
                    .Select(i => new
                    {
                        ID = i.InventoryID,
                        Product = i.Product?.Name ?? "Unknown",
                        Category = i.Product?.Category?.Name ?? "Uncategorized",
                        Quantity = i.Quantity,
                        MinStock = i.Product?.MinimumStockLevel ?? 0,
                        Status = GetStockStatus(i.Quantity, i.Product?.MinimumStockLevel ?? 0),
                        LastUpdated = i.LastUpdated.ToString("yyyy-MM-dd HH:mm")
                    }).ToList();

                dgvInventory.DataSource = filteredData;
                ApplyInventoryStatusFormatting();
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Error filtering inventory data: {ex.Message}");
            }
        }

        #endregion

        #region Event Handlers

        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            await LoadDataAsync();
        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            try
            {
                var productForm = new ProductManagementForm(_apiService);
                productForm.ShowDialog(this);
                
                // Refresh data after the form closes
                _ = LoadDataAsync();
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Error opening product management: {ex.Message}");
            }
        }



        private void btnBulkUpdateStock_Click(object sender, EventArgs e)
        {
            try
            {
                var bulkUpdateForm = new BulkStockUpdateForm(_apiService);
                var result = bulkUpdateForm.ShowDialog(this);
                
                if (result == DialogResult.OK)
                {
                    // Refresh all data after successful bulk update
                    _ = LoadDataAsync();
                    ShowInfoMessage("Bulk stock update completed successfully!");
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Error opening bulk update form: {ex.Message}");
            }
        }

        private void btnUpdateStockInv_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvInventory.SelectedRows.Count > 0)
                {
                    var selectedRow = dgvInventory.SelectedRows[0];
                    var inventoryId = Convert.ToInt32(selectedRow.Cells["ID"].Value);
                    
                    // Find the inventory item from our current data
                    var inventoryItem = _currentInventory?.FirstOrDefault(i => i.InventoryID == inventoryId);
                    
                    if (inventoryItem != null)
                    {
                        // Open stock update dialog
                        var stockUpdateDialog = new StockUpdateDialog(_apiService, inventoryItem);
                        var result = stockUpdateDialog.ShowDialog(this);
                        
                        if (result == DialogResult.OK)
                        {
                            // Refresh inventory data after successful update
                            _ = LoadDataAsync();
                            ShowInfoMessage("Stock update completed successfully!");
                        }
                    }
                    else
                    {
                        ShowErrorMessage("Unable to find the selected inventory item. Please refresh the data and try again.");
                    }
                }
                else
                {
                    ShowInfoMessage("Please select an inventory item to update stock levels.");
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Error updating stock: {ex.Message}");
            }
        }

        private void btnBulkUpdateStockInv_Click(object sender, EventArgs e)
        {
            try
            {
                var bulkUpdateForm = new BulkStockUpdateForm(_apiService);
                var result = bulkUpdateForm.ShowDialog(this);
                
                if (result == DialogResult.OK)
                {
                    // Refresh all data after successful bulk update
                    _ = LoadDataAsync();
                    ShowInfoMessage("Bulk stock update completed successfully!");
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Error opening bulk update form: {ex.Message}");
            }
        }

        private void txtSearchInventory_TextChanged(object sender, EventArgs e)
        {
            try
            {
                var textBox = sender as TextBox;
                if (textBox != null && textBox.ForeColor != Color.Gray)
                {
                    FilterInventoryData(textBox.Text);
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Error in search text changed: {ex.Message}");
            }
        }

        private void TxtSearchInventory_Enter(object sender, EventArgs e)
        {
            try
            {
                var textBox = sender as TextBox;
                if (textBox != null && textBox.Text == "🔍 Search inventory...")
                {
                    textBox.Text = "";
                    textBox.ForeColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Error in search enter: {ex.Message}");
            }
        }

        private void TxtSearchInventory_Leave(object sender, EventArgs e)
        {
            try
            {
                var textBox = sender as TextBox;
                if (textBox != null && string.IsNullOrWhiteSpace(textBox.Text))
                {
                    textBox.Text = "🔍 Search inventory...";
                    textBox.ForeColor = Color.Gray;
                    FilterInventoryData(""); // Reset filter
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Error in search leave: {ex.Message}");
            }
        }

        private void btnManageCategories_Click(object sender, EventArgs e)
        {
            try
            {
                var categoryForm = new CategoryManagementForm(_apiService);
                categoryForm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Error opening category management: {ex.Message}");
            }
        }

        private void btnManageProducts_Click(object sender, EventArgs e)
        {
            try
            {
                var productForm = new ProductManagementForm(_apiService);
                productForm.ShowDialog(this);
                
                // Refresh data after the form closes
                _ = LoadDataAsync();
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Error opening product management: {ex.Message}");
            }
        }

        private void MainDashboard_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                _apiService?.Dispose();
            }
            catch (Exception ex)
            {
                // Log error but don't prevent form closing
                System.Diagnostics.Debug.WriteLine($"Error disposing ApiService: {ex.Message}");
            }
        }

        private void txtSearchCategories_TextChanged(object sender, EventArgs e)
        {
            try
            {
                var textBox = sender as TextBox;
                if (textBox != null && textBox.ForeColor != Color.Gray)
                {
                    FilterCategoriesData(textBox.Text);
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Error in category search text changed: {ex.Message}");
            }
        }

        private void txtSearchCategories_Enter(object sender, EventArgs e)
        {
            try
            {
                var textBox = sender as TextBox;
                if (textBox != null && textBox.Text == "🔍 Search categories...")
                {
                    textBox.Text = "";
                    textBox.ForeColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Error in category search enter: {ex.Message}");
            }
        }

        private void txtSearchCategories_Leave(object sender, EventArgs e)
        {
            try
            {
                var textBox = sender as TextBox;
                if (textBox != null && string.IsNullOrWhiteSpace(textBox.Text))
                {
                    textBox.Text = "🔍 Search categories...";
                    textBox.ForeColor = Color.Gray;
                    FilterCategoriesData(""); // Reset filter
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Error in category search leave: {ex.Message}");
            }
        }

        private void FilterCategoriesData(string searchTerm)
        {
            try
            {
                if (_currentCategories == null) return;

                if (string.IsNullOrWhiteSpace(searchTerm) || searchTerm == "🔍 Search categories...")
                {
                    _filteredCategories = new List<Category>(_currentCategories);
                }
                else
                {
                    _filteredCategories = _currentCategories.Where(c =>
                        (c.Name?.ToLower().Contains(searchTerm.ToLower()) ?? false) ||
                        (c.Description?.ToLower().Contains(searchTerm.ToLower()) ?? false)
                    ).ToList();
                }

                UpdateCategoriesGrid();
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Error filtering categories data: {ex.Message}");
            }
        }

        // Orders Event Handlers
        private void btnCreateOrder_Click(object sender, EventArgs e)
        {
            try
            {
                var orderCreationForm = new OrderCreationForm(_apiService);
                var result = orderCreationForm.ShowDialog(this);
                
                if (result == DialogResult.OK)
                {
                    // Refresh the orders data after successful creation
                    _ = LoadDataAsync();
                    ShowInfoMessage("Order created successfully!");
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Error opening order creation: {ex.Message}");
            }
        }

        private void btnManageOrders_Click(object sender, EventArgs e)
        {
            try
            {
                var orderManagementForm = new OrderManagementForm(_apiService);
                orderManagementForm.ShowDialog(this);
                
                // Refresh the orders data after the form closes
                _ = LoadDataAsync();
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Error opening order management: {ex.Message}");
            }
        }

        private void txtSearchOrders_TextChanged(object sender, EventArgs e)
        {
            try
            {
                var searchTerm = txtSearchOrders.Text;
                if (searchTerm == "🔍 Search orders...") return;
                FilterOrdersData(searchTerm);
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Error in order search text changed: {ex.Message}");
            }
        }

        private void txtSearchOrders_Enter(object sender, EventArgs e)
        {
            try
            {
                if (txtSearchOrders.Text == "🔍 Search orders...")
                {
                    txtSearchOrders.Text = "";
                    txtSearchOrders.ForeColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Error in order search enter: {ex.Message}");
            }
        }

        private void txtSearchOrders_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtSearchOrders.Text))
                {
                    txtSearchOrders.Text = "🔍 Search orders...";
                    txtSearchOrders.ForeColor = Color.Gray;
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Error in order search leave: {ex.Message}");
            }
        }

        private void dgvOrders_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                // Handle order selection - could show order details in the future
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Error in order selection changed: {ex.Message}");
            }
        }

        private void FilterOrdersData(string searchTerm)
        {
            try
            {
                if (_currentOrders == null) return;

                if (string.IsNullOrWhiteSpace(searchTerm) || searchTerm == "🔍 Search orders...")
                {
                    _filteredOrders = new List<Order>(_currentOrders);
                }
                else
                {
                    _filteredOrders = _currentOrders.Where(o =>
                        o.OrderID.ToString().Contains(searchTerm) ||
                        (o.Status?.ToLower().Contains(searchTerm.ToLower()) ?? false) ||
                        (o.Supplier?.Name?.ToLower().Contains(searchTerm.ToLower()) ?? false)
                    ).ToList();
                }

                UpdateOrdersGrid();
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Error filtering orders data: {ex.Message}");
            }
        }

        #endregion
    }
} 