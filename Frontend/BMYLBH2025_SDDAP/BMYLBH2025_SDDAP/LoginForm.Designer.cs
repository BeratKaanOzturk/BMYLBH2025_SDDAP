namespace BMYLBH2025_SDDAP
{
    partial class LoginForm
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
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnLogin = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.btnRegister = new System.Windows.Forms.Button();
            this.btnTestLogin = new System.Windows.Forms.Button();
            this.btnResendVerification = new System.Windows.Forms.Button();
            this.btnForgotPassword = new System.Windows.Forms.Button();
            this.lblErrorMessage = new System.Windows.Forms.Label();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.pnlLoginForm = new System.Windows.Forms.Panel();
            this.pnlMain.SuspendLayout();
            this.pnlLoginForm.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtEmail
            // 
            this.txtEmail.BackColor = System.Drawing.Color.White;
            this.txtEmail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtEmail.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtEmail.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.txtEmail.Location = new System.Drawing.Point(30, 70);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(340, 27);
            this.txtEmail.TabIndex = 0;
            this.txtEmail.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtEmail_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.label1.Location = new System.Drawing.Point(30, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 19);
            this.label1.TabIndex = 1;
            this.label1.Text = "📧 Email";
            // 
            // btnLogin
            // 
            this.btnLogin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnLogin.FlatAppearance.BorderSize = 0;
            this.btnLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogin.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnLogin.ForeColor = System.Drawing.Color.White;
            this.btnLogin.Location = new System.Drawing.Point(30, 180);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(160, 45);
            this.btnLogin.TabIndex = 2;
            this.btnLogin.Text = "🔐 LOGIN";
            this.btnLogin.UseVisualStyleBackColor = false;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.label2.Location = new System.Drawing.Point(30, 110);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(105, 19);
            this.label2.TabIndex = 1;
            this.label2.Text = "🔑 Password";
            // 
            // txtPassword
            // 
            this.txtPassword.BackColor = System.Drawing.Color.White;
            this.txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPassword.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.txtPassword.Location = new System.Drawing.Point(30, 135);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '●';
            this.txtPassword.Size = new System.Drawing.Size(340, 27);
            this.txtPassword.TabIndex = 1;
            this.txtPassword.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPassword_KeyPress);
            // 
            // btnRegister
            // 
            this.btnRegister.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnRegister.FlatAppearance.BorderSize = 0;
            this.btnRegister.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRegister.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnRegister.ForeColor = System.Drawing.Color.White;
            this.btnRegister.Location = new System.Drawing.Point(210, 180);
            this.btnRegister.Name = "btnRegister";
            this.btnRegister.Size = new System.Drawing.Size(160, 45);
            this.btnRegister.TabIndex = 3;
            this.btnRegister.Text = "👤 REGISTER";
            this.btnRegister.UseVisualStyleBackColor = false;
            this.btnRegister.Click += new System.EventHandler(this.btnRegister_Click);
            // 
            // btnTestLogin
            // 
            this.btnTestLogin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(196)))), ((int)(((byte)(15)))));
            this.btnTestLogin.FlatAppearance.BorderSize = 0;
            this.btnTestLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTestLogin.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnTestLogin.ForeColor = System.Drawing.Color.White;
            this.btnTestLogin.Location = new System.Drawing.Point(120, 240);
            this.btnTestLogin.Name = "btnTestLogin";
            this.btnTestLogin.Size = new System.Drawing.Size(160, 35);
            this.btnTestLogin.TabIndex = 4;
            this.btnTestLogin.Text = "⚡ TEST LOGIN";
            this.btnTestLogin.UseVisualStyleBackColor = false;
            this.btnTestLogin.Click += new System.EventHandler(this.btnTestLogin_Click);
            // 
            // btnResendVerification
            // 
            this.btnResendVerification.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnResendVerification.FlatAppearance.BorderSize = 0;
            this.btnResendVerification.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnResendVerification.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnResendVerification.ForeColor = System.Drawing.Color.White;
            this.btnResendVerification.Location = new System.Drawing.Point(30, 290);
            this.btnResendVerification.Name = "btnResendVerification";
            this.btnResendVerification.Size = new System.Drawing.Size(170, 35);
            this.btnResendVerification.TabIndex = 5;
            this.btnResendVerification.Text = "📧 Resend Verification";
            this.btnResendVerification.UseVisualStyleBackColor = false;
            this.btnResendVerification.Visible = false;
            this.btnResendVerification.Click += new System.EventHandler(this.btnResendVerification_Click);
            // 
            // btnForgotPassword
            // 
            this.btnForgotPassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(89)))), ((int)(((byte)(182)))));
            this.btnForgotPassword.FlatAppearance.BorderSize = 0;
            this.btnForgotPassword.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnForgotPassword.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnForgotPassword.ForeColor = System.Drawing.Color.White;
            this.btnForgotPassword.Location = new System.Drawing.Point(220, 290);
            this.btnForgotPassword.Name = "btnForgotPassword";
            this.btnForgotPassword.Size = new System.Drawing.Size(150, 35);
            this.btnForgotPassword.TabIndex = 6;
            this.btnForgotPassword.Text = "🔑 Forgot Password";
            this.btnForgotPassword.UseVisualStyleBackColor = false;
            this.btnForgotPassword.Visible = false;
            this.btnForgotPassword.Click += new System.EventHandler(this.btnForgotPassword_Click);
            // 
            // lblErrorMessage
            // 
            this.lblErrorMessage.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblErrorMessage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.lblErrorMessage.Location = new System.Drawing.Point(30, 340);
            this.lblErrorMessage.Name = "lblErrorMessage";
            this.lblErrorMessage.Size = new System.Drawing.Size(340, 25);
            this.lblErrorMessage.TabIndex = 7;
            this.lblErrorMessage.Text = "";
            this.lblErrorMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblErrorMessage.Visible = false;
            // 
            // pnlMain
            // 
            this.pnlMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.pnlMain.Controls.Add(this.pnlLoginForm);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Padding = new System.Windows.Forms.Padding(40);
            this.pnlMain.Size = new System.Drawing.Size(500, 480);
            this.pnlMain.TabIndex = 8;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.lblTitle.Location = new System.Drawing.Point(25, 15);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(350, 32);
            this.lblTitle.TabIndex = 9;
            this.lblTitle.Text = "📦 Inventory Management";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(140)))), ((int)(((byte)(141)))));
            this.lblSubtitle.Location = new System.Drawing.Point(135, 55);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(130, 15);
            this.lblSubtitle.TabIndex = 10;
            this.lblSubtitle.Text = "Please sign in to continue";
            this.lblSubtitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlLoginForm
            // 
            this.pnlLoginForm.BackColor = System.Drawing.Color.White;
            this.pnlLoginForm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlLoginForm.Controls.Add(this.lblTitle);
            this.pnlLoginForm.Controls.Add(this.lblSubtitle);
            this.pnlLoginForm.Controls.Add(this.label1);
            this.pnlLoginForm.Controls.Add(this.txtEmail);
            this.pnlLoginForm.Controls.Add(this.label2);
            this.pnlLoginForm.Controls.Add(this.txtPassword);
            this.pnlLoginForm.Controls.Add(this.btnLogin);
            this.pnlLoginForm.Controls.Add(this.btnRegister);
            this.pnlLoginForm.Controls.Add(this.btnTestLogin);
            this.pnlLoginForm.Controls.Add(this.btnResendVerification);
            this.pnlLoginForm.Controls.Add(this.btnForgotPassword);
            this.pnlLoginForm.Controls.Add(this.lblErrorMessage);
            this.pnlLoginForm.Location = new System.Drawing.Point(40, 40);
            this.pnlLoginForm.Name = "pnlLoginForm";
            this.pnlLoginForm.Size = new System.Drawing.Size(400, 380);
            this.pnlLoginForm.TabIndex = 0;
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(500, 480);
            this.Controls.Add(this.pnlMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login - Inventory Management System";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LoginForm_FormClosing);
            this.pnlMain.ResumeLayout(false);
            this.pnlLoginForm.ResumeLayout(false);
            this.pnlLoginForm.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Button btnRegister;
        private System.Windows.Forms.Button btnTestLogin;
        private System.Windows.Forms.Button btnResendVerification;
        private System.Windows.Forms.Button btnForgotPassword;
        private System.Windows.Forms.Label lblErrorMessage;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Panel pnlLoginForm;
    }
}