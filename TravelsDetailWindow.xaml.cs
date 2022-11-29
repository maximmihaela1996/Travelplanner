
using System.Windows;
using Travelpal.Interfaces;
using Travelpal.Managers;
using Travelpal.Models;

namespace Travelpal
{

    public partial class TravelsDetailWindow : Window
    {
        Travel selectedTravel;
        private TravelManager travelManager;
        IUser SignedIn;

        public TravelsDetailWindow(Travel selectedTravel, TravelManager travelManager, IUser SignedIn)
        {
            InitializeComponent();
            this.travelManager= travelManager;
            this.selectedTravel= selectedTravel;
            this.SignedIn = SignedIn;

            if (SignedIn is Admin)
            {
                LoadInfoTravel();
                EnableTextboxes();
            }

        }

        public void LoadInfoTravel()
        {
            txtDestination.Text = selectedTravel.Destination;
            txtCountry.Text = selectedTravel.Country.ToString();
            txtTravellersNo.Text = selectedTravel.Travellers.ToString();
            txtTravelType.Text = GetTravelType();
        }
        public string GetTravelType()
        {
            string travelType;

            if (selectedTravel is Vacation)
            {
                travelType = "Vacation";
                lbAllInclusive.Visibility = Visibility.Visible;
                txtAllInclusive.Visibility = Visibility.Visible;
                if (((Vacation)selectedTravel).AllInclusive == true)
                {
                    txtAllInclusive.Text = "All Inclusive";
                }
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
        public void EnableTextboxes()
        {
            txtDestination.IsEnabled = false;
            txtCountry.IsEnabled = false;
            txtTravellersNo.IsEnabled = false;
            txtTravelType.IsEnabled = false;
            txtTripType .IsEnabled = false;
        }
    }
}
