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
    public partial class OrderCreationForm : Form
    {
        #region Fields

        private readonly ApiService _apiService;
        private List<Product> _availableProducts;
        private List<Supplier> _availableSuppliers;
        private List<OrderDetail> _orderItems;
        private Order _currentOrder;
        private bool _isEditMode;
        private int _editOrderId;

        #endregion

        #region Constructor

        public OrderCreationForm(ApiService apiService, Order existingOrder = null)
        {
            _apiService = apiService ?? throw new ArgumentNullException(nameof(apiService));
            _orderItems = new List<OrderDetail>();
            _availableProducts = new List<Product>();
            _availableSuppliers = new List<Supplier>();
            _isEditMode = existingOrder != null;
            _editOrderId = existingOrder?.OrderID ?? 0;
            
            InitializeComponent();
            InitializeFormSettings();
            
            if (existingOrder != null)
            {
                LoadExistingOrder(existingOrder);
            }
            else
            {
                InitializeNewOrder();
            }
            
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
            txtProductSearch.Text = "🔍 Search products...";
            txtProductSearch.ForeColor = Color.Gray;

            // Style the DataGridViews
            StyleDataGridView(dgvAvailableProducts);
            StyleDataGridView(dgvOrderItems);

            // Initialize current order
            _currentOrder = new Order();
            
            // Initialize suppliers dropdown with placeholder
            cmbSupplier.Items.Clear();
            cmbSupplier.Items.Add(new { SupplierID = 0, Name = "Loading suppliers..." });
            cmbSupplier.DisplayMember = "Name";
            cmbSupplier.ValueMember = "SupplierID";
            cmbSupplier.SelectedIndex = 0;
            
            // Set initial state
            UpdateOrderSummary();
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

        private void InitializeNewOrder()
        {
            _currentOrder = new Order();
            this.Text = "Create New Order";
            btnSaveOrder.Text = "💾 Create Order";
        }

        private void LoadExistingOrder(Order existingOrder)
        {
            _currentOrder = existingOrder;
            _orderItems = existingOrder.OrderDetails?.ToList() ?? new List<OrderDetail>();
            this.Text = $"Edit Order #{existingOrder.OrderID}";
            btnSaveOrder.Text = "💾 Update Order";
            
            // Populate form fields
            dtpOrderDate.Value = existingOrder.OrderDate;
            if (existingOrder.SupplierID > 0)
            {
                // Will be set when suppliers are loaded
            }
        }

        #endregion

        #region Data Loading

        private async Task LoadDataAsync()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                lblStatus.Text = "Loading data...";
                lblStatus.ForeColor = Color.Blue;

                // Load data concurrently
                var productsTask = _apiService.GetAllProductsAsync();
                var suppliersTask = LoadSuppliersAsync();

                await Task.WhenAll(productsTask, suppliersTask);

                _availableProducts = productsTask.Result ?? new List<Product>();
                _availableSuppliers = suppliersTask.Result ?? new List<Supplier>();

                System.Diagnostics.Debug.WriteLine($"LoadDataAsync: Products loaded = {_availableProducts.Count}");
                System.Diagnostics.Debug.WriteLine($"LoadDataAsync: Suppliers loaded = {_availableSuppliers.Count}");

                // Update UI
                UpdateProductsGrid();
                UpdateSuppliersComboBox();
                UpdateOrderItemsGrid();
                UpdateOrderSummary();

                lblStatus.Text = "Ready";
                lblStatus.ForeColor = Color.Green;
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Error loading data: {ex.Message}");
                lblStatus.Text = "Error loading data";
                lblStatus.ForeColor = Color.Red;
                
                // Ensure we have fallback data even if everything fails
                if (_availableSuppliers == null || _availableSuppliers.Count == 0)
                {
                    _availableSuppliers = GetFallbackSuppliers();
                    UpdateSuppliersComboBox();
                }
                
                if (_availableProducts == null || _availableProducts.Count == 0)
                {
                    _availableProducts = new List<Product>();
                    UpdateProductsGrid();
                }
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private async Task<List<Supplier>> LoadSuppliersAsync()
        {
            // TEMPORARY: Always use fallback suppliers for testing
            // This ensures the dropdown is populated even if the backend is not running
            System.Diagnostics.Debug.WriteLine("Using fallback suppliers for testing");
            return GetFallbackSuppliers();
            
            /* Original code - uncomment when backend is running
            try
            {
                var suppliers = await _apiService.GetAllSuppliersAsync();
                if (suppliers != null && suppliers.Count > 0)
                {
                    return suppliers;
                }
                else
                {
                    // API returned empty list, use fallback
                    return GetFallbackSuppliers();
                }
            }
            catch (Exception ex)
            {
                // API call failed, use fallback suppliers silently
                // Don't show error message here to avoid multiple popups
                System.Diagnostics.Debug.WriteLine($"Failed to load suppliers from server: {ex.Message}. Using fallback data.");
                return GetFallbackSuppliers();
            }
            */
        }

        private List<Supplier> GetFallbackSuppliers()
        {
            return new List<Supplier>
            {
                new Supplier { SupplierID = 1, Name = "TechCorp Ltd", ContactPerson = "John Smith", Email = "john@techcorp.com", Phone = "+1-555-0123" },
                new Supplier { SupplierID = 2, Name = "Office Solutions Inc", ContactPerson = "Sarah Johnson", Email = "sarah@officesolutions.com", Phone = "+1-555-0456" },
                new Supplier { SupplierID = 3, Name = "Furniture World", ContactPerson = "Mike Brown", Email = "mike@furnitureworld.com", Phone = "+1-555-0789" }
            };
        }

        private void UpdateProductsGrid()
        {
            if (_availableProducts == null) return;

            var displayProducts = _availableProducts.Select(p => new
            {
                ID = p.ProductID,
                Name = p.Name,
                Description = p.Description,
                Category = p.Category?.Name ?? p.CategoryName ?? "Unknown",
                Stock = GetProductStock(p.ProductID),
                Price = p.Price,
                PriceFormatted = p.Price.ToString("C", System.Globalization.CultureInfo.CurrentUICulture),
                IsLowStock = GetProductStock(p.ProductID) <= p.MinimumStockLevel
            }).ToList();

            dgvAvailableProducts.DataSource = displayProducts;

            // Configure columns
            if (dgvAvailableProducts.Columns.Count > 0)
            {
                dgvAvailableProducts.Columns["ID"].Visible = false;
                dgvAvailableProducts.Columns["Name"].HeaderText = "Product Name";
                dgvAvailableProducts.Columns["Name"].Width = 200;
                dgvAvailableProducts.Columns["Description"].HeaderText = "Description";
                dgvAvailableProducts.Columns["Description"].Width = 250;
                dgvAvailableProducts.Columns["Category"].HeaderText = "Category";
                dgvAvailableProducts.Columns["Category"].Width = 120;
                dgvAvailableProducts.Columns["Stock"].HeaderText = "Stock";
                dgvAvailableProducts.Columns["Stock"].Width = 80;
                dgvAvailableProducts.Columns["Price"].Visible = false; // Hide raw price
                dgvAvailableProducts.Columns["PriceFormatted"].HeaderText = "Price";
                dgvAvailableProducts.Columns["PriceFormatted"].Width = 100;
                dgvAvailableProducts.Columns["IsLowStock"].HeaderText = "Low Stock";
                dgvAvailableProducts.Columns["IsLowStock"].Width = 80;

                // Style low stock rows
                foreach (DataGridViewRow row in dgvAvailableProducts.Rows)
                {
                    if (Convert.ToBoolean(row.Cells["IsLowStock"].Value))
                    {
                        row.DefaultCellStyle.BackColor = Color.LightYellow;
                        row.DefaultCellStyle.ForeColor = Color.DarkOrange;
                    }
                }
            }
        }

        private void UpdateSuppliersComboBox()
        {
            try
            {
                cmbSupplier.Items.Clear();
                cmbSupplier.Items.Add(new { SupplierID = 0, Name = "-- Select Supplier --" });
                
                System.Diagnostics.Debug.WriteLine($"UpdateSuppliersComboBox: _availableSuppliers count = {_availableSuppliers?.Count ?? 0}");
                
                if (_availableSuppliers != null)
                {
                    foreach (var supplier in _availableSuppliers)
                    {
                        cmbSupplier.Items.Add(new { SupplierID = supplier.SupplierID, Name = supplier.Name });
                        System.Diagnostics.Debug.WriteLine($"Added supplier: {supplier.Name} (ID: {supplier.SupplierID})");
                    }
                }
                
                cmbSupplier.DisplayMember = "Name";
                cmbSupplier.ValueMember = "SupplierID";
                
                if (cmbSupplier.Items.Count > 0)
                {
                    cmbSupplier.SelectedIndex = 0;
                }
                
                System.Diagnostics.Debug.WriteLine($"ComboBox total items: {cmbSupplier.Items.Count}");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in UpdateSuppliersComboBox: {ex.Message}");
                ShowErrorMessage($"Error updating suppliers dropdown: {ex.Message}");
            }

            // Set existing supplier if in edit mode
            if (_isEditMode && _currentOrder.SupplierID > 0)
            {
                for (int i = 0; i < cmbSupplier.Items.Count; i++)
                {
                    var item = cmbSupplier.Items[i];
                    if (item != null)
                    {
                        var itemType = item.GetType();
                        var supplierIdProperty = itemType.GetProperty("SupplierID");
                        if (supplierIdProperty != null)
                        {
                            var supplierId = (int)supplierIdProperty.GetValue(item);
                            if (supplierId == _currentOrder.SupplierID)
                            {
                                cmbSupplier.SelectedIndex = i;
                                break;
                            }
                        }
                    }
                }
            }
        }

        private void UpdateOrderItemsGrid()
        {
            if (_orderItems == null) return;

            var displayItems = _orderItems.Select(item => new
            {
                ProductID = item.ProductID,
                ProductName = GetProductName(item.ProductID),
                Quantity = item.Quantity,
                UnitPrice = item.UnitPrice,
                UnitPriceFormatted = item.UnitPrice.ToString("C", System.Globalization.CultureInfo.CurrentUICulture),
                Subtotal = item.CalculateSubtotal(),
                SubtotalFormatted = item.CalculateSubtotal().ToString("C", System.Globalization.CultureInfo.CurrentUICulture)
            }).ToList();

            dgvOrderItems.DataSource = displayItems;

            // Configure columns
            if (dgvOrderItems.Columns.Count > 0)
            {
                dgvOrderItems.Columns["ProductID"].Visible = false;
                dgvOrderItems.Columns["ProductName"].HeaderText = "Product Name";
                dgvOrderItems.Columns["ProductName"].Width = 200;
                dgvOrderItems.Columns["Quantity"].HeaderText = "Quantity";
                dgvOrderItems.Columns["Quantity"].Width = 80;
                dgvOrderItems.Columns["UnitPrice"].Visible = false;
                dgvOrderItems.Columns["UnitPriceFormatted"].HeaderText = "Unit Price";
                dgvOrderItems.Columns["UnitPriceFormatted"].Width = 100;
                dgvOrderItems.Columns["Subtotal"].Visible = false;
                dgvOrderItems.Columns["SubtotalFormatted"].HeaderText = "Subtotal";
                dgvOrderItems.Columns["SubtotalFormatted"].Width = 100;
            }
        }

        #endregion

        #region Helper Methods

        private string GetProductName(int productId)
        {
            return _availableProducts?.FirstOrDefault(p => p.ProductID == productId)?.Name ?? "Unknown Product";
        }

        private int GetProductStock(int productId)
        {
            // First try to get from product's StockQuantity property
            var product = _availableProducts?.FirstOrDefault(p => p.ProductID == productId);
            if (product?.StockQuantity.HasValue == true)
            {
                return product.StockQuantity.Value;
            }
            
            // Fallback to mock data or default
            switch (productId)
            {
                case 1: return 25; // Wireless Mouse
                case 2: return 8;  // Mechanical Keyboard  
                case 3: return 15; // USB-C Cable
                default: return 0;
            }
        }

        private void UpdateOrderSummary()
        {
            var totalItems = _orderItems.Sum(item => item.Quantity);
            var totalAmount = _orderItems.Sum(item => item.CalculateSubtotal());

            lblTotalItems.Text = $"Items: {totalItems}";
            lblTotalAmount.Text = $"Total: {totalAmount:C}";

            // Update current order
            _currentOrder.TotalAmount = totalAmount;
        }

        private void UpdateButtonStates()
        {
            btnAddToOrder.Enabled = dgvAvailableProducts.SelectedRows.Count > 0 && nudQuantity.Value > 0;
            btnRemoveItem.Enabled = dgvOrderItems.SelectedRows.Count > 0;
            btnUpdateQuantity.Enabled = dgvOrderItems.SelectedRows.Count > 0 && nudNewQuantity.Value > 0;
            btnSaveOrder.Enabled = _orderItems.Count > 0 && cmbSupplier.SelectedIndex > 0;
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

        #region Event Handlers

        private void txtProductSearch_TextChanged(object sender, EventArgs e)
        {
            var searchTerm = txtProductSearch.Text;
            if (searchTerm == "🔍 Search products...") return;
            
            FilterProducts(searchTerm);
        }

        private void txtProductSearch_Enter(object sender, EventArgs e)
        {
            if (txtProductSearch.Text == "🔍 Search products...")
            {
                txtProductSearch.Text = "";
                txtProductSearch.ForeColor = Color.Black;
            }
        }

        private void txtProductSearch_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtProductSearch.Text))
            {
                txtProductSearch.Text = "🔍 Search products...";
                txtProductSearch.ForeColor = Color.Gray;
            }
        }

        private void FilterProducts(string searchTerm)
        {
            if (_availableProducts == null) return;

            List<Product> filteredProducts;
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                filteredProducts = _availableProducts;
            }
            else
            {
                filteredProducts = _availableProducts.Where(p =>
                    (p.Name?.ToLower().Contains(searchTerm.ToLower()) ?? false) ||
                    (p.Description?.ToLower().Contains(searchTerm.ToLower()) ?? false) ||
                    (p.Category?.Name?.ToLower().Contains(searchTerm.ToLower()) ?? false)
                ).ToList();
            }

            var displayData = filteredProducts.Select(p => new
            {
                ID = p.ProductID,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                PriceFormatted = p.Price.ToString("C", System.Globalization.CultureInfo.CurrentUICulture),
                Category = p.Category?.Name ?? "Uncategorized",
                Stock = GetProductStock(p.ProductID)
            }).ToList();

            dgvAvailableProducts.DataSource = displayData;
        }

        private void dgvAvailableProducts_SelectionChanged(object sender, EventArgs e)
        {
            UpdateButtonStates();
            
            if (dgvAvailableProducts.SelectedRows.Count > 0)
            {
                var row = dgvAvailableProducts.SelectedRows[0];
                var price = Convert.ToDecimal(row.Cells["Price"].Value);
                lblSelectedProductPrice.Text = $"Price: {price.ToString("C", System.Globalization.CultureInfo.CurrentUICulture)}";
            }
            else
            {
                lblSelectedProductPrice.Text = "Price: --";
            }
        }

        private void dgvOrderItems_SelectionChanged(object sender, EventArgs e)
        {
            UpdateButtonStates();
            
            if (dgvOrderItems.SelectedRows.Count > 0)
            {
                var row = dgvOrderItems.SelectedRows[0];
                var currentQuantity = Convert.ToInt32(row.Cells["Quantity"].Value);
                nudNewQuantity.Value = currentQuantity;
            }
        }

        private void nudQuantity_ValueChanged(object sender, EventArgs e)
        {
            UpdateButtonStates();
        }

        private void nudNewQuantity_ValueChanged(object sender, EventArgs e)
        {
            UpdateButtonStates();
        }

        private void cmbSupplier_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateButtonStates();
            
            var selectedItem = cmbSupplier.SelectedItem;
            if (selectedItem != null)
            {
                var itemType = selectedItem.GetType();
                var supplierIdProperty = itemType.GetProperty("SupplierID");
                if (supplierIdProperty != null)
                {
                    var supplierId = (int)supplierIdProperty.GetValue(selectedItem);
                    if (supplierId > 0)
                    {
                        _currentOrder.SupplierID = supplierId;
                    }
                }
            }
        }

        private void btnAddToOrder_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvAvailableProducts.SelectedRows.Count == 0)
                {
                    ShowErrorMessage("Please select a product to add.");
                    return;
                }

                var row = dgvAvailableProducts.SelectedRows[0];
                var productId = Convert.ToInt32(row.Cells["ID"].Value);
                var price = Convert.ToDecimal(row.Cells["Price"].Value);
                var quantity = (int)nudQuantity.Value;
                var productName = row.Cells["Name"].Value.ToString();
                var availableStock = GetProductStock(productId);

                // Check stock before adding to order
                var existingItem = _orderItems.FirstOrDefault(item => item.ProductID == productId);
                var currentOrderQuantity = existingItem?.Quantity ?? 0;
                var totalRequestedQuantity = currentOrderQuantity + quantity;

                if (totalRequestedQuantity > availableStock)
                {
                    ShowErrorMessage($"Insufficient stock for {productName}.\n" +
                                   $"Available: {availableStock}\n" +
                                   $"Currently in order: {currentOrderQuantity}\n" +
                                   $"Requesting: {quantity}\n" +
                                   $"Total would be: {totalRequestedQuantity}");
                    return;
                }

                // Add to order
                if (existingItem != null)
                {
                    existingItem.Quantity += quantity;
                }
                else
                {
                    var orderDetail = new OrderDetail
                    {
                        ProductID = productId,
                        Quantity = quantity,
                        UnitPrice = price
                    };
                    _orderItems.Add(orderDetail);
                }

                UpdateOrderItemsGrid();
                UpdateOrderSummary();
                UpdateButtonStates();

                ShowInfoMessage($"Added {quantity} x {productName} to order.");
                nudQuantity.Value = 1; // Reset quantity
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Error adding product to order: {ex.Message}");
            }
        }

        private void btnRemoveItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvOrderItems.SelectedRows.Count == 0)
                {
                    ShowErrorMessage("Please select an item to remove.");
                    return;
                }

                var row = dgvOrderItems.SelectedRows[0];
                var productId = Convert.ToInt32(row.Cells["ProductID"].Value);
                var productName = row.Cells["ProductName"].Value.ToString();

                var itemToRemove = _orderItems.FirstOrDefault(item => item.ProductID == productId);
                if (itemToRemove != null)
                {
                    _orderItems.Remove(itemToRemove);
                    UpdateOrderItemsGrid();
                    UpdateOrderSummary();
                    UpdateButtonStates();
                    ShowInfoMessage($"Removed {productName} from order.");
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Error removing item: {ex.Message}");
            }
        }

        private void btnUpdateQuantity_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvOrderItems.SelectedRows.Count == 0)
                {
                    ShowErrorMessage("Please select an item to update.");
                    return;
                }

                var row = dgvOrderItems.SelectedRows[0];
                var productId = Convert.ToInt32(row.Cells["ProductID"].Value);
                var productName = row.Cells["ProductName"].Value.ToString();
                var newQuantity = (int)nudNewQuantity.Value;

                var itemToUpdate = _orderItems.FirstOrDefault(item => item.ProductID == productId);
                if (itemToUpdate != null)
                {
                    if (newQuantity <= 0)
                    {
                        _orderItems.Remove(itemToUpdate);
                        ShowInfoMessage($"Removed {productName} from order (quantity was 0).");
                    }
                    else
                    {
                        // Check stock before updating quantity
                        var availableStock = GetProductStock(productId);
                        if (newQuantity > availableStock)
                        {
                            ShowErrorMessage($"Insufficient stock for {productName}.\n" +
                                           $"Available: {availableStock}\n" +
                                           $"Requested: {newQuantity}");
                            return;
                        }

                        itemToUpdate.Quantity = newQuantity;
                        ShowInfoMessage($"Updated {productName} quantity to {newQuantity}.");
                    }
                    
                    UpdateOrderItemsGrid();
                    UpdateOrderSummary();
                    UpdateButtonStates();
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Error updating quantity: {ex.Message}");
            }
        }

        private async void btnSaveOrder_Click(object sender, EventArgs e)
        {
            try
            {
                if (_orderItems.Count == 0)
                {
                    ShowErrorMessage("Please add at least one item to the order.");
                    return;
                }

                if (cmbSupplier.SelectedIndex <= 0)
                {
                    ShowErrorMessage("Please select a supplier.");
                    return;
                }

                this.Cursor = Cursors.WaitCursor;
                btnSaveOrder.Enabled = false;
                lblStatus.Text = "Checking backend connectivity...";
                lblStatus.ForeColor = Color.Blue;

                // Check backend connectivity first
                var isBackendAvailable = await _apiService.IsBackendAvailableAsync();
                if (!isBackendAvailable)
                {
                    ShowErrorMessage("Cannot connect to backend server. Please ensure the backend is running and try again.");
                    lblStatus.Text = "Backend not available";
                    lblStatus.ForeColor = Color.Red;
                    return;
                }

                lblStatus.Text = _isEditMode ? "Updating order..." : "Creating order...";
                lblStatus.ForeColor = Color.Blue;

                // Prepare order data
                _currentOrder.OrderDate = dtpOrderDate.Value;
                _currentOrder.OrderDetails = _orderItems;
                _currentOrder.RecalculateTotal();

                Order savedOrder;
                if (_isEditMode)
                {
                    savedOrder = await _apiService.UpdateOrderAsync(_editOrderId, _currentOrder);
                }
                else
                {
                    savedOrder = await _apiService.CreateOrderAsync(_currentOrder);
                }

                if (savedOrder != null)
                {
                    lblStatus.Text = _isEditMode ? "Order updated successfully!" : "Order created successfully!";
                    lblStatus.ForeColor = Color.Green;
                    
                    ShowInfoMessage($"Order #{savedOrder.OrderID} {(_isEditMode ? "updated" : "created")} successfully!");
                    
                    // Close the form after a short delay
                    await Task.Delay(1500);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    throw new Exception("Failed to save order - no data returned from server.");
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Error saving order: {ex.Message}");
                lblStatus.Text = "Error saving order";
                lblStatus.ForeColor = Color.Red;
            }
            finally
            {
                this.Cursor = Cursors.Default;
                btnSaveOrder.Enabled = true;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (_orderItems.Count > 0)
            {
                var result = MessageBox.Show(
                    "You have unsaved changes. Are you sure you want to cancel?",
                    "Confirm Cancel",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.No)
                    return;
            }

            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        #endregion
    }
} 