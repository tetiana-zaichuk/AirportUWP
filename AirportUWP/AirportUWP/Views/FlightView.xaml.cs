using System;
using System.Collections.Generic;
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
    public sealed partial class FlightView : Page
    {
        public FlightView()
        {
            FlightViewModel = new FlightViewModel();
            this.InitializeComponent();
        }
        public FlightViewModel FlightViewModel { get; set; }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await FlightViewModel.UpdateListAsync();
        }

        private void flightList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Flight selected = (Flight)Flight.SelectedItem;
            splitView.DataContext = selected;
            splitView.IsPaneOpen = !splitView.IsPaneOpen;
        }

        private async void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            if (splitView.DataContext != null)
            {
                var type = splitView.DataContext as Flight;
                await FlightViewModel.DeleteAsync(type.id);
            }
        }
        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            ButtonAdd.IsEnabled = false;
            AddForm.Visibility = Visibility.Visible;

        }

        private void ButtonCancel1_Click(object sender, RoutedEventArgs e)
        {
            ButtonAdd.IsEnabled = true;
            AddForm.Visibility = Visibility.Collapsed;
        }

        private async void ButtonSave1_Click(object sender, RoutedEventArgs e)
        {
            var ob = new Flight();
            ob.departure = Departure1.Text;
            ob.destination = Destination1.Text;
            DateTime value;
            if (DateTime.TryParse(DepartureTime1.Text, out value))
                ob.departureTime = value;
            else return;
            if (DateTime.TryParse(ArrivalTime1.Text, out value))
                ob.arrivalTime = value;
            else return;
            await FlightViewModel.AddAsync(ob);
            AddForm.Visibility = Visibility.Collapsed;
            ButtonAdd.IsEnabled = true;
        }

        private async void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            TextBox t = new TextBox();
            t.IsReadOnly = false;
            ButtonEdit.IsEnabled = false;
            ButtonSave.Visibility = Visibility.Visible;
            ButtonCancel.Visibility = Visibility.Visible;
        }

        private async void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            if (splitView.DataContext != null)
            {
                var ob = splitView.DataContext as Flight;
                ob.departure = Departure.Text;
                ob.destination = Destination.Text;
                DateTime value;
                if (DateTime.TryParse(DepartureTime.Text, out value))
                    ob.departureTime = value;
                if (DateTime.TryParse(ArrivalTime.Text, out value))
                    ob.arrivalTime = value;
                await FlightViewModel.UpdateAsync(ob);
            }
            ButtonSave.Visibility = Visibility.Collapsed;
            ButtonCancel.Visibility = Visibility.Collapsed;
            ButtonEdit.IsEnabled = true;
        }

        private async void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            ButtonSave.Visibility = Visibility.Collapsed;
            ButtonCancel.Visibility = Visibility.Collapsed;
            ButtonEdit.IsEnabled = true;
        }
    }
}
