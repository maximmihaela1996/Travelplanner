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
    /// <summary>
    /// Interaction logic for AddTravelWindow.xaml
    /// </summary>
    public partial class AddTravelWindow : Window
    {
        private User SignedIn;
        private UserManager userManager;
        private TravelManager travelManager;
        public AddTravelWindow(UserManager userManager, TravelManager travelManager, IUser signedIn)
        {
            InitializeComponent();
            this.SignedIn = signedIn as User;
            this.userManager = userManager;
            this.travelManager = travelManager;

            cbCountry.ItemsSource = Enum.GetValues(typeof(Countries));
            cbTravelType.ItemsSource = travelManager.TravelTypes;
            cbTripType.ItemsSource = Enum.GetValues(typeof(TripTypes));
        }
        private string FindOutTravelType()
        {
            if (cbTravelType.SelectedItem != null)
            {
                if (cbTravelType.SelectedItem == "Trip")
                {
                    return "Trip";
                    cbTripType.ItemsSource = Enum.GetValues(typeof(TripTypes));
                }
                else if (cbTravelType.SelectedItem == "Vacation")
                {
                    return "Vacation";
                    cbAllInclusive.Visibility = Visibility.Visible;
                }
            }
            return null;
        }
        private bool ValidateInput()
        {
            if ( !string.IsNullOrEmpty(txtDestination.Text) && !string.IsNullOrEmpty(txtTravellersNo.Text) && cbTravelType.SelectedItem != null && cbCountry.SelectedItem != null)
                if (FindOutTravelType() == "Trip")
                {
                    if (cbTripType.SelectedItem != null)
                    {
                        return true;
                    }else
                    {
                        MessageBox.Show("Please choose first a trip type!");
                        return false;
                    }
                }
            else if (FindOutTravelType() == "Vacation")
            {
                    cbAllInclusive.Visibility = Visibility.Visible;
                return true;
            }else  
            {
                    MessageBox.Show("All the fields must be filled!");
            }
            return false;
        }
        private void btnAddTravel_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateInput())
            {
                if (FindOutTravelType() == "Trip")
                {
                    Trip newTrip = new(txtDestination.Text, (Countries)cbCountry.SelectedItem, Convert.ToInt32(txtTravellersNo.Text),(TripTypes)cbTripType.SelectedItem, SignedIn);
                    travelManager.AddNewTravel(newTrip);
                }else if (FindOutTravelType() == "Vacation")
                {
                    Vacation newVacation;

                    if (cbAllInclusive.IsChecked == true)
                    {
                        newVacation = new(txtDestination.Text, (Countries)cbCountry.SelectedItem, Convert.ToInt32(txtTravellersNo.Text), true, SignedIn);
                    }
                    else
                    {
                        newVacation = new(txtDestination.Text, (Countries)cbCountry.SelectedItem, Convert.ToInt32(txtTravellersNo.Text),false, SignedIn);
                    }
                    travelManager.AddNewTravel(newVacation);
                }

                MessageBox.Show("Added!");
                this.Close();
            }
        }

        private void ModifyTravelType(string travelType)
        {
            if (travelType == "Trip")
            {
                cbAllInclusive.Visibility = Visibility.Hidden;
                //txtTripType.Visibility = Visibility.Visible;
                cbTripType.Visibility = Visibility.Visible;
                cbTripType.ItemsSource = Enum.GetValues(typeof(TripTypes)); ;
            }
            else if (travelType == "Vacation")
            {
                //txtTripType.Visibility = Visibility.Hidden;
                cbTripType.Visibility = Visibility.Hidden;
                cbAllInclusive.Visibility = Visibility.Visible; ;
            }
        }

        private void cbTravelType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void cbCountry_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void cbTripType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ModifyTravelType(FindOutTravelType());
        }
    }
}
