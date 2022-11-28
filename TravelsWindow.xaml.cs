using System;
using System.Collections.Generic;
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
        private UserManager userManager;
        private TravelManager travelManager;
        private IUser SignedIn;
        private bool isUser;
        private User user;
        public TravelsWindow(UserManager userManager, TravelManager travelManager, bool isUser, IUser SignedIn)
        {
            InitializeComponent();
            this.travelManager = travelManager;
            this.userManager = userManager;
            this.SignedIn = SignedIn;

            lblUserName.Content =  userManager.SignedIn.Username;
        }
        // Updates the UI based on user type (user or admin)
        private void UpdateUserUI()
        {
        //If the user is signed as User --> Desable the Button TravelDetails
            if (isUser)
            {
                btnTravelDetails.Visibility = Visibility.Hidden;
            }
            else
            {
                btnAddTravel.Visibility = Visibility.Hidden;
            } 
        }

        private void LoadTravelList()
        {
            lvTravels.Items.Clear();
            if (isUser)
            {
                if (SignedIn is User)
                {
                    user = SignedIn as User;

                    foreach (Travel travel in user.UsersTravels)
                    {
                        //this.user = SignedIn as User;
                        ListViewItem listViewTravels = new();
                        listViewTravels.Content = travel.GetInfo();
                        lvTravels.Items.Add(listViewTravels);
                    }
                }
            }
            else if (!isUser)
            {
                foreach (Travel travel in travelManager.GetAllTravels())
                {
                    ListViewItem listViewItem = new();
                    listViewItem.Tag = travel;
                    listViewItem.Content = travel.GetInfo();
                    lvTravels.Items.Add(listViewItem);
                }
            }
        }

        private void btnCloseWindow_Click(object sender, RoutedEventArgs e)
        {
            //Close the current window and open the Main window
            this.Close();
            MainWindow main = new();
            main.Show();
        }
    }
}
