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
            this.btnUpdateStock = new System.Windows.Forms.Button();
            this.btnAddProduct = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.pnlSummaryCards = new System.Windows.Forms.Panel();
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
            this.tabProducts = new System.Windows.Forms.TabPage();
            this.pnlProducts = new System.Windows.Forms.Panel();
            this.dgvProducts = new System.Windows.Forms.DataGridView();
            this.tabReports = new System.Windows.Forms.TabPage();
            this.pnlReports = new System.Windows.Forms.Panel();
            this.lblReportsTitle = new System.Windows.Forms.Label();
            this.tabControl.SuspendLayout();
            this.tabDashboard.SuspendLayout();
            this.pnlDashboard.SuspendLayout();
            this.pnlSummaryCards.SuspendLayout();
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
            this.tabReports.SuspendLayout();
            this.pnlReports.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabDashboard);
            this.tabControl.Controls.Add(this.tabInventory);
            this.tabControl.Controls.Add(this.tabProducts);
            this.tabControl.Controls.Add(this.tabReports);
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
            this.tabDashboard.Location = new System.Drawing.Point(4, 32);
            this.tabDashboard.Name = "tabDashboard";
            this.tabDashboard.Size = new System.Drawing.Size(1192, 764);
            this.tabDashboard.TabIndex = 0;
            this.tabDashboard.Text = "📊 Dashboard";
            // 
            // pnlDashboard
            // 
            this.pnlDashboard.BackColor = System.Drawing.Color.White;
            this.pnlDashboard.Controls.Add(this.btnUpdateStock);
            this.pnlDashboard.Controls.Add(this.btnAddProduct);
            this.pnlDashboard.Controls.Add(this.btnRefresh);
            this.pnlDashboard.Controls.Add(this.pnlSummaryCards);
            this.pnlDashboard.Controls.Add(this.lblDashboardTitle);
            this.pnlDashboard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDashboard.Location = new System.Drawing.Point(0, 0);
            this.pnlDashboard.Name = "pnlDashboard";
            this.pnlDashboard.Padding = new System.Windows.Forms.Padding(20);
            this.pnlDashboard.Size = new System.Drawing.Size(1192, 764);
            this.pnlDashboard.TabIndex = 0;
            // 
            // btnUpdateStock
            // 
            this.btnUpdateStock.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(196)))), ((int)(((byte)(15)))));
            this.btnUpdateStock.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdateStock.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnUpdateStock.ForeColor = System.Drawing.Color.White;
            this.btnUpdateStock.Location = new System.Drawing.Point(410, 210);
            this.btnUpdateStock.Name = "btnUpdateStock";
            this.btnUpdateStock.Size = new System.Drawing.Size(150, 40);
            this.btnUpdateStock.TabIndex = 4;
            this.btnUpdateStock.Text = "📈 Update Stock";
            this.btnUpdateStock.UseVisualStyleBackColor = false;
            this.btnUpdateStock.Click += new System.EventHandler(this.btnUpdateStock_Click);
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
            this.pnlSummaryCards.Controls.Add(this.pnlProductsCard);
            this.pnlSummaryCards.Controls.Add(this.pnlLowStockCard);
            this.pnlSummaryCards.Controls.Add(this.pnlTotalValueCard);
            this.pnlSummaryCards.Location = new System.Drawing.Point(20, 70);
            this.pnlSummaryCards.Name = "pnlSummaryCards";
            this.pnlSummaryCards.Size = new System.Drawing.Size(1000, 120);
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
            this.lblProductsCount.Size = new System.Drawing.Size(25, 30);
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
            this.lblProductsTitle.Size = new System.Drawing.Size(126, 19);
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
            this.lblLowStockCount.Size = new System.Drawing.Size(25, 30);
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
            this.lblLowStockTitle.Size = new System.Drawing.Size(132, 19);
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
            this.lblTotalValue.Size = new System.Drawing.Size(67, 30);
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
            this.lblTotalValueTitle.Size = new System.Drawing.Size(161, 19);
            this.lblTotalValueTitle.TabIndex = 0;
            this.lblTotalValueTitle.Text = "💰 Total Inventory Value";
            // 
            // lblDashboardTitle
            // 
            this.lblDashboardTitle.AutoSize = true;
            this.lblDashboardTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblDashboardTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.lblDashboardTitle.Location = new System.Drawing.Point(20, 20);
            this.lblDashboardTitle.Name = "lblDashboardTitle";
            this.lblDashboardTitle.Size = new System.Drawing.Size(378, 32);
            this.lblDashboardTitle.TabIndex = 0;
            this.lblDashboardTitle.Text = "Inventory Management Dashboard";
            // 
            // tabInventory
            // 
            this.tabInventory.BackColor = System.Drawing.Color.White;
            this.tabInventory.Controls.Add(this.pnlInventory);
            this.tabInventory.Location = new System.Drawing.Point(4, 32);
            this.tabInventory.Name = "tabInventory";
            this.tabInventory.Size = new System.Drawing.Size(1192, 764);
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
            this.pnlInventory.Size = new System.Drawing.Size(1192, 764);
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
            this.dgvInventory.Size = new System.Drawing.Size(1172, 684);
            this.dgvInventory.TabIndex = 1;
            // 
            // pnlInventorySearch
            // 
            this.pnlInventorySearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
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
            this.lblSearchInventory.Size = new System.Drawing.Size(95, 15);
            this.lblSearchInventory.TabIndex = 1;
            this.lblSearchInventory.Text = "Search Inventory:";
            // 
            // txtSearchInventory
            // 
            this.txtSearchInventory.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtSearchInventory.Location = new System.Drawing.Point(15, 15);
            this.txtSearchInventory.Name = "txtSearchInventory";
            this.txtSearchInventory.Size = new System.Drawing.Size(250, 25);
            this.txtSearchInventory.TabIndex = 0;
            this.txtSearchInventory.TextChanged += new System.EventHandler(this.txtSearchInventory_TextChanged);
            this.txtSearchInventory.Enter += new System.EventHandler(this.TxtSearchInventory_Enter);
            this.txtSearchInventory.Leave += new System.EventHandler(this.TxtSearchInventory_Leave);
            // 
            // tabProducts
            // 
            this.tabProducts.BackColor = System.Drawing.Color.White;
            this.tabProducts.Controls.Add(this.pnlProducts);
            this.tabProducts.Location = new System.Drawing.Point(4, 32);
            this.tabProducts.Name = "tabProducts";
            this.tabProducts.Size = new System.Drawing.Size(1192, 764);
            this.tabProducts.TabIndex = 2;
            this.tabProducts.Text = "🏷️ Products";
            // 
            // pnlProducts
            // 
            this.pnlProducts.BackColor = System.Drawing.Color.White;
            this.pnlProducts.Controls.Add(this.dgvProducts);
            this.pnlProducts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlProducts.Location = new System.Drawing.Point(0, 0);
            this.pnlProducts.Name = "pnlProducts";
            this.pnlProducts.Padding = new System.Windows.Forms.Padding(10);
            this.pnlProducts.Size = new System.Drawing.Size(1192, 764);
            this.pnlProducts.TabIndex = 0;
            // 
            // dgvProducts
            // 
            this.dgvProducts.AllowUserToAddRows = false;
            this.dgvProducts.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvProducts.BackgroundColor = System.Drawing.Color.White;
            this.dgvProducts.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvProducts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvProducts.Location = new System.Drawing.Point(10, 10);
            this.dgvProducts.MultiSelect = false;
            this.dgvProducts.Name = "dgvProducts";
            this.dgvProducts.ReadOnly = true;
            this.dgvProducts.RowHeadersVisible = false;
            this.dgvProducts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvProducts.Size = new System.Drawing.Size(1172, 744);
            this.dgvProducts.TabIndex = 0;
            // 
            // tabReports
            // 
            this.tabReports.BackColor = System.Drawing.Color.White;
            this.tabReports.Controls.Add(this.pnlReports);
            this.tabReports.Location = new System.Drawing.Point(4, 32);
            this.tabReports.Name = "tabReports";
            this.tabReports.Size = new System.Drawing.Size(1192, 764);
            this.tabReports.TabIndex = 3;
            this.tabReports.Text = "📊 Reports";
            // 
            // pnlReports
            // 
            this.pnlReports.BackColor = System.Drawing.Color.White;
            this.pnlReports.Controls.Add(this.lblReportsTitle);
            this.pnlReports.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlReports.Location = new System.Drawing.Point(0, 0);
            this.pnlReports.Name = "pnlReports";
            this.pnlReports.Padding = new System.Windows.Forms.Padding(20);
            this.pnlReports.Size = new System.Drawing.Size(1192, 764);
            this.pnlReports.TabIndex = 0;
            // 
            // lblReportsTitle
            // 
            this.lblReportsTitle.AutoSize = true;
            this.lblReportsTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblReportsTitle.Location = new System.Drawing.Point(20, 20);
            this.lblReportsTitle.Name = "lblReportsTitle";
            this.lblReportsTitle.Size = new System.Drawing.Size(190, 30);
            this.lblReportsTitle.TabIndex = 0;
            this.lblReportsTitle.Text = "Reports & Analytics";
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
            ((System.ComponentModel.ISupportInitialize)(this.dgvProducts)).EndInit();
            this.tabReports.ResumeLayout(false);
            this.pnlReports.ResumeLayout(false);
            this.pnlReports.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabDashboard;
        private System.Windows.Forms.Panel pnlDashboard;
        private System.Windows.Forms.Button btnUpdateStock;
        private System.Windows.Forms.Button btnAddProduct;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Panel pnlSummaryCards;
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
        private System.Windows.Forms.TabPage tabProducts;
        private System.Windows.Forms.Panel pnlProducts;
        private System.Windows.Forms.DataGridView dgvProducts;
        private System.Windows.Forms.TabPage tabReports;
        private System.Windows.Forms.Panel pnlReports;
        private System.Windows.Forms.Label lblReportsTitle;
    }
} 