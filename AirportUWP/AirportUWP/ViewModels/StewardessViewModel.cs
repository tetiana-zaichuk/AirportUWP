using System.Collections.ObjectModel;
using System.Threading.Tasks;
using AirportUWP.Models;
using AirportUWP.Services;

namespace AirportUWP.ViewModels
{
    public class StewardessViewModel
    {
        public ObservableCollection<Stewardess> Stewardesses { get; set; } = new ObservableCollection<Stewardess>();
        public StewardessService _stewardessService;

        public StewardessViewModel()
        {
            _stewardessService = new StewardessService();
        }

        public async Task UpdateListAsync()
        {
            var newCollection = new ObservableCollection<Stewardess>(await _stewardessService.GetAsync());
            Stewardesses.Clear();
            foreach (var item in newCollection)
            {
                Stewardesses.Add(item);
            }
        }

        public async Task AddAsync(Stewardess ob)
        {
            await _stewardessService.PostAsync(ob);
            await UpdateListAsync();
        }

        public async Task UpdateAsync(Stewardess ob)
        {
            await _stewardessService.UpdateAsync(ob);
            await UpdateListAsync();
        }

        public async Task DeleteAsync(int id)
        {
            await _stewardessService.DeleteByIdAsync(id);
            await UpdateListAsync();
        }
    }
}
