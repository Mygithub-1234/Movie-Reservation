using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Movie_Reservation.Data;
using Movie_Reservation.Models;

namespace Movie_Reservation.Controllers
{
    [Route("api/v1.0/[controller]/")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly MovieBookingContext _context;

        public MovieController(MovieBookingContext context)
        {
            _context = context; 
        }
        /// <summary>
        /// Adds a new movie
        /// </summary>
        /// <param name="movie"></param>
        /// <returns></returns>
        [HttpPost("addmovie")]
        public async Task<IActionResult> Create(MovieRequest movie)
        {
            var request = new Movie()
            {
                Title = movie.Title,
                Description = movie.Description,
                Genre = movie.Genre,
                ReleaseDate = movie.ReleaseDate
            };
            await _context.Movies.AddAsync(request);
            await _context.SaveChangesAsync();
            return Ok(request);
        }
        /// <summary>
        /// Gets specified movie by name
        /// </summary>
        /// <returns></returns>
        [HttpGet("movies/search/moviename")]
        public JsonResult GetMovieByName(string moviename)
        {
            var result =_context.Movies.Where(m => m.Title== moviename).FirstOrDefault();
            return new JsonResult(Ok(result));
        }
        /// <summary>
        /// View all available movies
        /// </summary>
        /// <returns></returns>
        [HttpGet("all")]
        public JsonResult GetAll()
        {
            var result = _context.Movies.ToList();
            return new JsonResult(Ok(result));
        }
        /// <summary>
        /// Updates movie details
        /// </summary>
        /// <param name="movie"></param>
        /// <returns></returns>
        [HttpPut("UpdateMovie")]
        public JsonResult Edit(MovieRequest movie)
        {
            var movieInDb = _context.Movies.Find(movie.Title);

            if (movieInDb == null)
                return new JsonResult(NotFound());
            movieInDb.Genre = movie.Genre;
            movieInDb.Title = movie.Title;
            movieInDb.Description = movie.Description;

            _context.SaveChanges();
            return new JsonResult(Ok(movie));

        }

        /// <summary>
        /// Deletes a movie
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("delete/moviedelrequest")]
        public JsonResult Delete(int id)
        {
            var result = _context.Movies.Find(id);

            if (result == null)
                return new JsonResult(NotFound());
            _context.Movies.Remove(result);
            _context.SaveChanges();

            return new JsonResult(Ok(result));
        }

    }
}
