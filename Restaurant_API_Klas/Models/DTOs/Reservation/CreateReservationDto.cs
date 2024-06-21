using Restaurant_API_Klas.Enums;

namespace Restaurant_API_Klas.Models.DTOs.Reservation
{
    public class CreateReservationDto
    {
        public int CustomerId { get; set; }
        public int TableId { get; set; }
        public DateTime DateTime { get; set; }
        public SpecialRequests SpecialRequests { get; set; }
    }
}
