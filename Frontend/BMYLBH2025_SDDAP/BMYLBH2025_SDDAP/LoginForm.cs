using System;
using System.Windows.Forms;
using BMYLBH2025_SDDAP.Services;
using System.Threading.Tasks;

namespace BMYLBH2025_SDDAP
{
    public partial class LoginForm : Form
    {
        private readonly ApiService _apiService;

        public LoginForm()
        {
            InitializeComponent();
            _apiService = new ApiService();
        }

        private async Task btnLogin_Click(object sender, EventArgs e)
        {
            // Validate input
            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("Please enter your email address.", "Validation Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Please enter your password.", "Validation Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                
                if (!string.IsNullOrEmpty(authResponse.Token))
                {
                    // Store token and open main form
                    _apiService.SetAuthToken(authResponse.Token);
                    
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
                    MessageBox.Show("Invalid login credentials.", "Login Failed", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Login failed: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Reset button state
                btnLogin.Enabled = btnRegister.Enabled = btnTestLogin.Enabled = true;
                btnLogin.Text = "LOGIN";
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            var registerForm = new RegisterForm();
            registerForm.ShowDialog();
        }

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow Enter key to trigger login
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnLogin_Click(sender, e);
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
            await btnLogin_Click(sender, e);
        }
    }
}
