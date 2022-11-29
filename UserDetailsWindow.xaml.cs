using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Travelpal.Enums;
using Travelpal.Interfaces;
using Travelpal.Managers;
using Travelpal.Models;

namespace Travelpal
{
    public partial class UserDetailsWindow : Window
    {
        private UserManager userManager;
        private User SignedIn;

        private string newPassword;
        private string newUsername;
        public UserDetailsWindow(UserManager userManager, IUser SignedIn)
        {
            InitializeComponent();
            this.userManager = userManager;
            this.SignedIn = SignedIn as User;
            SetUserDetails();
        }
        private void SetUserDetails()
        {
            txtUsername.Text = SignedIn.Username;
            txtNewPassword.Password = SignedIn.Password;
            cbCountry.ItemsSource = Enum.GetValues(typeof(Countries));
            cbCountry.Text = SignedIn.Location.ToString();
        }

        public bool VerifyOldPassword()
        {
                string verifyOldPassword = txtOldPassword.Password;

                if (verifyOldPassword == SignedIn.Password)
                {
                    newPassword = txtNewPassword.Password;
                    string confirmNewPassword = txtConfirmNewPassword.Password;
                    if (newPassword == confirmNewPassword)
                    {
                        return true;
                    }
                }
            return false;
        }
            private bool ValidateInputsUpdateUser()
            {
                newUsername = txtUsername.Text;
                if (userManager.ValidateUserExisting(newUsername, newPassword))
                {
                    string verifyOldPassword = txtOldPassword.Password;

                    if (verifyOldPassword == SignedIn.Password)
                    {
                        newPassword = txtNewPassword.Password;
                        string confirmNewPassword = txtConfirmNewPassword.Password;

                        if (newPassword == confirmNewPassword)
                        {
                            if (userManager.ValidateNewUser(newUsername, newPassword))
                            {
                                return true;
                            }
                            else { return false; }//Password must have minimum 6 characters";
                        }
                        else { return false; } //"You wrote two different passwords! Please fill in the files more carefully");
                    }
                    else { return false; }//MessageBox.Show("Your old password does not match with the one in the input");
                }
            return false;
        }
        private void btnChangeUserInfo_Click(object sender, RoutedEventArgs e)
        {
          if (!ValidateInputsUpdateUser())
            {
                MessageBox.Show("Please make sure that you entered the old password correctly and that the new password and the password confirmation meets the condition (it must be longer than 5 characters)");
            }
            if (ValidateInputsUpdateUser())
            {
                SignedIn.Password = newPassword;
                SignedIn.Username = newUsername;

                // userManager.UpdateUser(SignedIn);

                if (userManager.UpdateUser(SignedIn))
                {
                    MessageBox.Show("The password was updated succesfully!");
                    MainWindow mainWindow = new();
                    mainWindow.ShowDialog();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("I could not the user");
                }

            }
        }
    }
}
