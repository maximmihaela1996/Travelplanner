
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
        private TravelManager? travelManager;
        //Create a new instance of class travelWindow
        //TravelsWindow travelsWindow ;
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
            List<IUser> users = userManager.GetAllUsers();

            string username = txtUsername.Text;
            string password = txtPassword.Password;
            bool isFoundUser = false;

            if (txtUsername.Text != null && txtPassword != null)
            {
                foreach (IUser user in users)
                {
                    if (user.Username == username && user.Password == password)
                    {
                        isFoundUser = true;

                        if (isFoundUser)
                        {
                            TravelsWindow travelsWindow = new(userManager, travelManager, user);
                            travelsWindow.Show();
                            this.Close();
                        }
                    }
                }
                if (!isFoundUser)
                {
                    MessageBox.Show("Upps! Username or password does not matching!");
                }
                txtUsername.Clear();
                txtPassword.Clear();
            }else
            {
                Console.WriteLine("Complete the files first!");
            }
        }
    }
}