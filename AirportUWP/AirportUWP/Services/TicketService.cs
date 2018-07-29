﻿using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AirportUWP.Models;
using Newtonsoft.Json;

namespace AirportUWP.Services
{
    public class TicketService
    {
        private readonly HttpClient _client;
        private readonly Uri _uri = new Uri("http://localhost:38236/api/Tickets/");
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<Ticket> Items { get; set; } = new ObservableCollection<Ticket>();

        public TicketService()
        {
            if (_client == null)
            {
                _client = new HttpClient();
            }
        }
        public Ticket selectedItem;

        public Ticket SelectedItem
        {
            get => selectedItem;
            set
            {
                selectedItem = value;
                NotifyPropertyChanged(() => SelectedItem);
            }
        }

        public void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void NotifyPropertyChanged(Expression<Func<object>> target)
        {
            if (target != null)
            {
                var body = target.Body as MemberExpression;
                if (body != null)
                {
                    NotifyPropertyChanged(body.Member.Name);
                }
            }
        }

        public async Task<ObservableCollection<Ticket>> GetAsync()
        {
            var response = await _client.GetStringAsync(_uri).ConfigureAwait(false);
            return await Task.Run(() => JsonConvert.DeserializeObject<ObservableCollection<Ticket>>(response));
        }

        public async Task DeleteByIdAsync(int id)
        {
            await _client.DeleteAsync(new Uri("" + _uri + id));
        }

        public async Task PostAsync(Ticket type)
        {
            var json = new StringContent(JsonConvert.SerializeObject(type), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(_uri, json);
        }

        public async Task UpdateAsync(Ticket type)
        {
            var id = type.id;
            var json = new StringContent(JsonConvert.SerializeObject(type), Encoding.UTF8, "application/json");
            var response = await _client.PutAsync(new Uri("" + _uri + id), json);
        }
    }
}
