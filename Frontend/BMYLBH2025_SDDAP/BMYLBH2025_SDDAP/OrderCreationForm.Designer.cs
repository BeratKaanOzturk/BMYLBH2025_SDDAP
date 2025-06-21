namespace BMYLBH2025_SDDAP
{
    partial class OrderCreationForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlMain = new System.Windows.Forms.Panel();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.pnlProducts = new System.Windows.Forms.Panel();
            this.dgvAvailableProducts = new System.Windows.Forms.DataGridView();
            this.pnlProductControls = new System.Windows.Forms.Panel();
            this.lblSelectedProductPrice = new System.Windows.Forms.Label();
            this.btnAddToOrder = new System.Windows.Forms.Button();
            this.nudQuantity = new System.Windows.Forms.NumericUpDown();
            this.lblQuantity = new System.Windows.Forms.Label();
            this.pnlProductSearch = new System.Windows.Forms.Panel();
            this.txtProductSearch = new System.Windows.Forms.TextBox();
            this.lblProductsTitle = new System.Windows.Forms.Label();
            this.pnlOrder = new System.Windows.Forms.Panel();
            this.pnlOrderSummary = new System.Windows.Forms.Panel();
            this.pnlSummaryInfo = new System.Windows.Forms.Panel();
            this.lblTotalAmount = new System.Windows.Forms.Label();
            this.lblTotalItems = new System.Windows.Forms.Label();
            this.pnlOrderActions = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSaveOrder = new System.Windows.Forms.Button();
            this.dgvOrderItems = new System.Windows.Forms.DataGridView();
            this.pnlOrderControls = new System.Windows.Forms.Panel();
            this.btnUpdateQuantity = new System.Windows.Forms.Button();
            this.nudNewQuantity = new System.Windows.Forms.NumericUpDown();
            this.lblNewQuantity = new System.Windows.Forms.Label();
            this.btnRemoveItem = new System.Windows.Forms.Button();
            this.pnlOrderDetails = new System.Windows.Forms.Panel();
            this.dtpOrderDate = new System.Windows.Forms.DateTimePicker();
            this.lblOrderDate = new System.Windows.Forms.Label();
            this.cmbSupplier = new System.Windows.Forms.ComboBox();
            this.lblSupplier = new System.Windows.Forms.Label();
            this.lblOrderTitle = new System.Windows.Forms.Label();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblFormTitle = new System.Windows.Forms.Label();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.pnlProducts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAvailableProducts)).BeginInit();
            this.pnlProductControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudQuantity)).BeginInit();
            this.pnlProductSearch.SuspendLayout();
            this.pnlOrder.SuspendLayout();
            this.pnlOrderSummary.SuspendLayout();
            this.pnlSummaryInfo.SuspendLayout();
            this.pnlOrderActions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrderItems)).BeginInit();
            this.pnlOrderControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudNewQuantity)).BeginInit();
            this.pnlOrderDetails.SuspendLayout();
            this.pnlHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.BackColor = System.Drawing.Color.White;
            this.pnlMain.Controls.Add(this.splitContainer);
            this.pnlMain.Controls.Add(this.pnlHeader);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Padding = new System.Windows.Forms.Padding(10);
            this.pnlMain.Size = new System.Drawing.Size(1400, 900);
            this.pnlMain.TabIndex = 0;
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(10, 80);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.pnlProducts);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.pnlOrder);
            this.splitContainer.Size = new System.Drawing.Size(1380, 810);
            this.splitContainer.SplitterDistance = 690;
            this.splitContainer.TabIndex = 1;
            // 
            // pnlProducts
            // 
            this.pnlProducts.BackColor = System.Drawing.Color.White;
            this.pnlProducts.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlProducts.Controls.Add(this.dgvAvailableProducts);
            this.pnlProducts.Controls.Add(this.pnlProductControls);
            this.pnlProducts.Controls.Add(this.pnlProductSearch);
            this.pnlProducts.Controls.Add(this.lblProductsTitle);
            this.pnlProducts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlProducts.Location = new System.Drawing.Point(0, 0);
            this.pnlProducts.Name = "pnlProducts";
            this.pnlProducts.Padding = new System.Windows.Forms.Padding(15);
            this.pnlProducts.Size = new System.Drawing.Size(690, 810);
            this.pnlProducts.TabIndex = 0;
            // 
            // dgvAvailableProducts
            // 
            this.dgvAvailableProducts.AllowUserToAddRows = false;
            this.dgvAvailableProducts.AllowUserToDeleteRows = false;
            this.dgvAvailableProducts.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvAvailableProducts.BackgroundColor = System.Drawing.Color.White;
            this.dgvAvailableProducts.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvAvailableProducts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvAvailableProducts.Location = new System.Drawing.Point(15, 95);
            this.dgvAvailableProducts.MultiSelect = false;
            this.dgvAvailableProducts.Name = "dgvAvailableProducts";
            this.dgvAvailableProducts.ReadOnly = true;
            this.dgvAvailableProducts.RowHeadersVisible = false;
            this.dgvAvailableProducts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAvailableProducts.Size = new System.Drawing.Size(658, 620);
            this.dgvAvailableProducts.TabIndex = 3;
            this.dgvAvailableProducts.SelectionChanged += new System.EventHandler(this.dgvAvailableProducts_SelectionChanged);
            // 
            // pnlProductControls
            // 
            this.pnlProductControls.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.pnlProductControls.Controls.Add(this.lblSelectedProductPrice);
            this.pnlProductControls.Controls.Add(this.btnAddToOrder);
            this.pnlProductControls.Controls.Add(this.nudQuantity);
            this.pnlProductControls.Controls.Add(this.lblQuantity);
            this.pnlProductControls.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlProductControls.Location = new System.Drawing.Point(15, 715);
            this.pnlProductControls.Name = "pnlProductControls";
            this.pnlProductControls.Padding = new System.Windows.Forms.Padding(10);
            this.pnlProductControls.Size = new System.Drawing.Size(658, 78);
            this.pnlProductControls.TabIndex = 2;
            // 
            // lblSelectedProductPrice
            // 
            this.lblSelectedProductPrice.AutoSize = true;
            this.lblSelectedProductPrice.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblSelectedProductPrice.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.lblSelectedProductPrice.Location = new System.Drawing.Point(10, 45);
            this.lblSelectedProductPrice.Name = "lblSelectedProductPrice";
            this.lblSelectedProductPrice.Size = new System.Drawing.Size(64, 19);
            this.lblSelectedProductPrice.TabIndex = 3;
            this.lblSelectedProductPrice.Text = "Price: --";
            // 
            // btnAddToOrder
            // 
            this.btnAddToOrder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnAddToOrder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddToOrder.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnAddToOrder.ForeColor = System.Drawing.Color.White;
            this.btnAddToOrder.Location = new System.Drawing.Point(520, 10);
            this.btnAddToOrder.Name = "btnAddToOrder";
            this.btnAddToOrder.Size = new System.Drawing.Size(120, 35);
            this.btnAddToOrder.TabIndex = 2;
            this.btnAddToOrder.Text = "➕ Add to Order";
            this.btnAddToOrder.UseVisualStyleBackColor = false;
            this.btnAddToOrder.Click += new System.EventHandler(this.btnAddToOrder_Click);
            // 
            // nudQuantity
            // 
            this.nudQuantity.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.nudQuantity.Location = new System.Drawing.Point(430, 15);
            this.nudQuantity.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudQuantity.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudQuantity.Name = "nudQuantity";
            this.nudQuantity.Size = new System.Drawing.Size(80, 25);
            this.nudQuantity.TabIndex = 1;
            this.nudQuantity.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudQuantity.ValueChanged += new System.EventHandler(this.nudQuantity_ValueChanged);
            // 
            // lblQuantity
            // 
            this.lblQuantity.AutoSize = true;
            this.lblQuantity.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblQuantity.Location = new System.Drawing.Point(360, 17);
            this.lblQuantity.Name = "lblQuantity";
            this.lblQuantity.Size = new System.Drawing.Size(64, 19);
            this.lblQuantity.TabIndex = 0;
            this.lblQuantity.Text = "Quantity:";
            // 
            // pnlProductSearch
            // 
            this.pnlProductSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.pnlProductSearch.Controls.Add(this.txtProductSearch);
            this.pnlProductSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlProductSearch.Location = new System.Drawing.Point(15, 50);
            this.pnlProductSearch.Name = "pnlProductSearch";
            this.pnlProductSearch.Padding = new System.Windows.Forms.Padding(10);
            this.pnlProductSearch.Size = new System.Drawing.Size(658, 45);
            this.pnlProductSearch.TabIndex = 1;
            // 
            // txtProductSearch
            // 
            this.txtProductSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtProductSearch.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtProductSearch.ForeColor = System.Drawing.Color.Gray;
            this.txtProductSearch.Location = new System.Drawing.Point(10, 10);
            this.txtProductSearch.Name = "txtProductSearch";
            this.txtProductSearch.Size = new System.Drawing.Size(638, 25);
            this.txtProductSearch.TabIndex = 0;
            this.txtProductSearch.Text = "🔍 Search products...";
            this.txtProductSearch.TextChanged += new System.EventHandler(this.txtProductSearch_TextChanged);
            this.txtProductSearch.Enter += new System.EventHandler(this.txtProductSearch_Enter);
            this.txtProductSearch.Leave += new System.EventHandler(this.txtProductSearch_Leave);
            // 
            // lblProductsTitle
            // 
            this.lblProductsTitle.AutoSize = true;
            this.lblProductsTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblProductsTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblProductsTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.lblProductsTitle.Location = new System.Drawing.Point(15, 15);
            this.lblProductsTitle.Name = "lblProductsTitle";
            this.lblProductsTitle.Padding = new System.Windows.Forms.Padding(0, 5, 0, 10);
            this.lblProductsTitle.Size = new System.Drawing.Size(175, 35);
            this.lblProductsTitle.TabIndex = 0;
            this.lblProductsTitle.Text = "📦 Available Products";
            // 
            // pnlOrder
            // 
            this.pnlOrder.BackColor = System.Drawing.Color.White;
            this.pnlOrder.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlOrder.Controls.Add(this.pnlOrderSummary);
            this.pnlOrder.Controls.Add(this.dgvOrderItems);
            this.pnlOrder.Controls.Add(this.pnlOrderControls);
            this.pnlOrder.Controls.Add(this.pnlOrderDetails);
            this.pnlOrder.Controls.Add(this.lblOrderTitle);
            this.pnlOrder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlOrder.Location = new System.Drawing.Point(0, 0);
            this.pnlOrder.Name = "pnlOrder";
            this.pnlOrder.Padding = new System.Windows.Forms.Padding(15);
            this.pnlOrder.Size = new System.Drawing.Size(686, 810);
            this.pnlOrder.TabIndex = 0;
            // 
            // pnlOrderSummary
            // 
            this.pnlOrderSummary.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.pnlOrderSummary.Controls.Add(this.pnlSummaryInfo);
            this.pnlOrderSummary.Controls.Add(this.pnlOrderActions);
            this.pnlOrderSummary.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlOrderSummary.Location = new System.Drawing.Point(15, 715);
            this.pnlOrderSummary.Name = "pnlOrderSummary";
            this.pnlOrderSummary.Padding = new System.Windows.Forms.Padding(10);
            this.pnlOrderSummary.Size = new System.Drawing.Size(654, 78);
            this.pnlOrderSummary.TabIndex = 4;
            // 
            // pnlSummaryInfo
            // 
            this.pnlSummaryInfo.Controls.Add(this.lblTotalAmount);
            this.pnlSummaryInfo.Controls.Add(this.lblTotalItems);
            this.pnlSummaryInfo.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlSummaryInfo.Location = new System.Drawing.Point(10, 10);
            this.pnlSummaryInfo.Name = "pnlSummaryInfo";
            this.pnlSummaryInfo.Size = new System.Drawing.Size(300, 58);
            this.pnlSummaryInfo.TabIndex = 1;
            // 
            // lblTotalAmount
            // 
            this.lblTotalAmount.AutoSize = true;
            this.lblTotalAmount.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTotalAmount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.lblTotalAmount.Location = new System.Drawing.Point(0, 30);
            this.lblTotalAmount.Name = "lblTotalAmount";
            this.lblTotalAmount.Size = new System.Drawing.Size(95, 21);
            this.lblTotalAmount.TabIndex = 1;
            this.lblTotalAmount.Text = "Total: $0.00";
            // 
            // lblTotalItems
            // 
            this.lblTotalItems.AutoSize = true;
            this.lblTotalItems.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblTotalItems.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.lblTotalItems.Location = new System.Drawing.Point(0, 5);
            this.lblTotalItems.Name = "lblTotalItems";
            this.lblTotalItems.Size = new System.Drawing.Size(57, 19);
            this.lblTotalItems.TabIndex = 0;
            this.lblTotalItems.Text = "Items: 0";
            // 
            // pnlOrderActions
            // 
            this.pnlOrderActions.Controls.Add(this.btnCancel);
            this.pnlOrderActions.Controls.Add(this.btnSaveOrder);
            this.pnlOrderActions.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlOrderActions.Location = new System.Drawing.Point(400, 10);
            this.pnlOrderActions.Name = "pnlOrderActions";
            this.pnlOrderActions.Size = new System.Drawing.Size(244, 58);
            this.pnlOrderActions.TabIndex = 0;
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(10, 15);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(110, 35);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "❌ Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSaveOrder
            // 
            this.btnSaveOrder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnSaveOrder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaveOrder.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSaveOrder.ForeColor = System.Drawing.Color.White;
            this.btnSaveOrder.Location = new System.Drawing.Point(130, 15);
            this.btnSaveOrder.Name = "btnSaveOrder";
            this.btnSaveOrder.Size = new System.Drawing.Size(110, 35);
            this.btnSaveOrder.TabIndex = 0;
            this.btnSaveOrder.Text = "💾 Save Order";
            this.btnSaveOrder.UseVisualStyleBackColor = false;
            this.btnSaveOrder.Click += new System.EventHandler(this.btnSaveOrder_Click);
            // 
            // dgvOrderItems
            // 
            this.dgvOrderItems.AllowUserToAddRows = false;
            this.dgvOrderItems.AllowUserToDeleteRows = false;
            this.dgvOrderItems.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvOrderItems.BackgroundColor = System.Drawing.Color.White;
            this.dgvOrderItems.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvOrderItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvOrderItems.Location = new System.Drawing.Point(15, 160);
            this.dgvOrderItems.MultiSelect = false;
            this.dgvOrderItems.Name = "dgvOrderItems";
            this.dgvOrderItems.ReadOnly = true;
            this.dgvOrderItems.RowHeadersVisible = false;
            this.dgvOrderItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvOrderItems.Size = new System.Drawing.Size(654, 475);
            this.dgvOrderItems.TabIndex = 3;
            this.dgvOrderItems.SelectionChanged += new System.EventHandler(this.dgvOrderItems_SelectionChanged);
            // 
            // pnlOrderControls
            // 
            this.pnlOrderControls.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.pnlOrderControls.Controls.Add(this.btnUpdateQuantity);
            this.pnlOrderControls.Controls.Add(this.nudNewQuantity);
            this.pnlOrderControls.Controls.Add(this.lblNewQuantity);
            this.pnlOrderControls.Controls.Add(this.btnRemoveItem);
            this.pnlOrderControls.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlOrderControls.Location = new System.Drawing.Point(15, 635);
            this.pnlOrderControls.Name = "pnlOrderControls";
            this.pnlOrderControls.Padding = new System.Windows.Forms.Padding(10);
            this.pnlOrderControls.Size = new System.Drawing.Size(654, 80);
            this.pnlOrderControls.TabIndex = 2;
            // 
            // btnUpdateQuantity
            // 
            this.btnUpdateQuantity.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(193)))), ((int)(((byte)(7)))));
            this.btnUpdateQuantity.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdateQuantity.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnUpdateQuantity.ForeColor = System.Drawing.Color.White;
            this.btnUpdateQuantity.Location = new System.Drawing.Point(250, 35);
            this.btnUpdateQuantity.Name = "btnUpdateQuantity";
            this.btnUpdateQuantity.Size = new System.Drawing.Size(120, 30);
            this.btnUpdateQuantity.TabIndex = 3;
            this.btnUpdateQuantity.Text = "📝 Update Qty";
            this.btnUpdateQuantity.UseVisualStyleBackColor = false;
            this.btnUpdateQuantity.Click += new System.EventHandler(this.btnUpdateQuantity_Click);
            // 
            // nudNewQuantity
            // 
            this.nudNewQuantity.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.nudNewQuantity.Location = new System.Drawing.Point(120, 37);
            this.nudNewQuantity.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudNewQuantity.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudNewQuantity.Name = "nudNewQuantity";
            this.nudNewQuantity.Size = new System.Drawing.Size(80, 25);
            this.nudNewQuantity.TabIndex = 2;
            this.nudNewQuantity.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudNewQuantity.ValueChanged += new System.EventHandler(this.nudNewQuantity_ValueChanged);
            // 
            // lblNewQuantity
            // 
            this.lblNewQuantity.AutoSize = true;
            this.lblNewQuantity.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblNewQuantity.Location = new System.Drawing.Point(10, 39);
            this.lblNewQuantity.Name = "lblNewQuantity";
            this.lblNewQuantity.Size = new System.Drawing.Size(104, 19);
            this.lblNewQuantity.TabIndex = 1;
            this.lblNewQuantity.Text = "New Quantity:";
            // 
            // btnRemoveItem
            // 
            this.btnRemoveItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.btnRemoveItem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemoveItem.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnRemoveItem.ForeColor = System.Drawing.Color.White;
            this.btnRemoveItem.Location = new System.Drawing.Point(10, 10);
            this.btnRemoveItem.Name = "btnRemoveItem";
            this.btnRemoveItem.Size = new System.Drawing.Size(120, 25);
            this.btnRemoveItem.TabIndex = 0;
            this.btnRemoveItem.Text = "🗑️ Remove Item";
            this.btnRemoveItem.UseVisualStyleBackColor = false;
            this.btnRemoveItem.Click += new System.EventHandler(this.btnRemoveItem_Click);
            // 
            // pnlOrderDetails
            // 
            this.pnlOrderDetails.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.pnlOrderDetails.Controls.Add(this.dtpOrderDate);
            this.pnlOrderDetails.Controls.Add(this.lblOrderDate);
            this.pnlOrderDetails.Controls.Add(this.cmbSupplier);
            this.pnlOrderDetails.Controls.Add(this.lblSupplier);
            this.pnlOrderDetails.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlOrderDetails.Location = new System.Drawing.Point(15, 50);
            this.pnlOrderDetails.Name = "pnlOrderDetails";
            this.pnlOrderDetails.Padding = new System.Windows.Forms.Padding(10);
            this.pnlOrderDetails.Size = new System.Drawing.Size(654, 110);
            this.pnlOrderDetails.TabIndex = 1;
            // 
            // dtpOrderDate
            // 
            this.dtpOrderDate.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpOrderDate.Location = new System.Drawing.Point(10, 70);
            this.dtpOrderDate.Name = "dtpOrderDate";
            this.dtpOrderDate.Size = new System.Drawing.Size(200, 25);
            this.dtpOrderDate.TabIndex = 3;
            // 
            // lblOrderDate
            // 
            this.lblOrderDate.AutoSize = true;
            this.lblOrderDate.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblOrderDate.Location = new System.Drawing.Point(10, 50);
            this.lblOrderDate.Name = "lblOrderDate";
            this.lblOrderDate.Size = new System.Drawing.Size(85, 19);
            this.lblOrderDate.TabIndex = 2;
            this.lblOrderDate.Text = "Order Date:";
            // 
            // cmbSupplier
            // 
            this.cmbSupplier.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSupplier.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbSupplier.FormattingEnabled = true;
            this.cmbSupplier.Location = new System.Drawing.Point(10, 30);
            this.cmbSupplier.Name = "cmbSupplier";
            this.cmbSupplier.Size = new System.Drawing.Size(300, 25);
            this.cmbSupplier.TabIndex = 1;
            this.cmbSupplier.SelectedIndexChanged += new System.EventHandler(this.cmbSupplier_SelectedIndexChanged);
            // 
            // lblSupplier
            // 
            this.lblSupplier.AutoSize = true;
            this.lblSupplier.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblSupplier.Location = new System.Drawing.Point(10, 10);
            this.lblSupplier.Name = "lblSupplier";
            this.lblSupplier.Size = new System.Drawing.Size(67, 19);
            this.lblSupplier.TabIndex = 0;
            this.lblSupplier.Text = "Supplier:";
            // 
            // lblOrderTitle
            // 
            this.lblOrderTitle.AutoSize = true;
            this.lblOrderTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblOrderTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblOrderTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.lblOrderTitle.Location = new System.Drawing.Point(15, 15);
            this.lblOrderTitle.Name = "lblOrderTitle";
            this.lblOrderTitle.Padding = new System.Windows.Forms.Padding(0, 5, 0, 10);
            this.lblOrderTitle.Size = new System.Drawing.Size(115, 35);
            this.lblOrderTitle.TabIndex = 0;
            this.lblOrderTitle.Text = "🛒 Order Cart";
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.pnlHeader.Controls.Add(this.lblStatus);
            this.pnlHeader.Controls.Add(this.lblFormTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(10, 10);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Padding = new System.Windows.Forms.Padding(15, 10, 15, 10);
            this.pnlHeader.Size = new System.Drawing.Size(1380, 70);
            this.pnlHeader.TabIndex = 0;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblStatus.ForeColor = System.Drawing.Color.White;
            this.lblStatus.Location = new System.Drawing.Point(1320, 10);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Padding = new System.Windows.Forms.Padding(0, 15, 0, 0);
            this.lblStatus.Size = new System.Drawing.Size(45, 34);
            this.lblStatus.TabIndex = 1;
            this.lblStatus.Text = "Ready";
            // 
            // lblFormTitle
            // 
            this.lblFormTitle.AutoSize = true;
            this.lblFormTitle.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblFormTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblFormTitle.ForeColor = System.Drawing.Color.White;
            this.lblFormTitle.Location = new System.Drawing.Point(15, 10);
            this.lblFormTitle.Name = "lblFormTitle";
            this.lblFormTitle.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.lblFormTitle.Size = new System.Drawing.Size(194, 42);
            this.lblFormTitle.TabIndex = 0;
            this.lblFormTitle.Text = "🛒 Create Order";
            // 
            // OrderCreationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1400, 900);
            this.Controls.Add(this.pnlMain);
            this.MinimumSize = new System.Drawing.Size(1200, 800);
            this.Name = "OrderCreationForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Create Order - Inventory Management System";
            this.pnlMain.ResumeLayout(false);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.pnlProducts.ResumeLayout(false);
            this.pnlProducts.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAvailableProducts)).EndInit();
            this.pnlProductControls.ResumeLayout(false);
            this.pnlProductControls.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudQuantity)).EndInit();
            this.pnlProductSearch.ResumeLayout(false);
            this.pnlProductSearch.PerformLayout();
            this.pnlOrder.ResumeLayout(false);
            this.pnlOrder.PerformLayout();
            this.pnlOrderSummary.ResumeLayout(false);
            this.pnlSummaryInfo.ResumeLayout(false);
            this.pnlSummaryInfo.PerformLayout();
            this.pnlOrderActions.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrderItems)).EndInit();
            this.pnlOrderControls.ResumeLayout(false);
            this.pnlOrderControls.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudNewQuantity)).EndInit();
            this.pnlOrderDetails.ResumeLayout(false);
            this.pnlOrderDetails.PerformLayout();
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblFormTitle;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.Panel pnlProducts;
        private System.Windows.Forms.Label lblProductsTitle;
        private System.Windows.Forms.Panel pnlProductSearch;
        private System.Windows.Forms.TextBox txtProductSearch;
        private System.Windows.Forms.Panel pnlProductControls;
        private System.Windows.Forms.Button btnAddToOrder;
        private System.Windows.Forms.NumericUpDown nudQuantity;
        private System.Windows.Forms.Label lblQuantity;
        private System.Windows.Forms.Label lblSelectedProductPrice;
        private System.Windows.Forms.DataGridView dgvAvailableProducts;
        private System.Windows.Forms.Panel pnlOrder;
        private System.Windows.Forms.Label lblOrderTitle;
        private System.Windows.Forms.Panel pnlOrderDetails;
        private System.Windows.Forms.ComboBox cmbSupplier;
        private System.Windows.Forms.Label lblSupplier;
        private System.Windows.Forms.DateTimePicker dtpOrderDate;
        private System.Windows.Forms.Label lblOrderDate;
        private System.Windows.Forms.Panel pnlOrderControls;
        private System.Windows.Forms.Button btnRemoveItem;
        private System.Windows.Forms.Button btnUpdateQuantity;
        private System.Windows.Forms.NumericUpDown nudNewQuantity;
        private System.Windows.Forms.Label lblNewQuantity;
        private System.Windows.Forms.DataGridView dgvOrderItems;
        private System.Windows.Forms.Panel pnlOrderSummary;
        private System.Windows.Forms.Panel pnlOrderActions;
        private System.Windows.Forms.Button btnSaveOrder;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Panel pnlSummaryInfo;
        private System.Windows.Forms.Label lblTotalAmount;
        private System.Windows.Forms.Label lblTotalItems;
    }
} 