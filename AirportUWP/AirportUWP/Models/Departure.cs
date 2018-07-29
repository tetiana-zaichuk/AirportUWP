using System;

namespace AirportUWP.Models
{
    public class Departure
    {
        public int id { get; set; }
        public Flight flight { get; set; }
        public DateTime departureDate { get; set; }
        public Crew crew { get; set; }
        public Aircraft aircraft { get; set; }
    }
}
