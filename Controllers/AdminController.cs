using Microsoft.AspNetCore.Mvc;
using Movie_Reservation.Data;
using Movie_Reservation.Models;

namespace Movie_Reservation.Controllers
{
    [Route("api/v1.0/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly ApiContext _context;

        public AdminController(ApiContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Registers new user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost("register")]
        public JsonResult Register(User user)
        {
            _context.Users.Add(user);
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
        public JsonResult Login(User user)
        {
            var result = _context.Users.Where(s => s.UserName == user.UserName && s.Password == user.Password && s.IsActive == 1).FirstOrDefault();
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
        public ActionResult ForgotPassword(User model)
        {
            var user = _context.Users.Find(model.UserEmail);
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
