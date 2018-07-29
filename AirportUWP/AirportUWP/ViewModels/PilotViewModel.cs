using System.Collections.ObjectModel;
using System.Threading.Tasks;
using AirportUWP.Models;
using AirportUWP.Services;

namespace AirportUWP.ViewModels
{
    public class PilotViewModel
    {
        public ObservableCollection<Pilot> Pilots { get; set; } = new ObservableCollection<Pilot>();
        public PilotService _pilotService;

        public PilotViewModel()
        {
            _pilotService = new PilotService();
        }

        public async Task UpdateListAsync()
        {
            var newCollection = new ObservableCollection<Pilot>(await _pilotService.GetAsync());
            Pilots.Clear();
            foreach (var item in newCollection)
            {
                Pilots.Add(item);
            }
        }

        public async Task AddAsync(Pilot ob)
        {
            await _pilotService.PostAsync(ob);
            await UpdateListAsync();
        }

        public async Task UpdateAsync(Pilot ob)
        {
            await _pilotService.UpdateAsync(ob);
            await UpdateListAsync();
        }

        public async Task DeleteAsync(int id)
        {
            await _pilotService.DeleteByIdAsync(id);
            await UpdateListAsync();
        }
    }
}
