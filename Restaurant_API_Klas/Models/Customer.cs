namespace Restaurant_API_Klas.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        // Navigation property for relationships
        public List<Reservation> Reservations { get; set; }
    }
}
