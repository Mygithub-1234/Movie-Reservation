﻿namespace Movie_Reservation.Models
{
    public class MovieRequest
    {
        public string? Title { get; set; }

        public string? Description { get; set; }

        public string? Genre { get; set; }

        public DateTime? ReleaseDate { get; set; } = default(DateTime?);
    }
}
