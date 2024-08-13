using System.ComponentModel.DataAnnotations;

namespace Movie_Reservation.Models
{
    public class BookingSeat
    {
        [Key]
        public int Booking_Seat_Id { get; set; }
        public int Booking_Id { get; set; }
        public int Seat_Id { get; set; }
    }
}
