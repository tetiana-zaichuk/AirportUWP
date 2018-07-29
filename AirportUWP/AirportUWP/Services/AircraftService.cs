using System;
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
    public class AircraftService
    {
        private readonly HttpClient _client;
        private readonly Uri _uri = new Uri("http://localhost:38236/api/Aircrafts/");
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<Aircraft> Items { get; set; } = new ObservableCollection<Aircraft>();

        public AircraftService()
        {
            if (_client == null)
            {
                _client = new HttpClient();
            }
        }
        public Aircraft selectedItem;

        public Aircraft SelectedItem
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

        public async Task<ObservableCollection<Aircraft>> GetAsync()
        {
            var response = await _client.GetStringAsync(_uri).ConfigureAwait(false);
            return await Task.Run(() => JsonConvert.DeserializeObject<ObservableCollection<Aircraft>>(response));
        }

        public async Task DeleteByIdAsync(int id)
        {
            await _client.DeleteAsync(new Uri("" + _uri + id));
        }

        public async Task PostAsync(Aircraft type)
        {
            var json = new StringContent(JsonConvert.SerializeObject(type), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(_uri, json);
        }

        public async Task UpdateAsync(Aircraft type)
        {
            var id = type.id;
            var json = new StringContent(JsonConvert.SerializeObject(type), Encoding.UTF8, "application/json");
            var response = await _client.PutAsync(new Uri("" + _uri + id), json);
        }
    }
}
