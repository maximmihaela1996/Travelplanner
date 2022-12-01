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
using Travelpal.Enums;
using Travelpal.Interfaces;
using Travelpal.Managers;
using Travelpal.Models;

namespace Travelpal
{
    public partial class AddTravelWindow : Window
    {
        //Create a new instans of object User
        private User SignedIn;
        //Create the instans of the userManager
        private UserManager userManager;
        //Create the instans of the travelManager
        private TravelManager travelManager;
        public AddTravelWindow(UserManager userManager, TravelManager travelManager, User SignedIn)
        {
            InitializeComponent();
            //Initialize the object User with the user that is currently signed in
            this.SignedIn = SignedIn;
            //Initialize userManager and travelManager
            this.userManager = userManager;
            this.travelManager = travelManager;

            //Take Countries List from the class Enum
            cbCountry.ItemsSource = Enum.GetValues(typeof(Countries));
            //Take the TravelType List from the userManager
            cbTravelType.ItemsSource = travelManager.TravelTypes;
            cbTripType.ItemsSource = Enum.GetValues(typeof(TripTypes));
        }
        //  Method that determines which type of trip the user chooses (Trip, Vacation)
        private string FindOutTravelType()
        {
            if (cbTravelType.SelectedItem != null)
            {
                if (cbTravelType.SelectedItem == "Trip")
                {
                    //If an element of the combo-box TravelTryp is selected -> return true
                    return "Trip";
                }
                else if (cbTravelType.SelectedItem == "Vacation")
                {
                    //If an element of the combo-box Vacation is selected -> return true
                    return "Vacation";
                }
            }
            //If no elements of the combo-box are selected -> return false
            return null;
        }
        // The method that changes the visibility of the combo-boxes depending on the user's choice
        private void DesableEnableCb(string travelType)
        {
            //When the user chooses a Trip type trip --> AllInclusive is deactivated and cb && lb Trip shows
            if (travelType == "Trip")
            {
                cbAllInclusive.Visibility = Visibility.Hidden;
                lbTripType.Visibility = Visibility.Visible;
                cbTripType.Visibility = Visibility.Visible;
                cbTripType.ItemsSource = Enum.GetValues(typeof(TripTypes));
            }
            else if (travelType == "Vacation")
            {
                lbTripType.Visibility = Visibility.Hidden;
                cbTripType.Visibility = Visibility.Hidden;
                cbAllInclusive.Visibility = Visibility.Visible;
            }
        }
        // Bool method that verify if all fields are filled 
        private bool VerifyTheInputs()
        {
            if (!string.IsNullOrEmpty(txtDestination.Text) && !string.IsNullOrEmpty(txtTravellersNo.Text) && cbTravelType.SelectedItem != null && cbCountry.SelectedItem != null)
            {
                if (cbTravelType.SelectedItem != null)
                { //If an element of the combo-box (Trip or vacation) is selected -> return true
                    if (FindOutTravelType() == "Trip")
                    {return true; }
                    
                    if (FindOutTravelType() == "Vacation")
                    {return true;}
                }else
                {//If no elements of the combo-box are selected -> return false
                    MessageBox.Show("Please choose a trip type!");
                }
            }
            return false;
        }
        private void btnAddTravel_Click(object sender, RoutedEventArgs e)
        {
            //Verify if the Method VerifyTheInputs meet the requirements
            if (VerifyTheInputs())
                {
                //If the travel is of the type Trip
                if (FindOutTravelType() == "Trip")
                    {
                    //Create a new object of type Trip 
                        Trip newTrip = new(txtDestination.Text, (Countries)cbCountry.SelectedItem, Convert.ToInt32(txtTravellersNo.Text), (TripTypes)cbTripType.SelectedItem, SignedIn);
                       //Send the new trip to the method AddNewTravel(from TravelManager) 
                        travelManager.AddNewTravel(newTrip);
                    //SignedIn.travels.Add(newTrip);

                }//If the travel is of the type Trip
                else if (FindOutTravelType() == "Vacation")
                    {  
                    //Create an empty object of type Vacation
                        Vacation newVacation;
                        //Verify if the the cb AllInclusive is checked
                        if (cbAllInclusive.IsChecked == true)
                        {
                            //If yes, create a new object Vacation with the bool isAllInclusive  true;
                            newVacation = new(txtDestination.Text, (Countries)cbCountry.SelectedItem, Convert.ToInt32(txtTravellersNo.Text), true, SignedIn);
                            //Send the new trip to the method AddNewTravel(from TravelManager)     
                            travelManager.AddNewTravel(newVacation);
                        }else
                        {
                        //If yes, create a new object Vacation with the bool isAllInclusive  true;
                            newVacation = new(txtDestination.Text, (Countries)cbCountry.SelectedItem, Convert.ToInt32(txtTravellersNo.Text), false, SignedIn);
                            travelManager.AddNewTravel(newVacation);
                        }
                    }
                    MessageBox.Show("Congratulations! Your travel has been successfully added!!");
                    TravelsWindow travelsWindow = new(userManager, travelManager, (User)SignedIn);
                    this.Close();
                    travelsWindow.ShowDialog();   
            }

            // If the Method VerifyTheInputs doesn't meet the requirements -> Message
            else if (!VerifyTheInputs())
            {
                MessageBox.Show("All fields must be filled!");
            }
        }
        private void cbTravelType_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            cbTripType.Visibility = Visibility.Visible;
            //Call the method to check if the cbTravel was selected and desable/enable the others cb
            DesableEnableCb(FindOutTravelType());
        }
        private void cbTripType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            TravelsWindow travelsWindow = new(userManager, travelManager, SignedIn);
            this.Close();
            travelsWindow.ShowDialog();
        }
    }
}
