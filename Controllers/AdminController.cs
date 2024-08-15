using Microsoft.AspNetCore.Mvc;
using Movie_Reservation.Data;
using Movie_Reservation.Models;

namespace Movie_Reservation.Controllers
{
    [Route("api/v1.0/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly MovieBookingContext _context;

        public AdminController(MovieBookingContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Registers new user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost("register")]
        public JsonResult Register(GeneralUser user)
        {
            _context.GeneralUsers.Add(user);
            //Save changes
            _context.SaveChanges();

            return new JsonResult(Ok(user));
        }

        /// <summary>
        /// Login based on successful authentication
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>

        [HttpPost("login")]
        public JsonResult Login(GeneralUser user)
        {
            var result = _context.GeneralUsers.Where(s => s.Name == user.Name && s.Password == user.Password).FirstOrDefault();
            if (result != null)
            {
                return new JsonResult(Ok(user));
            }
            return new JsonResult("Not Found");
        }

        /// <summary>
        /// User submits their email address.
        //  API checks if the email exists in the database.
        //  If the email exists, generate a reset password token and send it to the user's email.
        //  User clicks the reset password link in the email.
        //  API validates the token and allows the user to set a new password.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>

        [HttpPost("forgot-password")]
        public ActionResult ForgotPassword(GeneralUser model)
        {
            var user = _context.GeneralUsers.Find(model.Email);
            if (user == null)

            {
                // Handle user not found
                return NotFound();
            }
            //yet to implement complete functionality
            return Ok(new { message = "Password reset email sent" });
        }

    }
}
