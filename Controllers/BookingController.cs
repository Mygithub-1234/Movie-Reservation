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
        private readonly MovieBookingContext _context;

        public BookingController(MovieBookingContext context)
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
                //yet to implement
                // Return booking confirmation
                return Ok();
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
        public ActionResult UpdateTicket(int showId, int movieId)
        {

            try
            {
                //yet to implement
                // Return booking confirmation
                return Ok();
            }
            catch (Exception ex)
            {
                // Handle exception
                return StatusCode(500, ex.Message);
            }


        }
    }
}
