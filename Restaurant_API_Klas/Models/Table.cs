using Restaurant_API_Klas.Enums;

namespace Restaurant_API_Klas.Models
{
    public class Table
    {
        public int TableId { get; set; }
        public int Capacity { get; set; }
        public SpecialRequests Location { get; set; } 

        // Navigation property for relationships
        public List<Reservation> Reservations { get; set; }
    }
}
