using System.Collections.ObjectModel;
using System.Threading.Tasks;
using AirportUWP.Models;
using AirportUWP.Services;

namespace AirportUWP.ViewModels
{
    public class FlightViewModel
    {
        public ObservableCollection<Flight> Flights { get; set; } = new ObservableCollection<Flight>();
        public FlightService _flightService;

        public FlightViewModel()
        {
            _flightService = new FlightService();
        }

        public async Task UpdateListAsync()
        {
            var newCollection = new ObservableCollection<Flight>(await _flightService.GetAsync());
            Flights.Clear();
            foreach (var item in newCollection)
            {
                Flights.Add(item);
            }
        }

        public async Task UpdateAsync(Flight ob)
        {
            await _flightService.UpdateAsync(ob);
            await UpdateListAsync();
        }

        public async Task DeleteAsync(int id)
        {
            await _flightService.DeleteByIdAsync(id);
            await UpdateListAsync();
        }
    }
}
