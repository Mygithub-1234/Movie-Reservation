namespace Movie_Reservation.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public int Show_Id { get; set; }
        public int No_Of_Tickets { get; set; }
        public int Amount { get; set; }
        public int User_Id { get; set; }
        public DateTime BookingDate { get; set; }
    }
}
