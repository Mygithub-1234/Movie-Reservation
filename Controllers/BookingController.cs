using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Movie_Reservation.Data;
using Movie_Reservation.Models;

namespace Movie_Reservation.Controllers
{
    [Route("api/v1.0/[controller]/")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly ApiContext _context;

        public BookingController(ApiContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Books Tickets
        /// </summary>
        /// <param name="showId"></param>
        /// <param name="userId"></param>
        /// <param name="seatNumbers"></param>
        /// <returns></returns>
        [HttpPost("ticket")]
        public ActionResult BookTickets(int showId, int userId, List<int> seatNumbers)
        {
            try
            {
                var show = _context.Shows.Include(s => s.Available_Seats).FirstOrDefault(s => s.Id == showId);
                if (show == null)
                {
                    return NotFound();
                }

                // Check seat availability
                var availableSeats = show.Available_Seats.ToList();
                if (availableSeats.Count != seatNumbers.Count)
                {
                    return BadRequest("Seats not available");
                }

                // Create booking and update seat status
                var booking = new Booking
                {
                    User_Id = userId,
                    Show_Id = showId,
                    // ... other booking details
                };
                _context.Bookings.Add(booking);

                foreach (var seat in availableSeats)
                {
                    seat.Status = true;
                    _context.BookingSeats.Add(new BookingSeat { Booking_Id = booking.Id, Seat_Id = seat.Id });
                }

                _context.SaveChanges();
                // Return booking confirmation
                return Ok(booking);
            }
            catch (Exception ex)
            {
                // Handle exception
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Updates Ticket Status - For Cancellation
        /// </summary>
        /// <returns></returns>
        [HttpPatch("update/ticket")]
        public ActionResult UpdateTicket(int showId, int movieId )
        {
            try
            {
                var show = _context.Shows.FirstOrDefault(s => s.Id == showId && s.Movie_Id ==movieId);
                if (show == null)
                {
                    return NotFound();
                }

                // Check seat availability
                var availableSeats = show.Available_Seats.ToList();
                foreach (var seat in availableSeats)
                {
                    seat.Status = false;
                 }

                _context.SaveChanges();
                // Return booking confirmation
                return Ok(show);
            }
            catch (Exception ex)
            {
                // Handle exception
                return StatusCode(500, ex.Message);
            }
        }
    }
}
