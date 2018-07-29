using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
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
using AirportUWP.ViewModels;
using Newtonsoft.Json;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace AirportUWP.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AircraftTypeView : Page
    {
        public ObservableCollection<AircraftType> AircraftTypes { get; set; }
        public AircraftTypeViewModel AircraftTypeViewModel { get; set; }

        public AircraftTypeView()
        {
            this.InitializeComponent();
            //AircraftTypeViewModel = new AircraftTypeViewModel();
            //AircraftTypeViewModel.GetAsync();
            AircraftTypes = new ObservableCollection<AircraftType>();
            /*AircraftTypes = new ObservableCollection<AircraftType>()
            {
                new AircraftType(){ aircraftModel = "Tupolev Tu-134", seatsNumber = 80, carrying = 47000},
                new AircraftType(){ aircraftModel = "Tupolev Tu-204", seatsNumber = 196, carrying = 107900},
                new AircraftType(){ aircraftModel = "Ilyushin IL-62", seatsNumber = 138, carrying = 280300}
            };*/
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            AircraftTypes = new ObservableCollection<AircraftType>()
            {
                new AircraftType(){ aircraftModel = "Tupolev Tu-134", seatsNumber = 80, carrying = 47000},
                new AircraftType(){ aircraftModel = "Tupolev Tu-204", seatsNumber = 196, carrying = 107900},
                new AircraftType(){ aircraftModel = "Ilyushin IL-62", seatsNumber = 138, carrying = 280300}
            };
            //Frame.Navigate(typeof(MainPage));
        }

        private async void aircraftTypesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AircraftType selectedPhone = (AircraftType)aircraftTypes.SelectedItem;
            await new Windows.UI.Popups.MessageDialog($"Selected {selectedPhone.aircraftModel}").ShowAsync();
        }

        //private void Button_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        //{
        //    string title = titleTextBox.Text;
        //    string company = companyTextBox.Text;
        //    AircraftTypeViewModel.GetAsync();
        //    companyTextBox.Text = titleTextBox.Text = String.Empty;
        //}

    }
}

