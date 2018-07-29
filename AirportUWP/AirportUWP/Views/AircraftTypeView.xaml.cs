using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
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
        public AircraftTypeView()
        {
            AircraftTypeViewModel = new AircraftTypeViewModel();
            this.InitializeComponent();
        }
        public AircraftTypeViewModel AircraftTypeViewModel { get; set; }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await AircraftTypeViewModel.UpdateListAsync();
        }

        private void aircraftTypesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AircraftType selected = (AircraftType)AircraftType.SelectedItem;
            splitView.DataContext = selected;
            splitView.IsPaneOpen = !splitView.IsPaneOpen;
        }

        private async void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            if (splitView.DataContext != null)
            {
                var type = splitView.DataContext as AircraftType;
                await AircraftTypeViewModel.DeleteAsync(type.id);
            }
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
                var type = splitView.DataContext as AircraftType;
                type.aircraftModel = AircraftModel.Text;
                int value;
                if (int.TryParse(SeatsNumber.Text, out value))
                    type.seatsNumber = value;
                if (int.TryParse(Carrying.Text, out value))
                    type.carrying = value;
                await AircraftTypeViewModel.UpdateAsync(type);
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

