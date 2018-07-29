using System;
using System.Collections.Generic;

namespace AirportUWP.Models
{
    public class Flight
    {
        public int id { get; set; }
        public string departure { get; set; }
        public DateTime departureTime { get; set; }
        public string destination { get; set; }
        public DateTime arrivalTime { get; set; }
        public List<Ticket> tickets { get; set; }
    }
}
