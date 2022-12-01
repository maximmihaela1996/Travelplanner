
using System.Windows;
using Travelpal.Interfaces;
using Travelpal.Managers;
using Travelpal.Models;

namespace Travelpal
{
    public partial class TravelsDetailWindow : Window
    {
        //Travel, travelManager and SignedIn declaration
        Travel selectedTravel;
        private TravelManager travelManager;
        IUser SignedIn;

        public TravelsDetailWindow(Travel selectedTravel, TravelManager travelManager, IUser SignedIn)
        {
            InitializeComponent();
            this.travelManager= travelManager;
            this.selectedTravel= selectedTravel;
            this.SignedIn = SignedIn;

            //Check if it is a user or admin then load the infodetails for the travel in inputs
            if (SignedIn is Admin)
            {
                LoadInfoTravel();
                EnableTextboxes();
            }
            if(SignedIn is User)
            {
                LoadInfoTravel();
                EnableTextboxes();
            }
        }
        //Method that loading travels info
        public void LoadInfoTravel()
        {
            txtDestination.Text = selectedTravel.Destination;
            txtCountry.Text = selectedTravel.Country.ToString();
            txtTravellersNo.Text = selectedTravel.Travellers.ToString();
            //Call the method that check which type traveled has 
            txtTravelType.Text = GetTravelType();

        }
        // Method that returns what kind of travel it is (Trip, Vacation)
        public string GetTravelType()
        {
            string travelType;

            if (selectedTravel is Vacation)
            {
                travelType = "Vacation";
                lbAllInclusive.Visibility = Visibility.Visible;
                txtAllInclusive.Visibility = Visibility.Visible;
                txtAllInclusive.Text = VerifyIfAllInclusive();
                return travelType;

            }else if(selectedTravel is Trip)
            {
                travelType = "Trip";
                lbTripType.Visibility = Visibility.Visible;
                txtTripType.Visibility = Visibility.Visible;
                txtTripType.Text = ((Trip)selectedTravel).TripType.ToString();

                return travelType;
            }
            return null;
        }

        public string VerifyIfAllInclusive() 
        {
            //Check if allInclusive is checked
            if (((Vacation)selectedTravel).AllInclusive == true)
            {
                string isAllInclusive;
                //Set the check box value into the inputs
                isAllInclusive = "All Inclusive";
                return isAllInclusive;
            }
            else { return txtAllInclusive.Text = "Standard";  }
        }

        public void EnableTextboxes()
        {
            txtDestination.IsEnabled = false;
            txtCountry.IsEnabled = false;
            txtTravellersNo.IsEnabled = false;
            txtTravelType.IsEnabled = false;
            txtTripType .IsEnabled = false;
            txtAllInclusive.IsEnabled = false;
        }
    }
}
