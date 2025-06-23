using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using BMYLBH2025_SDDAP.Models;
using BMYLBH2025_SDDAP.Services;

namespace BMYLBH2025_SDDAP
{
    public partial class BulkStockUpdateForm : Form
    {
        #region Fields

        private readonly ApiService _apiService;
        private List<Inventory> _inventoryItems;
        private List<BulkUpdateItem> _updateItems;
        private bool _isLoading = false;
        private int _processedCount = 0;
        private int _totalCount = 0;

        #endregion

        #region Constructor

        public BulkStockUpdateForm(ApiService apiService)
        {
            _apiService = apiService ?? throw new ArgumentNullException(nameof(apiService));
            InitializeComponent();
            InitializeForm();
            _ = LoadInventoryDataAsync();
        }

        #endregion

        #region Initialization

        private void InitializeForm()
        {
            try
            {
                this.Text = "Bulk Stock Update";
                this.Size = new Size(800, 600);
                this.StartPosition = FormStartPosition.CenterParent;
                this.MinimumSize = new Size(650, 500);

                // Initialize update items list
                _updateItems = new List<BulkUpdateItem>();

                // Configure DataGridView
                ConfigureDataGridView();
                
                // Initialize progress bar
                progressBar.Visible = false;
                lblProgress.Visible = false;
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Error initializing form: {ex.Message}");
            }
        }

        private void ConfigureDataGridView()
        {
            try
            {
                dgvBulkUpdate.AutoGenerateColumns = false;
                dgvBulkUpdate.AllowUserToAddRows = false;
                dgvBulkUpdate.AllowUserToDeleteRows = false;
                dgvBulkUpdate.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvBulkUpdate.MultiSelect = true;

                // Add columns
                var selectColumn = new DataGridViewCheckBoxColumn
                {
                    Name = "Select",
                    HeaderText = "✓",
                    Width = 40,
                    DataPropertyName = "IsSelected"
                };
                dgvBulkUpdate.Columns.Add(selectColumn);

                var productColumn = new DataGridViewTextBoxColumn
                {
                    Name = "Product",
                    HeaderText = "Product",
                    Width = 200,
                    DataPropertyName = "ProductName",
                    ReadOnly = true
                };
                dgvBulkUpdate.Columns.Add(productColumn);

                var currentStockColumn = new DataGridViewTextBoxColumn
                {
                    Name = "CurrentStock",
                    HeaderText = "Current Stock",
                    Width = 100,
                    DataPropertyName = "CurrentStock",
                    ReadOnly = true
                };
                dgvBulkUpdate.Columns.Add(currentStockColumn);

                var newStockColumn = new DataGridViewTextBoxColumn
                {
                    Name = "NewStock",
                    HeaderText = "New Stock",
                    Width = 100,
                    DataPropertyName = "NewStock"
                };
                dgvBulkUpdate.Columns.Add(newStockColumn);

                var adjustmentColumn = new DataGridViewTextBoxColumn
                {
                    Name = "Adjustment",
                    HeaderText = "Adjustment",
                    Width = 100,
                    DataPropertyName = "AdjustmentDisplay",
                    ReadOnly = true
                };
                dgvBulkUpdate.Columns.Add(adjustmentColumn);

                var statusColumn = new DataGridViewTextBoxColumn
                {
                    Name = "Status",
                    HeaderText = "Status",
                    Width = 120,
                    DataPropertyName = "CurrentStatus",
                    ReadOnly = true
                };
                dgvBulkUpdate.Columns.Add(statusColumn);

                // Style the grid
                StyleDataGridView();
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Error configuring DataGridView: {ex.Message}");
            }
        }

        private void StyleDataGridView()
        {
            dgvBulkUpdate.EnableHeadersVisualStyles = false;
            dgvBulkUpdate.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(52, 152, 219);
            dgvBulkUpdate.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvBulkUpdate.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgvBulkUpdate.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(250, 250, 250);
            dgvBulkUpdate.DefaultCellStyle.SelectionBackColor = Color.FromArgb(52, 152, 219);
            dgvBulkUpdate.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvBulkUpdate.ColumnHeadersHeight = 35;
            dgvBulkUpdate.RowTemplate.Height = 30;
        }

