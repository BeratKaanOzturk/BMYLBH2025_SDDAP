namespace BMYLBH2025_SDDAP
{
    partial class StockUpdateDialog
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
            this.groupBoxCurrentInfo = new System.Windows.Forms.GroupBox();
            this.lblCurrentStatus = new System.Windows.Forms.Label();
            this.lblMinimumStock = new System.Windows.Forms.Label();
            this.lblCurrentStock = new System.Windows.Forms.Label();
            this.lblProductName = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBoxUpdateStock = new System.Windows.Forms.GroupBox();
            this.lblNewStatus = new System.Windows.Forms.Label();
            this.lblAdjustment = new System.Windows.Forms.Label();
            this.numNewQuantity = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.lblAdjustmentLabel = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBoxCurrentInfo.SuspendLayout();
            this.groupBoxUpdateStock.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numNewQuantity)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBoxCurrentInfo
            // 
            this.groupBoxCurrentInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxCurrentInfo.Controls.Add(this.lblCurrentStatus);
            this.groupBoxCurrentInfo.Controls.Add(this.lblMinimumStock);
            this.groupBoxCurrentInfo.Controls.Add(this.lblCurrentStock);
            this.groupBoxCurrentInfo.Controls.Add(this.lblProductName);
            this.groupBoxCurrentInfo.Controls.Add(this.label5);
            this.groupBoxCurrentInfo.Controls.Add(this.label4);
            this.groupBoxCurrentInfo.Controls.Add(this.label3);
            this.groupBoxCurrentInfo.Controls.Add(this.label1);
            this.groupBoxCurrentInfo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.groupBoxCurrentInfo.Location = new System.Drawing.Point(12, 12);
            this.groupBoxCurrentInfo.Name = "groupBoxCurrentInfo";
            this.groupBoxCurrentInfo.Size = new System.Drawing.Size(410, 130);
            this.groupBoxCurrentInfo.TabIndex = 0;
            this.groupBoxCurrentInfo.TabStop = false;
            this.groupBoxCurrentInfo.Text = "📦 Current Stock Information";
            // 
            // lblCurrentStatus
            // 
            this.lblCurrentStatus.AutoSize = true;
            this.lblCurrentStatus.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblCurrentStatus.Location = new System.Drawing.Point(150, 100);
            this.lblCurrentStatus.Name = "lblCurrentStatus";
            this.lblCurrentStatus.Size = new System.Drawing.Size(49, 15);
            this.lblCurrentStatus.TabIndex = 7;
            this.lblCurrentStatus.Text = "Normal";
            // 
            // lblMinimumStock
            // 
            this.lblMinimumStock.AutoSize = true;
            this.lblMinimumStock.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblMinimumStock.Location = new System.Drawing.Point(150, 75);
            this.lblMinimumStock.Name = "lblMinimumStock";
            this.lblMinimumStock.Size = new System.Drawing.Size(13, 15);
            this.lblMinimumStock.TabIndex = 6;
            this.lblMinimumStock.Text = "0";
            // 
            // lblCurrentStock
            // 
            this.lblCurrentStock.AutoSize = true;
            this.lblCurrentStock.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblCurrentStock.Location = new System.Drawing.Point(150, 50);
            this.lblCurrentStock.Name = "lblCurrentStock";
            this.lblCurrentStock.Size = new System.Drawing.Size(13, 15);
            this.lblCurrentStock.TabIndex = 5;
            this.lblCurrentStock.Text = "0";
            // 
            // lblProductName
            // 
            this.lblProductName.AutoSize = true;
            this.lblProductName.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblProductName.Location = new System.Drawing.Point(150, 25);
            this.lblProductName.Name = "lblProductName";
            this.lblProductName.Size = new System.Drawing.Size(59, 15);
            this.lblProductName.TabIndex = 4;
            this.lblProductName.Text = "Product 1";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label5.Location = new System.Drawing.Point(15, 100);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(86, 15);
            this.label5.TabIndex = 3;
            this.label5.Text = "Current Status:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label4.Location = new System.Drawing.Point(15, 75);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(95, 15);
            this.label4.TabIndex = 2;
            this.label4.Text = "Minimum Stock:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label3.Location = new System.Drawing.Point(15, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 15);
            this.label3.TabIndex = 1;
            this.label3.Text = "Current Stock:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label1.Location = new System.Drawing.Point(15, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Product:";
            // 
            // groupBoxUpdateStock
            // 
            this.groupBoxUpdateStock.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxUpdateStock.Controls.Add(this.lblNewStatus);
            this.groupBoxUpdateStock.Controls.Add(this.lblAdjustment);
            this.groupBoxUpdateStock.Controls.Add(this.numNewQuantity);
            this.groupBoxUpdateStock.Controls.Add(this.label8);
            this.groupBoxUpdateStock.Controls.Add(this.lblAdjustmentLabel);
            this.groupBoxUpdateStock.Controls.Add(this.label6);
            this.groupBoxUpdateStock.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.groupBoxUpdateStock.Location = new System.Drawing.Point(12, 155);
            this.groupBoxUpdateStock.Name = "groupBoxUpdateStock";
            this.groupBoxUpdateStock.Size = new System.Drawing.Size(410, 120);
            this.groupBoxUpdateStock.TabIndex = 1;
            this.groupBoxUpdateStock.TabStop = false;
            this.groupBoxUpdateStock.Text = "✏️ Update Stock Quantity";
            // 
            // lblNewStatus
            // 
            this.lblNewStatus.AutoSize = true;
            this.lblNewStatus.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblNewStatus.Location = new System.Drawing.Point(150, 90);
            this.lblNewStatus.Name = "lblNewStatus";
            this.lblNewStatus.Size = new System.Drawing.Size(49, 15);
            this.lblNewStatus.TabIndex = 12;
            this.lblNewStatus.Text = "Normal";
            // 
            // lblAdjustment
            // 
            this.lblAdjustment.AutoSize = true;
            this.lblAdjustment.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblAdjustment.Location = new System.Drawing.Point(150, 60);
            this.lblAdjustment.Name = "lblAdjustment";
            this.lblAdjustment.Size = new System.Drawing.Size(17, 19);
            this.lblAdjustment.TabIndex = 11;
            this.lblAdjustment.Text = "0";
            // 
            // numNewQuantity
            // 
            this.numNewQuantity.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.numNewQuantity.Location = new System.Drawing.Point(150, 25);
            this.numNewQuantity.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.numNewQuantity.Name = "numNewQuantity";
            this.numNewQuantity.Size = new System.Drawing.Size(120, 25);
            this.numNewQuantity.TabIndex = 10;
            this.numNewQuantity.ValueChanged += new System.EventHandler(this.numNewQuantity_ValueChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label8.Location = new System.Drawing.Point(15, 90);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(71, 15);
            this.label8.TabIndex = 9;
            this.label8.Text = "New Status:";
            // 
            // lblAdjustmentLabel
            // 
            this.lblAdjustmentLabel.AutoSize = true;
            this.lblAdjustmentLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblAdjustmentLabel.Location = new System.Drawing.Point(15, 62);
            this.lblAdjustmentLabel.Name = "lblAdjustmentLabel";
            this.lblAdjustmentLabel.Size = new System.Drawing.Size(75, 15);
            this.lblAdjustmentLabel.TabIndex = 8;
            this.lblAdjustmentLabel.Text = "Adjustment:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label6.Location = new System.Drawing.Point(15, 28);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(81, 15);
            this.label6.TabIndex = 4;
            this.label6.Text = "New Quantity:";
            // 
            // btnUpdate
            // 
            this.btnUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUpdate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdate.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnUpdate.ForeColor = System.Drawing.Color.White;
            this.btnUpdate.Location = new System.Drawing.Point(267, 290);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(120, 35);
            this.btnUpdate.TabIndex = 2;
            this.btnUpdate.Text = "💾 Update Stock";
            this.btnUpdate.UseVisualStyleBackColor = false;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnCancel.Location = new System.Drawing.Point(141, 290);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(120, 35);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "❌ Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // StockUpdateDialog
            // 
            this.AcceptButton = this.btnUpdate;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(434, 337);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.groupBoxUpdateStock);
            this.Controls.Add(this.groupBoxCurrentInfo);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "StockUpdateDialog";
            this.Text = "Update Stock Quantity";
            this.groupBoxCurrentInfo.ResumeLayout(false);
            this.groupBoxCurrentInfo.PerformLayout();
            this.groupBoxUpdateStock.ResumeLayout(false);
            this.groupBoxUpdateStock.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numNewQuantity)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxCurrentInfo;
        private System.Windows.Forms.Label lblCurrentStatus;
        private System.Windows.Forms.Label lblMinimumStock;
        private System.Windows.Forms.Label lblCurrentStock;
        private System.Windows.Forms.Label lblProductName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBoxUpdateStock;
        private System.Windows.Forms.Label lblNewStatus;
        private System.Windows.Forms.Label lblAdjustment;
        private System.Windows.Forms.NumericUpDown numNewQuantity;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblAdjustmentLabel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnCancel;
    }
} 