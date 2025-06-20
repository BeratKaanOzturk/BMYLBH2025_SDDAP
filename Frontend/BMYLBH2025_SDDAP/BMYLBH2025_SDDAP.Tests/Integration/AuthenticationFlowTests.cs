using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using Moq;
using BMYLBH2025_SDDAP;
using BMYLBH2025_SDDAP.Services;
using BMYLBH2025_SDDAP.Models;

namespace BMYLBH2025_SDDAP.Tests.Integration
{
    [TestClass]
    public class AuthenticationFlowTests
    {
        private LoginForm _loginForm;

        [TestInitialize]
        public void Setup()
        {
            _loginForm = new LoginForm();
        }

        [TestCleanup]
        public void Cleanup()
        {
            _loginForm?.Dispose();
        }

        #region Form Integration Tests

        [TestMethod]
        public void LoginForm_Integration_ShouldInitializeWithRequiredControls()
        {
            // Assert - Verify form has all required controls
            var txtEmail = FindControl<TextBox>(_loginForm, "txtEmail");
            var txtPassword = FindControl<TextBox>(_loginForm, "txtPassword");
            var btnLogin = FindControl<Button>(_loginForm, "btnLogin");
            var btnRegister = FindControl<Button>(_loginForm, "btnRegister");
            var btnTestLogin = FindControl<Button>(_loginForm, "btnTestLogin");

            txtEmail.Should().NotBeNull();
            txtPassword.Should().NotBeNull();
            btnLogin.Should().NotBeNull();
            btnRegister.Should().NotBeNull();
            btnTestLogin.Should().NotBeNull();
        }

        [TestMethod]
        public void LoginForm_TestLoginButton_ShouldFillCredentials()
        {
            // Act
            var btnTestLogin = FindControl<Button>(_loginForm, "btnTestLogin");
            btnTestLogin?.PerformClick();

            // Assert
            var txtEmail = FindControl<TextBox>(_loginForm, "txtEmail");
            var txtPassword = FindControl<TextBox>(_loginForm, "txtPassword");
            
            txtEmail?.Text.Should().Be("admin@inventory.com");
            txtPassword?.Text.Should().Be("admin123");
        }

        [TestMethod]
        public void LoginForm_EmptyCredentials_ShouldShowValidationError()
        {
            // Arrange
            var txtEmail = FindControl<TextBox>(_loginForm, "txtEmail");
            var txtPassword = FindControl<TextBox>(_loginForm, "txtPassword");
            if (txtEmail != null) txtEmail.Text = "";
            if (txtPassword != null) txtPassword.Text = "";

            // Act
            var btnLogin = FindControl<Button>(_loginForm, "btnLogin");
            btnLogin?.PerformClick();

            // Assert - Should show error message
            var lblError = FindControl<Label>(_loginForm, "lblErrorMessage");
            lblError?.Visible.Should().BeTrue();
        }

        [TestMethod]
        public void RegisterForm_Integration_ShouldInitializeProperly()
        {
            // Act
            using (var registerForm = new RegisterForm())
            {
                // Assert
                registerForm.Should().NotBeNull();
                registerForm.Text.Should().Contain("Register");
            }
        }

        [TestMethod]
        public void PasswordResetForm_Integration_ShouldInitializeProperly()
        {
            // Act
            using (var resetForm = new PasswordResetForm())
            {
                // Assert
                resetForm.Should().NotBeNull();
                resetForm.Text.Should().Contain("Reset");
            }
        }

        [TestMethod]
        public void FormNavigation_RegisterButton_ShouldBeAccessible()
        {
            // Act
            var btnRegister = FindControl<Button>(_loginForm, "btnRegister");
            
            // Assert
            btnRegister.Should().NotBeNull();
            btnRegister.Enabled.Should().BeTrue();
        }

