using Microsoft.EntityFrameworkCore;
using Movie_Reservation.Models;

namespace Movie_Reservation.Data
{
    public class ApiContext:DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Movie> Movies { get; set; }

        public DbSet<Theater> Theaters { get; set; }

        public DbSet<Show> Shows { get; set; }

        public DbSet<Booking> Bookings { get; set; }

        public DbSet<BookingSeat> BookingSeats { get; set; }

        public ApiContext(DbContextOptions<ApiContext> Options): base(Options) { }
    }
}
