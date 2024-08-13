using Microsoft.AspNetCore.Mvc;
using Movie_Reservation.Data;
using Movie_Reservation.Models;

namespace Movie_Reservation.Controllers
{
    [Route("api/v1.0/[controller]/")]
    [ApiController]
    public class TheaterController : Controller
    {
        private readonly ApiContext _context;

        public TheaterController(ApiContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Adds new theater
        /// </summary>
        /// <param name="theater"></param>
        /// <returns></returns>
        [HttpPost("addTheater")]
        public JsonResult Create(Theater theater)
        {
            _context.Theaters.Add(theater);
            //Save changes
            _context.SaveChanges();

            return new JsonResult(Ok(theater));
        }
        /// <summary>
        /// Gets specified theater
        /// </summary>
        /// <returns></returns>
        [HttpGet("search/theatername")]
        public JsonResult GetTheaterById([FromBody]string theatername)
        {
            var result = _context.Theaters.Where(m => m.TheaterName== theatername).FirstOrDefault();
            return new JsonResult(Ok(result));
        }

        /// <summary>
        /// Gets all available theaters
        /// </summary>
        /// <returns></returns>
        [HttpGet("all")]
        public JsonResult GetAll()
        {
            var result = _context.Theaters.ToList();
            return new JsonResult(Ok(result));
        }
        /// <summary>
        /// Updates theater details
        /// </summary>
        /// <param name="theater"></param>
        /// <returns></returns>
        [HttpPut("update/theater")]
        public JsonResult Edit(Theater theater)
        {
            var theaterInDb = _context.Theaters.Find(theater.Id);

            if (theaterInDb == null)
                return new JsonResult(NotFound());
            theaterInDb.TheaterName = theater.TheaterName;
            //theaterInDb.MovieId = theater.MovieId;

            _context.SaveChanges();
            return new JsonResult(Ok(theater));

        }

        /// <summary>
        /// Deletes a theater
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("delete/theaterdelrequest")]
        public JsonResult Delete(int id)
        {
            var result = _context.Theaters.Find(id);

            if (result == null)
                return new JsonResult(NotFound());
            _context.Theaters.Remove(result);
            _context.SaveChanges();

            return new JsonResult(Ok(result));
        }

    }
}
