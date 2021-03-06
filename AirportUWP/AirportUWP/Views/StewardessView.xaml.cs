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
    public sealed partial class StewardessView : Page
    {
        public StewardessView()
        {
            StewardessViewModel = new StewardessViewModel();
            this.InitializeComponent();
        }
        public StewardessViewModel StewardessViewModel { get; set; }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await StewardessViewModel.UpdateListAsync();
        }

        private void stewardessList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Stewardess selected = (Stewardess)Stewardess.SelectedItem;
            splitView.DataContext = selected;
            splitView.IsPaneOpen = !splitView.IsPaneOpen;
        }

        private async void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            if (splitView.DataContext != null)
            {
                var type = splitView.DataContext as Stewardess;
                await StewardessViewModel.DeleteAsync(type.id);
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
            var ob = new Stewardess();
            ob.firstName = FirstName1.Text;
            ob.lastName = LastName1.Text;
            DateTime value1;
            int value;
            if (DateTime.TryParse(Dob1.Text, out value1))
                ob.dob = value1;
            else return;
            if (int.TryParse(CrewId1.Text, out value))
                ob.crewId = value;
            else return;
            await StewardessViewModel.AddAsync(ob);
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
            FirstName.IsReadOnly = false;
            LastName.IsReadOnly = false;
            Dob.IsReadOnly = false;
            CrewId.IsReadOnly = false;
        }

        private async void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            if (splitView.DataContext != null)
            {
                var ob = splitView.DataContext as Stewardess;
                ob.firstName = FirstName.Text;
                ob.lastName = LastName.Text;
                DateTime value1;
                int value;
                if (DateTime.TryParse(Dob.Text, out value1))
                    ob.dob = value1;
                if (int.TryParse(CrewId.Text, out value))
                    ob.crewId = value;
                await StewardessViewModel.UpdateAsync(ob);
            }
            ButtonSave.Visibility = Visibility.Collapsed;
            ButtonCancel.Visibility = Visibility.Collapsed;
            FirstName.IsReadOnly = true;
            LastName.IsReadOnly = true;
            Dob.IsReadOnly = true;
            CrewId.IsReadOnly = true;
            ButtonEdit.IsEnabled = true;
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            ButtonSave.Visibility = Visibility.Collapsed;
            ButtonCancel.Visibility = Visibility.Collapsed;
            FirstName.IsReadOnly = true;
            LastName.IsReadOnly = true;
            Dob.IsReadOnly = true;
            CrewId.IsReadOnly = true;
            ButtonEdit.IsEnabled = true;
        }
    }
}
