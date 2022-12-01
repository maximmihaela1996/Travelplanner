using System;
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
        //Declare new instans av objects userManager, travelmanager, IUser, user, admin
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
                //populate the listView with the user's travels
                LoadTravelList();
            }
            //The same for Admin
            else if (SignedInAsUserOrAdmin is Admin)
            {
                this.adminLogged = SignedInAsUserOrAdmin as Admin;
                loggedAsUser = false;
                lblUserName.Content = adminLogged.Username;
                //Hide AddTravel and UserDetails (only the user can access them)
                btnAddTravel.IsEnabled = false;
                btnUserDetails.IsEnabled = false;
                btnInfoApp.IsEnabled = false;
                LoadTravelList();
            }
        }
        //Method that goes througt the list of Travels (in the travelManager)
        private void LoadTravelList()
        {
            //Clear the list before loading it again
            lvTravels.Items.Clear();
            if (!loggedAsUser)
            {
                //If the Users list has some travels 
                if (UserManager.UsersList != null)
                {
                    //Go through the list of users
                    foreach (var user in UserManager.UsersList)
                    {
                        if (user.GetType() == typeof(User))
                        {
                            //Go through the travel list of each user
                            foreach (Travel travel in ((User)user).travels)
                            {
                                //Create a new listView and add all the travels into it
                                ListViewItem listViewTravels = new();
                                listViewTravels.Tag = travel;
                                //Take the travel info from the method GetInfo (travel Class)
                                listViewTravels.Content = travel.GetInfo();
                                //Display/Add the travels in the ListView
                                lvTravels.Items.Add(listViewTravels);
                            }
                        }
                    }
                }
                else { lvTravels.Items.Add("Oops! No travels found"); }
                //If there are no travels in the list, displays a message
            }
            else if (loggedAsUser)
            {
                if (userLogged.travels.Count() != 0)
                {
                    foreach (Travel travel in userLogged.travels)
                    {
                        ListViewItem listViewItem = new();
                        listViewItem.Tag = travel;
                        listViewItem.Content = travel.GetInfo();
                        lvTravels.Items.Add(listViewItem);
                    }
                    //If there are no travels in the list, displays a message
                }else { lvTravels.Items.Add("Oops! No travel found"); } 
            }
        }
        private void btnCloseWindow_Click(object sender, RoutedEventArgs e)
            {
                //Close the current window and open the Main window
                MainWindow mainWindow = new();
                mainWindow.Show();
                this.Close();
            }
            private void btnTravelDetails_Click(object sender, RoutedEventArgs e)
            {
                //Verify if the user has selected a travel
                if (lvTravels.SelectedItem != null)
                {
                    //Make the travelDetails button accessible
                    btnTravelDetails.IsEnabled = true;
                    ListViewItem selectedItem = (ListViewItem)lvTravels.SelectedItem;
                    TravelsDetailWindow travelDetailsWindow = new((Travel)selectedItem.Tag, travelManager, SignedIn);
                    travelDetailsWindow.ShowDialog();
                }
                else
                {
                    MessageBox.Show("To be able to see travel details, you must first select a travel from the list");
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
            //Verify if the user has selected a travel
            if (lvTravels_SelectionChanged != null)
            {
                //Make the delete button accessible
                btnDeleteTravel.IsEnabled = true;
                //Verify if the user is Admin
                if (SignedIn is Admin)
                {
                    ListViewItem item = (ListViewItem)lvTravels.SelectedItem;
                    Travel selectedTravel = (Travel)item.Tag;
                    //Call the DeleteTravel Method from travelManager
                    travelManager.DeleteTravel(selectedTravel);
                    MessageBox.Show("The travel was succesfully deleted!");
                    //Load the travelList again
                    LoadTravelList();
                }
                //Same for user
                else if (SignedIn is User)
                {
                    ListViewItem item = (ListViewItem)lvTravels.SelectedItem;
                    Travel selectedTravel = (Travel)item.Tag;
                    travelManager.DeleteTravel(selectedTravel, SignedIn);
                    MessageBox.Show("The userstravel was succesfully deleted!");
                    LoadTravelList();
                }
            }else
            {
                //Message if no travel is selected
                MessageBox.Show("Please click first on the travel you want to remove");
            }
        }
        private void btnUserDetails_Click(object sender, RoutedEventArgs e)
        {
            UserDetailsWindow userDetails = new(userManager, travelManager, SignedIn);
            userDetails.Show();
        }
        private void lvTravels_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnInfoApp_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(" Warning! " + "\n" +
                  "1. Make sure you fill in all the information in the fields!" + "\n" +
                  "2. Make sure that your username is not less than 3 characters and your password less than 5!" + "\n" +
                  "3. Select first from the list if you want to delete a trip!" + "\n" +
                  "4. Plan with us your unforgettable trip !", "Info");
        }

        private void btnAboutAs_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("With the travelPal application you can plan your trips efficiently, save time and energy!" + "\n \n" + "Let's go!", "Info");
        }
    }
}
