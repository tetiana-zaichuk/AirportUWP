using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportUWP.Models
{
    class Aircraft
    {
        public int Id { get; set; }
        public string AircraftName { get; set; }
        public AircraftType AircraftType { get; set; }
        public DateTime AircraftReleaseDate { get; set; }
        public TimeSpan ExploitationTimeSpan { get; set; }


    }
}
