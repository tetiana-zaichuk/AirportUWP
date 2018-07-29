using System;

namespace AirportUWP.Models
{
    public class Aircraft
    {
        public int id { get; set; }
        public string aircraftName { get; set; }
        public AircraftType aircraftType { get; set; }
        public DateTime aircraftReleaseDate { get; set; }
        public TimeSpan exploitationTimeSpan { get; set; }
    }
}
