namespace BMYLBH2025_SDDAP
{
    partial class BulkStockUpdateForm
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
            this.panelTop = new System.Windows.Forms.Panel();
            this.lblTotalItems = new System.Windows.Forms.Label();
            this.lblSelectedItems = new System.Windows.Forms.Label();
            this.btnSelectLowStock = new System.Windows.Forms.Button();
            this.btnDeselectAll = new System.Windows.Forms.Button();
            this.btnSelectAll = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvBulkUpdate = new System.Windows.Forms.DataGridView();
            this.panelBottom = new System.Windows.Forms.Panel();
            this.lblProgress = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnApplyUpdates = new System.Windows.Forms.Button();
            this.panelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBulkUpdate)).BeginInit();
            this.panelBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.panelTop.Controls.Add(this.lblTotalItems);
            this.panelTop.Controls.Add(this.lblSelectedItems);
            this.panelTop.Controls.Add(this.btnSelectLowStock);
            this.panelTop.Controls.Add(this.btnDeselectAll);
            this.panelTop.Controls.Add(this.btnSelectAll);
            this.panelTop.Controls.Add(this.label1);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(784, 80);
            this.panelTop.TabIndex = 0;
            // 
            // lblTotalItems
            // 
            this.lblTotalItems.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTotalItems.AutoSize = true;
            this.lblTotalItems.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblTotalItems.Location = new System.Drawing.Point(600, 50);
            this.lblTotalItems.Name = "lblTotalItems";
            this.lblTotalItems.Size = new System.Drawing.Size(77, 15);
            this.lblTotalItems.TabIndex = 5;
            this.lblTotalItems.Text = "Total Items: 0";
            // 
            // lblSelectedItems
            // 
            this.lblSelectedItems.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSelectedItems.AutoSize = true;
            this.lblSelectedItems.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblSelectedItems.Location = new System.Drawing.Point(600, 25);
            this.lblSelectedItems.Name = "lblSelectedItems";
            this.lblSelectedItems.Size = new System.Drawing.Size(101, 15);
            this.lblSelectedItems.TabIndex = 4;
            this.lblSelectedItems.Text = "Selected: 0 items";
            // 
            // btnSelectLowStock
            // 
            this.btnSelectLowStock.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelectLowStock.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnSelectLowStock.Location = new System.Drawing.Point(290, 40);
            this.btnSelectLowStock.Name = "btnSelectLowStock";
            this.btnSelectLowStock.Size = new System.Drawing.Size(130, 30);
            this.btnSelectLowStock.TabIndex = 3;
            this.btnSelectLowStock.Text = "⚠️ Select Low Stock";
            this.btnSelectLowStock.UseVisualStyleBackColor = true;
            this.btnSelectLowStock.Click += new System.EventHandler(this.btnSelectLowStock_Click);
            // 
            // btnDeselectAll
            // 
            this.btnDeselectAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeselectAll.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnDeselectAll.Location = new System.Drawing.Point(150, 40);
            this.btnDeselectAll.Name = "btnDeselectAll";
            this.btnDeselectAll.Size = new System.Drawing.Size(120, 30);
            this.btnDeselectAll.TabIndex = 2;
            this.btnDeselectAll.Text = "❌ Deselect All";
            this.btnDeselectAll.UseVisualStyleBackColor = true;
            this.btnDeselectAll.Click += new System.EventHandler(this.btnDeselectAll_Click);
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelectAll.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnSelectAll.Location = new System.Drawing.Point(20, 40);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(120, 30);
            this.btnSelectAll.TabIndex = 1;
            this.btnSelectAll.Text = "✅ Select All";
            this.btnSelectAll.UseVisualStyleBackColor = true;
            this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(15, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(338, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "📦 Bulk Stock Update - Select items to update";
            // 
            // dgvBulkUpdate
            // 
            this.dgvBulkUpdate.AllowUserToAddRows = false;
            this.dgvBulkUpdate.AllowUserToDeleteRows = false;
            this.dgvBulkUpdate.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvBulkUpdate.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBulkUpdate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvBulkUpdate.Location = new System.Drawing.Point(0, 80);
            this.dgvBulkUpdate.Name = "dgvBulkUpdate";
            this.dgvBulkUpdate.Size = new System.Drawing.Size(784, 391);
            this.dgvBulkUpdate.TabIndex = 1;
            this.dgvBulkUpdate.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvBulkUpdate_CellValueChanged);
            this.dgvBulkUpdate.CurrentCellDirtyStateChanged += new System.EventHandler(this.dgvBulkUpdate_CurrentCellDirtyStateChanged);
            // 
            // panelBottom
            // 
            this.panelBottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.panelBottom.Controls.Add(this.lblProgress);
            this.panelBottom.Controls.Add(this.progressBar);
            this.panelBottom.Controls.Add(this.btnCancel);
            this.panelBottom.Controls.Add(this.btnApplyUpdates);
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Location = new System.Drawing.Point(0, 471);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(784, 90);
            this.panelBottom.TabIndex = 2;
            // 
            // lblProgress
            // 
            this.lblProgress.AutoSize = true;
            this.lblProgress.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblProgress.Location = new System.Drawing.Point(20, 45);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(95, 15);
            this.lblProgress.TabIndex = 3;
            this.lblProgress.Text = "Processing... 0/0";
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(20, 20);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(400, 20);
            this.progressBar.TabIndex = 2;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnCancel.Location = new System.Drawing.Point(542, 35);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(110, 35);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "❌ Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnApplyUpdates
            // 
            this.btnApplyUpdates.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnApplyUpdates.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnApplyUpdates.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnApplyUpdates.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnApplyUpdates.ForeColor = System.Drawing.Color.White;
            this.btnApplyUpdates.Location = new System.Drawing.Point(658, 35);
            this.btnApplyUpdates.Name = "btnApplyUpdates";
            this.btnApplyUpdates.Size = new System.Drawing.Size(110, 35);
            this.btnApplyUpdates.TabIndex = 0;
            this.btnApplyUpdates.Text = "💾 Apply Updates";
            this.btnApplyUpdates.UseVisualStyleBackColor = false;
            this.btnApplyUpdates.Click += new System.EventHandler(this.btnApplyUpdates_Click);
            // 
            // BulkStockUpdateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.dgvBulkUpdate);
            this.Controls.Add(this.panelBottom);
            this.Controls.Add(this.panelTop);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "BulkStockUpdateForm";
            this.Text = "Bulk Stock Update";
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBulkUpdate)).EndInit();
            this.panelBottom.ResumeLayout(false);
            this.panelBottom.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Label lblTotalItems;
        private System.Windows.Forms.Label lblSelectedItems;
        private System.Windows.Forms.Button btnSelectLowStock;
        private System.Windows.Forms.Button btnDeselectAll;
        private System.Windows.Forms.Button btnSelectAll;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvBulkUpdate;
        private System.Windows.Forms.Panel panelBottom;
        private System.Windows.Forms.Label lblProgress;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnApplyUpdates;
    }
} 