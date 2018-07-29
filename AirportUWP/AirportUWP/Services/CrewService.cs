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
    public class CrewService
    {
        private readonly HttpClient _client;
        private readonly Uri _uri = new Uri("http://localhost:38236/api/Crew/");
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<Crew> Items { get; set; } = new ObservableCollection<Crew>();

        public CrewService()
        {
            if (_client == null)
            {
                _client = new HttpClient();
            }
        }
        public Crew selectedItem;

        public Crew SelectedItem
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

        public async Task<ObservableCollection<Crew>> GetAsync()
        {
            var response = await _client.GetStringAsync(_uri).ConfigureAwait(false);
            return await Task.Run(() => JsonConvert.DeserializeObject<ObservableCollection<Crew>>(response));
        }

        public async Task DeleteByIdAsync(int id)
        {
            await _client.DeleteAsync(new Uri("" + _uri + id));
        }

        public async Task PostAsync(Crew type)
        {
            var json = new StringContent(JsonConvert.SerializeObject(type), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(_uri, json);
        }

        public async Task UpdateAsync(Crew type)
        {
            var id = type.id;
            var json = new StringContent(JsonConvert.SerializeObject(type), Encoding.UTF8, "application/json");
            var response = await _client.PutAsync(new Uri("" + _uri + id), json);
        }
    }
}
