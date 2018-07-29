using System;

namespace AirportUWP.Models
{
    public class Stewardess
    {
        public int id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public DateTime dob { get; set; }
        public int? crewId { get; set; }
        public Crew crew { get; set; }
    }
}
