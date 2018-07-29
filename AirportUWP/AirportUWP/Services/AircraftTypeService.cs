using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Text;
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
            }
        }
        public AircraftType selectedItem;

        public AircraftType SelectedItem
        {
            get
            {
                return selectedItem;
            }
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
        
        /*public async Task UpdateList()
        {
            var newCollection = new ObservableCollection<AircraftType>(await GetAsync());
            Items.Clear();
            foreach (var item in newCollection)
            {
                Items.Add(item);
            }
        }*/

        public async Task<ObservableCollection<AircraftType>> GetAsync()
        {
            var response = await _client.GetStringAsync(_uri).ConfigureAwait(false);
            //var responseString = await response.Content.ReadAsStringAsync();
            return await Task.Run(() => JsonConvert.DeserializeObject<ObservableCollection<AircraftType>>(response));
             //result;
        }
        /*
        public async Task<AircraftType> GetByIdAsync(int id)
        {
            var response = await _client.GetAsync(new Uri(""+_uri + id));
            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<AircraftType>(responseString);
            return result;
        }

        public async Task PostAsync(AircraftType type)
        {
            string json = await Task.Run(() => JsonConvert.SerializeObject(type));
            var httpContent = new HttpFormUrlEncodedContent(new Dictionary<string, string>
            {
                ["value"] = json
            });
            var response = await _client.PostAsync(_uri, httpContent);
        }

        public async Task UpdateAsync(AircraftType type)
        {
            var id = type.id;
            string json = await Task.Run(() => JsonConvert.SerializeObject(type));
            var httpContent = new HttpFormUrlEncodedContent(new Dictionary<string, string>
            {
                ["value"] = json
            });
            var response = await _client.PutAsync(new Uri("" + _uri + id), httpContent);
        }

        public async Task DeleteByIdAsync(int id)
        {
            var response = await _client.DeleteAsync(new Uri("" + _uri + id));
        }*/
    }
}
