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
        public ObservableCollection<AircraftType> AircraftTypes { get; set; }= new ObservableCollection<AircraftType>();
        //public AircraftType AircraftType { get; set; }
       

        public AircraftTypeViewModel()
        {
           // Title = "AircraftType";
            _aircraftTypeService = new AircraftTypeService();
           // AircraftTypes 
            /*AircraftTypes = new ObservableCollection<AircraftType>()
            {
                new AircraftType(){ aircraftModel = "Tupolev Tu-134", seatsNumber = 80, carrying = 47000},
                new AircraftType(){ aircraftModel = "Tupolev Tu-204", seatsNumber = 196, carrying = 107900},
                new AircraftType(){ aircraftModel = "Ilyushin IL-62", seatsNumber = 138, carrying = 280300}
            };*/
        }

        public AircraftTypeService _aircraftTypeService;
        
        //public async Task/*<ObservableCollection<AircraftType>>*/ GetAsync()
        //{
        //    AircraftTypes = await _aircraftTypeService.GetAsync();
        //    NotifyPropertyChanged(() => AircraftTypes);
        //   //return AircraftTypes;
        //}

        public async Task UpdateList()
        {
            var newCollection = new ObservableCollection<AircraftType>(await _aircraftTypeService.GetAsync());
            AircraftTypes.Clear();
            foreach (var item in newCollection)
            {
                AircraftTypes.Add(item);
            }
        }
        /*
        public async Task<AircraftType> GetAsync(int id)
        {
            AircraftType = await _aircraftTypeService.GetByIdAsync(id);
            return AircraftType;
        }*/
    }
}