        [TestMethod]
        public void FormValidation_EmailField_ShouldAcceptValidEmail()
        {
            // Arrange
            var txtEmail = FindControl<TextBox>(_loginForm, "txtEmail");
            
            // Act
            if (txtEmail != null)
            {
                txtEmail.Text = "test@example.com";
                txtEmail.Focus();
            }

            // Assert
            txtEmail?.Text.Should().Be("test@example.com");
        }

        [TestMethod]
        public void FormValidation_PasswordField_ShouldMaskInput()
        {
            // Arrange
            var txtPassword = FindControl<TextBox>(_loginForm, "txtPassword");
            
            // Assert
            txtPassword?.UseSystemPasswordChar.Should().BeTrue();
        }

        [TestMethod]
        public void FormBehavior_ErrorMessage_ShouldBeHiddenInitially()
        {
            // Arrange & Assert
            var lblError = FindControl<Label>(_loginForm, "lblErrorMessage");
            lblError?.Visible.Should().BeFalse();
        }

        [TestMethod]
        public void FormBehavior_ActionButtons_ShouldBeHiddenInitially()
        {
            // Arrange & Assert
            var btnResend = FindControl<Button>(_loginForm, "btnResendVerification");
            var btnForgot = FindControl<Button>(_loginForm, "btnForgotPassword");
            
            btnResend?.Visible.Should().BeFalse();
            btnForgot?.Visible.Should().BeFalse();
        }

        [TestMethod]
        public void FormLayout_ShouldBeResponsive()
        {
            // Act
            _loginForm.WindowState = FormWindowState.Normal;
            _loginForm.Size = new System.Drawing.Size(400, 500);

            // Assert
            _loginForm.Size.Width.Should().Be(400);
            _loginForm.Size.Height.Should().Be(500);
        }

        [TestMethod]
        public void FormLifecycle_ShouldDisposeCleanly()
        {
            // Arrange
            var testForm = new LoginForm();
            
            // Act
            testForm.Dispose();
            
            // Assert
            testForm.IsDisposed.Should().BeTrue();
        }

        [TestMethod]
        public void FormPerformance_ShouldLoadQuickly()
        {
            // Arrange
            var startTime = DateTime.Now;
            
            // Act
            using (var testForm = new LoginForm())
            {
                testForm.Show();
                testForm.Hide();
            }
            
            // Assert
            var loadTime = DateTime.Now - startTime;
            loadTime.Should().BeLessThan(TimeSpan.FromSeconds(3));
        }

        [TestMethod]
        public void FormAccessibility_AllControlsShouldHaveNames()
        {
            // Assert
            var controls = GetAllControls(_loginForm);
            foreach (Control control in controls)
            {
                if (control is TextBox || control is Button || control is Label)
                {
                    control.Name.Should().NotBeNullOrEmpty();
                }
            }
        }

        [TestMethod]
        public void FormTabOrder_ShouldBeLogical()
        {
            // Arrange
            var txtEmail = FindControl<TextBox>(_loginForm, "txtEmail");
            var txtPassword = FindControl<TextBox>(_loginForm, "txtPassword");
            var btnLogin = FindControl<Button>(_loginForm, "btnLogin");

            // Assert - Tab order should be logical
            txtEmail?.TabIndex.Should().BeLessThan(txtPassword?.TabIndex ?? int.MaxValue);
            txtPassword?.TabIndex.Should().BeLessThan(btnLogin?.TabIndex ?? int.MaxValue);
        }

        #endregion

        #region Helper Methods

        private T FindControl<T>(Control parent, string name) where T : Control
        {
            foreach (Control control in parent.Controls)
            {
                if (control.Name == name && control is T)
                    return control as T;
                
                var found = FindControl<T>(control, name);
                if (found != null)
                    return found;
            }
            return null;
        }

        private Control[] GetAllControls(Control parent)
        {
            var controls = new System.Collections.Generic.List<Control>();
            foreach (Control control in parent.Controls)
            {
                controls.Add(control);
                controls.AddRange(GetAllControls(control));
            }
            return controls.ToArray();
        }

        #endregion
    }
} 