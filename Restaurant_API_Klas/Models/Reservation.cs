using Restaurant_API_Klas.Enums;

namespace Restaurant_API_Klas.Models
{
    public class Reservation
    {
        public int ReservationId { get; set; }
        public int CustomerId { get; set; }
        public int TableId { get; set; }
        public DateTime DateTime { get; set; }
        public SpecialRequests SpecialRequests { get; set; }

        // Navigation properties
        public Customer Customer { get; set; } = null!;
        public Table Table { get; set; } = null!;
    }
}
