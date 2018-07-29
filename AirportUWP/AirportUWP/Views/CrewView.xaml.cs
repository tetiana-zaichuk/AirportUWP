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
    public sealed partial class CrewView : Page
    {
        public CrewView()
        {
            CrewViewModel = new CrewViewModel();
            this.InitializeComponent();
        }
        public CrewViewModel CrewViewModel { get; set; }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await CrewViewModel.UpdateListAsync();
        }

        private void crewList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Crew selected = (Crew)Crew.SelectedItem;
            splitView.DataContext = selected;
            splitView.IsPaneOpen = !splitView.IsPaneOpen;
        }

        private async void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            if (splitView.DataContext != null)
            {
                var type = splitView.DataContext as Crew;
                await CrewViewModel.DeleteAsync(type.id);
            }
        }

        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            TextBox t = new TextBox();
            t.IsReadOnly = false;
            ButtonEdit.IsEnabled = false;
            ButtonSave.Visibility = Visibility.Visible;
            ButtonCancel.Visibility = Visibility.Visible;
        }

        private async void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            /*if (splitView.DataContext != null)
            {
                var ob = splitView.DataContext as Crew;
                ob.aircraftModel = AircraftModel.Text;
                int value;
                if (int.TryParse(SeatsNumber.Text, out value))
                    ob.seatsNumber = value;
                if (int.TryParse(Carrying.Text, out value))
                    ob.carrying = value;
                await CrewViewModel.UpdateAsync(ob);
            }
            ButtonSave.Visibility = Visibility.Collapsed;
            ButtonCancel.Visibility = Visibility.Collapsed;
            ButtonEdit.IsEnabled = true;*/
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            ButtonSave.Visibility = Visibility.Collapsed;
            ButtonCancel.Visibility = Visibility.Collapsed;
            ButtonEdit.IsEnabled = true;
        }
    }
}
