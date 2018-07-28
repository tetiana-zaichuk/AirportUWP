using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirportUWP.Models;
using AirportUWP.Services;

namespace AirportUWP.ViewModels
{
    public class AircraftTypeViewModel //: BaseViewModel
    {
        public ObservableCollection<AircraftType> AircraftTypes { get; set; }
        public AircraftType AircraftType { get; set; }
        public AircraftTypeService _aircraftTypeService;

        public AircraftTypeViewModel()
        {
            _aircraftTypeService = new AircraftTypeService();
        }

        public async Task<ObservableCollection<AircraftType>> GetAsync()
        {
            AircraftTypes = await _aircraftTypeService.GetAsync();
            return AircraftTypes;
        }

        public async Task<AircraftType> GetAsync(int id)
        {
            AircraftType = await _aircraftTypeService.GetByIdAsync(id);
            return AircraftType;
        }
    }
}
