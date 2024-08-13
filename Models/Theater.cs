using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Movie_Reservation.Models
{
    public class Theater
    {
        [Key]
        public int Id { get; set; }
        public string TheaterName { get; set; }

        public List<Movie> Movies { get; set; }
    }
}
