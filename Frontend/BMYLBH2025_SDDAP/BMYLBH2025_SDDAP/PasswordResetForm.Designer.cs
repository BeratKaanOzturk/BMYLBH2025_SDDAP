namespace BMYLBH2025_SDDAP
{
    partial class PasswordResetForm
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblMessage = new System.Windows.Forms.Label();
            this.grpEmailSection = new System.Windows.Forms.GroupBox();
            this.btnSendOTP = new System.Windows.Forms.Button();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.grpOTPVerification = new System.Windows.Forms.GroupBox();
            this.lblCountdown = new System.Windows.Forms.Label();
            this.btnVerifyOTP = new System.Windows.Forms.Button();
            this.txtOTP = new System.Windows.Forms.TextBox();
            this.lblOTP = new System.Windows.Forms.Label();
            this.grpPasswordReset = new System.Windows.Forms.GroupBox();
            this.lblPasswordStrength = new System.Windows.Forms.Label();
            this.btnResetPassword = new System.Windows.Forms.Button();
            this.txtConfirmPassword = new System.Windows.Forms.TextBox();
            this.lblConfirmPassword = new System.Windows.Forms.Label();
            this.txtNewPassword = new System.Windows.Forms.TextBox();
            this.lblNewPassword = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.grpEmailSection.SuspendLayout();
            this.grpOTPVerification.SuspendLayout();
            this.grpPasswordReset.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.lblTitle.Location = new System.Drawing.Point(12, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(165, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Reset Password";
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblMessage.Location = new System.Drawing.Point(12, 50);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(0, 15);
            this.lblMessage.TabIndex = 1;
            this.lblMessage.Visible = false;
            // 
            // grpEmailSection
            // 
            this.grpEmailSection.Controls.Add(this.btnSendOTP);
            this.grpEmailSection.Controls.Add(this.txtEmail);
            this.grpEmailSection.Controls.Add(this.lblEmail);
            this.grpEmailSection.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.grpEmailSection.Location = new System.Drawing.Point(12, 80);
            this.grpEmailSection.Name = "grpEmailSection";
            this.grpEmailSection.Size = new System.Drawing.Size(460, 100);
            this.grpEmailSection.TabIndex = 2;
            this.grpEmailSection.TabStop = false;
            this.grpEmailSection.Text = "Step 1: Enter Email Address";
            // 
            // btnSendOTP
            // 
            this.btnSendOTP.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnSendOTP.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSendOTP.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnSendOTP.ForeColor = System.Drawing.Color.White;
            this.btnSendOTP.Location = new System.Drawing.Point(340, 50);
            this.btnSendOTP.Name = "btnSendOTP";
            this.btnSendOTP.Size = new System.Drawing.Size(110, 35);
            this.btnSendOTP.TabIndex = 2;
            this.btnSendOTP.Text = "Send Reset Code";
            this.btnSendOTP.UseVisualStyleBackColor = false;
            this.btnSendOTP.Click += new System.EventHandler(this.btnSendOTP_Click);
            // 
            // txtEmail
            // 
            this.txtEmail.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtEmail.Location = new System.Drawing.Point(10, 55);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(320, 25);
            this.txtEmail.TabIndex = 1;
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblEmail.Location = new System.Drawing.Point(10, 30);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(39, 15);
            this.lblEmail.TabIndex = 0;
            this.lblEmail.Text = "Email:";
            // 
            // grpOTPVerification
            // 
            this.grpOTPVerification.Controls.Add(this.lblCountdown);
            this.grpOTPVerification.Controls.Add(this.btnVerifyOTP);
            this.grpOTPVerification.Controls.Add(this.txtOTP);
            this.grpOTPVerification.Controls.Add(this.lblOTP);
            this.grpOTPVerification.Enabled = false;
            this.grpOTPVerification.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.grpOTPVerification.Location = new System.Drawing.Point(12, 190);
            this.grpOTPVerification.Name = "grpOTPVerification";
            this.grpOTPVerification.Size = new System.Drawing.Size(460, 120);
            this.grpOTPVerification.TabIndex = 3;
            this.grpOTPVerification.TabStop = false;
            this.grpOTPVerification.Text = "Step 2: Enter Verification Code";
            // 
            // lblCountdown
            // 
            this.lblCountdown.AutoSize = true;
            this.lblCountdown.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblCountdown.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.lblCountdown.Location = new System.Drawing.Point(10, 90);
            this.lblCountdown.Name = "lblCountdown";
            this.lblCountdown.Size = new System.Drawing.Size(0, 13);
            this.lblCountdown.TabIndex = 3;
            // 
            // btnVerifyOTP
            // 
            this.btnVerifyOTP.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnVerifyOTP.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVerifyOTP.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnVerifyOTP.ForeColor = System.Drawing.Color.White;
            this.btnVerifyOTP.Location = new System.Drawing.Point(200, 50);
            this.btnVerifyOTP.Name = "btnVerifyOTP";
            this.btnVerifyOTP.Size = new System.Drawing.Size(100, 35);
            this.btnVerifyOTP.TabIndex = 2;
            this.btnVerifyOTP.Text = "Verify Code";
            this.btnVerifyOTP.UseVisualStyleBackColor = false;
            this.btnVerifyOTP.Click += new System.EventHandler(this.btnVerifyOTP_Click);
            // 
            // txtOTP
            // 
            this.txtOTP.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.txtOTP.Location = new System.Drawing.Point(10, 55);
            this.txtOTP.MaxLength = 6;
            this.txtOTP.Name = "txtOTP";
            this.txtOTP.Size = new System.Drawing.Size(180, 29);
            this.txtOTP.TabIndex = 1;
            this.txtOTP.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtOTP.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtOTP_KeyPress);
            // 
            // lblOTP
            // 
            this.lblOTP.AutoSize = true;
            this.lblOTP.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblOTP.Location = new System.Drawing.Point(10, 30);
            this.lblOTP.Name = "lblOTP";
            this.lblOTP.Size = new System.Drawing.Size(119, 15);
            this.lblOTP.TabIndex = 0;
            this.lblOTP.Text = "6-digit Code (from email):";
            // 
            // grpPasswordReset
            // 
            this.grpPasswordReset.Controls.Add(this.lblPasswordStrength);
            this.grpPasswordReset.Controls.Add(this.btnResetPassword);
            this.grpPasswordReset.Controls.Add(this.txtConfirmPassword);
            this.grpPasswordReset.Controls.Add(this.lblConfirmPassword);
            this.grpPasswordReset.Controls.Add(this.txtNewPassword);
            this.grpPasswordReset.Controls.Add(this.lblNewPassword);
            this.grpPasswordReset.Enabled = false;
            this.grpPasswordReset.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.grpPasswordReset.Location = new System.Drawing.Point(12, 320);
            this.grpPasswordReset.Name = "grpPasswordReset";
            this.grpPasswordReset.Size = new System.Drawing.Size(460, 150);
            this.grpPasswordReset.TabIndex = 4;
            this.grpPasswordReset.TabStop = false;
            this.grpPasswordReset.Text = "Step 3: Set New Password";
            // 
            // lblPasswordStrength
            // 
            this.lblPasswordStrength.AutoSize = true;
            this.lblPasswordStrength.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblPasswordStrength.Location = new System.Drawing.Point(250, 55);
            this.lblPasswordStrength.Name = "lblPasswordStrength";
            this.lblPasswordStrength.Size = new System.Drawing.Size(0, 13);
            this.lblPasswordStrength.TabIndex = 5;
            // 
            // btnResetPassword
            // 
            this.btnResetPassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.btnResetPassword.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnResetPassword.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnResetPassword.ForeColor = System.Drawing.Color.White;
            this.btnResetPassword.Location = new System.Drawing.Point(340, 100);
            this.btnResetPassword.Name = "btnResetPassword";
            this.btnResetPassword.Size = new System.Drawing.Size(110, 35);
            this.btnResetPassword.TabIndex = 4;
            this.btnResetPassword.Text = "Reset Password";
            this.btnResetPassword.UseVisualStyleBackColor = false;
            this.btnResetPassword.Click += new System.EventHandler(this.btnResetPassword_Click);
            // 
            // txtConfirmPassword
            // 
            this.txtConfirmPassword.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtConfirmPassword.Location = new System.Drawing.Point(10, 105);
            this.txtConfirmPassword.Name = "txtConfirmPassword";
            this.txtConfirmPassword.PasswordChar = '*';
            this.txtConfirmPassword.Size = new System.Drawing.Size(320, 25);
            this.txtConfirmPassword.TabIndex = 3;
            // 
            // lblConfirmPassword
            // 
            this.lblConfirmPassword.AutoSize = true;
            this.lblConfirmPassword.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblConfirmPassword.Location = new System.Drawing.Point(10, 80);
            this.lblConfirmPassword.Name = "lblConfirmPassword";
            this.lblConfirmPassword.Size = new System.Drawing.Size(107, 15);
            this.lblConfirmPassword.TabIndex = 2;
            this.lblConfirmPassword.Text = "Confirm Password:";
            // 
            // txtNewPassword
            // 
            this.txtNewPassword.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtNewPassword.Location = new System.Drawing.Point(10, 50);
            this.txtNewPassword.Name = "txtNewPassword";
            this.txtNewPassword.PasswordChar = '*';
            this.txtNewPassword.Size = new System.Drawing.Size(230, 25);
            this.txtNewPassword.TabIndex = 1;
            this.txtNewPassword.TextChanged += new System.EventHandler(this.txtNewPassword_TextChanged);
            // 
            // lblNewPassword
            // 
            this.lblNewPassword.AutoSize = true;
            this.lblNewPassword.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblNewPassword.Location = new System.Drawing.Point(10, 25);
            this.lblNewPassword.Name = "lblNewPassword";
            this.lblNewPassword.Size = new System.Drawing.Size(87, 15);
            this.lblNewPassword.TabIndex = 0;
            this.lblNewPassword.Text = "New Password:";
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(165)))), ((int)(((byte)(166)))));
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(12, 485);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 35);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // PasswordResetForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(484, 532);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.grpPasswordReset);
            this.Controls.Add(this.grpOTPVerification);
            this.Controls.Add(this.grpEmailSection);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.lblTitle);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PasswordResetForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Password Reset - Inventory Management System";
            this.grpEmailSection.ResumeLayout(false);
            this.grpEmailSection.PerformLayout();
            this.grpOTPVerification.ResumeLayout(false);
            this.grpOTPVerification.PerformLayout();
            this.grpPasswordReset.ResumeLayout(false);
            this.grpPasswordReset.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.GroupBox grpEmailSection;
        private System.Windows.Forms.Button btnSendOTP;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.GroupBox grpOTPVerification;
        private System.Windows.Forms.Label lblCountdown;
        private System.Windows.Forms.Button btnVerifyOTP;
        private System.Windows.Forms.TextBox txtOTP;
        private System.Windows.Forms.Label lblOTP;
        private System.Windows.Forms.GroupBox grpPasswordReset;
        private System.Windows.Forms.Label lblPasswordStrength;
        private System.Windows.Forms.Button btnResetPassword;
        private System.Windows.Forms.TextBox txtConfirmPassword;
        private System.Windows.Forms.Label lblConfirmPassword;
        private System.Windows.Forms.TextBox txtNewPassword;
        private System.Windows.Forms.Label lblNewPassword;
        private System.Windows.Forms.Button btnCancel;
    }
} 