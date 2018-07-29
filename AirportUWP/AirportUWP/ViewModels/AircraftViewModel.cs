using System.Collections.ObjectModel;
using System.Threading.Tasks;
using AirportUWP.Models;
using AirportUWP.Services;

namespace AirportUWP.ViewModels
{
    public class AircraftViewModel
    {
        public ObservableCollection<Aircraft> Aircrafts { get; set; } = new ObservableCollection<Aircraft>();
        public AircraftService _aircraftService;

        public AircraftViewModel()
        {
            _aircraftService = new AircraftService();
        }

        public async Task UpdateListAsync()
        {
            var newCollection = new ObservableCollection<Aircraft>(await _aircraftService.GetAsync());
            Aircrafts.Clear();
            foreach (var item in newCollection)
            {
                Aircrafts.Add(item);
            }
        }

        public async Task AddAsync(Aircraft ob)
        {
            await _aircraftService.PostAsync(ob);
            await UpdateListAsync();
        }

        public async Task UpdateAsync(Aircraft ob)
        {
            await _aircraftService.UpdateAsync(ob);
            await UpdateListAsync();
        }

        public async Task DeleteAsync(int id)
        {
            await _aircraftService.DeleteByIdAsync(id);
            await UpdateListAsync();
        }
    }
}
