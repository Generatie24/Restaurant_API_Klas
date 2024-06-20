using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurant_API_Klas.Data;
using Restaurant_API_Klas.Models.DTOs;
using Restaurant_API_Klas.Extensions;

namespace Restaurant_API_Klas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly RestaurantContext _context;
        public ReservationsController(RestaurantContext context)
        {
                _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReservationDetailsDto>>> GetReservations()
        {
            var reservations = await _context.Reservations
                .Include(r => r.Customer)
                .Include(r => r.Table)
                .ToListAsync(); 
            var reservationsDto = reservations.Select(r => r.ToReservationDetailsDto());

            //return Ok(reservations.AsQueryable().ToReservationDetailsDto());

            return Ok(reservationsDto);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ReservationDetailsDto>> GetReservation(int id)
        {
            var reservation = await _context.Reservations
                .Include(r => r.Customer)
                .Include(r => r.Table)
                .FirstOrDefaultAsync(r => r.ReservationId == id);

            if (reservation == null)
            {
                return NotFound();
            }

            return Ok(reservation.ToReservationDetailsDto());
        }

    }
}
