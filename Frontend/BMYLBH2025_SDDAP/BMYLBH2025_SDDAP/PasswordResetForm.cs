using System;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using BMYLBH2025_SDDAP.Services;
using BMYLBH2025_SDDAP.Models;

namespace BMYLBH2025_SDDAP
{
    public partial class PasswordResetForm : Form
    {
        private readonly ApiService _apiService;
        private string _userEmail;
        private bool _isOTPVerified = false;

        public PasswordResetForm(string email = "")
        {
            InitializeComponent();
            _apiService = new ApiService();
            
            if (!string.IsNullOrWhiteSpace(email))
            {
                txtEmail.Text = email;
            }
            
            // Initially disable subsequent sections
            grpOTPVerification.Enabled = false;
            grpPasswordReset.Enabled = false;
        }

        private async void btnSendOTP_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                ShowError("Please enter your email address.");
                return;
            }

            if (!IsValidEmail(txtEmail.Text))
            {
                ShowError("Please enter a valid email address.");
                return;
            }

            try
            {
                btnSendOTP.Enabled = false;
                btnSendOTP.Text = "Sending...";
                
                var request = new ForgotPasswordRequest
                {
                    Email = txtEmail.Text
                };
                
                var response = await _apiService.ForgotPasswordAsync(request);
                
                if (response.Success)
                {
                    _userEmail = txtEmail.Text;
                    ShowSuccess("Password reset code sent to your email!");
                    
                    // Enable OTP verification section
                    grpOTPVerification.Enabled = true;
                    txtOTP.Focus();
                    
                    // Start countdown timer
                    StartCountdownTimer();
                }
                else
                {
                    ShowError(response.Message);
                }
            }
            catch (Exception ex)
            {
                ShowError($"Error sending reset code: {ex.Message}");
            }
            finally
            {
                btnSendOTP.Enabled = true;
                btnSendOTP.Text = "Send Reset Code";
            }
        }

        private async void btnVerifyOTP_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtOTP.Text))
            {
                ShowError("Please enter the 6-digit code.");
                return;
            }

            if (txtOTP.Text.Length != 6)
            {
                ShowError("Please enter a valid 6-digit code.");
                return;
            }

            try
            {
                btnVerifyOTP.Enabled = false;
                btnVerifyOTP.Text = "Verifying...";
                
                var request = new VerifyOTPRequest
                {
                    Email = _userEmail,
                    OTP = txtOTP.Text
                };
                
                var response = await _apiService.VerifyResetOTPAsync(request);
                
                if (response.Success)
                {
                    _isOTPVerified = true;
                    ShowSuccess("Code verified successfully!");
                    
                    // Enable password reset section
                    grpPasswordReset.Enabled = true;
                    txtNewPassword.Focus();
                    
                    // Disable OTP section
                    grpOTPVerification.Enabled = false;
                }
                else
                {
                    ShowError(response.Message);
                }
            }
            catch (Exception ex)
            {
                ShowError($"Error verifying code: {ex.Message}");
            }
            finally
            {
                btnVerifyOTP.Enabled = true;
                btnVerifyOTP.Text = "Verify Code";
            }
        }

        private async void btnResetPassword_Click(object sender, EventArgs e)
        {
            if (!_isOTPVerified)
            {
                ShowError("Please verify your OTP code first.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtNewPassword.Text))
            {
                ShowError("Please enter a new password.");
                return;
            }

            if (txtNewPassword.Text.Length < 6)
            {
                ShowError("Password must be at least 6 characters long.");
                return;
            }

            if (txtNewPassword.Text != txtConfirmPassword.Text)
            {
                ShowError("Passwords do not match.");
                return;
            }

            try
            {
                btnResetPassword.Enabled = false;
                btnResetPassword.Text = "Resetting...";
                
                var request = new ResetPasswordRequest
                {
                    Email = _userEmail,
                    OTP = txtOTP.Text,
                    NewPassword = txtNewPassword.Text
                };
                
                var response = await _apiService.ResetPasswordAsync(request);
                
                if (response.Success)
                {
                    ShowSuccess("Password reset successfully!");
                    
                    // Close form after 2 seconds
                    await Task.Delay(2000);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    ShowError(response.Message);
                }
            }
            catch (Exception ex)
            {
                ShowError($"Error resetting password: {ex.Message}");
            }
            finally
            {
                btnResetPassword.Enabled = true;
                btnResetPassword.Text = "Reset Password";
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void StartCountdownTimer()
        {
            var timer = new Timer();
            int countdown = 300; // 5 minutes
            
            timer.Interval = 1000; // 1 second
            timer.Tick += (s, e) =>
            {
                countdown--;
                int minutes = countdown / 60;
                int seconds = countdown % 60;
                
                lblCountdown.Text = $"Code expires in: {minutes:D2}:{seconds:D2}";
                
                if (countdown <= 0)
                {
                    timer.Stop();
                    lblCountdown.Text = "Code expired. Please request a new one.";
                    grpOTPVerification.Enabled = false;
                }
            };
            
            timer.Start();
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

        private void ShowError(string message)
        {
            lblMessage.Text = message;
            lblMessage.ForeColor = Color.Red;
            lblMessage.Visible = true;
        }

        private void ShowSuccess(string message)
        {
            lblMessage.Text = message;
            lblMessage.ForeColor = Color.Green;
            lblMessage.Visible = true;
        }

        private void txtOTP_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Only allow digits
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtNewPassword_TextChanged(object sender, EventArgs e)
        {
            UpdatePasswordStrength();
        }

        private void UpdatePasswordStrength()
        {
            var password = txtNewPassword.Text;
            var strength = "Weak";
            var color = Color.Red;

            if (password.Length >= 8)
            {
                strength = "Medium";
                color = Color.Orange;
                
                if (password.Length >= 12 && HasSpecialCharacters(password))
                {
                    strength = "Strong";
                    color = Color.Green;
                }
            }

            lblPasswordStrength.Text = $"Password Strength: {strength}";
            lblPasswordStrength.ForeColor = color;
        }

        private bool HasSpecialCharacters(string password)
        {
            return password.IndexOfAny("!@#$%^&*()_+-=[]{}|;:,.<>?".ToCharArray()) != -1;
        }
    }
} 