using System;
using System.Windows;
using Travelpal.Enums;
using Travelpal.Interfaces;
using Travelpal.Managers;
using Travelpal.Models;

namespace Travelpal
{
    public partial class UserDetailsWindow : Window
    {
        private TravelManager travelManager;
        private UserManager userManager;
        private User SignedIn;

        private string newPassword;
        private string newUsername;
        public UserDetailsWindow(UserManager userManager, TravelManager travelManager, IUser SignedIn)
        {
            InitializeComponent();
            this.travelManager = travelManager;
            this.userManager = userManager;
            this.SignedIn = SignedIn as User;
            //Calling the method that loads the user's data into inputs
            SetUserDetails();
        }
        //The method that takes the user's info and sets them in the inputs
        private void SetUserDetails()
        {
            txtUsername.Text = SignedIn.Username;
            cbCountry.ItemsSource = Enum.GetValues(typeof(Countries));
            cbCountry.Text = SignedIn.Location.ToString();
        }
        //The method that checks if the inputs are correctly completed
        private bool ValidateInputsUpdateUser()
            {
                newUsername = txtUsername.Text;
                newPassword = txtNewPassword.Password;
                string confirmNewPassword = txtConfirmNewPassword.Password;

            if (userManager.ValidateUserExisting(newUsername, newPassword))
                {
                   
                    string verifyOldPassword = txtVerifyOldPassword.Password;
                //Check if the user wrote the old password correctly
                    if (verifyOldPassword == SignedIn.Password)
                    {
                        if (newPassword == confirmNewPassword)
                        {
                            if (userManager.ValidateNewUser(newUsername, newPassword)) //Call the method that verify user and pass length
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
            //Verify the condition above
          if (!ValidateInputsUpdateUser())
            {
                MessageBox.Show("Please make sure that you entered the old password correctly and that the new password and the password confirmation meets the condition (it must be longer than 5 characters)");
            }
            if (ValidateInputsUpdateUser())
            {
                //If it is true, assign the new updates to the current user and sends the user to the update method 
                SignedIn.Password = newPassword;
                SignedIn.Username = newUsername;
                SignedIn.Location = (Countries)cbCountry.SelectedItem;

                if (userManager.UpdateUser(SignedIn)) // Send an updated obiect to the Method UpdateUser
                {
                    MessageBox.Show("The user was updated succesfully!");
                    MainWindow mainWindow = new();
                    this.Close();
                    mainWindow.ShowDialog();
                } else
                {
                    MessageBox.Show("The update could not be executed");
                }
            }
        }
        private void btnCloseWindow_Click(object sender, RoutedEventArgs e)
        {
            TravelsWindow travelsWindow = new( userManager, travelManager, SignedIn);
            travelsWindow.Show();
            this.Close();
        }
    }
}
