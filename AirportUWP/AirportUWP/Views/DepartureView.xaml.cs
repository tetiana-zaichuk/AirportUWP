﻿using System;
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
    public sealed partial class DepartureView : Page
    {
        public DepartureView()
        {
            DepartureViewModel = new DepartureViewModel();
            this.InitializeComponent();
        }
        public DepartureViewModel DepartureViewModel { get; set; }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await DepartureViewModel.UpdateListAsync();
        }

        private void departureList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Departure selected = (Departure)Departure.SelectedItem;
            splitView.DataContext = selected;
            splitView.IsPaneOpen = !splitView.IsPaneOpen;
        }

        private async void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            if (splitView.DataContext != null)
            {
                var type = splitView.DataContext as Departure;
                await DepartureViewModel.DeleteAsync(type.id);
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
            /*if (splitView.DataContext != null)
            {
                var ob = splitView.DataContext as Departure;
                ob.aircraftModel = AircraftModel.Text;
                int value;
                if (int.TryParse(SeatsNumber.Text, out value))
                    ob.seatsNumber = value;
                if (int.TryParse(Carrying.Text, out value))
                    ob.carrying = value;
                await DepartureViewModel.UpdateAsync(ob);
            }
            ButtonSave.Visibility = Visibility.Collapsed;
            ButtonCancel.Visibility = Visibility.Collapsed;
            ButtonEdit.IsEnabled = true;*/
        }

        private async void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            ButtonSave.Visibility = Visibility.Collapsed;
            ButtonCancel.Visibility = Visibility.Collapsed;
            ButtonEdit.IsEnabled = true;
        }
    }
}
