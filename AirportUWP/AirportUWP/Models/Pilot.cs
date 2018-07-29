using System;

namespace AirportUWP.Models
{
    public class Pilot
    {
        public int id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public DateTime dob { get; set; }
        public int experience { get; set; }
        public int? crewId { get; set; }
    }
}
