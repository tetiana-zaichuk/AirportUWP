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
    public sealed partial class TicketView : Page
    {
        public TicketView()
        {
            TicketViewModel = new TicketViewModel();
            this.InitializeComponent();
        }
        public TicketViewModel TicketViewModel { get; set; }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await TicketViewModel.UpdateListAsync();
        }

        private void ticketList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Ticket selected = (Ticket)Ticket.SelectedItem;
            splitView.DataContext = selected;
            splitView.IsPaneOpen = !splitView.IsPaneOpen;
        }

        private async void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            if (splitView.DataContext != null)
            {
                var type = splitView.DataContext as Ticket;
                await TicketViewModel.DeleteAsync(type.id);
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
            var ob = new Ticket();
            decimal value1;
            int value;
            if (decimal.TryParse(price1.Text, out value1))
                ob.price = value1;
            else return;
            if (int.TryParse(flightId1.Text, out value))
                ob.flightId = value;
            else return;
            await TicketViewModel.AddAsync(ob);
            AddForm.Visibility = Visibility.Collapsed;
            ButtonAdd.IsEnabled = true;
        }

        private async void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            ButtonEdit.IsEnabled = false;
            ButtonSave.Visibility = Visibility.Visible;
            ButtonCancel.Visibility = Visibility.Visible;
            price.IsReadOnly = false;
            flightId.IsReadOnly = false;
        }

        private async void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            if (splitView.DataContext != null)
            {
                var ob = splitView.DataContext as Ticket;
                decimal value1;
                int value;
                if (decimal.TryParse(price.Text, out value1))
                    ob.price = value1;
                if (int.TryParse(flightId.Text, out value))
                    ob.flightId = value;
                await TicketViewModel.UpdateAsync(ob);
            }
            ButtonSave.Visibility = Visibility.Collapsed;
            ButtonCancel.Visibility = Visibility.Collapsed;
            price.IsReadOnly = true;
            flightId.IsReadOnly = true;
            ButtonEdit.IsEnabled = true;
        }

        private async void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            ButtonSave.Visibility = Visibility.Collapsed;
            ButtonCancel.Visibility = Visibility.Collapsed;
            price.IsReadOnly = true;
            flightId.IsReadOnly = true;
            ButtonEdit.IsEnabled = true;
        }
    }
}
