using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Movie_Reservation.Models
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }
        public string? MovieType { get; set; }
        public string? MovieName { get; set; }
    }
}
