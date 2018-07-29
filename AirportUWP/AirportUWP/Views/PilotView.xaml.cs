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
    public sealed partial class PilotView : Page
    {
        public PilotView()
        {
            PilotViewModel = new PilotViewModel();
            this.InitializeComponent();
        }
        public PilotViewModel PilotViewModel { get; set; }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await PilotViewModel.UpdateListAsync();
        }

        private void pilotList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Pilot selected = (Pilot)Pilot.SelectedItem;
            splitView.DataContext = selected;
            splitView.IsPaneOpen = !splitView.IsPaneOpen;
        }

        private async void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            if (splitView.DataContext != null)
            {
                var type = splitView.DataContext as Pilot;
                await PilotViewModel.DeleteAsync(type.id);
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
                var ob = splitView.DataContext as Pilot;
                ob.firstName = FirstName.Text;
                ob.lastName = LastName.Text;
                DateTime value1;
                int value;
                if (DateTime.TryParse(Dob.Text, out value1))
                    ob.dob = value1;
                if (int.TryParse(CrewId.Text, out value))
                    ob.crewId = value;
                if (int.TryParse(Experience.Text, out value))
                    ob.experience = value;
                await PilotViewModel.UpdateAsync(ob);
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
