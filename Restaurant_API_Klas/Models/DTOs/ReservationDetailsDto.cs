using Restaurant_API_Klas.Enums;

namespace Restaurant_API_Klas.Models.DTOs
{
    public class ReservationDetailsDto
    {
        public int ReservationId { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public int TableId { get; set; }
        public DateTime DateTime { get; set; }
        public SpecialRequests SpecialRequests { get; set; }
    }
}
