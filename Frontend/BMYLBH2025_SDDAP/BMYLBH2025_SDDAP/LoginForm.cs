using System;
using System.Windows.Forms;
using BMYLBH2025_SDDAP.Services;
using System.Threading.Tasks;

namespace BMYLBH2025_SDDAP
{
    public partial class LoginForm : Form
    {
        private readonly ApiService _apiService;
        private string _lastErrorEmail; // Store email for resend verification or forgot password

        public LoginForm()
        {
            InitializeComponent();
            _apiService = new ApiService();
            HideErrorElements(); // Initially hide all error UI elements
        }

        private void HideErrorElements()
        {
            lblErrorMessage.Visible = false;
            btnResendVerification.Visible = false;
            btnForgotPassword.Visible = false;
        }

        private async Task Login()
        {
            // Hide previous error messages
            HideErrorElements();

            // Validate input
            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                ShowErrorMessage("Please enter your email address.");
                txtEmail.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                ShowErrorMessage("Please enter your password.");
                txtPassword.Focus();
                return;
            }

            // Show loading state
            btnLogin.Enabled = btnRegister.Enabled = btnTestLogin.Enabled = false;
            btnLogin.Text = "LOGGING IN...";
            
            try
            {
                // Call backend API
                var authResponse = await _apiService.LoginAsync(txtEmail.Text.Trim(), txtPassword.Text);
                
                if (authResponse.Success && authResponse.Data?.Token != null)
                {
                    // Store token and open main form
                    _apiService.SetAuthToken(authResponse.Data.Token);
                    
                    MessageBox.Show("Login successful!", "Success", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    // Open main inventory dashboard
                    var mainForm = new MainDashboard(_apiService);
                    this.Hide();
                    mainForm.ShowDialog();
                    this.Close();
                }
                else
                {
                    // Handle specific error types
                    HandleLoginError(authResponse);
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Login failed: {ex.Message}");
            }
            finally
            {
                // Reset button state
                btnLogin.Enabled = btnRegister.Enabled = btnTestLogin.Enabled = true;
                btnLogin.Text = "LOGIN";
            }
        }

        private void HandleLoginError(ApiResponse<LoginResponseData> response)
        {
            _lastErrorEmail = response.Data?.Email ?? txtEmail.Text.Trim();
            
            ShowErrorMessage(response.Message);

            // Show appropriate action buttons based on error type
            if (response.Data?.ShowResendVerification == true)
            {
                btnResendVerification.Visible = true;
            }
            
            if (response.Data?.ShowForgotPassword == true)
            {
                btnForgotPassword.Visible = true;
            }
        }

        private void ShowErrorMessage(string message)
        {
            lblErrorMessage.Text = message;
            lblErrorMessage.Visible = true;
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            var registerForm = new RegisterForm();
            registerForm.ShowDialog();
        }

        private async void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow Enter key to trigger login
            if (e.KeyChar == (char)Keys.Enter)
            {
                await Login();
            }
        }

        private void txtEmail_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow Enter key to move to password field
            if (e.KeyChar == (char)Keys.Enter)
            {
                txtPassword.Focus();
            }
        }

        private void LoginForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Clean up API service
            _apiService?.Dispose();
        }

        // Test login with default admin credentials
        private async void btnTestLogin_Click(object sender, EventArgs e)
        {
            txtEmail.Text = "admin@inventory.com";
            txtPassword.Text = "admin123";
            await Login();
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            await Login();
        }

        private async void btnResendVerification_Click(object sender, EventArgs e)
        {
            try
            {
                btnResendVerification.Enabled = false;
                btnResendVerification.Text = "Sending...";

                var response = await _apiService.ResendVerificationEmailAsync(_lastErrorEmail);
                
                MessageBox.Show(response.Message, "Verification Email", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                HideErrorElements();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to send verification email: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnResendVerification.Enabled = true;
                btnResendVerification.Text = "📧 Resend Verification Email";
            }
        }

        private void btnForgotPassword_Click(object sender, EventArgs e)
        {
            var email = !string.IsNullOrWhiteSpace(_lastErrorEmail) ? _lastErrorEmail : txtEmail.Text.Trim();
            
            using (var resetForm = new PasswordResetForm(email))
            {
                if (resetForm.ShowDialog(this) == DialogResult.OK)
                {
                    MessageBox.Show("Password reset successfully! You can now log in with your new password.", 
                        "Password Reset Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    // Clear password field and focus on it
                    txtPassword.Clear();
                    txtPassword.Focus();
                    HideErrorElements();
                }
            }
        }
    }
}
