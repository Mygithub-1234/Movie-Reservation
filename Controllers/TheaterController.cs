using Microsoft.AspNetCore.Mvc;
using Movie_Reservation.Data;
using Movie_Reservation.Models;

namespace Movie_Reservation.Controllers
{
    [Route("api/v1.0/[controller]/")]
    [ApiController]
    public class TheaterController : Controller
    {
        private readonly MovieBookingContext _context;

        public TheaterController(MovieBookingContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Adds new theater
        /// </summary>
        /// <param name="theater"></param>
        /// <returns></returns>
        //[HttpPost("addTheater")]
        //public JsonResult Create(TheaterRequest theater)
        //{
        //    _context.Theaters.Add(theater);
        //    //Save changes
        //    _context.SaveChanges();

        //    return new JsonResult(Ok(theater));
        //}
        [HttpPost("addTheater")]
        public async Task<IActionResult> Create(TheaterRequest theater)
        {
            var request = new Theater() { 
                Name = theater.Name,
                City = theater.City,
                ContactInfo = theater.ContactInfo
            };
            await _context.Theaters.AddAsync(request);
            await _context.SaveChangesAsync();
            return Ok(request);
        }

        /// <summary>
        /// Gets specified theater
        /// </summary>
        /// <returns></returns>
        [HttpGet("theater/search/theatername")]
        public JsonResult GetTheaterById([FromBody]string theatername)
        {
            var result = _context.Theaters.Where(m => m.Name== theatername).FirstOrDefault();
            return new JsonResult(Ok(result));
        }

        /// <summary>
        /// Gets all available theaters
        /// </summary>
        /// <returns></returns>
        [HttpGet]
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
        [HttpPut]
        public JsonResult Edit(TheaterRequest theater)
        {
            var theaterInDb = _context.Theaters.Find(theater.Name);

            if (theaterInDb == null)
                return new JsonResult(NotFound());
            theaterInDb.Name = theater.Name;
            theaterInDb.City = theater.City;
            theaterInDb.ContactInfo = theater.ContactInfo;

            _context.SaveChanges();
            return new JsonResult(Ok(theater));

        }

        /// <summary>
        /// Deletes a theater
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
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