        #endregion

        #region Data Loading

        private async Task LoadInventoryDataAsync()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                btnApplyUpdates.Enabled = false;

                _inventoryItems = await _apiService.GetAllInventoryAsync();
                
                // Convert to bulk update items
                _updateItems = _inventoryItems?.Select(i => new BulkUpdateItem
                {
                    InventoryId = i.InventoryID,
                    ProductId = i.ProductID,
                    ProductName = i.Product?.Name ?? "Unknown",
                    CurrentStock = i.Quantity,
                    NewStock = i.Quantity,
                    MinimumStock = i.Product?.MinimumStockLevel ?? 0,
                    IsSelected = false
                }).ToList() ?? new List<BulkUpdateItem>();

                // Update grid
                RefreshGrid();
                
                lblTotalItems.Text = $"Total Items: {_updateItems.Count}";
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Error loading inventory data: {ex.Message}");
            }
            finally
            {
                this.Cursor = Cursors.Default;
                btnApplyUpdates.Enabled = true;
            }
        }

        private void RefreshGrid()
        {
            try
            {
                // Calculate adjustments and status
                foreach (var item in _updateItems)
                {
                    item.Adjustment = item.NewStock - item.CurrentStock;
                    item.AdjustmentDisplay = item.Adjustment.ToString("+#;-#;0");
                    item.CurrentStatus = GetStockStatus(item.CurrentStock, item.MinimumStock);
                    item.NewStatus = GetStockStatus(item.NewStock, item.MinimumStock);
                }

                dgvBulkUpdate.DataSource = null;
                dgvBulkUpdate.DataSource = _updateItems;
                
                // Apply row colors based on status
                ApplyRowColors();
                
                // Update selection count
                UpdateSelectionCount();
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Error refreshing grid: {ex.Message}");
            }
        }

        #endregion

        #region Event Handlers

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            foreach (var item in _updateItems)
            {
                item.IsSelected = true;
            }
            RefreshGrid();
        }

        private void btnDeselectAll_Click(object sender, EventArgs e)
        {
            foreach (var item in _updateItems)
            {
                item.IsSelected = false;
            }
            RefreshGrid();
        }

        private void btnSelectLowStock_Click(object sender, EventArgs e)
        {
            foreach (var item in _updateItems)
            {
                item.IsSelected = item.CurrentStatus == "Low Stock" || item.CurrentStatus == "Out of Stock";
            }
            RefreshGrid();
        }

        private async void btnApplyUpdates_Click(object sender, EventArgs e)
        {
            if (_isLoading) return;

            try
            {
                var selectedItems = _updateItems.Where(i => i.IsSelected && i.Adjustment != 0).ToList();
                
                if (selectedItems.Count == 0)
                {
                    ShowInfoMessage("No items selected or no changes to apply.");
                    return;
                }

                // Confirm bulk update
                var result = MessageBox.Show(
                    $"You are about to update {selectedItems.Count} items.\n\n" +
                    $"Are you sure you want to proceed with the bulk stock update?",
                    "Confirm Bulk Update",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.No)
                    return;

                // Show progress
                _isLoading = true;
                progressBar.Visible = true;
                lblProgress.Visible = true;
                progressBar.Maximum = selectedItems.Count;
                progressBar.Value = 0;
                _processedCount = 0;
                _totalCount = selectedItems.Count;

                btnApplyUpdates.Enabled = false;
                btnCancel.Text = "Close";

                // Process updates
                var successCount = 0;
                var errorCount = 0;
                var errors = new List<string>();

                foreach (var item in selectedItems)
                {
                    try
                    {
                        lblProgress.Text = $"Updating {item.ProductName}... ({_processedCount + 1}/{_totalCount})";
                        Application.DoEvents();

                        var success = await _apiService.UpdateStockAsync(item.ProductId, item.NewStock);
                        
                        if (success)
                        {
                            successCount++;
                            item.CurrentStock = item.NewStock;
                            item.Adjustment = 0;
                        }
                        else
                        {
                            errorCount++;
                            errors.Add($"Failed to update {item.ProductName}");
                        }
                    }
                    catch (Exception ex)
                    {
                        errorCount++;
                        errors.Add($"Error updating {item.ProductName}: {ex.Message}");
                    }

                    _processedCount++;
                    progressBar.Value = _processedCount;
                }

                // Show completion message
                var message = $"Bulk update completed!\n\n" +
                             $"✅ Successfully updated: {successCount} items\n" +
                             $"❌ Failed updates: {errorCount} items";

                if (errors.Count > 0 && errors.Count <= 5)
                {
                    message += "\n\nErrors:\n" + string.Join("\n", errors);
                }
                else if (errors.Count > 5)
                {
                    message += $"\n\nFirst 5 errors:\n" + string.Join("\n", errors.Take(5)) + $"\n... and {errors.Count - 5} more";
                }

                MessageBox.Show(message, "Bulk Update Complete", MessageBoxButtons.OK, 
                    errorCount == 0 ? MessageBoxIcon.Information : MessageBoxIcon.Warning);

                // Refresh grid to show updated values
                RefreshGrid();

                if (successCount > 0)
                {
                    this.DialogResult = DialogResult.OK;
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Error during bulk update: {ex.Message}");
            }
            finally
            {
                _isLoading = false;
                progressBar.Visible = false;
                lblProgress.Visible = false;
                btnApplyUpdates.Enabled = true;
                btnCancel.Text = "Cancel";
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (_isLoading)
            {
                var result = MessageBox.Show(
                    "Bulk update is in progress. Are you sure you want to cancel?",
                    "Cancel Update",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.No)
                    return;
            }

            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void dgvBulkUpdate_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && 
                    e.ColumnIndex < dgvBulkUpdate.Columns.Count &&
                    dgvBulkUpdate.Columns[e.ColumnIndex].Name == "NewStock")
                {
                    RefreshGrid();
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Error handling cell value change: {ex.Message}");
            }
        }

        private void dgvBulkUpdate_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgvBulkUpdate.IsCurrentCellDirty)
                {
                    dgvBulkUpdate.CommitEdit(DataGridViewDataErrorContexts.Commit);
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Error committing cell edit: {ex.Message}");
            }
        }

        #endregion

        #region Helper Methods

        private void ApplyRowColors()
        {
            for (int i = 0; i < dgvBulkUpdate.Rows.Count; i++)
            {
                var item = _updateItems[i];
                var row = dgvBulkUpdate.Rows[i];

                if (item.CurrentStatus == "Out of Stock")
                {
                    row.DefaultCellStyle.BackColor = Color.FromArgb(255, 235, 235);
                }
                else if (item.CurrentStatus == "Low Stock")
                {
                    row.DefaultCellStyle.BackColor = Color.FromArgb(255, 248, 235);
                }
                else if (item.Adjustment != 0)
                {
                    row.DefaultCellStyle.BackColor = Color.FromArgb(235, 248, 255);
                }
            }
        }

        private void UpdateSelectionCount()
        {
            var selectedCount = _updateItems.Count(i => i.IsSelected);
            var changedCount = _updateItems.Count(i => i.IsSelected && i.Adjustment != 0);
            lblSelectedItems.Text = $"Selected: {selectedCount} items ({changedCount} with changes)";
        }

        private string GetStockStatus(int currentStock, int minStock)
        {
            if (minStock <= 0) return "Normal";
            
            if (currentStock <= 0) return "Out of Stock";
            if (currentStock <= minStock) return "Low Stock";
            if (currentStock <= minStock * 1.5) return "Normal";
            return "Well Stocked";
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

    #region Helper Classes

    public class BulkUpdateItem : INotifyPropertyChanged
    {
        private bool _isSelected;
        private int _newStock;

        public int InventoryId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int CurrentStock { get; set; }
        public int MinimumStock { get; set; }
        public int Adjustment { get; set; }
        public string AdjustmentDisplay { get; set; }
        public string CurrentStatus { get; set; }
        public string NewStatus { get; set; }

        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                _isSelected = value;
                OnPropertyChanged(nameof(IsSelected));
            }
        }

        public int NewStock
        {
            get => _newStock;
            set
            {
                _newStock = value;
                OnPropertyChanged(nameof(NewStock));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    #endregion
} 