using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
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
using AirportUWP.ViewModels;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace AirportUWP.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AircraftTypeView : Page
    {
        public ObservableCollection<AircraftType> AircraftTypes { get; set; }
        public AircraftTypeViewModel AircraftTypeViewModel;

        public AircraftTypeView()
        {
            this.InitializeComponent();
            AircraftTypeViewModel = new AircraftTypeViewModel();
            
        /*AircraftTypes = new ObservableCollection<AircraftType>()
        {
            new AircraftType(){ AircraftModel = "Tupolev Tu-134", SeatsNumber = 80, Carrying = 47000},
            new AircraftType(){ AircraftModel = "Tupolev Tu-204", SeatsNumber = 196, Carrying = 107900},
            new AircraftType(){ AircraftModel = "Ilyushin IL-62", SeatsNumber = 138, Carrying = 280300}
        };*/
    }

        private async void aircraftTypesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AircraftType selectedPhone = (AircraftType)aircraftTypes.SelectedItem;
            await new Windows.UI.Popups.MessageDialog($"Выбран {selectedPhone.AircraftModel}").ShowAsync();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null)
            {
                AircraftTypes = AircraftTypeViewModel.GetAsync().Result;
            }
        }
        //// обработчик кнопки
        //private void Button_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        //{
        //    string title = titleTextBox.Text;
        //    string company = companyTextBox.Text;
        //    // добавление нового объекта
        //    AircraftTypeViewModel.GetAsync();
        //    companyTextBox.Text = titleTextBox.Text = String.Empty;
        //}
    }
}

