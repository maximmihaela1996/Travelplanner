﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography.Xml;
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
using Travelpal.Interfaces;
using Travelpal.Managers;
using Travelpal.Models;

namespace Travelpal
{
    public partial class TravelsWindow : Window
    {
        //declare new instans av objects userManager, travelmanager, IUser, user, admin
        //and a bool that vill check if the User is logged in as User or admin
        private UserManager userManager;
        private TravelManager travelManager;
        private IUser SignedIn;
        private User userLogged;
        private Admin adminLogged;
        private bool loggedAsUser;
        public TravelsWindow(UserManager userManager, TravelManager travelManager, IUser SignedInAsUserOrAdmin)
        {
            InitializeComponent();
            //Initialize userManager, travelManager and the user
            this.userManager = userManager;
            this.SignedIn = SignedInAsUserOrAdmin;
            this.travelManager = travelManager = new(userManager, SignedIn);
           
            //If the user who is logged in is of the User type
            if (SignedInAsUserOrAdmin is User)
            {
                //Define it as User and assign the value to the object user (this.user)
                this.userLogged = SignedInAsUserOrAdmin as User;
                //boolean become true -> is signed in as a User
                loggedAsUser = true;
                //set the label name as users name
                lblUserName.Content = userLogged.Username;
                //Hide the button TravelDetails (only the admin can access the travels details)
                btnTravelDetails.Visibility = Visibility.Hidden;
                //populate the listView with the user's travels
                LoadTravelList();
            }
            //The same for Admin
             else if (SignedInAsUserOrAdmin is Admin)
             {
                this.adminLogged = SignedInAsUserOrAdmin as Admin;
                loggedAsUser = false;
                adminLogged = SignedInAsUserOrAdmin as Admin;
                lblUserName.Content = adminLogged.Username;
                //Hide AddTravel and UserDetails (only the user can access them)
                btnAddTravel.IsEnabled = false; 
                btnUserDetails.IsEnabled = false;
                LoadTravelList();
            }
        }     
        //Method that goes througt the list of Travels (in the travelManager)
        private void LoadTravelList()
        {
            //lvTravels.Items.Clear();
            //Check the user is Admin
            if (!loggedAsUser)
            {
                //If the list Travels have some travels in
                if(UserManager.UsersList != null)
                {
                    foreach (var user in UserManager.UsersList)
                    {
                        if (user.GetType() == typeof(User))
                        {
                            //Go through the list
                            foreach (Travel travel in ((User)user).travels)
                            {
                                //Create a new listView and add all the travels into it
                                ListViewItem listViewTravels = new();
                                listViewTravels.Tag = travel;
                                //Take the travel info with the method GetInfo (travel Class)
                                listViewTravels.Content = travel.GetInfo();
                                //Display/Add the travels in the ListView
                                lvTravels.Items.Add(listViewTravels);
                            }
                        }
                    }
                    //If there are no travelers in the list, displays a message
                }

                else { lvTravels.Items.Add("Oops! No trips found"); }
            }

            //The same for User
            else if (loggedAsUser)
                if (userLogged.travels.Count() != 0)
                {
                    foreach (Travel travel in userLogged.travels)
                    {
                        ListViewItem listViewItem = new();
                        listViewItem.Tag = travel;
                        listViewItem.Content = travel.GetInfo();
                        lvTravels.Items.Add(listViewItem);
                    }
                }
                else { lvTravels.Items.Add("Oops! No trips found"); }      
        }
        private void btnCloseWindow_Click(object sender, RoutedEventArgs e)
        {
            //Close the current window and open the Main window
            this.Close();
            MainWindow main = new();
            main.Show();
        }
        private void btnTravelDetails_Click(object sender, RoutedEventArgs e)
        {
            if (lvTravels.SelectedItem != null)
            {
                btnTravelDetails.IsEnabled = true;
                ListViewItem selectedItem = lvTravels.SelectedItem as ListViewItem;
                TravelsDetailWindow travelDetailsWindow = new(selectedItem.Tag as Travel, travelManager, SignedIn);
                travelDetailsWindow.ShowDialog();
            }
            else
            {
                MessageBox.Show("To be able to see the trip details, you must select a trip first ");
            }
        }
        private void btnAddTravel_Click(object sender, RoutedEventArgs e)
        {
            AddTravelWindow addTravelWindow = new(userManager, travelManager, (User)SignedIn);
            addTravelWindow.Show();
            this.Close();
        }
        private void btnDeleteTravel_Click(object sender, RoutedEventArgs e)
        {
            if (lvTravels.SelectedItem != null)
            {
                if (SignedIn is Admin)
                {

                    ListViewItem selectedItem = lvTravels.SelectedItem as ListViewItem;
                    Travel selectedTravel = lvTravels.Tag as Travel;
                    if (travelManager.DeleteTravel(selectedTravel)) 
                    {
                        MessageBox.Show("The travel was deleted succesfully!");
                    }
                    else
                    {
                        MessageBox.Show("Something went wrong!");
                    }
                      
                }
                else if (SignedIn is User)
                {
                    ListViewItem selectedItem = lvTravels.SelectedItem as ListViewItem;
                    Travel selectedTravel = lvTravels.Tag as Travel;
                    userManager.DeleteTravel(selectedTravel);
                }
            }
            {
                MessageBox.Show("To be able to delete a travel, you must select a travel first ");
            }
        }
        private void btnUserDetails_Click(object sender, RoutedEventArgs e)
        {
            UserDetailsWindow userDetails = new(userManager, SignedIn);
            userDetails.Show();
        }
        private void lvTravels_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
