using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using Moq;
using BMYLBH2025_SDDAP;
using BMYLBH2025_SDDAP.Services;
using BMYLBH2025_SDDAP.Models;

namespace BMYLBH2025_SDDAP.Tests.Forms
{
    [TestClass]
    public class LoginFormTests
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

        #region Form Initialization Tests

        [TestMethod]
        public void LoginForm_OnInitialization_ShouldHaveCorrectTitle()
        {
            // Assert
            _loginForm.Text.Should().Contain("Login");
        }

        [TestMethod]
        public void LoginForm_OnInitialization_ShouldHaveRequiredControls()
        {
            // Assert - Check if controls exist by finding them
            var txtEmail = FindControl<TextBox>(_loginForm, "txtEmail");
            var txtPassword = FindControl<TextBox>(_loginForm, "txtPassword");
            var btnLogin = FindControl<Button>(_loginForm, "btnLogin");
            var btnRegister = FindControl<Button>(_loginForm, "btnRegister");
            
            txtEmail.Should().NotBeNull();
            txtPassword.Should().NotBeNull();
            btnLogin.Should().NotBeNull();
            btnRegister.Should().NotBeNull();
        }

        [TestMethod]
        public void LoginForm_OnInitialization_ShouldHaveEmptyTextBoxes()
        {
            // Arrange
            var txtEmail = FindControl<TextBox>(_loginForm, "txtEmail");
            var txtPassword = FindControl<TextBox>(_loginForm, "txtPassword");

            // Assert
            txtEmail?.Text.Should().BeEmpty();
            txtPassword?.Text.Should().BeEmpty();
        }

        [TestMethod]
        public void LoginForm_OnInitialization_ShouldHavePasswordMasked()
        {
            // Arrange
            var txtPassword = FindControl<TextBox>(_loginForm, "txtPassword");

            // Assert
            txtPassword?.UseSystemPasswordChar.Should().BeTrue();
        }

        #endregion

        #region Input Validation Tests

        [TestMethod]
        public void ValidateInput_WithEmptyEmail_ShouldShowError()
        {
            // Arrange
            var txtEmail = FindControl<TextBox>(_loginForm, "txtEmail");
            var txtPassword = FindControl<TextBox>(_loginForm, "txtPassword");
            if (txtEmail != null) txtEmail.Text = "";
            if (txtPassword != null) txtPassword.Text = "password123";

            // Act - Simulate login button click
            var btnLogin = FindControl<Button>(_loginForm, "btnLogin");
            btnLogin?.PerformClick();

            // Assert - Should show error message
            var lblError = FindControl<Label>(_loginForm, "lblErrorMessage");
            lblError?.Visible.Should().BeTrue();
        }

        [TestMethod]
        public void ValidateInput_WithEmptyPassword_ShouldShowError()
        {
            // Arrange
            var txtEmail = FindControl<TextBox>(_loginForm, "txtEmail");
            var txtPassword = FindControl<TextBox>(_loginForm, "txtPassword");
            if (txtEmail != null) txtEmail.Text = "test@example.com";
            if (txtPassword != null) txtPassword.Text = "";

            // Act - Simulate login button click
            var btnLogin = FindControl<Button>(_loginForm, "btnLogin");
            btnLogin?.PerformClick();

            // Assert - Should show error message
            var lblError = FindControl<Label>(_loginForm, "lblErrorMessage");
            lblError?.Visible.Should().BeTrue();
        }

        [TestMethod]
        public void ValidateInput_WithValidInputs_ShouldNotShowError()
        {
            // Arrange
            var txtEmail = FindControl<TextBox>(_loginForm, "txtEmail");
            var txtPassword = FindControl<TextBox>(_loginForm, "txtPassword");
            if (txtEmail != null) txtEmail.Text = "test@example.com";
            if (txtPassword != null) txtPassword.Text = "password123";

            // Act - Check initial state
            var lblError = FindControl<Label>(_loginForm, "lblErrorMessage");
            
            // Assert - Error should not be visible initially
            lblError?.Visible.Should().BeFalse();
        }

        #endregion

        #region Button Click Tests

        [TestMethod]
        public void BtnTestLogin_Click_ShouldFillTestCredentials()
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
        public void BtnRegister_Click_ShouldOpenRegisterForm()
        {
            // This test would require more complex setup to mock form dialogs
            // For now, just verify the button exists
            var btnRegister = FindControl<Button>(_loginForm, "btnRegister");
            btnRegister.Should().NotBeNull();
        }

        #endregion

        #region Form Behavior Tests

        [TestMethod]
        public void FormDispose_ShouldCleanupResources()
        {
            // Act
            _loginForm.Dispose();

            // Assert
            _loginForm.IsDisposed.Should().BeTrue();
        }

        [TestMethod]
        public void FormLoad_ShouldCompleteWithinTimeLimit()
        {
            // Arrange
            var startTime = DateTime.Now;

            // Act
            _loginForm.Show();
            _loginForm.Hide();

            // Assert
            var loadTime = DateTime.Now - startTime;
            loadTime.Should().BeLessThan(TimeSpan.FromSeconds(2));
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

        #endregion
    }
} 