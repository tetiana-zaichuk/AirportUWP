namespace AirportUWP.Models
{
    public class Ticket
    {
        public int id { get; set; }
        public decimal price { get; set; }
        public int flightId { get; set; }
        public Flight flight { get; set; }
    }
}
