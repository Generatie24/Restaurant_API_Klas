using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurant_API_Klas.Data;
using Restaurant_API_Klas.Extensions;
using Restaurant_API_Klas.Models.DTOs.Reservation;

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

        // Read all reservations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReservationDetailsDto>>> GetReservations()
        {
            var reservations = await _context.Reservations
                .Include(r => r.Customer)
                .Include(r => r.Table)
                .ToListAsync();

            // use in case of small to middle size data, create variable read data into variable and work with variable

            //var reservationsDto = reservations.Select(r => r.ToReservationDetailsDto());
            //return Ok(reservationsDto);


            // use in case of large data and this is comming directly from database and not variable
            return Ok(reservations.AsQueryable().ToReservationDetailsDtos());


        }

        // Read one reservation
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


        // Create a new reservation
        [HttpPost]
        public async Task<ActionResult<ReservationDetailsDto>> PostReserVation(CreateReservationDto dto)
        {
            if (!_context.Customers.Any(c => c.CustomerId == dto.CustomerId))
            {
                return BadRequest($"Customer with Id {dto.CustomerId} does not exist.");
            }

            if (!_context.Tables.Any(t => t.TableId == dto.TableId))
            {
                return BadRequest($"Table with Id {dto.TableId} does not exist.");
            }

            var reservation = dto.ToReservation();
            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();

            // load related objects for created reservation
            var createdReservation = await _context.Reservations
                .Include(r => r.Customer)
                .Include(r => r.Table)
                .FirstOrDefaultAsync(r => r.ReservationId == reservation.ReservationId);


            // The CreatedAtAction method generates a URI to the GetReservation action, using the id of the newly created reservation.
            //The URI is included in the Location header of the response, allowing the client to fetch the newly created reservation.
            // The ToReservationDetailsDto extension method is used to convert the Reservation entity to a ReservationDetailsDto object.
           
            return CreatedAtAction(nameof(GetReservation),  // Read Action
                new { id = reservation.ReservationId },     // Route value
                createdReservation.ToReservationDetailsDto()); // Response body
        }

        // update/edit  one reservation
        [HttpPut("{id}")]

        public async Task<IActionResult> PutReservation(int id, UpdateReservationDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }

            reservation.UpdateReservation(dto);
            _context.Entry(reservation).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // delete one reservation
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReservation(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }

            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync();
            return NoContent();
        }

    }
}
