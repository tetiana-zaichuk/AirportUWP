using System.Collections.Generic;

namespace AirportUWP.Models
{
    public class Crew
    {
        public int id { get; set; }
        public Pilot pilot { get; set; }
        public List<Stewardess> stewardesses { get; set; }
    }
}
