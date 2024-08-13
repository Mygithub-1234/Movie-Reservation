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

        #region
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

        #endregion

        //#region
        /////// <summary>
        /////// Adds a new movie -Admin
        /////// </summary>
        /////// <param name="movie"></param>
        /////// <returns></returns>
        ////[HttpPost]
        ////public JsonResult CreateMovie(Movie movie)
        ////{
        ////    _context.Movies.Add(movie);
        ////    //Save changes
        ////    _context.SaveChanges();

        ////    return new JsonResult(Ok(movie));
        ////}
        /////// <summary>
        ///// Gets specified movie
        ///// </summary>
        ///// <returns></returns>
        //[HttpGet]
        //public JsonResult GetMovieById(int id)
        //{
        //    var result = _context.Movies.Where(m => m.Id == id).FirstOrDefault();
        //    return new JsonResult(Ok(result));
        //}
        ///// <summary>
        ///// Gets all available movies
        ///// </summary>
        ///// <returns></returns>
        //[HttpGet]
        //public JsonResult GetAllMovies()
        //{
        //    var result = _context.Movies.ToList();
        //    return new JsonResult(Ok(result));
        //}

        ///// <summary>
        ///// Searches movie by MovieName
        ///// </summary>
        ///// <returns></returns>
        //[HttpGet("Search/MovieName")]
        //public JsonResult GetMovieByName(string criteria)
        //{
        //    var result = _context.Movies.Find(criteria);
        //    return new JsonResult(Ok(result));
        //}
        ///// <summary>
        ///// Updates movie details- Admin
        ///// </summary>
        ///// <param name="movie"></param>
        ///// <returns></returns>
        //[HttpPut]
        //public JsonResult EditMovie(Movie movie)
        //{
        //    var movieInDb = _context.Movies.Find(movie.Id);

        //    if (movieInDb == null)
        //        return new JsonResult(NotFound());
        //    movieInDb.MovieType = movie.MovieType;
        //    movieInDb.MovieName = movie.MovieName;

        //    _context.SaveChanges();
        //    return new JsonResult(Ok(movie));

        //}

        ///// <summary>
        ///// Deletes a movie -Admin
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //[HttpDelete]
        //public JsonResult DeleteMovie(int id)
        //{
        //    var result = _context.Movies.Find(id);

        //    if (result == null)
        //        return new JsonResult(NotFound());
        //    _context.Movies.Remove(result);
        //    _context.SaveChanges();

        //    return new JsonResult(Ok(result));
        //}
        //#endregion

        //#region
        /////// <summary>
        /////// Adds new theater -Admin
        /////// </summary>
        /////// <param name="theater"></param>
        /////// <returns></returns>
        ////[HttpPost]
        ////public JsonResult CreateTheater(Theater theater)
        ////{
        ////    _context.Theaters.Add(theater);
        ////    //Save changes
        ////    _context.SaveChanges();

        ////    return new JsonResult(Ok(theater));
        ////}
        ///// <summary>
        ///// Gets specified theater
        ///// </summary>
        ///// <returns></returns>
        //[HttpGet]
        //public JsonResult GetTheaterById(int id)
        //{
        //    var result = _context.Theaters.Where(m => m.Id == id).FirstOrDefault();
        //    return new JsonResult(Ok(result));
        //}

        ///// <summary>
        ///// Gets all available theaters
        ///// </summary>
        ///// <returns></returns>
        //[HttpGet]
        //public JsonResult GetAllTheaters()
        //{
        //    var result = _context.Theaters.ToList();
        //    return new JsonResult(Ok(result));
        //}
        ///// <summary>
        ///// Updates theater details -Admin
        ///// </summary>
        ///// <param name="theater"></param>
        ///// <returns></returns>
        //[HttpPut]
        //public JsonResult EditTheater(Theater theater)
        //{
        //    var theaterInDb = _context.Theaters.Find(theater.Id);

        //    if (theaterInDb == null)
        //        return new JsonResult(NotFound());
        //    theaterInDb.TheaterName = theater.TheaterName;
        //    //theaterInDb.MovieId = theater.MovieId;

        //    _context.SaveChanges();
        //    return new JsonResult(Ok(theater));

        //}

        ///// <summary>
        ///// Deletes a theater -Admin
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //[HttpDelete]
        //public JsonResult DeleteTheater(int id)
        //{
        //    var result = _context.Theaters.Find(id);

        //    if (result == null)
        //        return new JsonResult(NotFound());
        //    _context.Theaters.Remove(result);
        //    _context.SaveChanges();

        //    return new JsonResult(Ok(result));
        //}

        //#endregion
    }
}
