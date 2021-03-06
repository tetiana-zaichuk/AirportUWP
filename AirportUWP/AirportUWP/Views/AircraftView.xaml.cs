﻿using System;
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
    public sealed partial class AircraftView : Page
    {
        public AircraftView()
        {
            AircraftViewModel = new AircraftViewModel();
            this.InitializeComponent();
        }
        public AircraftViewModel AircraftViewModel { get; set; }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await AircraftViewModel.UpdateListAsync();
        }

        private void aircraftList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Aircraft selected = (Aircraft)Aircraft.SelectedItem;
            splitView.DataContext = selected;
            splitView.IsPaneOpen = !splitView.IsPaneOpen;
        }

        private async void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            if (splitView.DataContext != null)
            {
                var type = splitView.DataContext as Aircraft;
                await AircraftViewModel.DeleteAsync(type.id);
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
            var ob = new Aircraft();
            ob.aircraftName = AircraftName1.Text;
            DateTime value;
            TimeSpan value2;
            if (DateTime.TryParse(AircraftReleaseDate1.Text, out value))
                ob.aircraftReleaseDate = value;
            else return;
            if (TimeSpan.TryParse(ExploitationTimeSpan1.Text, out value2))
                ob.exploitationTimeSpan = value2;
            else return;
            await AircraftViewModel.AddAsync(ob);
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
            AircraftName.IsReadOnly = false;
            AircraftReleaseDate.IsReadOnly = false;
            ExploitationTimeSpan.IsReadOnly = false;
        }

        private async void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            if (splitView.DataContext != null)
            {
                var ob = splitView.DataContext as Aircraft;
                ob.aircraftName = AircraftName.Text;
                DateTime value;
                TimeSpan value2;
                if (DateTime.TryParse(AircraftReleaseDate.Text, out value))
                    ob.aircraftReleaseDate = value;
                if (TimeSpan.TryParse(ExploitationTimeSpan.Text, out value2))
                    ob.exploitationTimeSpan = value2;
                await AircraftViewModel.UpdateAsync(ob);
            }
            ButtonSave.Visibility = Visibility.Collapsed;
            ButtonCancel.Visibility = Visibility.Collapsed;
            AircraftName.IsReadOnly = true;
            AircraftReleaseDate.IsReadOnly = true;
            ExploitationTimeSpan.IsReadOnly = true;
            ButtonEdit.IsEnabled = true;
        }

        private async void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            ButtonSave.Visibility = Visibility.Collapsed;
            ButtonCancel.Visibility = Visibility.Collapsed;
            AircraftName.IsReadOnly = true;
            AircraftReleaseDate.IsReadOnly = true;
            ExploitationTimeSpan.IsReadOnly = true;
            ButtonEdit.IsEnabled = true;
        }

    }
}
