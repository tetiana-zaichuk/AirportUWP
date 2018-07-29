﻿using System;
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
        //public ObservableCollection<AircraftType> AircraftTypes { get; set; }

        public AircraftTypeView()
        {
            AircraftTypeViewModel = new AircraftTypeViewModel();
            this.InitializeComponent();
            /*AircraftTypes = new ObservableCollection<AircraftType>()
            {
                new AircraftType(){ aircraftModel = "Tupolev Tu-134", seatsNumber = 80, carrying = 47000},
                new AircraftType(){ aircraftModel = "Tupolev Tu-204", seatsNumber = 196, carrying = 107900},
                new AircraftType(){ aircraftModel = "Ilyushin IL-62", seatsNumber = 138, carrying = 280300}
            };*/
        }
        public AircraftTypeViewModel AircraftTypeViewModel { get; set; }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await AircraftTypeViewModel.UpdateListAsync();
        }

        private void aircraftTypesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AircraftType selected = (AircraftType)AircraftType.SelectedItem;
            //await new Windows.UI.Popups.MessageDialog($"Selected {selected.aircraftModel}").ShowAsync();
            splitView.DataContext = selected;
            splitView.IsPaneOpen = !splitView.IsPaneOpen;
        }

        private async void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            if (splitView.DataContext != null)
            {
                var r=splitView.DataContext as AircraftType;
                await AircraftTypeViewModel.DeleteAsync(r.id);
            }

            ButtonDelete.Content = "Clicked!";
        }

        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            ButtonEdit.Content = "Clickedd!";
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

