using Restaurant_API_Klas.Models;

using Restaurant_API_Klas.Models.DTOs;

namespace Restaurant_API_Klas.Extensions
{
    public static class ReservationMappingExtensions
    {
        public static ReservationDetailsDto ToReservationDetailsDto(this Reservation reservation)
        {
            return new ReservationDetailsDto
            {
                ReservationId = reservation.ReservationId,
                CustomerId = reservation.CustomerId,
                CustomerName = reservation.Customer.Name,
                TableId = reservation.TableId,
                DateTime = reservation.DateTime,
                SpecialRequests = reservation.SpecialRequests
            };

        }

        public static IQueryable<ReservationDetailsDto> ToReservationDetailsDtos(this IQueryable<Reservation> reservations)
        {
            return reservations.Select(r => r.ToReservationDetailsDto());
        }
    }
}
