using System.Collections.ObjectModel;
using System.Threading.Tasks;
using AirportUWP.Models;
using AirportUWP.Services;

namespace AirportUWP.ViewModels
{
    public class TicketViewModel
    {
        public ObservableCollection<Ticket> Tickets { get; set; } = new ObservableCollection<Ticket>();
        public TicketService _ticketService;

        public TicketViewModel()
        {
            _ticketService = new TicketService();
        }

        public async Task UpdateListAsync()
        {
            var newCollection = new ObservableCollection<Ticket>(await _ticketService.GetAsync());
            Tickets.Clear();
            foreach (var item in newCollection)
            {
                Tickets.Add(item);
            }
        }

        public async Task AddAsync(Ticket ob)
        {
            await _ticketService.PostAsync(ob);
            await UpdateListAsync();
        }

        public async Task UpdateAsync(Ticket ob)
        {
            await _ticketService.UpdateAsync(ob);
            await UpdateListAsync();
        }

        public async Task DeleteAsync(int id)
        {
            await _ticketService.DeleteByIdAsync(id);
            await UpdateListAsync();
        }
    }
}
