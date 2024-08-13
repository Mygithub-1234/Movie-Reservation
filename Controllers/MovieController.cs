using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movie_Reservation.Data;
using Movie_Reservation.Models;

namespace Movie_Reservation.Controllers
{
    [Route("api/v1.0/[controller]/")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly ApiContext _context;

        public MovieController(ApiContext context)
        {
            _context = context; 
        }
        /// <summary>
        /// Adds a new movie
        /// </summary>
        /// <param name="movie"></param>
        /// <returns></returns>
        [HttpPost("addmovie")]
        public JsonResult Create(Movie movie)
        {
            _context.Movies.Add(movie);
            //Save changes
            _context.SaveChanges();

            return new JsonResult(Ok(movie));
        }
        /// <summary>
        /// Gets specified movie by name
        /// </summary>
        /// <returns></returns>
        [HttpGet("movies/search/moviename")]
        public JsonResult GetMovieByName([FromBody]string moviename)
        {
            var result =_context.Movies.Where(m => m.MovieName== moviename).FirstOrDefault();
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
        public JsonResult Edit(Movie movie)
        {
            var movieInDb = _context.Movies.Find(movie.Id);

            if (movieInDb == null)
                return new JsonResult(NotFound());
            movieInDb.MovieType = movie.MovieType;
            movieInDb.MovieName = movie.MovieName;

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
