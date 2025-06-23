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
    public partial class OrderManagementForm : Form
    {
        #region Fields

        private readonly ApiService _apiService;
        private List<Order> _orders;
        private List<Order> _filteredOrders;
        private Order _selectedOrder;

        #endregion

        #region Constructor

        public OrderManagementForm(ApiService apiService)
        {
            _apiService = apiService ?? throw new ArgumentNullException(nameof(apiService));
            _orders = new List<Order>();
            _filteredOrders = new List<Order>();
            
            InitializeComponent();
            InitializeFormSettings();
            _ = LoadDataAsync();
        }

        #endregion

        #region Initialization

        private void InitializeFormSettings()
        {
            // Set form properties
            this.WindowState = FormWindowState.Maximized;
            this.ShowInTaskbar = false;
            this.StartPosition = FormStartPosition.CenterParent;

            // Set placeholder text for search
            txtSearch.Text = "🔍 Search orders...";
            txtSearch.ForeColor = Color.Gray;

            // Style the DataGridViews
            StyleDataGridView(dgvOrders);
            StyleDataGridView(dgvOrderDetails);

            // Set initial state
            UpdateButtonStates();
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
                this.Cursor = Cursors.WaitCursor;
                lblStatus.Text = "Loading orders...";
                lblStatus.ForeColor = Color.Blue;

                _orders = await _apiService.GetAllOrdersAsync() ?? new List<Order>();
                _filteredOrders = new List<Order>(_orders);

                UpdateOrdersGrid();
                UpdateOrderSummary();

                lblStatus.Text = $"Loaded {_orders.Count} orders";
                lblStatus.ForeColor = Color.Green;
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Error loading orders: {ex.Message}");
                lblStatus.Text = "Error loading orders";
                lblStatus.ForeColor = Color.Red;
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void UpdateOrdersGrid()
        {
            if (dgvOrders == null || _filteredOrders == null) return;

            var displayData = _filteredOrders.Select(o => new
            {
                ID = o.OrderID,
                OrderDate = o.OrderDate.ToString("yyyy-MM-dd"),
                SupplierName = o.Supplier?.Name ?? "Unknown Supplier",
                Status = o.Status,
                TotalAmount = o.TotalAmount,
                TotalAmountFormatted = o.TotalAmount.ToString("C"),
                ItemCount = o.OrderDetails?.Count ?? 0,
                StatusColor = GetStatusColor(o.Status)
            }).ToList();

            dgvOrders.DataSource = displayData;

            // Configure columns
            if (dgvOrders.Columns.Count > 0)
            {
                dgvOrders.Columns["ID"].HeaderText = "Order ID";
                dgvOrders.Columns["ID"].Width = 80;
                dgvOrders.Columns["OrderDate"].HeaderText = "Order Date";
                dgvOrders.Columns["OrderDate"].Width = 120;
                dgvOrders.Columns["SupplierName"].HeaderText = "Supplier";
                dgvOrders.Columns["SupplierName"].Width = 200;
                dgvOrders.Columns["Status"].HeaderText = "Status";
                dgvOrders.Columns["Status"].Width = 100;
                dgvOrders.Columns["TotalAmount"].Visible = false;
                dgvOrders.Columns["TotalAmountFormatted"].HeaderText = "Total Amount";
                dgvOrders.Columns["TotalAmountFormatted"].Width = 120;
                dgvOrders.Columns["ItemCount"].HeaderText = "Items";
                dgvOrders.Columns["ItemCount"].Width = 80;
                dgvOrders.Columns["StatusColor"].Visible = false;

                // Color code rows based on status
                dgvOrders.CellFormatting += DgvOrders_CellFormatting;
            }
        }

        private void DgvOrders_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < _filteredOrders.Count)
            {
                var order = _filteredOrders[e.RowIndex];
                var statusColor = GetStatusColor(order.Status);
                
                // Set row color based on status
                dgvOrders.Rows[e.RowIndex].DefaultCellStyle.BackColor = statusColor;
                dgvOrders.Rows[e.RowIndex].DefaultCellStyle.ForeColor = 
                    statusColor == Color.LightGray ? Color.Black : Color.Black;
            }
        }

        private Color GetStatusColor(string status)
        {
            switch (status?.ToLower())
            {
                case "pending":
                    return Color.LightYellow;
                case "processing":
                    return Color.LightBlue;
                case "completed":
                    return Color.LightGreen;
                case "cancelled":
                    return Color.LightCoral;
                default:
                    return Color.White;
            }
        }

        private void UpdateOrderDetailsGrid()
        {
            if (dgvOrderDetails == null || _selectedOrder?.OrderDetails == null) return;

            var displayData = _selectedOrder.OrderDetails.Select(item => new
            {
                ID = item.OrderDetailID,
                ProductID = item.ProductID,
                ProductName = item.Product?.Name ?? "Unknown Product",
                Quantity = item.Quantity,
                UnitPrice = item.UnitPrice,
                UnitPriceFormatted = item.UnitPrice.ToString("C"),
                Subtotal = item.CalculateSubtotal(),
                SubtotalFormatted = item.CalculateSubtotal().ToString("C")
            }).ToList();

            dgvOrderDetails.DataSource = displayData;

            // Configure columns
            if (dgvOrderDetails.Columns.Count > 0)
            {
                dgvOrderDetails.Columns["ID"].Visible = false;
                dgvOrderDetails.Columns["ProductID"].Visible = false;
                dgvOrderDetails.Columns["ProductName"].HeaderText = "Product";
                dgvOrderDetails.Columns["ProductName"].Width = 250;
                dgvOrderDetails.Columns["Quantity"].HeaderText = "Quantity";
                dgvOrderDetails.Columns["Quantity"].Width = 80;
                dgvOrderDetails.Columns["UnitPrice"].Visible = false;
                dgvOrderDetails.Columns["UnitPriceFormatted"].HeaderText = "Unit Price";
                dgvOrderDetails.Columns["UnitPriceFormatted"].Width = 100;
                dgvOrderDetails.Columns["Subtotal"].Visible = false;
                dgvOrderDetails.Columns["SubtotalFormatted"].HeaderText = "Subtotal";
                dgvOrderDetails.Columns["SubtotalFormatted"].Width = 120;
            }
        }

        private void UpdateOrderSummary()
        {
            var totalOrders = _orders.Count;
            var totalValue = _orders.Sum(o => o.TotalAmount);
            var pendingOrders = _orders.Count(o => o.Status?.ToLower() == "pending");
            var completedOrders = _orders.Count(o => o.Status?.ToLower() == "completed");

            lblTotalOrders.Text = $"Total Orders: {totalOrders}";
            lblTotalValue.Text = $"Total Value: {totalValue:C}";
            lblPendingOrders.Text = $"Pending: {pendingOrders}";
            lblCompletedOrders.Text = $"Completed: {completedOrders}";
        }

        #endregion

        #region Helper Methods

        private void UpdateButtonStates()
        {
            var hasSelection = dgvOrders.SelectedRows.Count > 0;
            btnEditOrder.Enabled = hasSelection;
            btnDeleteOrder.Enabled = hasSelection;
            btnViewDetails.Enabled = hasSelection;
            
            // Enable cancel only for pending orders
            btnCancelOrder.Enabled = hasSelection && _selectedOrder?.Status?.ToLower() == "pending";
        }

        private void ShowErrorMessage(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void ShowInfoMessage(string message)
        {
            MessageBox.Show(message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ShowSuccessMessage(string message)
        {
            MessageBox.Show(message, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #endregion

        #region Event Handlers

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            var searchTerm = txtSearch.Text;
            if (searchTerm == "🔍 Search orders...") return;
            
            FilterOrders(searchTerm);
        }

        private void txtSearch_Enter(object sender, EventArgs e)
        {
            if (txtSearch.Text == "🔍 Search orders...")
            {
                txtSearch.Text = "";
                txtSearch.ForeColor = Color.Black;
            }
        }

        private void txtSearch_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                txtSearch.Text = "🔍 Search orders...";
                txtSearch.ForeColor = Color.Gray;
            }
        }

        private void FilterOrders(string searchTerm)
        {
            if (_orders == null) return;

            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                _filteredOrders = new List<Order>(_orders);
            }
            else
            {
                _filteredOrders = _orders.Where(o =>
                    (o.OrderID.ToString().Contains(searchTerm)) ||
                    (o.Supplier?.Name?.ToLower().Contains(searchTerm.ToLower()) ?? false) ||
                    (o.Status?.ToLower().Contains(searchTerm.ToLower()) ?? false) ||
                    (o.OrderDate.ToString("yyyy-MM-dd").Contains(searchTerm))
                ).ToList();
            }

            UpdateOrdersGrid();
        }

        private void dgvOrders_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvOrders.SelectedRows.Count > 0)
            {
                var selectedIndex = dgvOrders.SelectedRows[0].Index;
                if (selectedIndex >= 0 && selectedIndex < _filteredOrders.Count)
                {
                    _selectedOrder = _filteredOrders[selectedIndex];
                    UpdateOrderDetailsGrid();
                    DisplayOrderInfo();
                }
            }
            else
            {
                _selectedOrder = null;
                dgvOrderDetails.DataSource = null;
                ClearOrderInfo();
            }
            
            UpdateButtonStates();
        }

        private void DisplayOrderInfo()
        {
            if (_selectedOrder == null) return;

            lblOrderId.Text = $"Order ID: {_selectedOrder.OrderID}";
            lblOrderDate.Text = $"Date: {_selectedOrder.OrderDate:yyyy-MM-dd}";
            lblOrderSupplier.Text = $"Supplier: {_selectedOrder.Supplier?.Name ?? "Unknown"}";
            lblOrderStatus.Text = $"Status: {_selectedOrder.Status}";
            lblOrderTotal.Text = $"Total: {_selectedOrder.TotalAmount:C}";
            
            // Set status color
            var statusColor = GetStatusColor(_selectedOrder.Status);
            lblOrderStatus.BackColor = statusColor;
        }

        private void ClearOrderInfo()
        {
            lblOrderId.Text = "Order ID: --";
            lblOrderDate.Text = "Date: --";
            lblOrderSupplier.Text = "Supplier: --";
            lblOrderStatus.Text = "Status: --";
            lblOrderTotal.Text = "Total: --";
            lblOrderStatus.BackColor = Color.Transparent;
        }

        private void btnCreateOrder_Click(object sender, EventArgs e)
        {
            try
            {
                var orderCreationForm = new OrderCreationForm(_apiService);
                var result = orderCreationForm.ShowDialog(this);
                
                if (result == DialogResult.OK)
                {
                    _ = LoadDataAsync();
                    ShowSuccessMessage("Order created successfully!");
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Error opening order creation form: {ex.Message}");
            }
        }

        private void btnEditOrder_Click(object sender, EventArgs e)
        {
            try
            {
                if (_selectedOrder == null)
                {
                    ShowErrorMessage("Please select an order to edit.");
                    return;
                }

                var orderCreationForm = new OrderCreationForm(_apiService, _selectedOrder);
                var result = orderCreationForm.ShowDialog(this);
                
                if (result == DialogResult.OK)
                {
                    _ = LoadDataAsync();
                    ShowSuccessMessage("Order updated successfully!");
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Error opening order edit form: {ex.Message}");
            }
        }

        private async void btnDeleteOrder_Click(object sender, EventArgs e)
        {
            try
            {
                if (_selectedOrder == null)
                {
                    ShowErrorMessage("Please select an order to delete.");
                    return;
                }

                var result = MessageBox.Show(
                    $"Are you sure you want to delete Order #{_selectedOrder.OrderID}?\n\nThis action cannot be undone.",
                    "Confirm Delete",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    this.Cursor = Cursors.WaitCursor;
                    lblStatus.Text = "Deleting order...";
                    lblStatus.ForeColor = Color.Blue;

                    var success = await _apiService.DeleteOrderAsync(_selectedOrder.OrderID);
                    
                    if (success)
                    {
                        await LoadDataAsync();
                        ShowSuccessMessage($"Order #{_selectedOrder?.OrderID} deleted successfully!");
                    }
                    else
                    {
                        ShowErrorMessage("Failed to delete order. Please try again.");
                    }
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Error deleting order: {ex.Message}");
                lblStatus.Text = "Error deleting order";
                lblStatus.ForeColor = Color.Red;
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private async void btnCancelOrder_Click(object sender, EventArgs e)
        {
            try
            {
                if (_selectedOrder == null)
                {
                    ShowErrorMessage("Please select an order to cancel.");
                    return;
                }

                if (_selectedOrder.Status?.ToLower() != "pending")
                {
                    ShowErrorMessage("Only pending orders can be cancelled.");
                    return;
                }

                var result = MessageBox.Show(
                    $"Are you sure you want to cancel Order #{_selectedOrder.OrderID}?",
                    "Confirm Cancel",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    this.Cursor = Cursors.WaitCursor;
                    lblStatus.Text = "Cancelling order...";
                    lblStatus.ForeColor = Color.Blue;

                    _selectedOrder.Status = "Cancelled";
                    var updatedOrder = await _apiService.UpdateOrderAsync(_selectedOrder.OrderID, _selectedOrder);
                    
                    if (updatedOrder != null)
                    {
                        await LoadDataAsync();
                        ShowSuccessMessage($"Order #{_selectedOrder.OrderID} cancelled successfully!");
                    }
                    else
                    {
                        ShowErrorMessage("Failed to cancel order. Please try again.");
                    }
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Error cancelling order: {ex.Message}");
                lblStatus.Text = "Error cancelling order";
                lblStatus.ForeColor = Color.Red;
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void btnViewDetails_Click(object sender, EventArgs e)
        {
            if (_selectedOrder == null)
            {
                ShowErrorMessage("Please select an order to view details.");
                return;
            }

            // Focus on the order details grid
            dgvOrderDetails.Focus();
            
            // Show detailed information
            var details = $"Order Details:\n\n" +
                         $"Order ID: {_selectedOrder.OrderID}\n" +
                         $"Date: {_selectedOrder.OrderDate:yyyy-MM-dd HH:mm}\n" +
                         $"Supplier: {_selectedOrder.Supplier?.Name ?? "Unknown"}\n" +
                         $"Status: {_selectedOrder.Status}\n" +
                         $"Total Items: {_selectedOrder.OrderDetails?.Count ?? 0}\n" +
                         $"Total Amount: {_selectedOrder.TotalAmount:C}\n\n" +
                         "Items:\n";

            if (_selectedOrder.OrderDetails != null)
            {
                foreach (var item in _selectedOrder.OrderDetails)
                {
                    details += $"• {item.Product?.Name ?? "Unknown Product"} - Qty: {item.Quantity}, Price: {item.UnitPrice:C}, Subtotal: {item.CalculateSubtotal():C}\n";
                }
            }

            MessageBox.Show(details, "Order Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            await LoadDataAsync();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion
    }
} 