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
            this.SuspendLayout();
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(173, 4);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(175, 20);
            this.txtEmail.TabIndex = 0;
            this.txtEmail.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtEmail_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(119, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "EMAIL : ";
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(136, 58);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(127, 49);
            this.btnLogin.TabIndex = 2;
            this.btnLogin.Text = "LOGIN";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(97, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "PASSWORD";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(173, 30);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(175, 20);
            this.txtPassword.TabIndex = 0;
            this.txtPassword.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPassword_KeyPress);
            // 
            // btnRegister
            // 
            this.btnRegister.Location = new System.Drawing.Point(3, 58);
            this.btnRegister.Name = "btnRegister";
            this.btnRegister.Size = new System.Drawing.Size(127, 49);
            this.btnRegister.TabIndex = 2;
            this.btnRegister.Text = "REGISTER";
            this.btnRegister.UseVisualStyleBackColor = true;
            this.btnRegister.Click += new System.EventHandler(this.btnRegister_Click);
            // 
            // btnTestLogin
            // 
            this.btnTestLogin.Location = new System.Drawing.Point(269, 58);
            this.btnTestLogin.Name = "btnTestLogin";
            this.btnTestLogin.Size = new System.Drawing.Size(127, 49);
            this.btnTestLogin.TabIndex = 2;
            this.btnTestLogin.Text = "TEST LOGIN";
            this.btnTestLogin.UseVisualStyleBackColor = true;
            this.btnTestLogin.Click += new System.EventHandler(this.btnTestLogin_Click);
            // 
            // btnResendVerification
            // 
            this.btnResendVerification.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnResendVerification.ForeColor = System.Drawing.Color.White;
            this.btnResendVerification.Location = new System.Drawing.Point(3, 140);
            this.btnResendVerification.Name = "btnResendVerification";
            this.btnResendVerification.Size = new System.Drawing.Size(190, 35);
            this.btnResendVerification.TabIndex = 3;
            this.btnResendVerification.Text = "📧 Resend Verification Email";
            this.btnResendVerification.UseVisualStyleBackColor = false;
            this.btnResendVerification.Visible = false;
            this.btnResendVerification.Click += new System.EventHandler(this.btnResendVerification_Click);
            // 
            // btnForgotPassword
            // 
            this.btnForgotPassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(89)))), ((int)(((byte)(182)))));
            this.btnForgotPassword.ForeColor = System.Drawing.Color.White;
            this.btnForgotPassword.Location = new System.Drawing.Point(206, 140);
            this.btnForgotPassword.Name = "btnForgotPassword";
            this.btnForgotPassword.Size = new System.Drawing.Size(190, 35);
            this.btnForgotPassword.TabIndex = 4;
            this.btnForgotPassword.Text = "🔑 Forgot Password";
            this.btnForgotPassword.UseVisualStyleBackColor = false;
            this.btnForgotPassword.Visible = false;
            this.btnForgotPassword.Click += new System.EventHandler(this.btnForgotPassword_Click);
            // 
            // lblErrorMessage
            // 
            this.lblErrorMessage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.lblErrorMessage.Location = new System.Drawing.Point(3, 110);
            this.lblErrorMessage.Name = "lblErrorMessage";
            this.lblErrorMessage.Size = new System.Drawing.Size(393, 25);
            this.lblErrorMessage.TabIndex = 5;
            this.lblErrorMessage.Text = "";
            this.lblErrorMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblErrorMessage.Visible = false;
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(444, 185);
            this.Controls.Add(this.lblErrorMessage);
            this.Controls.Add(this.btnForgotPassword);
            this.Controls.Add(this.btnResendVerification);
            this.Controls.Add(this.btnRegister);
            this.Controls.Add(this.btnTestLogin);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtEmail);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login - Inventory Management System";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LoginForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

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
    }
}