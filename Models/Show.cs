using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Movie_Reservation.Models
{
    public class Show
    {
        [Key]
        public int Id { get; set; }

        public int Theater_Id { get; set; }

        public int Movie_Id { get; set; }

        public List<Seat> Available_Seats { get; set; }

    }
}
