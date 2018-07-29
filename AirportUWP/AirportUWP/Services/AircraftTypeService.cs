using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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

        public AircraftTypeService()
        {
            _client = new HttpClient();
        }

        public async Task<ObservableCollection<AircraftType>> GetAsync()
        {
            var response = await _client.GetAsync(_uri).ConfigureAwait(false);
            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ObservableCollection<AircraftType>>(responseString);
            return result;
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
