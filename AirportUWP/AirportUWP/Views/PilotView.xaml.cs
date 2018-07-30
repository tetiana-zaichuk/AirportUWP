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
            CrewViewModel = new CrewViewModel();
            this.InitializeComponent();
        }
        public PilotViewModel PilotViewModel { get; set; }
        public CrewViewModel CrewViewModel { get; set; }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await PilotViewModel.UpdateListAsync();
            crewList.ItemsSource = await CrewViewModel.GetAsync();
            crewList1.ItemsSource = crewList.ItemsSource;
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

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            ButtonAdd.IsEnabled = false;
            AddForm.Visibility = Visibility.Visible;
            crewList1.Visibility = Visibility.Visible;
        }

        private void ButtonCancel1_Click(object sender, RoutedEventArgs e)
        {
            ButtonAdd.IsEnabled = true;
            AddForm.Visibility = Visibility.Collapsed;
            crewList1.Visibility = Visibility.Collapsed;
        }

        private async void ButtonSave1_Click(object sender, RoutedEventArgs e)
        {
            var ob = new Pilot();
            ob.firstName = FirstName1.Text;
            ob.lastName = LastName1.Text;
            DateTime value1;
            int value;
            if (DateTime.TryParse(Dob1.Text, out value1))
                ob.dob = value1;
            else return;
            var Crew = crewList1.SelectedItem as Crew;
                if (Crew != null)
                    ob.crewId = Crew.id;
                else return;
            if (int.TryParse(Experience1.Text, out value))
                ob.experience = value;
            else return;
            await PilotViewModel.AddAsync(ob);
            AddForm.Visibility = Visibility.Collapsed;
            crewList1.Visibility = Visibility.Collapsed;
            ButtonAdd.IsEnabled = true;
        }

        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            ButtonEdit.IsEnabled = false;
            crewList.Visibility = Visibility.Visible;
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
                if (int.TryParse(Experience.Text, out value))
                    ob.experience = value;
                else return;
                var Crew = crewList.SelectedItem as Crew;
                if (Crew != null)
                    ob.crewId = Crew.id;
                else return;

                await PilotViewModel.UpdateAsync(ob);
            }
            ButtonSave.Visibility = Visibility.Collapsed;
            ButtonCancel.Visibility = Visibility.Collapsed;
            crewList.Visibility = Visibility.Collapsed;
            ButtonEdit.IsEnabled = true;
        }

        private async void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            ButtonSave.Visibility = Visibility.Collapsed;
            ButtonCancel.Visibility = Visibility.Collapsed;
            crewList.Visibility = Visibility.Collapsed;
            ButtonEdit.IsEnabled = true;
        }
    }
}
