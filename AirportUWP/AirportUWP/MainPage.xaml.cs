using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using AirportUWP.Models;
using AirportUWP.Views;
using Newtonsoft.Json;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace AirportUWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            MenuFrame.Navigate(typeof(AircraftTypeView /*FlightView*/));
            TitleTextBlock.Text = "Flights";
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (/*FlightView*/AircraftTypeView.IsSelected)
            {
                MenuFrame.Navigate(typeof(AircraftTypeView /*FlightView*/));
                TitleTextBlock.Text = "Flights";
            }
            else if (AircraftView.IsSelected)
            {
                MenuFrame.Navigate(typeof(AircraftView));
                TitleTextBlock.Text = "Aircrafts";
            }
            else if (AircraftTypeView.IsSelected)
            {
                MenuFrame.Navigate(typeof(AircraftTypeView));
                TitleTextBlock.Text = "Aircraft types";
            }
            else if (DepartureView.IsSelected)
            {
                MenuFrame.Navigate(typeof(DepartureView));
                TitleTextBlock.Text = "Departures";
            }
            else if (CrewView.IsSelected)
            {
                MenuFrame.Navigate(typeof(CrewView));
                TitleTextBlock.Text = "Crews";
            }
            else if (PilotView.IsSelected)
            {
                MenuFrame.Navigate(typeof(PilotView));
                TitleTextBlock.Text = "Pilots";
            }
            else if (StewardessView.IsSelected)
            {
                MenuFrame.Navigate(typeof(StewardessView));
                TitleTextBlock.Text = "Stewardesses";
            }
            else if (TicketView.IsSelected)
            {
                MenuFrame.Navigate(typeof(TicketView));
                TitleTextBlock.Text = "Tickets";
            }
        }

        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            mySplitView.IsPaneOpen = !mySplitView.IsPaneOpen;
        }

        /*private void Forward_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AircraftTypeView));
        }*/
    }
}
