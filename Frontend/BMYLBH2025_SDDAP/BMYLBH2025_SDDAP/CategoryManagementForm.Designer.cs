namespace BMYLBH2025_SDDAP
{
    partial class CategoryManagementForm
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
            this.pnlContent = new System.Windows.Forms.Panel();
            this.pnlCategoryList = new System.Windows.Forms.Panel();
            this.dgvCategories = new System.Windows.Forms.DataGridView();
            this.pnlListHeader = new System.Windows.Forms.Panel();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lblSearch = new System.Windows.Forms.Label();
            this.lblCategoriesTitle = new System.Windows.Forms.Label();
            this.pnlCategoryDetails = new System.Windows.Forms.Panel();
            this.pnlButtons = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.pnlForm = new System.Windows.Forms.Panel();
            this.pnlStats = new System.Windows.Forms.Panel();
            this.lblTotalValue = new System.Windows.Forms.Label();
            this.lblTotalValueTitle = new System.Windows.Forms.Label();
            this.lblProductCount = new System.Windows.Forms.Label();
            this.lblProductCountTitle = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.lblDescription = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.txtCategoryId = new System.Windows.Forms.TextBox();
            this.lblCategoryId = new System.Windows.Forms.Label();
            this.lblDetailsTitle = new System.Windows.Forms.Label();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlMain.SuspendLayout();
            this.pnlContent.SuspendLayout();
            this.pnlCategoryList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCategories)).BeginInit();
            this.pnlListHeader.SuspendLayout();
            this.pnlCategoryDetails.SuspendLayout();
            this.pnlButtons.SuspendLayout();
            this.pnlForm.SuspendLayout();
            this.pnlStats.SuspendLayout();
            this.pnlHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.pnlMain.Controls.Add(this.pnlContent);
            this.pnlMain.Controls.Add(this.pnlHeader);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Padding = new System.Windows.Forms.Padding(10);
            this.pnlMain.Size = new System.Drawing.Size(1200, 700);
            this.pnlMain.TabIndex = 0;
            // 
            // pnlContent
            // 
            this.pnlContent.Controls.Add(this.pnlCategoryList);
            this.pnlContent.Controls.Add(this.pnlCategoryDetails);
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Location = new System.Drawing.Point(10, 70);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Size = new System.Drawing.Size(1180, 620);
            this.pnlContent.TabIndex = 1;
            // 
            // pnlCategoryList
            // 
            this.pnlCategoryList.BackColor = System.Drawing.Color.White;
            this.pnlCategoryList.Controls.Add(this.dgvCategories);
            this.pnlCategoryList.Controls.Add(this.pnlListHeader);
            this.pnlCategoryList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCategoryList.Location = new System.Drawing.Point(0, 0);
            this.pnlCategoryList.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.pnlCategoryList.Name = "pnlCategoryList";
            this.pnlCategoryList.Padding = new System.Windows.Forms.Padding(15);
            this.pnlCategoryList.Size = new System.Drawing.Size(780, 620);
            this.pnlCategoryList.TabIndex = 0;
            // 
            // dgvCategories
            // 
            this.dgvCategories.AllowUserToAddRows = false;
            this.dgvCategories.AllowUserToDeleteRows = false;
            this.dgvCategories.BackgroundColor = System.Drawing.Color.White;
            this.dgvCategories.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvCategories.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCategories.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCategories.Location = new System.Drawing.Point(15, 100);
            this.dgvCategories.MultiSelect = false;
            this.dgvCategories.Name = "dgvCategories";
            this.dgvCategories.ReadOnly = true;
            this.dgvCategories.RowHeadersVisible = false;
            this.dgvCategories.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCategories.Size = new System.Drawing.Size(750, 505);
            this.dgvCategories.TabIndex = 1;
            this.dgvCategories.SelectionChanged += new System.EventHandler(this.dgvCategories_SelectionChanged);
            // 
            // pnlListHeader
            // 
            this.pnlListHeader.Controls.Add(this.btnRefresh);
            this.pnlListHeader.Controls.Add(this.btnAdd);
            this.pnlListHeader.Controls.Add(this.txtSearch);
            this.pnlListHeader.Controls.Add(this.lblSearch);
            this.pnlListHeader.Controls.Add(this.lblCategoriesTitle);
            this.pnlListHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlListHeader.Location = new System.Drawing.Point(15, 15);
            this.pnlListHeader.Name = "pnlListHeader";
            this.pnlListHeader.Size = new System.Drawing.Size(750, 85);
            this.pnlListHeader.TabIndex = 0;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.btnRefresh.FlatAppearance.BorderSize = 0;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnRefresh.ForeColor = System.Drawing.Color.White;
            this.btnRefresh.Location = new System.Drawing.Point(650, 50);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(100, 30);
            this.btnRefresh.TabIndex = 4;
            this.btnRefresh.Text = "🔄 Refresh";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnAdd.FlatAppearance.BorderSize = 0;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Location = new System.Drawing.Point(540, 50);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(100, 30);
            this.btnAdd.TabIndex = 3;
            this.btnAdd.Text = "➕ Add New";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtSearch.Location = new System.Drawing.Point(70, 52);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(450, 25);
            this.txtSearch.TabIndex = 2;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSearch.Location = new System.Drawing.Point(0, 55);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(45, 15);
            this.lblSearch.TabIndex = 1;
            this.lblSearch.Text = "Search:";
            // 
            // lblCategoriesTitle
            // 
            this.lblCategoriesTitle.AutoSize = true;
            this.lblCategoriesTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblCategoriesTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(37)))), ((int)(((byte)(41)))));
            this.lblCategoriesTitle.Location = new System.Drawing.Point(0, 0);
            this.lblCategoriesTitle.Name = "lblCategoriesTitle";
            this.lblCategoriesTitle.Size = new System.Drawing.Size(106, 25);
            this.lblCategoriesTitle.TabIndex = 0;
            this.lblCategoriesTitle.Text = "Categories";
            // 
            // pnlCategoryDetails
            // 
            this.pnlCategoryDetails.BackColor = System.Drawing.Color.White;
            this.pnlCategoryDetails.Controls.Add(this.pnlButtons);
            this.pnlCategoryDetails.Controls.Add(this.pnlForm);
            this.pnlCategoryDetails.Controls.Add(this.lblDetailsTitle);
            this.pnlCategoryDetails.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlCategoryDetails.Location = new System.Drawing.Point(780, 0);
            this.pnlCategoryDetails.Name = "pnlCategoryDetails";
            this.pnlCategoryDetails.Padding = new System.Windows.Forms.Padding(15);
            this.pnlCategoryDetails.Size = new System.Drawing.Size(400, 620);
            this.pnlCategoryDetails.TabIndex = 1;
            // 
            // pnlButtons
            // 
            this.pnlButtons.Controls.Add(this.btnCancel);
            this.pnlButtons.Controls.Add(this.btnSave);
            this.pnlButtons.Controls.Add(this.btnDelete);
            this.pnlButtons.Controls.Add(this.btnEdit);
            this.pnlButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlButtons.Location = new System.Drawing.Point(15, 550);
            this.pnlButtons.Name = "pnlButtons";
            this.pnlButtons.Size = new System.Drawing.Size(370, 55);
            this.pnlButtons.TabIndex = 2;
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(280, 10);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 35);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Visible = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(190, 10);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(80, 35);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "💾 Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Visible = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.btnDelete.FlatAppearance.BorderSize = 0;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.Location = new System.Drawing.Point(100, 10);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(80, 35);
            this.btnDelete.TabIndex = 1;
            this.btnDelete.Text = "🗑️ Delete";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(193)))), ((int)(((byte)(7)))));
            this.btnEdit.FlatAppearance.BorderSize = 0;
            this.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEdit.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnEdit.ForeColor = System.Drawing.Color.Black;
            this.btnEdit.Location = new System.Drawing.Point(10, 10);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(80, 35);
            this.btnEdit.TabIndex = 0;
            this.btnEdit.Text = "✏️ Edit";
            this.btnEdit.UseVisualStyleBackColor = false;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // pnlForm
            // 
            this.pnlForm.Controls.Add(this.pnlStats);
            this.pnlForm.Controls.Add(this.txtDescription);
            this.pnlForm.Controls.Add(this.lblDescription);
            this.pnlForm.Controls.Add(this.txtName);
            this.pnlForm.Controls.Add(this.lblName);
            this.pnlForm.Controls.Add(this.txtCategoryId);
            this.pnlForm.Controls.Add(this.lblCategoryId);
            this.pnlForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlForm.Location = new System.Drawing.Point(15, 40);
            this.pnlForm.Name = "pnlForm";
            this.pnlForm.Size = new System.Drawing.Size(370, 565);
            this.pnlForm.TabIndex = 1;
            // 
            // pnlStats
            // 
            this.pnlStats.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.pnlStats.Controls.Add(this.lblTotalValue);
            this.pnlStats.Controls.Add(this.lblTotalValueTitle);
            this.pnlStats.Controls.Add(this.lblProductCount);
            this.pnlStats.Controls.Add(this.lblProductCountTitle);
            this.pnlStats.Location = new System.Drawing.Point(0, 200);
            this.pnlStats.Name = "pnlStats";
            this.pnlStats.Padding = new System.Windows.Forms.Padding(15);
            this.pnlStats.Size = new System.Drawing.Size(370, 120);
            this.pnlStats.TabIndex = 6;
            // 
            // lblTotalValue
            // 
            this.lblTotalValue.AutoSize = true;
            this.lblTotalValue.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTotalValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.lblTotalValue.Location = new System.Drawing.Point(15, 80);
            this.lblTotalValue.Name = "lblTotalValue";
            this.lblTotalValue.Size = new System.Drawing.Size(50, 21);
            this.lblTotalValue.TabIndex = 3;
            this.lblTotalValue.Text = "$0.00";
            // 
            // lblTotalValueTitle
            // 
            this.lblTotalValueTitle.AutoSize = true;
            this.lblTotalValueTitle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblTotalValueTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.lblTotalValueTitle.Location = new System.Drawing.Point(15, 60);
            this.lblTotalValueTitle.Name = "lblTotalValueTitle";
            this.lblTotalValueTitle.Size = new System.Drawing.Size(66, 15);
            this.lblTotalValueTitle.TabIndex = 2;
            this.lblTotalValueTitle.Text = "Total Value:";
            // 
            // lblProductCount
            // 
            this.lblProductCount.AutoSize = true;
            this.lblProductCount.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblProductCount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.lblProductCount.Location = new System.Drawing.Point(15, 35);
            this.lblProductCount.Name = "lblProductCount";
            this.lblProductCount.Size = new System.Drawing.Size(19, 21);
            this.lblProductCount.TabIndex = 1;
            this.lblProductCount.Text = "0";
            // 
            // lblProductCountTitle
            // 
            this.lblProductCountTitle.AutoSize = true;
            this.lblProductCountTitle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblProductCountTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.lblProductCountTitle.Location = new System.Drawing.Point(15, 15);
            this.lblProductCountTitle.Name = "lblProductCountTitle";
            this.lblProductCountTitle.Size = new System.Drawing.Size(88, 15);
            this.lblProductCountTitle.TabIndex = 0;
            this.lblProductCountTitle.Text = "Product Count:";
            // 
            // txtDescription
            // 
            this.txtDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDescription.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtDescription.Location = new System.Drawing.Point(0, 120);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.ReadOnly = true;
            this.txtDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDescription.Size = new System.Drawing.Size(370, 60);
            this.txtDescription.TabIndex = 5;
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblDescription.Location = new System.Drawing.Point(0, 100);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(74, 15);
            this.lblDescription.TabIndex = 4;
            this.lblDescription.Text = "Description:";
            // 
            // txtName
            // 
            this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtName.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtName.Location = new System.Drawing.Point(0, 70);
            this.txtName.Name = "txtName";
            this.txtName.ReadOnly = true;
            this.txtName.Size = new System.Drawing.Size(370, 25);
            this.txtName.TabIndex = 3;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblName.Location = new System.Drawing.Point(0, 50);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(43, 15);
            this.lblName.TabIndex = 2;
            this.lblName.Text = "Name:";
            // 
            // txtCategoryId
            // 
            this.txtCategoryId.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtCategoryId.Location = new System.Drawing.Point(0, 20);
            this.txtCategoryId.Name = "txtCategoryId";
            this.txtCategoryId.ReadOnly = true;
            this.txtCategoryId.Size = new System.Drawing.Size(100, 25);
            this.txtCategoryId.TabIndex = 1;
            // 
            // lblCategoryId
            // 
            this.lblCategoryId.AutoSize = true;
            this.lblCategoryId.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblCategoryId.Location = new System.Drawing.Point(0, 0);
            this.lblCategoryId.Name = "lblCategoryId";
            this.lblCategoryId.Size = new System.Drawing.Size(76, 15);
            this.lblCategoryId.TabIndex = 0;
            this.lblCategoryId.Text = "Category ID:";
            // 
            // lblDetailsTitle
            // 
            this.lblDetailsTitle.AutoSize = true;
            this.lblDetailsTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblDetailsTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblDetailsTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(37)))), ((int)(((byte)(41)))));
            this.lblDetailsTitle.Location = new System.Drawing.Point(15, 15);
            this.lblDetailsTitle.Name = "lblDetailsTitle";
            this.lblDetailsTitle.Size = new System.Drawing.Size(158, 25);
            this.lblDetailsTitle.TabIndex = 0;
            this.lblDetailsTitle.Text = "Category Details";
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(10, 10);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Padding = new System.Windows.Forms.Padding(20, 15, 20, 15);
            this.pnlHeader.Size = new System.Drawing.Size(1180, 60);
            this.pnlHeader.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(20, 15);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(276, 32);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Category Management";
            // 
            // CategoryManagementForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.Controls.Add(this.pnlMain);
            this.MinimumSize = new System.Drawing.Size(1200, 700);
            this.Name = "CategoryManagementForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Category Management - Inventory Management System";
            this.Load += new System.EventHandler(this.CategoryManagementForm_Load);
            this.pnlMain.ResumeLayout(false);
            this.pnlContent.ResumeLayout(false);
            this.pnlCategoryList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCategories)).EndInit();
            this.pnlListHeader.ResumeLayout(false);
            this.pnlListHeader.PerformLayout();
            this.pnlCategoryDetails.ResumeLayout(false);
            this.pnlCategoryDetails.PerformLayout();
            this.pnlButtons.ResumeLayout(false);
            this.pnlForm.ResumeLayout(false);
            this.pnlForm.PerformLayout();
            this.pnlStats.ResumeLayout(false);
            this.pnlStats.PerformLayout();
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.Panel pnlCategoryList;
        private System.Windows.Forms.DataGridView dgvCategories;
        private System.Windows.Forms.Panel pnlListHeader;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.Label lblCategoriesTitle;
        private System.Windows.Forms.Panel pnlCategoryDetails;
        private System.Windows.Forms.Panel pnlButtons;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Panel pnlForm;
        private System.Windows.Forms.Panel pnlStats;
        private System.Windows.Forms.Label lblTotalValue;
        private System.Windows.Forms.Label lblTotalValueTitle;
        private System.Windows.Forms.Label lblProductCount;
        private System.Windows.Forms.Label lblProductCountTitle;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox txtCategoryId;
        private System.Windows.Forms.Label lblCategoryId;
        private System.Windows.Forms.Label lblDetailsTitle;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
    }
} 