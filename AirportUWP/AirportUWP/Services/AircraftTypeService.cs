using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AirportUWP.Models;
using Newtonsoft.Json;

namespace AirportUWP.Services
{
    public class AircraftTypeService
    {
        private readonly HttpClient _client;
        private readonly Uri _uri = new Uri("http://localhost:38236/api/AircraftsTypes/");
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<AircraftType> Items { get; set; } = new ObservableCollection<AircraftType>();

        public AircraftTypeService()
        {
            if (_client == null)
            {
                _client = new HttpClient();
                _client.Timeout = TimeSpan.FromMinutes(10);
            }
        }
        public AircraftType selectedItem;

        public AircraftType SelectedItem
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
        
        public async Task<ObservableCollection<AircraftType>> GetAsync()
        {
            var response = await _client.GetStringAsync(_uri).ConfigureAwait(false);
            return await Task.Run(() => JsonConvert.DeserializeObject<ObservableCollection<AircraftType>>(response));
        }

        public async Task DeleteByIdAsync(int id)
        {
            await _client.DeleteAsync(new Uri("" + _uri + id));
        }
        
        public async Task PostAsync(AircraftType type)
        {
            var json = new StringContent(JsonConvert.SerializeObject(type), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(_uri, json);
        }

        public async Task UpdateAsync(AircraftType type)
        {
            var id = type.id;
            var json = new StringContent(JsonConvert.SerializeObject(type), Encoding.UTF8, "application/json");
            var response = await _client.PutAsync(new Uri("" + _uri + id), json);
        }
    }
}
