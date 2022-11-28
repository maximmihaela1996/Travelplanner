
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using Travelpal.Interfaces;
using Travelpal.Managers;
using Travelpal.Models;
namespace Travelpal
{
    public partial class MainWindow : Window
    {
        //Create a new instance of the userManager class
        private UserManager userManager;
        //Create a new instance of the travelManager class
        private TravelManager travelManager;
        //Create a new instance of class travelWindow
        TravelsWindow travelsWindow;
        public MainWindow()
        {
            InitializeComponent();
            this.userManager = new();
        }
        public MainWindow(UserManager userManager, TravelManager travelManager)
        {
            InitializeComponent();
            //The userManager and userTravel classes is initialized
            this.userManager = userManager;
            this.travelManager = travelManager;
        }

        //Button that leads to the registration page
        private void btnRegistration_Click(object sender, RoutedEventArgs e)
        {
            //create a RegisterWindow instans
            RegisterWindow registerWindow = new(userManager);
            registerWindow.ShowDialog();
        }
        //SignIn Button which leads to the TravelWindow window if a user or admin is found
        private void btnSignUp_Click(object sender, RoutedEventArgs e)
        {

            string username = txtUsername.Text;
            string password = txtPassword.Password;

            if (txtUsername.Text != null && txtPassword != null)
            {
                //var bool that which shows or not if a user exists in the list of users 
                bool isFoundUser = userManager.SignInAsUserOrAdmin(username, password);
                //var bool that which shows if the user is a User
                bool isUser = userManager.SignInAsUser();

                if (isFoundUser)
                {
                    List<IUser> users = userManager.GetAllUsers();
                    foreach (IUser user in users)
                    {
                        travelsWindow = new(userManager, travelManager, isUser);
                        travelsWindow.Show();
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Upps! Username or password does not matching");
                    MessageBox.Show("Please fill in again or try to register you!");
                }
                txtUsername.Clear();
                txtPassword.Clear();
            }
            else
            {
                Console.WriteLine("Fill in the files first!");
            }
        }
    }
}
