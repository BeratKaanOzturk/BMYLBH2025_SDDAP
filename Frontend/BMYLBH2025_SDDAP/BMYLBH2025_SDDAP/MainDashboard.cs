using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using BMYLBH2025_SDDAP.Services;
using System.Drawing;

namespace BMYLBH2025_SDDAP
{
    public partial class MainDashboard : Form
    {
        #region Fields

        private readonly ApiService _apiService;
        private List<InventoryItem> _currentInventory;
        private List<Product> _currentProducts;

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
            // Set placeholder text for search (since Designer doesn't support this in older .NET)
            txtSearchInventory.Text = "🔍 Search inventory...";
            txtSearchInventory.ForeColor = Color.Gray;
            txtSearchInventory.Enter += TxtSearchInventory_Enter;
            txtSearchInventory.Leave += TxtSearchInventory_Leave;

            // Apply modern styling to data grids
            StyleDataGridView(dgvInventory);
            StyleDataGridView(dgvProducts);
        }

        private void StyleDataGridView(DataGridView dgv)
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
                var totalValueTask = _apiService.GetTotalInventoryValueAsync();
                var lowStockTask = _apiService.GetLowStockItemsAsync();

                await Task.WhenAll(inventoryTask, productsTask, totalValueTask, lowStockTask);

                // Store results
                _currentInventory = inventoryTask.Result;
                _currentProducts = productsTask.Result;

                // Update UI
                UpdateInventoryGrid();
                UpdateProductsGrid();
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

        private void UpdateProductsGrid()
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

        private void UpdateDashboardSummary(decimal totalValue, int lowStockCount)
        {
            lblTotalValue.Text = totalValue.ToString("C");
            lblLowStockCount.Text = lowStockCount.ToString();
            lblProductsCount.Text = _currentProducts?.Count.ToString() ?? "0";

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

        #endregion

        #region Event Handlers

        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            await LoadDataAsync();
        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            ShowInfoMessage("Add Product feature will be implemented in the next phase!");
        }

        private void btnUpdateStock_Click(object sender, EventArgs e)
        {
            if (dgvInventory.SelectedRows.Count > 0)
            {
                var selectedRow = dgvInventory.SelectedRows[0];
                var productName = selectedRow.Cells["Product"].Value?.ToString();
                ShowInfoMessage($"Update Stock feature for '{productName}' will be implemented in the next phase!");
            }
            else
            {
                ShowInfoMessage("Please select an inventory item to update stock levels.");
            }
        }

        private void txtSearchInventory_TextChanged(object sender, EventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox != null && textBox.ForeColor != Color.Gray)
            {
                FilterInventoryData(textBox.Text);
            }
        }

        private void TxtSearchInventory_Enter(object sender, EventArgs e)
        {
            if (sender is TextBox textBox && textBox.Text == "🔍 Search inventory...")
            {
                textBox.Text = "";
                textBox.ForeColor = Color.Black;
            }
        }

        private void TxtSearchInventory_Leave(object sender, EventArgs e)
        {
            if (sender is TextBox textBox && string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = "🔍 Search inventory...";
                textBox.ForeColor = Color.Gray;
                FilterInventoryData(""); // Reset filter
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

        #endregion
    }
} 