using System;
using System.Drawing;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows.Forms;
using BMYLBH2025_SDDAP.Models;
using BMYLBH2025_SDDAP.Services;

namespace BMYLBH2025_SDDAP
{
    public partial class StockUpdateDialog : Form
    {
        #region Fields

        private readonly ApiService _apiService;
        private readonly Inventory _inventoryItem;
        private bool _isLoading = false;

        #endregion

        #region Constructor

        public StockUpdateDialog(ApiService apiService, Inventory inventoryItem)
        {
            _apiService = apiService ?? throw new ArgumentNullException(nameof(apiService));
            _inventoryItem = inventoryItem ?? throw new ArgumentNullException(nameof(inventoryItem));
            
            InitializeComponent();
            InitializeDialog();
        }

        #endregion

        #region Initialization

        private void InitializeDialog()
        {
            try
            {
                // Set form properties
                this.Text = "Update Stock Quantity";
                this.Size = new Size(450, 380);
                this.StartPosition = FormStartPosition.CenterParent;
                this.FormBorderStyle = FormBorderStyle.FixedDialog;
                this.MaximizeBox = false;
                this.MinimizeBox = false;
                this.ShowInTaskbar = false;

                // Load current data
                LoadCurrentStockInfo();
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Error initializing dialog: {ex.Message}");
            }
        }

        private void LoadCurrentStockInfo()
        {
            try
            {
                // Fill current information
                lblProductName.Text = _inventoryItem.Product?.Name ?? "Unknown Product";
                lblCurrentStock.Text = _inventoryItem.Quantity.ToString();
                lblMinimumStock.Text = _inventoryItem.Product?.MinimumStockLevel.ToString() ?? "Not set";
                
                // Set status and color
                var status = GetStockStatus(_inventoryItem.Quantity, _inventoryItem.Product?.MinimumStockLevel ?? 0);
                lblCurrentStatus.Text = status;
                lblCurrentStatus.ForeColor = GetStatusColor(status);

                // Set default new quantity
                numNewQuantity.Value = _inventoryItem.Quantity;
                numNewQuantity.Minimum = 0;
                numNewQuantity.Maximum = 999999;

                // Calculate and show adjustment
                UpdateAdjustmentDisplay();
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Error loading stock info: {ex.Message}");
            }
        }

        #endregion

        #region Event Handlers

        private void numNewQuantity_ValueChanged(object sender, EventArgs e)
        {
            UpdateAdjustmentDisplay();
        }

        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            if (_isLoading) return;

            try
            {
                _isLoading = true;
                btnUpdate.Enabled = false;
                btnCancel.Enabled = false;
                btnUpdate.Text = "Updating...";

                var newQuantity = (int)numNewQuantity.Value;
                var currentQuantity = _inventoryItem.Quantity;
                
                // Validate quantity
                if (newQuantity < 0)
                {
                    ShowErrorMessage("Quantity cannot be negative.");
                    return;
                }

                // Confirm if setting quantity below minimum stock
                var minStock = _inventoryItem.Product?.MinimumStockLevel ?? 0;
                if (newQuantity < minStock && minStock > 0)
                {
                    var result = MessageBox.Show(
                        $"Warning: Setting quantity to {newQuantity} is below the minimum stock level of {minStock}.\n\n" +
                        "This will mark the item as 'Low Stock'. Do you want to continue?",
                        "Low Stock Warning",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning);

                    if (result == DialogResult.No)
                        return;
                }

                // Confirm large adjustments
                var adjustment = newQuantity - currentQuantity;
                if (Math.Abs(adjustment) > 100)
                {
                    var result = MessageBox.Show(
                        $"You are making a large stock adjustment of {adjustment:+#;-#;0} units.\n\n" +
                        $"Current Stock: {currentQuantity}\n" +
                        $"New Stock: {newQuantity}\n\n" +
                        "Are you sure you want to proceed?",
                        "Large Stock Adjustment",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    if (result == DialogResult.No)
                        return;
                }

                // Update stock via API
                var success = await _apiService.UpdateStockAsync(_inventoryItem.ProductID, newQuantity);
                
                if (success)
                {
                    MessageBox.Show(
                        $"Stock successfully updated!\n\n" +
                        $"Product: {_inventoryItem.Product?.Name}\n" +
                        $"Previous Stock: {currentQuantity}\n" +
                        $"New Stock: {newQuantity}\n" +
                        $"Adjustment: {adjustment:+#;-#;0}",
                        "Stock Updated",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    ShowErrorMessage("Failed to update stock. Please check your connection and try again.");
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Error updating stock: {ex.Message}");
            }
            finally
            {
                _isLoading = false;
                btnUpdate.Enabled = true;
                btnCancel.Enabled = true;
                btnUpdate.Text = "💾 Update Stock";
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        #endregion

        #region Helper Methods

        private void UpdateAdjustmentDisplay()
        {
            try
            {
                var newQuantity = (int)numNewQuantity.Value;
                var currentQuantity = _inventoryItem.Quantity;
                var adjustment = newQuantity - currentQuantity;

                lblAdjustment.Text = adjustment.ToString("+#;-#;0");
                
                if (adjustment > 0)
                {
                    lblAdjustment.ForeColor = Color.Green;
                    lblAdjustmentLabel.Text = "Stock Increase:";
                }
                else if (adjustment < 0)
                {
                    lblAdjustment.ForeColor = Color.Red;
                    lblAdjustmentLabel.Text = "Stock Decrease:";
                }
                else
                {
                    lblAdjustment.ForeColor = Color.Black;
                    lblAdjustmentLabel.Text = "No Change:";
                }

                // Update new status preview
                var newStatus = GetStockStatus(newQuantity, _inventoryItem.Product?.MinimumStockLevel ?? 0);
                lblNewStatus.Text = newStatus;
                lblNewStatus.ForeColor = GetStatusColor(newStatus);
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Error updating adjustment display: {ex.Message}");
            }
        }

        private string GetStockStatus(int currentStock, int minStock)
        {
            if (minStock <= 0) return "Normal";
            
            if (currentStock <= 0) return "Out of Stock";
            if (currentStock <= minStock) return "Low Stock";
            if (currentStock <= minStock * 1.5) return "Normal";
            return "Well Stocked";
        }

        private Color GetStatusColor(string status)
        {
            switch (status)
            {
                case "Out of Stock": return Color.Red;
                case "Low Stock": return Color.Orange;
                case "Normal": return Color.Green;
                case "Well Stocked": return Color.DarkGreen;
                default: return Color.Black;
            }
        }

        private void ShowErrorMessage(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        #endregion
    }
} 