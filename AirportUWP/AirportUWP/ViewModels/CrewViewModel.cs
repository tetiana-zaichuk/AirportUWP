using System.Collections.ObjectModel;
using System.Threading.Tasks;
using AirportUWP.Models;
using AirportUWP.Services;

namespace AirportUWP.ViewModels
{
    public class CrewViewModel
    {
        public ObservableCollection<Crew> Crews { get; set; } = new ObservableCollection<Crew>();
        public CrewService _crewService;

        public CrewViewModel()
        {
            _crewService = new CrewService();
        }

        public async Task UpdateListAsync()
        {
            var newCollection = new ObservableCollection<Crew>(await _crewService.GetAsync());
            Crews.Clear();
            foreach (var item in newCollection)
            {
                Crews.Add(item);
            }
        }

        public async Task UpdateAsync(Crew ob)
        {
            await _crewService.UpdateAsync(ob);
            await UpdateListAsync();
        }

        public async Task DeleteAsync(int id)
        {
            await _crewService.DeleteByIdAsync(id);
            await UpdateListAsync();
        }
    }
}
