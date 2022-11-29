using System;
using System.Collections.Generic;
using System.Windows;
using Travelpal.Enums;
using Travelpal.Interfaces;
using Travelpal.Managers;
using Travelpal.Models;

namespace Travelpal
{
    public partial class RegisterWindow : Window
    {
        private UserManager userManager =  new();
        
        public RegisterWindow(UserManager userManager)
        {
            InitializeComponent();
            this.userManager = userManager;
            cbChooseCountry.ItemsSource = Enum.GetValues(typeof(Countries));
        }
        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            //Check if all the inputs are in a correct form
            if (CheckInputs())
            {
                Countries userCountry = (Countries)cbChooseCountry.SelectedItem;
                //If CheckInputs return true --> Create a new user with specific values
                User newUser = new(txtUsername.Text, txtPassword.Password, userCountry);
                //If user added succesfully, then an messageBox will appear
                if (userManager.AddNewUser(newUser))
                {
                    MessageBox.Show("Congratulations! You have successfully registered! Now you can plan your trip", "Success!", MessageBoxButton.OK, MessageBoxImage.Information);
                    //userManager.UpdateList();
                    this.Close();                }
                else
                {
                    // If the user is already taked
                    MessageBox.Show("The user it already exists", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }
        private bool CheckInputs()
        {
            if (!string.IsNullOrEmpty(txtUsername.Text) && !string.IsNullOrEmpty(txtPassword.Password) && !string.IsNullOrEmpty(txtConfirmParssword.Password) && cbChooseCountry.SelectedItem != null)
            {
                //booleans that return if the if the conditions in the method ValidateNewUser are fulfilled 
                bool newUserIsValid = userManager.ValidateNewUser(txtUsername.Text, txtPassword.Password);
                //check usersLength is correct
                if (newUserIsValid)
                {
                    //check if passwords match each other
                    if (txtPassword.Password == txtConfirmParssword.Password)
                    {
                        return true;
                    }else
                    {
                        //If the passwords do not match -->  a warning is displayed
                        MessageBox.Show("Passwords must match!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return false;
                    }
                }else
                {
                    //If the username and password dont't fullfyll the condition (u > 3 && p > 5) 
                    MessageBox.Show("The Username must be at least 3 characters and password 5 characters!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return false;
                }
            }else
                {
                    // If the user didn't fill in all the files --> a warning is displayed
                    MessageBox.Show("All the fields must be filled! Please fill them in!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return false;
                }
            }
        }
}
