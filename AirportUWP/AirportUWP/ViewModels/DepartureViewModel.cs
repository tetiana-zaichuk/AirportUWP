using System.Collections.ObjectModel;
using System.Threading.Tasks;
using AirportUWP.Models;
using AirportUWP.Services;

namespace AirportUWP.ViewModels
{
    public class DepartureViewModel
    {
        public ObservableCollection<Departure> Departures { get; set; } = new ObservableCollection<Departure>();
        public DepartureService _departureService;

        public DepartureViewModel()
        {
            _departureService = new DepartureService();
        }

        public async Task UpdateListAsync()
        {
            var newCollection = new ObservableCollection<Departure>(await _departureService.GetAsync());
            Departures.Clear();
            foreach (var item in newCollection)
            {
                Departures.Add(item);
            }
        }

        public async Task UpdateAsync(Departure ob)
        {
            await _departureService.UpdateAsync(ob);
            await UpdateListAsync();
        }

        public async Task DeleteAsync(int id)
        {
            await _departureService.DeleteByIdAsync(id);
            await UpdateListAsync();
        }
    }
}
