namespace BMYLBH2025_SDDAP
{
    partial class OrderManagementForm
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
            this.pnlOrders = new System.Windows.Forms.Panel();
            this.dgvOrders = new System.Windows.Forms.DataGridView();
            this.pnlOrderActions = new System.Windows.Forms.Panel();
            this.btnViewDetails = new System.Windows.Forms.Button();
            this.btnCancelOrder = new System.Windows.Forms.Button();
            this.btnDeleteOrder = new System.Windows.Forms.Button();
            this.btnEditOrder = new System.Windows.Forms.Button();
            this.btnCreateOrder = new System.Windows.Forms.Button();
            this.pnlSearch = new System.Windows.Forms.Panel();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lblOrdersTitle = new System.Windows.Forms.Label();
            this.pnlOrderDetails = new System.Windows.Forms.Panel();
            this.dgvOrderDetails = new System.Windows.Forms.DataGridView();
            this.pnlOrderInfo = new System.Windows.Forms.Panel();
            this.lblOrderTotal = new System.Windows.Forms.Label();
            this.lblOrderStatus = new System.Windows.Forms.Label();
            this.lblOrderSupplier = new System.Windows.Forms.Label();
            this.lblOrderDate = new System.Windows.Forms.Label();
            this.lblOrderId = new System.Windows.Forms.Label();
            this.lblOrderDetailsTitle = new System.Windows.Forms.Label();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.pnlSummary = new System.Windows.Forms.Panel();
            this.lblCompletedOrders = new System.Windows.Forms.Label();
            this.lblPendingOrders = new System.Windows.Forms.Label();
            this.lblTotalValue = new System.Windows.Forms.Label();
            this.lblTotalOrders = new System.Windows.Forms.Label();
            this.pnlHeaderActions = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblFormTitle = new System.Windows.Forms.Label();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.pnlOrders.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrders)).BeginInit();
            this.pnlOrderActions.SuspendLayout();
            this.pnlSearch.SuspendLayout();
            this.pnlOrderDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrderDetails)).BeginInit();
            this.pnlOrderInfo.SuspendLayout();
            this.pnlHeader.SuspendLayout();
            this.pnlSummary.SuspendLayout();
            this.pnlHeaderActions.SuspendLayout();
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
            this.splitContainer.Location = new System.Drawing.Point(10, 120);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.pnlOrders);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.pnlOrderDetails);
            this.splitContainer.Size = new System.Drawing.Size(1380, 770);
            this.splitContainer.SplitterDistance = 690;
            this.splitContainer.TabIndex = 1;
            // 
            // pnlOrders
            // 
            this.pnlOrders.BackColor = System.Drawing.Color.White;
            this.pnlOrders.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlOrders.Controls.Add(this.dgvOrders);
            this.pnlOrders.Controls.Add(this.pnlOrderActions);
            this.pnlOrders.Controls.Add(this.pnlSearch);
            this.pnlOrders.Controls.Add(this.lblOrdersTitle);
            this.pnlOrders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlOrders.Location = new System.Drawing.Point(0, 0);
            this.pnlOrders.Name = "pnlOrders";
            this.pnlOrders.Padding = new System.Windows.Forms.Padding(15);
            this.pnlOrders.Size = new System.Drawing.Size(690, 770);
            this.pnlOrders.TabIndex = 0;
            // 
            // dgvOrders
            // 
            this.dgvOrders.AllowUserToAddRows = false;
            this.dgvOrders.AllowUserToDeleteRows = false;
            this.dgvOrders.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvOrders.BackgroundColor = System.Drawing.Color.White;
            this.dgvOrders.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvOrders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvOrders.Location = new System.Drawing.Point(15, 95);
            this.dgvOrders.MultiSelect = false;
            this.dgvOrders.Name = "dgvOrders";
            this.dgvOrders.ReadOnly = true;
            this.dgvOrders.RowHeadersVisible = false;
            this.dgvOrders.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvOrders.Size = new System.Drawing.Size(658, 580);
            this.dgvOrders.TabIndex = 3;
            this.dgvOrders.SelectionChanged += new System.EventHandler(this.dgvOrders_SelectionChanged);
            // 
            // pnlOrderActions
            // 
            this.pnlOrderActions.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.pnlOrderActions.Controls.Add(this.btnViewDetails);
            this.pnlOrderActions.Controls.Add(this.btnCancelOrder);
            this.pnlOrderActions.Controls.Add(this.btnDeleteOrder);
            this.pnlOrderActions.Controls.Add(this.btnEditOrder);
            this.pnlOrderActions.Controls.Add(this.btnCreateOrder);
            this.pnlOrderActions.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlOrderActions.Location = new System.Drawing.Point(15, 675);
            this.pnlOrderActions.Name = "pnlOrderActions";
            this.pnlOrderActions.Padding = new System.Windows.Forms.Padding(10);
            this.pnlOrderActions.Size = new System.Drawing.Size(658, 78);
            this.pnlOrderActions.TabIndex = 2;
            // 
            // btnViewDetails
            // 
            this.btnViewDetails.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(162)))), ((int)(((byte)(184)))));
            this.btnViewDetails.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnViewDetails.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnViewDetails.ForeColor = System.Drawing.Color.White;
            this.btnViewDetails.Location = new System.Drawing.Point(540, 10);
            this.btnViewDetails.Name = "btnViewDetails";
            this.btnViewDetails.Size = new System.Drawing.Size(110, 30);
            this.btnViewDetails.TabIndex = 4;
            this.btnViewDetails.Text = "👁️ View Details";
            this.btnViewDetails.UseVisualStyleBackColor = false;
            this.btnViewDetails.Click += new System.EventHandler(this.btnViewDetails_Click);
            // 
            // btnCancelOrder
            // 
            this.btnCancelOrder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(193)))), ((int)(((byte)(7)))));
            this.btnCancelOrder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelOrder.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnCancelOrder.ForeColor = System.Drawing.Color.White;
            this.btnCancelOrder.Location = new System.Drawing.Point(420, 10);
            this.btnCancelOrder.Name = "btnCancelOrder";
            this.btnCancelOrder.Size = new System.Drawing.Size(110, 30);
            this.btnCancelOrder.TabIndex = 3;
            this.btnCancelOrder.Text = "🚫 Cancel Order";
            this.btnCancelOrder.UseVisualStyleBackColor = false;
            this.btnCancelOrder.Click += new System.EventHandler(this.btnCancelOrder_Click);
            // 
            // btnDeleteOrder
            // 
            this.btnDeleteOrder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.btnDeleteOrder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteOrder.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnDeleteOrder.ForeColor = System.Drawing.Color.White;
            this.btnDeleteOrder.Location = new System.Drawing.Point(300, 10);
            this.btnDeleteOrder.Name = "btnDeleteOrder";
            this.btnDeleteOrder.Size = new System.Drawing.Size(110, 30);
            this.btnDeleteOrder.TabIndex = 2;
            this.btnDeleteOrder.Text = "🗑️ Delete";
            this.btnDeleteOrder.UseVisualStyleBackColor = false;
            this.btnDeleteOrder.Click += new System.EventHandler(this.btnDeleteOrder_Click);
            // 
            // btnEditOrder
            // 
            this.btnEditOrder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(193)))), ((int)(((byte)(7)))));
            this.btnEditOrder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditOrder.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnEditOrder.ForeColor = System.Drawing.Color.White;
            this.btnEditOrder.Location = new System.Drawing.Point(180, 10);
            this.btnEditOrder.Name = "btnEditOrder";
            this.btnEditOrder.Size = new System.Drawing.Size(110, 30);
            this.btnEditOrder.TabIndex = 1;
            this.btnEditOrder.Text = "✏️ Edit Order";
            this.btnEditOrder.UseVisualStyleBackColor = false;
            this.btnEditOrder.Click += new System.EventHandler(this.btnEditOrder_Click);
            // 
            // btnCreateOrder
            // 
            this.btnCreateOrder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnCreateOrder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCreateOrder.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnCreateOrder.ForeColor = System.Drawing.Color.White;
            this.btnCreateOrder.Location = new System.Drawing.Point(10, 10);
            this.btnCreateOrder.Name = "btnCreateOrder";
            this.btnCreateOrder.Size = new System.Drawing.Size(160, 30);
            this.btnCreateOrder.TabIndex = 0;
            this.btnCreateOrder.Text = "➕ Create New Order";
            this.btnCreateOrder.UseVisualStyleBackColor = false;
            this.btnCreateOrder.Click += new System.EventHandler(this.btnCreateOrder_Click);
            // 
            // pnlSearch
            // 
            this.pnlSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.pnlSearch.Controls.Add(this.txtSearch);
            this.pnlSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSearch.Location = new System.Drawing.Point(15, 50);
            this.pnlSearch.Name = "pnlSearch";
            this.pnlSearch.Padding = new System.Windows.Forms.Padding(10);
            this.pnlSearch.Size = new System.Drawing.Size(658, 45);
            this.pnlSearch.TabIndex = 1;
            // 
            // txtSearch
            // 
            this.txtSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtSearch.ForeColor = System.Drawing.Color.Gray;
            this.txtSearch.Location = new System.Drawing.Point(10, 10);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(638, 25);
            this.txtSearch.TabIndex = 0;
            this.txtSearch.Text = "🔍 Search orders...";
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            this.txtSearch.Enter += new System.EventHandler(this.txtSearch_Enter);
            this.txtSearch.Leave += new System.EventHandler(this.txtSearch_Leave);
            // 
            // lblOrdersTitle
            // 
            this.lblOrdersTitle.AutoSize = true;
            this.lblOrdersTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblOrdersTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblOrdersTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.lblOrdersTitle.Location = new System.Drawing.Point(15, 15);
            this.lblOrdersTitle.Name = "lblOrdersTitle";
            this.lblOrdersTitle.Padding = new System.Windows.Forms.Padding(0, 5, 0, 10);
            this.lblOrdersTitle.Size = new System.Drawing.Size(84, 35);
            this.lblOrdersTitle.TabIndex = 0;
            this.lblOrdersTitle.Text = "📋 Orders";
            // 
            // pnlOrderDetails
            // 
            this.pnlOrderDetails.BackColor = System.Drawing.Color.White;
            this.pnlOrderDetails.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlOrderDetails.Controls.Add(this.dgvOrderDetails);
            this.pnlOrderDetails.Controls.Add(this.pnlOrderInfo);
            this.pnlOrderDetails.Controls.Add(this.lblOrderDetailsTitle);
            this.pnlOrderDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlOrderDetails.Location = new System.Drawing.Point(0, 0);
            this.pnlOrderDetails.Name = "pnlOrderDetails";
            this.pnlOrderDetails.Padding = new System.Windows.Forms.Padding(15);
            this.pnlOrderDetails.Size = new System.Drawing.Size(686, 770);
            this.pnlOrderDetails.TabIndex = 0;
            // 
            // dgvOrderDetails
            // 
            this.dgvOrderDetails.AllowUserToAddRows = false;
            this.dgvOrderDetails.AllowUserToDeleteRows = false;
            this.dgvOrderDetails.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvOrderDetails.BackgroundColor = System.Drawing.Color.White;
            this.dgvOrderDetails.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvOrderDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvOrderDetails.Location = new System.Drawing.Point(15, 185);
            this.dgvOrderDetails.MultiSelect = false;
            this.dgvOrderDetails.Name = "dgvOrderDetails";
            this.dgvOrderDetails.ReadOnly = true;
            this.dgvOrderDetails.RowHeadersVisible = false;
            this.dgvOrderDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvOrderDetails.Size = new System.Drawing.Size(654, 568);
            this.dgvOrderDetails.TabIndex = 2;
            // 
            // pnlOrderInfo
            // 
            this.pnlOrderInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.pnlOrderInfo.Controls.Add(this.lblOrderTotal);
            this.pnlOrderInfo.Controls.Add(this.lblOrderStatus);
            this.pnlOrderInfo.Controls.Add(this.lblOrderSupplier);
            this.pnlOrderInfo.Controls.Add(this.lblOrderDate);
            this.pnlOrderInfo.Controls.Add(this.lblOrderId);
            this.pnlOrderInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlOrderInfo.Location = new System.Drawing.Point(15, 50);
            this.pnlOrderInfo.Name = "pnlOrderInfo";
            this.pnlOrderInfo.Padding = new System.Windows.Forms.Padding(15);
            this.pnlOrderInfo.Size = new System.Drawing.Size(654, 135);
            this.pnlOrderInfo.TabIndex = 1;
            // 
            // lblOrderTotal
            // 
            this.lblOrderTotal.AutoSize = true;
            this.lblOrderTotal.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblOrderTotal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.lblOrderTotal.Location = new System.Drawing.Point(15, 105);
            this.lblOrderTotal.Name = "lblOrderTotal";
            this.lblOrderTotal.Size = new System.Drawing.Size(75, 19);
            this.lblOrderTotal.TabIndex = 4;
            this.lblOrderTotal.Text = "Total: --";
            // 
            // lblOrderStatus
            // 
            this.lblOrderStatus.AutoSize = true;
            this.lblOrderStatus.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblOrderStatus.Location = new System.Drawing.Point(15, 80);
            this.lblOrderStatus.Name = "lblOrderStatus";
            this.lblOrderStatus.Padding = new System.Windows.Forms.Padding(5, 2, 5, 2);
            this.lblOrderStatus.Size = new System.Drawing.Size(77, 23);
            this.lblOrderStatus.TabIndex = 3;
            this.lblOrderStatus.Text = "Status: --";
            // 
            // lblOrderSupplier
            // 
            this.lblOrderSupplier.AutoSize = true;
            this.lblOrderSupplier.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblOrderSupplier.Location = new System.Drawing.Point(15, 55);
            this.lblOrderSupplier.Name = "lblOrderSupplier";
            this.lblOrderSupplier.Size = new System.Drawing.Size(81, 19);
            this.lblOrderSupplier.TabIndex = 2;
            this.lblOrderSupplier.Text = "Supplier: --";
            // 
            // lblOrderDate
            // 
            this.lblOrderDate.AutoSize = true;
            this.lblOrderDate.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblOrderDate.Location = new System.Drawing.Point(15, 30);
            this.lblOrderDate.Name = "lblOrderDate";
            this.lblOrderDate.Size = new System.Drawing.Size(57, 19);
            this.lblOrderDate.TabIndex = 1;
            this.lblOrderDate.Text = "Date: --";
            // 
            // lblOrderId
            // 
            this.lblOrderId.AutoSize = true;
            this.lblOrderId.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblOrderId.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.lblOrderId.Location = new System.Drawing.Point(15, 5);
            this.lblOrderId.Name = "lblOrderId";
            this.lblOrderId.Size = new System.Drawing.Size(85, 19);
            this.lblOrderId.TabIndex = 0;
            this.lblOrderId.Text = "Order ID: --";
            // 
            // lblOrderDetailsTitle
            // 
            this.lblOrderDetailsTitle.AutoSize = true;
            this.lblOrderDetailsTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblOrderDetailsTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblOrderDetailsTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.lblOrderDetailsTitle.Location = new System.Drawing.Point(15, 15);
            this.lblOrderDetailsTitle.Name = "lblOrderDetailsTitle";
            this.lblOrderDetailsTitle.Padding = new System.Windows.Forms.Padding(0, 5, 0, 10);
            this.lblOrderDetailsTitle.Size = new System.Drawing.Size(126, 35);
            this.lblOrderDetailsTitle.TabIndex = 0;
            this.lblOrderDetailsTitle.Text = "📄 Order Details";
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.pnlHeader.Controls.Add(this.pnlSummary);
            this.pnlHeader.Controls.Add(this.pnlHeaderActions);
            this.pnlHeader.Controls.Add(this.lblStatus);
            this.pnlHeader.Controls.Add(this.lblFormTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(10, 10);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Padding = new System.Windows.Forms.Padding(15, 10, 15, 10);
            this.pnlHeader.Size = new System.Drawing.Size(1380, 110);
            this.pnlHeader.TabIndex = 0;
            // 
            // pnlSummary
            // 
            this.pnlSummary.Controls.Add(this.lblCompletedOrders);
            this.pnlSummary.Controls.Add(this.lblPendingOrders);
            this.pnlSummary.Controls.Add(this.lblTotalValue);
            this.pnlSummary.Controls.Add(this.lblTotalOrders);
            this.pnlSummary.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlSummary.Location = new System.Drawing.Point(15, 60);
            this.pnlSummary.Name = "pnlSummary";
            this.pnlSummary.Size = new System.Drawing.Size(1150, 40);
            this.pnlSummary.TabIndex = 3;
            // 
            // lblCompletedOrders
            // 
            this.lblCompletedOrders.AutoSize = true;
            this.lblCompletedOrders.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblCompletedOrders.ForeColor = System.Drawing.Color.White;
            this.lblCompletedOrders.Location = new System.Drawing.Point(450, 10);
            this.lblCompletedOrders.Name = "lblCompletedOrders";
            this.lblCompletedOrders.Size = new System.Drawing.Size(99, 19);
            this.lblCompletedOrders.TabIndex = 3;
            this.lblCompletedOrders.Text = "Completed: 0";
            // 
            // lblPendingOrders
            // 
            this.lblPendingOrders.AutoSize = true;
            this.lblPendingOrders.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblPendingOrders.ForeColor = System.Drawing.Color.White;
            this.lblPendingOrders.Location = new System.Drawing.Point(350, 10);
            this.lblPendingOrders.Name = "lblPendingOrders";
            this.lblPendingOrders.Size = new System.Drawing.Size(81, 19);
            this.lblPendingOrders.TabIndex = 2;
            this.lblPendingOrders.Text = "Pending: 0";
            // 
            // lblTotalValue
            // 
            this.lblTotalValue.AutoSize = true;
            this.lblTotalValue.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTotalValue.ForeColor = System.Drawing.Color.White;
            this.lblTotalValue.Location = new System.Drawing.Point(200, 10);
            this.lblTotalValue.Name = "lblTotalValue";
            this.lblTotalValue.Size = new System.Drawing.Size(116, 19);
            this.lblTotalValue.TabIndex = 1;
            this.lblTotalValue.Text = "Total Value: $0";
            // 
            // lblTotalOrders
            // 
            this.lblTotalOrders.AutoSize = true;
            this.lblTotalOrders.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTotalOrders.ForeColor = System.Drawing.Color.White;
            this.lblTotalOrders.Location = new System.Drawing.Point(0, 10);
            this.lblTotalOrders.Name = "lblTotalOrders";
            this.lblTotalOrders.Size = new System.Drawing.Size(113, 19);
            this.lblTotalOrders.TabIndex = 0;
            this.lblTotalOrders.Text = "Total Orders: 0";
            // 
            // pnlHeaderActions
            // 
            this.pnlHeaderActions.Controls.Add(this.btnClose);
            this.pnlHeaderActions.Controls.Add(this.btnRefresh);
            this.pnlHeaderActions.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlHeaderActions.Location = new System.Drawing.Point(1165, 10);
            this.pnlHeaderActions.Name = "pnlHeaderActions";
            this.pnlHeaderActions.Size = new System.Drawing.Size(200, 50);
            this.pnlHeaderActions.TabIndex = 2;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(105, 10);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(90, 30);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "❌ Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnRefresh.ForeColor = System.Drawing.Color.White;
            this.btnRefresh.Location = new System.Drawing.Point(10, 10);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(90, 30);
            this.btnRefresh.TabIndex = 0;
            this.btnRefresh.Text = "🔄 Refresh";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblStatus.ForeColor = System.Drawing.Color.White;
            this.lblStatus.Location = new System.Drawing.Point(15, 35);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(45, 19);
            this.lblStatus.TabIndex = 1;
            this.lblStatus.Text = "Ready";
            // 
            // lblFormTitle
            // 
            this.lblFormTitle.AutoSize = true;
            this.lblFormTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblFormTitle.ForeColor = System.Drawing.Color.White;
            this.lblFormTitle.Location = new System.Drawing.Point(15, 10);
            this.lblFormTitle.Name = "lblFormTitle";
            this.lblFormTitle.Size = new System.Drawing.Size(231, 32);
            this.lblFormTitle.TabIndex = 0;
            this.lblFormTitle.Text = "🛒 Order Management";
            // 
            // OrderManagementForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1400, 900);
            this.Controls.Add(this.pnlMain);
            this.MinimumSize = new System.Drawing.Size(1200, 800);
            this.Name = "OrderManagementForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Order Management - Inventory Management System";
            this.pnlMain.ResumeLayout(false);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.pnlOrders.ResumeLayout(false);
            this.pnlOrders.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrders)).EndInit();
            this.pnlOrderActions.ResumeLayout(false);
            this.pnlSearch.ResumeLayout(false);
            this.pnlSearch.PerformLayout();
            this.pnlOrderDetails.ResumeLayout(false);
            this.pnlOrderDetails.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrderDetails)).EndInit();
            this.pnlOrderInfo.ResumeLayout(false);
            this.pnlOrderInfo.PerformLayout();
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlSummary.ResumeLayout(false);
            this.pnlSummary.PerformLayout();
            this.pnlHeaderActions.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblFormTitle;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Panel pnlHeaderActions;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Panel pnlSummary;
        private System.Windows.Forms.Label lblTotalOrders;
        private System.Windows.Forms.Label lblTotalValue;
        private System.Windows.Forms.Label lblPendingOrders;
        private System.Windows.Forms.Label lblCompletedOrders;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.Panel pnlOrders;
        private System.Windows.Forms.Label lblOrdersTitle;
        private System.Windows.Forms.Panel pnlSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Panel pnlOrderActions;
        private System.Windows.Forms.Button btnCreateOrder;
        private System.Windows.Forms.Button btnEditOrder;
        private System.Windows.Forms.Button btnDeleteOrder;
        private System.Windows.Forms.Button btnCancelOrder;
        private System.Windows.Forms.Button btnViewDetails;
        private System.Windows.Forms.DataGridView dgvOrders;
        private System.Windows.Forms.Panel pnlOrderDetails;
        private System.Windows.Forms.Label lblOrderDetailsTitle;
        private System.Windows.Forms.Panel pnlOrderInfo;
        private System.Windows.Forms.Label lblOrderId;
        private System.Windows.Forms.Label lblOrderDate;
        private System.Windows.Forms.Label lblOrderSupplier;
        private System.Windows.Forms.Label lblOrderStatus;
        private System.Windows.Forms.Label lblOrderTotal;
        private System.Windows.Forms.DataGridView dgvOrderDetails;
    }
} 