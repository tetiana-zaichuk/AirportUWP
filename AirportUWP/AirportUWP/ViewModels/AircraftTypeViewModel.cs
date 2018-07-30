using System.Collections.ObjectModel;
using System.Threading.Tasks;
using AirportUWP.Models;
using AirportUWP.Services;

namespace AirportUWP.ViewModels
{
    public class AircraftTypeViewModel
    {
        public ObservableCollection<AircraftType> AircraftTypes { get; set; }= new ObservableCollection<AircraftType>();
        public AircraftTypeService _aircraftTypeService;

        public AircraftTypeViewModel()
        {
            _aircraftTypeService = new AircraftTypeService();
        }
        
        public async Task UpdateListAsync()
        {
            var newCollection = new ObservableCollection<AircraftType>(await _aircraftTypeService.GetAsync());
            AircraftTypes.Clear();
            foreach (var item in newCollection)
            {
                AircraftTypes.Add(item);
            }
        }

        public async Task AddAsync(AircraftType ob)
        {
            Task t1 = _aircraftTypeService.PostAsync(ob);
            Task t2 = UpdateListAsync();
            await Task.WhenAll(new[] { t1, t2 });
        }

        public async Task UpdateAsync(AircraftType type)
        {
            await _aircraftTypeService.UpdateAsync(type);
            await UpdateListAsync();
        }

        public async Task DeleteAsync(int id)
        {
            await _aircraftTypeService.DeleteByIdAsync(id);
            await UpdateListAsync();
        }
    }
}
