namespace BMYLBH2025_SDDAP
{
    partial class MainDashboard
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
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabDashboard = new System.Windows.Forms.TabPage();
            this.pnlDashboard = new System.Windows.Forms.Panel();
            this.btnBulkUpdateStock = new System.Windows.Forms.Button();
            this.btnManageCategoriesDash = new System.Windows.Forms.Button();
            this.btnCreateOrderDash = new System.Windows.Forms.Button();
            this.btnAddProduct = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.pnlSummaryCards = new System.Windows.Forms.Panel();
            this.pnlOrdersCard = new System.Windows.Forms.Panel();
            this.lblOrdersCount = new System.Windows.Forms.Label();
            this.lblOrdersTitle = new System.Windows.Forms.Label();
            this.pnlProductsCard = new System.Windows.Forms.Panel();
            this.lblProductsCount = new System.Windows.Forms.Label();
            this.lblProductsTitle = new System.Windows.Forms.Label();
            this.pnlLowStockCard = new System.Windows.Forms.Panel();
            this.lblLowStockCount = new System.Windows.Forms.Label();
            this.lblLowStockTitle = new System.Windows.Forms.Label();
            this.pnlTotalValueCard = new System.Windows.Forms.Panel();
            this.lblTotalValue = new System.Windows.Forms.Label();
            this.lblTotalValueTitle = new System.Windows.Forms.Label();
            this.lblDashboardTitle = new System.Windows.Forms.Label();
            this.tabInventory = new System.Windows.Forms.TabPage();
            this.pnlInventory = new System.Windows.Forms.Panel();
            this.dgvInventory = new System.Windows.Forms.DataGridView();
            this.pnlInventorySearch = new System.Windows.Forms.Panel();
            this.lblSearchInventory = new System.Windows.Forms.Label();
            this.txtSearchInventory = new System.Windows.Forms.TextBox();
            this.btnUpdateStockInv = new System.Windows.Forms.Button();
            this.btnBulkUpdateStockInv = new System.Windows.Forms.Button();
            this.tabProducts = new System.Windows.Forms.TabPage();
            this.pnlProducts = new System.Windows.Forms.Panel();
            this.dgvProducts = new System.Windows.Forms.DataGridView();
            this.btnManageProducts = new System.Windows.Forms.Button();
            this.lblProductsTabTitle = new System.Windows.Forms.Label();
            this.tabCategories = new System.Windows.Forms.TabPage();
            this.pnlCategories = new System.Windows.Forms.Panel();
            this.dgvCategories = new System.Windows.Forms.DataGridView();
            this.pnlCategoriesSearch = new System.Windows.Forms.Panel();
            this.lblSearchCategories = new System.Windows.Forms.Label();
            this.txtSearchCategories = new System.Windows.Forms.TextBox();
            this.btnManageCategories = new System.Windows.Forms.Button();
            this.lblCategoriesTitle = new System.Windows.Forms.Label();
            this.tabOrders = new System.Windows.Forms.TabPage();
            this.pnlOrders = new System.Windows.Forms.Panel();
            this.dgvOrders = new System.Windows.Forms.DataGridView();
            this.pnlOrdersSearch = new System.Windows.Forms.Panel();
            this.lblSearchOrders = new System.Windows.Forms.Label();
            this.txtSearchOrders = new System.Windows.Forms.TextBox();
            this.btnManageOrders = new System.Windows.Forms.Button();
            this.btnCreateOrder = new System.Windows.Forms.Button();
            this.lblOrdersTabTitle = new System.Windows.Forms.Label();
            this.tabControl.SuspendLayout();
            this.tabDashboard.SuspendLayout();
            this.pnlDashboard.SuspendLayout();
            this.pnlSummaryCards.SuspendLayout();
            this.pnlOrdersCard.SuspendLayout();
            this.pnlProductsCard.SuspendLayout();
            this.pnlLowStockCard.SuspendLayout();
            this.pnlTotalValueCard.SuspendLayout();
            this.tabInventory.SuspendLayout();
            this.pnlInventory.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInventory)).BeginInit();
            this.pnlInventorySearch.SuspendLayout();
            this.tabProducts.SuspendLayout();
            this.pnlProducts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProducts)).BeginInit();
            this.tabCategories.SuspendLayout();
            this.pnlCategories.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCategories)).BeginInit();
            this.pnlCategoriesSearch.SuspendLayout();
            this.tabOrders.SuspendLayout();
            this.pnlOrders.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrders)).BeginInit();
            this.pnlOrdersSearch.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabDashboard);
            this.tabControl.Controls.Add(this.tabInventory);
            this.tabControl.Controls.Add(this.tabProducts);
            this.tabControl.Controls.Add(this.tabCategories);
            this.tabControl.Controls.Add(this.tabOrders);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1200, 800);
            this.tabControl.TabIndex = 0;
            // 
            // tabDashboard
            // 
            this.tabDashboard.BackColor = System.Drawing.Color.White;
            this.tabDashboard.Controls.Add(this.pnlDashboard);
            this.tabDashboard.Location = new System.Drawing.Point(4, 26);
            this.tabDashboard.Name = "tabDashboard";
            this.tabDashboard.Size = new System.Drawing.Size(1192, 770);
            this.tabDashboard.TabIndex = 0;
            this.tabDashboard.Text = "📊 Dashboard";
            // 
            // pnlDashboard
            // 
            this.pnlDashboard.BackColor = System.Drawing.Color.White;
            this.pnlDashboard.Controls.Add(this.btnBulkUpdateStock);
            this.pnlDashboard.Controls.Add(this.btnManageCategoriesDash);
            this.pnlDashboard.Controls.Add(this.btnCreateOrderDash);
            this.pnlDashboard.Controls.Add(this.btnAddProduct);
            this.pnlDashboard.Controls.Add(this.btnRefresh);
            this.pnlDashboard.Controls.Add(this.pnlSummaryCards);
            this.pnlDashboard.Controls.Add(this.lblDashboardTitle);
            this.pnlDashboard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDashboard.Location = new System.Drawing.Point(0, 0);
            this.pnlDashboard.Name = "pnlDashboard";
            this.pnlDashboard.Padding = new System.Windows.Forms.Padding(20);
            this.pnlDashboard.Size = new System.Drawing.Size(1192, 770);
            this.pnlDashboard.TabIndex = 0;
            // 
            // btnBulkUpdateStock
            // 
            this.btnBulkUpdateStock.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(126)))), ((int)(((byte)(34)))));
            this.btnBulkUpdateStock.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBulkUpdateStock.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnBulkUpdateStock.ForeColor = System.Drawing.Color.White;
            this.btnBulkUpdateStock.Location = new System.Drawing.Point(730, 210);
            this.btnBulkUpdateStock.Name = "btnBulkUpdateStock";
            this.btnBulkUpdateStock.Size = new System.Drawing.Size(150, 40);
            this.btnBulkUpdateStock.TabIndex = 6;
            this.btnBulkUpdateStock.Text = "📦 Bulk Update";
            this.btnBulkUpdateStock.UseVisualStyleBackColor = false;
            this.btnBulkUpdateStock.Click += new System.EventHandler(this.btnBulkUpdateStock_Click);
            // 
            // btnManageCategoriesDash
            // 
            this.btnManageCategoriesDash.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.btnManageCategoriesDash.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnManageCategoriesDash.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnManageCategoriesDash.ForeColor = System.Drawing.Color.White;
            this.btnManageCategoriesDash.Location = new System.Drawing.Point(570, 210);
            this.btnManageCategoriesDash.Name = "btnManageCategoriesDash";
            this.btnManageCategoriesDash.Size = new System.Drawing.Size(150, 40);
            this.btnManageCategoriesDash.TabIndex = 5;
            this.btnManageCategoriesDash.Text = "🗂️ Categories";
            this.btnManageCategoriesDash.UseVisualStyleBackColor = false;
            this.btnManageCategoriesDash.Click += new System.EventHandler(this.btnManageCategories_Click);
            // 
            // btnCreateOrderDash
            // 
            this.btnCreateOrderDash.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(89)))), ((int)(((byte)(182)))));
            this.btnCreateOrderDash.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCreateOrderDash.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnCreateOrderDash.ForeColor = System.Drawing.Color.White;
            this.btnCreateOrderDash.Location = new System.Drawing.Point(410, 210);
            this.btnCreateOrderDash.Name = "btnCreateOrderDash";
            this.btnCreateOrderDash.Size = new System.Drawing.Size(150, 40);
            this.btnCreateOrderDash.TabIndex = 4;
            this.btnCreateOrderDash.Text = "🛒 Create Order";
            this.btnCreateOrderDash.UseVisualStyleBackColor = false;
            this.btnCreateOrderDash.Click += new System.EventHandler(this.btnCreateOrder_Click);
            // 

            // 
            // btnAddProduct
            // 
            this.btnAddProduct.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnAddProduct.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddProduct.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnAddProduct.ForeColor = System.Drawing.Color.White;
            this.btnAddProduct.Location = new System.Drawing.Point(250, 210);
            this.btnAddProduct.Name = "btnAddProduct";
            this.btnAddProduct.Size = new System.Drawing.Size(150, 40);
            this.btnAddProduct.TabIndex = 3;
            this.btnAddProduct.Text = "➕ Add Product";
            this.btnAddProduct.UseVisualStyleBackColor = false;
            this.btnAddProduct.Click += new System.EventHandler(this.btnAddProduct_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnRefresh.ForeColor = System.Drawing.Color.White;
            this.btnRefresh.Location = new System.Drawing.Point(90, 210);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(150, 40);
            this.btnRefresh.TabIndex = 2;
            this.btnRefresh.Text = "🔄 Refresh Data";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // pnlSummaryCards
            // 
            this.pnlSummaryCards.Controls.Add(this.pnlOrdersCard);
            this.pnlSummaryCards.Controls.Add(this.pnlProductsCard);
            this.pnlSummaryCards.Controls.Add(this.pnlLowStockCard);
            this.pnlSummaryCards.Controls.Add(this.pnlTotalValueCard);
            this.pnlSummaryCards.Location = new System.Drawing.Point(20, 70);
            this.pnlSummaryCards.Name = "pnlSummaryCards";
            this.pnlSummaryCards.Size = new System.Drawing.Size(880, 120);
            this.pnlSummaryCards.TabIndex = 1;
            // 
            // pnlProductsCard
            // 
            this.pnlProductsCard.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.pnlProductsCard.Controls.Add(this.lblProductsCount);
            this.pnlProductsCard.Controls.Add(this.lblProductsTitle);
            this.pnlProductsCard.Location = new System.Drawing.Point(440, 0);
            this.pnlProductsCard.Name = "pnlProductsCard";
            this.pnlProductsCard.Size = new System.Drawing.Size(200, 100);
            this.pnlProductsCard.TabIndex = 2;
            // 
            // lblProductsCount
            // 
            this.lblProductsCount.AutoSize = true;
            this.lblProductsCount.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblProductsCount.ForeColor = System.Drawing.Color.White;
            this.lblProductsCount.Location = new System.Drawing.Point(15, 45);
            this.lblProductsCount.Name = "lblProductsCount";
            this.lblProductsCount.Size = new System.Drawing.Size(26, 30);
            this.lblProductsCount.TabIndex = 1;
            this.lblProductsCount.Text = "0";
            // 
            // lblProductsTitle
            // 
            this.lblProductsTitle.AutoSize = true;
            this.lblProductsTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblProductsTitle.ForeColor = System.Drawing.Color.White;
            this.lblProductsTitle.Location = new System.Drawing.Point(15, 15);
            this.lblProductsTitle.Name = "lblProductsTitle";
            this.lblProductsTitle.Size = new System.Drawing.Size(129, 19);
            this.lblProductsTitle.TabIndex = 0;
            this.lblProductsTitle.Text = "📦 Total Products";
            // 
            // pnlLowStockCard
            // 
            this.pnlLowStockCard.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.pnlLowStockCard.Controls.Add(this.lblLowStockCount);
            this.pnlLowStockCard.Controls.Add(this.lblLowStockTitle);
            this.pnlLowStockCard.Location = new System.Drawing.Point(220, 0);
            this.pnlLowStockCard.Name = "pnlLowStockCard";
            this.pnlLowStockCard.Size = new System.Drawing.Size(200, 100);
            this.pnlLowStockCard.TabIndex = 1;
            // 
            // lblLowStockCount
            // 
            this.lblLowStockCount.AutoSize = true;
            this.lblLowStockCount.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblLowStockCount.ForeColor = System.Drawing.Color.White;
            this.lblLowStockCount.Location = new System.Drawing.Point(15, 45);
            this.lblLowStockCount.Name = "lblLowStockCount";
            this.lblLowStockCount.Size = new System.Drawing.Size(26, 30);
            this.lblLowStockCount.TabIndex = 1;
            this.lblLowStockCount.Text = "0";
            // 
            // lblLowStockTitle
            // 
            this.lblLowStockTitle.AutoSize = true;
            this.lblLowStockTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblLowStockTitle.ForeColor = System.Drawing.Color.White;
            this.lblLowStockTitle.Location = new System.Drawing.Point(15, 15);
            this.lblLowStockTitle.Name = "lblLowStockTitle";
            this.lblLowStockTitle.Size = new System.Drawing.Size(141, 19);
            this.lblLowStockTitle.TabIndex = 0;
            this.lblLowStockTitle.Text = "⚠️ Low Stock Items";
            // 
            // pnlTotalValueCard
            // 
            this.pnlTotalValueCard.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.pnlTotalValueCard.Controls.Add(this.lblTotalValue);
            this.pnlTotalValueCard.Controls.Add(this.lblTotalValueTitle);
            this.pnlTotalValueCard.Location = new System.Drawing.Point(0, 0);
            this.pnlTotalValueCard.Name = "pnlTotalValueCard";
            this.pnlTotalValueCard.Size = new System.Drawing.Size(200, 100);
            this.pnlTotalValueCard.TabIndex = 0;
            // 
            // lblTotalValue
            // 
            this.lblTotalValue.AutoSize = true;
            this.lblTotalValue.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTotalValue.ForeColor = System.Drawing.Color.White;
            this.lblTotalValue.Location = new System.Drawing.Point(15, 45);
            this.lblTotalValue.Name = "lblTotalValue";
            this.lblTotalValue.Size = new System.Drawing.Size(71, 30);
            this.lblTotalValue.TabIndex = 1;
            this.lblTotalValue.Text = "$0.00";
            // 
            // lblTotalValueTitle
            // 
            this.lblTotalValueTitle.AutoSize = true;
            this.lblTotalValueTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTotalValueTitle.ForeColor = System.Drawing.Color.White;
            this.lblTotalValueTitle.Location = new System.Drawing.Point(15, 15);
            this.lblTotalValueTitle.Name = "lblTotalValueTitle";
            this.lblTotalValueTitle.Size = new System.Drawing.Size(172, 19);
            this.lblTotalValueTitle.TabIndex = 0;
            this.lblTotalValueTitle.Text = "💰 Total Inventory Value";
            // 
            // pnlOrdersCard
            // 
            this.pnlOrdersCard.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(89)))), ((int)(((byte)(182)))));
            this.pnlOrdersCard.Controls.Add(this.lblOrdersCount);
            this.pnlOrdersCard.Controls.Add(this.lblOrdersTitle);
            this.pnlOrdersCard.Location = new System.Drawing.Point(660, 0);
            this.pnlOrdersCard.Name = "pnlOrdersCard";
            this.pnlOrdersCard.Size = new System.Drawing.Size(200, 100);
            this.pnlOrdersCard.TabIndex = 3;
            // 
            // lblOrdersCount
            // 
            this.lblOrdersCount.AutoSize = true;
            this.lblOrdersCount.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblOrdersCount.ForeColor = System.Drawing.Color.White;
            this.lblOrdersCount.Location = new System.Drawing.Point(15, 45);
            this.lblOrdersCount.Name = "lblOrdersCount";
            this.lblOrdersCount.Size = new System.Drawing.Size(26, 30);
            this.lblOrdersCount.TabIndex = 1;
            this.lblOrdersCount.Text = "0";
            // 
            // lblOrdersTitle
            // 
            this.lblOrdersTitle.AutoSize = true;
            this.lblOrdersTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblOrdersTitle.ForeColor = System.Drawing.Color.White;
            this.lblOrdersTitle.Location = new System.Drawing.Point(15, 15);
            this.lblOrdersTitle.Name = "lblOrdersTitle";
            this.lblOrdersTitle.Size = new System.Drawing.Size(150, 19);
            this.lblOrdersTitle.TabIndex = 0;
            this.lblOrdersTitle.Text = "🛒 Total Orders";
            // 
            // lblDashboardTitle
            // 
            this.lblDashboardTitle.AutoSize = true;
            this.lblDashboardTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblDashboardTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.lblDashboardTitle.Location = new System.Drawing.Point(20, 20);
            this.lblDashboardTitle.Name = "lblDashboardTitle";
            this.lblDashboardTitle.Size = new System.Drawing.Size(415, 32);
            this.lblDashboardTitle.TabIndex = 0;
            this.lblDashboardTitle.Text = "Inventory Management Dashboard";
            // 
            // tabInventory
            // 
            this.tabInventory.BackColor = System.Drawing.Color.White;
            this.tabInventory.Controls.Add(this.pnlInventory);
            this.tabInventory.Location = new System.Drawing.Point(4, 26);
            this.tabInventory.Name = "tabInventory";
            this.tabInventory.Size = new System.Drawing.Size(1192, 770);
            this.tabInventory.TabIndex = 1;
            this.tabInventory.Text = "📋 Inventory";
            // 
            // pnlInventory
            // 
            this.pnlInventory.BackColor = System.Drawing.Color.White;
            this.pnlInventory.Controls.Add(this.dgvInventory);
            this.pnlInventory.Controls.Add(this.pnlInventorySearch);
            this.pnlInventory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlInventory.Location = new System.Drawing.Point(0, 0);
            this.pnlInventory.Name = "pnlInventory";
            this.pnlInventory.Padding = new System.Windows.Forms.Padding(10);
            this.pnlInventory.Size = new System.Drawing.Size(1192, 770);
            this.pnlInventory.TabIndex = 0;
            // 
            // dgvInventory
            // 
            this.dgvInventory.AllowUserToAddRows = false;
            this.dgvInventory.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvInventory.BackgroundColor = System.Drawing.Color.White;
            this.dgvInventory.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvInventory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvInventory.Location = new System.Drawing.Point(10, 70);
            this.dgvInventory.MultiSelect = false;
            this.dgvInventory.Name = "dgvInventory";
            this.dgvInventory.ReadOnly = true;
            this.dgvInventory.RowHeadersVisible = false;
            this.dgvInventory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvInventory.Size = new System.Drawing.Size(1172, 690);
            this.dgvInventory.TabIndex = 1;
            // 
            // pnlInventorySearch
            // 
            this.pnlInventorySearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.pnlInventorySearch.Controls.Add(this.btnBulkUpdateStockInv);
            this.pnlInventorySearch.Controls.Add(this.btnUpdateStockInv);
            this.pnlInventorySearch.Controls.Add(this.lblSearchInventory);
            this.pnlInventorySearch.Controls.Add(this.txtSearchInventory);
            this.pnlInventorySearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlInventorySearch.Location = new System.Drawing.Point(10, 10);
            this.pnlInventorySearch.Name = "pnlInventorySearch";
            this.pnlInventorySearch.Size = new System.Drawing.Size(1172, 60);
            this.pnlInventorySearch.TabIndex = 0;
            // 
            // lblSearchInventory
            // 
            this.lblSearchInventory.AutoSize = true;
            this.lblSearchInventory.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSearchInventory.Location = new System.Drawing.Point(280, 21);
            this.lblSearchInventory.Name = "lblSearchInventory";
            this.lblSearchInventory.Size = new System.Drawing.Size(98, 15);
            this.lblSearchInventory.TabIndex = 1;
            this.lblSearchInventory.Text = "Search Inventory:";
            // 
            // txtSearchInventory
            // 
            this.txtSearchInventory.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtSearchInventory.ForeColor = System.Drawing.Color.Gray;
            this.txtSearchInventory.Location = new System.Drawing.Point(15, 15);
            this.txtSearchInventory.Name = "txtSearchInventory";
            this.txtSearchInventory.Size = new System.Drawing.Size(250, 25);
            this.txtSearchInventory.TabIndex = 0;
            this.txtSearchInventory.Text = "🔍 Search inventory...";
            this.txtSearchInventory.TextChanged += new System.EventHandler(this.txtSearchInventory_TextChanged);
            this.txtSearchInventory.Enter += new System.EventHandler(this.TxtSearchInventory_Enter);
            this.txtSearchInventory.Leave += new System.EventHandler(this.TxtSearchInventory_Leave);
            //
            // btnUpdateStockInv
            //
            this.btnUpdateStockInv.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(196)))), ((int)(((byte)(15)))));
            this.btnUpdateStockInv.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdateStockInv.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnUpdateStockInv.ForeColor = System.Drawing.Color.White;
            this.btnUpdateStockInv.Location = new System.Drawing.Point(900, 12);
            this.btnUpdateStockInv.Name = "btnUpdateStockInv";
            this.btnUpdateStockInv.Size = new System.Drawing.Size(120, 35);
            this.btnUpdateStockInv.TabIndex = 2;
            this.btnUpdateStockInv.Text = "📈 Update Stock";
            this.btnUpdateStockInv.UseVisualStyleBackColor = false;
            this.btnUpdateStockInv.Click += new System.EventHandler(this.btnUpdateStockInv_Click);
            //
            // btnBulkUpdateStockInv
            //
            this.btnBulkUpdateStockInv.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(126)))), ((int)(((byte)(34)))));
            this.btnBulkUpdateStockInv.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBulkUpdateStockInv.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnBulkUpdateStockInv.ForeColor = System.Drawing.Color.White;
            this.btnBulkUpdateStockInv.Location = new System.Drawing.Point(1030, 12);
            this.btnBulkUpdateStockInv.Name = "btnBulkUpdateStockInv";
            this.btnBulkUpdateStockInv.Size = new System.Drawing.Size(120, 35);
            this.btnBulkUpdateStockInv.TabIndex = 3;
            this.btnBulkUpdateStockInv.Text = "📦 Bulk Update";
            this.btnBulkUpdateStockInv.UseVisualStyleBackColor = false;
            this.btnBulkUpdateStockInv.Click += new System.EventHandler(this.btnBulkUpdateStockInv_Click);
            // 
            // tabProducts
            // 
            this.tabProducts.BackColor = System.Drawing.Color.White;
            this.tabProducts.Controls.Add(this.pnlProducts);
            this.tabProducts.Location = new System.Drawing.Point(4, 26);
            this.tabProducts.Name = "tabProducts";
            this.tabProducts.Size = new System.Drawing.Size(1192, 770);
            this.tabProducts.TabIndex = 2;
            this.tabProducts.Text = "🏷️ Products";
            // 
            // pnlProducts
            // 
            this.pnlProducts.BackColor = System.Drawing.Color.White;
            this.pnlProducts.Controls.Add(this.dgvProducts);
            this.pnlProducts.Controls.Add(this.btnManageProducts);
            this.pnlProducts.Controls.Add(this.lblProductsTabTitle);
            this.pnlProducts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlProducts.Location = new System.Drawing.Point(0, 0);
            this.pnlProducts.Name = "pnlProducts";
            this.pnlProducts.Padding = new System.Windows.Forms.Padding(20);
            this.pnlProducts.Size = new System.Drawing.Size(1192, 770);
            this.pnlProducts.TabIndex = 0;
            // 
            // dgvProducts
            // 
            this.dgvProducts.AllowUserToAddRows = false;
            this.dgvProducts.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvProducts.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvProducts.BackgroundColor = System.Drawing.Color.White;
            this.dgvProducts.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvProducts.Location = new System.Drawing.Point(20, 140);
            this.dgvProducts.MultiSelect = false;
            this.dgvProducts.Name = "dgvProducts";
            this.dgvProducts.ReadOnly = true;
            this.dgvProducts.RowHeadersVisible = false;
            this.dgvProducts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvProducts.Size = new System.Drawing.Size(1152, 610);
            this.dgvProducts.TabIndex = 2;
            // 
            // btnManageProducts
            // 
            this.btnManageProducts.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.btnManageProducts.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnManageProducts.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnManageProducts.ForeColor = System.Drawing.Color.White;
            this.btnManageProducts.Location = new System.Drawing.Point(20, 80);
            this.btnManageProducts.Name = "btnManageProducts";
            this.btnManageProducts.Size = new System.Drawing.Size(200, 50);
            this.btnManageProducts.TabIndex = 1;
            this.btnManageProducts.Text = "🏷️ Manage Products";
            this.btnManageProducts.UseVisualStyleBackColor = false;
            this.btnManageProducts.Click += new System.EventHandler(this.btnManageProducts_Click);
            // 
            // lblProductsTabTitle
            // 
            this.lblProductsTabTitle.AutoSize = true;
            this.lblProductsTabTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblProductsTabTitle.Location = new System.Drawing.Point(20, 20);
            this.lblProductsTabTitle.Name = "lblProductsTabTitle";
            this.lblProductsTabTitle.Size = new System.Drawing.Size(240, 30);
            this.lblProductsTabTitle.TabIndex = 0;
            this.lblProductsTabTitle.Text = "Product Management";
            // 
            // tabCategories
            // 
            this.tabCategories.BackColor = System.Drawing.Color.White;
            this.tabCategories.Controls.Add(this.pnlCategories);
            this.tabCategories.Location = new System.Drawing.Point(4, 26);
            this.tabCategories.Name = "tabCategories";
            this.tabCategories.Size = new System.Drawing.Size(1192, 770);
            this.tabCategories.TabIndex = 3;
            this.tabCategories.Text = "🗂️ Categories";
            // 
            // pnlCategories
            // 
            this.pnlCategories.BackColor = System.Drawing.Color.White;
            this.pnlCategories.Controls.Add(this.dgvCategories);
            this.pnlCategories.Controls.Add(this.pnlCategoriesSearch);
            this.pnlCategories.Controls.Add(this.btnManageCategories);
            this.pnlCategories.Controls.Add(this.lblCategoriesTitle);
            this.pnlCategories.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCategories.Location = new System.Drawing.Point(0, 0);
            this.pnlCategories.Name = "pnlCategories";
            this.pnlCategories.Padding = new System.Windows.Forms.Padding(20);
            this.pnlCategories.Size = new System.Drawing.Size(1192, 770);
            this.pnlCategories.TabIndex = 0;
            // 
            // dgvCategories
            // 
            this.dgvCategories.AllowUserToAddRows = false;
            this.dgvCategories.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvCategories.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCategories.BackgroundColor = System.Drawing.Color.White;
            this.dgvCategories.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvCategories.Location = new System.Drawing.Point(20, 190);
            this.dgvCategories.MultiSelect = false;
            this.dgvCategories.Name = "dgvCategories";
            this.dgvCategories.ReadOnly = true;
            this.dgvCategories.RowHeadersVisible = false;
            this.dgvCategories.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCategories.Size = new System.Drawing.Size(1152, 560);
            this.dgvCategories.TabIndex = 3;
            // 
            // pnlCategoriesSearch
            // 
            this.pnlCategoriesSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlCategoriesSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.pnlCategoriesSearch.Controls.Add(this.lblSearchCategories);
            this.pnlCategoriesSearch.Controls.Add(this.txtSearchCategories);
            this.pnlCategoriesSearch.Location = new System.Drawing.Point(20, 140);
            this.pnlCategoriesSearch.Name = "pnlCategoriesSearch";
            this.pnlCategoriesSearch.Padding = new System.Windows.Forms.Padding(10);
            this.pnlCategoriesSearch.Size = new System.Drawing.Size(1152, 40);
            this.pnlCategoriesSearch.TabIndex = 2;
            // 
            // lblSearchCategories
            // 
            this.lblSearchCategories.AutoSize = true;
            this.lblSearchCategories.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSearchCategories.Location = new System.Drawing.Point(10, 12);
            this.lblSearchCategories.Name = "lblSearchCategories";
            this.lblSearchCategories.Size = new System.Drawing.Size(52, 19);
            this.lblSearchCategories.TabIndex = 0;
            this.lblSearchCategories.Text = "Search:";
            // 
            // txtSearchCategories
            // 
            this.txtSearchCategories.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearchCategories.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtSearchCategories.ForeColor = System.Drawing.Color.Gray;
            this.txtSearchCategories.Location = new System.Drawing.Point(70, 10);
            this.txtSearchCategories.Name = "txtSearchCategories";
            this.txtSearchCategories.Size = new System.Drawing.Size(1072, 25);
            this.txtSearchCategories.TabIndex = 1;
            this.txtSearchCategories.Text = "🔍 Search categories...";
            this.txtSearchCategories.TextChanged += new System.EventHandler(this.txtSearchCategories_TextChanged);
            this.txtSearchCategories.Enter += new System.EventHandler(this.txtSearchCategories_Enter);
            this.txtSearchCategories.Leave += new System.EventHandler(this.txtSearchCategories_Leave);
            // 
            // btnManageCategories
            // 
            this.btnManageCategories.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.btnManageCategories.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnManageCategories.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnManageCategories.ForeColor = System.Drawing.Color.White;
            this.btnManageCategories.Location = new System.Drawing.Point(20, 80);
            this.btnManageCategories.Name = "btnManageCategories";
            this.btnManageCategories.Size = new System.Drawing.Size(200, 50);
            this.btnManageCategories.TabIndex = 1;
            this.btnManageCategories.Text = "🗂️ Manage Categories";
            this.btnManageCategories.UseVisualStyleBackColor = false;
            this.btnManageCategories.Click += new System.EventHandler(this.btnManageCategories_Click);
            // 
            // lblCategoriesTitle
            // 
            this.lblCategoriesTitle.AutoSize = true;
            this.lblCategoriesTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblCategoriesTitle.Location = new System.Drawing.Point(20, 20);
            this.lblCategoriesTitle.Name = "lblCategoriesTitle";
            this.lblCategoriesTitle.Size = new System.Drawing.Size(253, 30);
            this.lblCategoriesTitle.TabIndex = 0;
            this.lblCategoriesTitle.Text = "Category Management";
            // 
            // tabOrders
            // 
            this.tabOrders.BackColor = System.Drawing.Color.White;
            this.tabOrders.Controls.Add(this.pnlOrders);
            this.tabOrders.Location = new System.Drawing.Point(4, 26);
            this.tabOrders.Name = "tabOrders";
            this.tabOrders.Size = new System.Drawing.Size(1192, 770);
            this.tabOrders.TabIndex = 4;
            this.tabOrders.Text = "🛒 Orders";
            // 
            // pnlOrders
            // 
            this.pnlOrders.BackColor = System.Drawing.Color.White;
            this.pnlOrders.Controls.Add(this.dgvOrders);
            this.pnlOrders.Controls.Add(this.pnlOrdersSearch);
            this.pnlOrders.Controls.Add(this.btnManageOrders);
            this.pnlOrders.Controls.Add(this.btnCreateOrder);
            this.pnlOrders.Controls.Add(this.lblOrdersTabTitle);
            this.pnlOrders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlOrders.Location = new System.Drawing.Point(0, 0);
            this.pnlOrders.Name = "pnlOrders";
            this.pnlOrders.Padding = new System.Windows.Forms.Padding(20);
            this.pnlOrders.Size = new System.Drawing.Size(1192, 770);
            this.pnlOrders.TabIndex = 0;
            // 
            // dgvOrders
            // 
            this.dgvOrders.AllowUserToAddRows = false;
            this.dgvOrders.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvOrders.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvOrders.BackgroundColor = System.Drawing.Color.White;
            this.dgvOrders.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvOrders.Location = new System.Drawing.Point(20, 190);
            this.dgvOrders.MultiSelect = false;
            this.dgvOrders.Name = "dgvOrders";
            this.dgvOrders.ReadOnly = true;
            this.dgvOrders.RowHeadersVisible = false;
            this.dgvOrders.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvOrders.Size = new System.Drawing.Size(1152, 560);
            this.dgvOrders.TabIndex = 4;
            this.dgvOrders.SelectionChanged += new System.EventHandler(this.dgvOrders_SelectionChanged);
            // 
            // pnlOrdersSearch
            // 
            this.pnlOrdersSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlOrdersSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.pnlOrdersSearch.Controls.Add(this.lblSearchOrders);
            this.pnlOrdersSearch.Controls.Add(this.txtSearchOrders);
            this.pnlOrdersSearch.Location = new System.Drawing.Point(20, 140);
            this.pnlOrdersSearch.Name = "pnlOrdersSearch";
            this.pnlOrdersSearch.Padding = new System.Windows.Forms.Padding(10);
            this.pnlOrdersSearch.Size = new System.Drawing.Size(1152, 40);
            this.pnlOrdersSearch.TabIndex = 3;
            // 
            // lblSearchOrders
            // 
            this.lblSearchOrders.AutoSize = true;
            this.lblSearchOrders.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSearchOrders.Location = new System.Drawing.Point(10, 12);
            this.lblSearchOrders.Name = "lblSearchOrders";
            this.lblSearchOrders.Size = new System.Drawing.Size(52, 19);
            this.lblSearchOrders.TabIndex = 0;
            this.lblSearchOrders.Text = "Search:";
            // 
            // txtSearchOrders
            // 
            this.txtSearchOrders.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearchOrders.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtSearchOrders.ForeColor = System.Drawing.Color.Gray;
            this.txtSearchOrders.Location = new System.Drawing.Point(70, 10);
            this.txtSearchOrders.Name = "txtSearchOrders";
            this.txtSearchOrders.Size = new System.Drawing.Size(1072, 25);
            this.txtSearchOrders.TabIndex = 1;
            this.txtSearchOrders.Text = "🔍 Search orders...";
            this.txtSearchOrders.TextChanged += new System.EventHandler(this.txtSearchOrders_TextChanged);
            this.txtSearchOrders.Enter += new System.EventHandler(this.txtSearchOrders_Enter);
            this.txtSearchOrders.Leave += new System.EventHandler(this.txtSearchOrders_Leave);
            // 
            // btnManageOrders
            // 
            this.btnManageOrders.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.btnManageOrders.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnManageOrders.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnManageOrders.ForeColor = System.Drawing.Color.White;
            this.btnManageOrders.Location = new System.Drawing.Point(250, 80);
            this.btnManageOrders.Name = "btnManageOrders";
            this.btnManageOrders.Size = new System.Drawing.Size(200, 50);
            this.btnManageOrders.TabIndex = 2;
            this.btnManageOrders.Text = "📋 Manage Orders";
            this.btnManageOrders.UseVisualStyleBackColor = false;
            this.btnManageOrders.Click += new System.EventHandler(this.btnManageOrders_Click);
            // 
            // btnCreateOrder
            // 
            this.btnCreateOrder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnCreateOrder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCreateOrder.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnCreateOrder.ForeColor = System.Drawing.Color.White;
            this.btnCreateOrder.Location = new System.Drawing.Point(20, 80);
            this.btnCreateOrder.Name = "btnCreateOrder";
            this.btnCreateOrder.Size = new System.Drawing.Size(200, 50);
            this.btnCreateOrder.TabIndex = 1;
            this.btnCreateOrder.Text = "➕ Create Order";
            this.btnCreateOrder.UseVisualStyleBackColor = false;
            this.btnCreateOrder.Click += new System.EventHandler(this.btnCreateOrder_Click);
            // 
            // lblOrdersTabTitle
            // 
            this.lblOrdersTabTitle.AutoSize = true;
            this.lblOrdersTabTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblOrdersTabTitle.Location = new System.Drawing.Point(20, 20);
            this.lblOrdersTabTitle.Name = "lblOrdersTabTitle";
            this.lblOrdersTabTitle.Size = new System.Drawing.Size(218, 30);
            this.lblOrdersTabTitle.TabIndex = 0;
            this.lblOrdersTabTitle.Text = "Order Management";
            // 
            // MainDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(1200, 800);
            this.Controls.Add(this.tabControl);
            this.Name = "MainDashboard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Inventory Management System - Dashboard";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainDashboard_FormClosing);
            this.tabControl.ResumeLayout(false);
            this.tabDashboard.ResumeLayout(false);
            this.pnlDashboard.ResumeLayout(false);
            this.pnlDashboard.PerformLayout();
            this.pnlSummaryCards.ResumeLayout(false);
            this.pnlOrdersCard.ResumeLayout(false);
            this.pnlOrdersCard.PerformLayout();
            this.pnlProductsCard.ResumeLayout(false);
            this.pnlProductsCard.PerformLayout();
            this.pnlLowStockCard.ResumeLayout(false);
            this.pnlLowStockCard.PerformLayout();
            this.pnlTotalValueCard.ResumeLayout(false);
            this.pnlTotalValueCard.PerformLayout();
            this.tabInventory.ResumeLayout(false);
            this.pnlInventory.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvInventory)).EndInit();
            this.pnlInventorySearch.ResumeLayout(false);
            this.pnlInventorySearch.PerformLayout();
            this.tabProducts.ResumeLayout(false);
            this.pnlProducts.ResumeLayout(false);
            this.pnlProducts.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProducts)).EndInit();
            this.tabCategories.ResumeLayout(false);
            this.pnlCategories.ResumeLayout(false);
            this.pnlCategories.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCategories)).EndInit();
            this.pnlCategoriesSearch.ResumeLayout(false);
            this.pnlCategoriesSearch.PerformLayout();
            this.tabOrders.ResumeLayout(false);
            this.pnlOrders.ResumeLayout(false);
            this.pnlOrders.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrders)).EndInit();
            this.pnlOrdersSearch.ResumeLayout(false);
            this.pnlOrdersSearch.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabDashboard;
        private System.Windows.Forms.Panel pnlDashboard;
        private System.Windows.Forms.Button btnAddProduct;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnManageCategoriesDash;
        private System.Windows.Forms.Button btnCreateOrderDash;
        private System.Windows.Forms.Button btnBulkUpdateStock;
        private System.Windows.Forms.Panel pnlSummaryCards;
        private System.Windows.Forms.Panel pnlOrdersCard;
        private System.Windows.Forms.Label lblOrdersCount;
        private System.Windows.Forms.Label lblOrdersTitle;
        private System.Windows.Forms.Panel pnlProductsCard;
        private System.Windows.Forms.Label lblProductsCount;
        private System.Windows.Forms.Label lblProductsTitle;
        private System.Windows.Forms.Panel pnlLowStockCard;
        private System.Windows.Forms.Label lblLowStockCount;
        private System.Windows.Forms.Label lblLowStockTitle;
        private System.Windows.Forms.Panel pnlTotalValueCard;
        private System.Windows.Forms.Label lblTotalValue;
        private System.Windows.Forms.Label lblTotalValueTitle;
        private System.Windows.Forms.Label lblDashboardTitle;
        private System.Windows.Forms.TabPage tabInventory;
        private System.Windows.Forms.Panel pnlInventory;
        private System.Windows.Forms.DataGridView dgvInventory;
        private System.Windows.Forms.Panel pnlInventorySearch;
        private System.Windows.Forms.Label lblSearchInventory;
        private System.Windows.Forms.TextBox txtSearchInventory;
        private System.Windows.Forms.Button btnUpdateStockInv;
        private System.Windows.Forms.Button btnBulkUpdateStockInv;
        private System.Windows.Forms.TabPage tabProducts;
        private System.Windows.Forms.Panel pnlProducts;
        private System.Windows.Forms.DataGridView dgvProducts;
        private System.Windows.Forms.Button btnManageProducts;
        private System.Windows.Forms.Label lblProductsTabTitle;
        private System.Windows.Forms.TabPage tabCategories;
        private System.Windows.Forms.Panel pnlCategories;
        private System.Windows.Forms.DataGridView dgvCategories;
        private System.Windows.Forms.Panel pnlCategoriesSearch;
        private System.Windows.Forms.Label lblSearchCategories;
        private System.Windows.Forms.TextBox txtSearchCategories;
        private System.Windows.Forms.Button btnManageCategories;
        private System.Windows.Forms.Label lblCategoriesTitle;
        private System.Windows.Forms.TabPage tabOrders;
        private System.Windows.Forms.Panel pnlOrders;
        private System.Windows.Forms.DataGridView dgvOrders;
        private System.Windows.Forms.Panel pnlOrdersSearch;
        private System.Windows.Forms.Label lblSearchOrders;
        private System.Windows.Forms.TextBox txtSearchOrders;
        private System.Windows.Forms.Button btnManageOrders;
        private System.Windows.Forms.Button btnCreateOrder;
        private System.Windows.Forms.Label lblOrdersTabTitle;
    }
} 