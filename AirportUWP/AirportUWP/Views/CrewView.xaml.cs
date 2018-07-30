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
            var ob = new Crew();
            ob.pilot = new Pilot();
            ob.pilot.firstName = FirstName1.Text;
            ob.pilot.lastName = LastName1.Text;
            DateTime value;
            int value1;
            if (DateTime.TryParse(Dob1.Text, out value))
                ob.pilot.dob = value;
            else return;
            if (int.TryParse(Experience1.Text, out value1))
                ob.pilot.experience = value1;
            else return;
            await CrewViewModel.AddAsync(ob);
            AddForm.Visibility = Visibility.Collapsed;
            ButtonAdd.IsEnabled = true;
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
            ButtonEdit.IsEnabled = false;
            ButtonSave.Visibility = Visibility.Visible;
            ButtonCancel.Visibility = Visibility.Visible;
            PLastName.IsReadOnly = false;
            PFirstName.IsReadOnly = false;
        }

        private async void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            if (splitView.DataContext != null)
            {
                var ob = splitView.DataContext as Crew;
                ob.pilot.firstName = PFirstName.Text;
                ob.pilot.lastName = PLastName.Text;
                await CrewViewModel.UpdateAsync(ob);
            }
            ButtonSave.Visibility = Visibility.Collapsed;
            ButtonCancel.Visibility = Visibility.Collapsed;
            PLastName.IsReadOnly = true;
            PFirstName.IsReadOnly = true;
            ButtonEdit.IsEnabled = true;
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            ButtonSave.Visibility = Visibility.Collapsed;
            ButtonCancel.Visibility = Visibility.Collapsed;
            PLastName.IsReadOnly = true;
            PFirstName.IsReadOnly = true;
            ButtonEdit.IsEnabled = true;
        }
    }
}
