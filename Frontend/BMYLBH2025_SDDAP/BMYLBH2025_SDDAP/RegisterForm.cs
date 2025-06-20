using BMYLBH2025_SDDAP.Services;
using BMYLBH2025_SDDAP.Models;
using System;
using System.Windows.Forms;

namespace BMYLBH2025_SDDAP
{
    public partial class RegisterForm : Form
    {
        private readonly ApiService _apiService;

        public RegisterForm()
        {
            InitializeComponent();
            _apiService = new ApiService();
        }

        private async void btnRegister_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate input
                if (!ValidateInput())
                {
                    return;
                }

                // Show loading state
                btnRegister.Enabled = false;
                btnRegister.Text = "REGISTERING...";

                // Create registration request
                var registerRequest = new RegisterRequest
                {
                    Email = txtEmail.Text.Trim(),
                    Password = txtPassword.Text,
                    FullName = txtFullName.Text.Trim()
                };

                // Call registration API
                var response = await _apiService.RegisterAsync(registerRequest);

                if (response != null && response.Success)
                {
                    MessageBox.Show(
                        "Registration successful! Please check your email to verify your account before logging in.",
                        "Registration Successful",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show(
                        response?.Message ?? "Registration failed. Please try again.",
                        "Registration Failed",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Registration failed: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                // Reset button state
                btnRegister.Enabled = true;
                btnRegister.Text = "REGISTER";
            }
        }

        private bool ValidateInput()
        {
            // Validate email
            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("Please enter your email address.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return false;
            }

            if (!IsValidEmail(txtEmail.Text))
            {
                MessageBox.Show("Please enter a valid email address.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return false;
            }

            // Validate full name
            if (string.IsNullOrWhiteSpace(txtFullName.Text))
            {
                MessageBox.Show("Please enter your full name.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtFullName.Focus();
                return false;
            }

            // Validate password
            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Please enter a password.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
                return false;
            }

            if (txtPassword.Text.Length < 6)
            {
                MessageBox.Show("Password must be at least 6 characters long.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
                return false;
            }

            // Validate password confirmation
            if (string.IsNullOrWhiteSpace(txtRePassword.Text))
            {
                MessageBox.Show("Please confirm your password.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtRePassword.Focus();
                return false;
            }

            if (txtPassword.Text != txtRePassword.Text)
            {
                MessageBox.Show("Passwords do not match.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtRePassword.Focus();
                txtRePassword.SelectAll();
                return false;
            }

            return true;
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private void txtField_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow Enter key to trigger registration
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnRegister_Click(sender, e);
            }
        }

        private void RegisterForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Clean up API service
            _apiService?.Dispose();
        }
    }
}
