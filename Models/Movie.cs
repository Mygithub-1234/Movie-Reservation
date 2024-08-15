using System;
using System.Collections.Generic;

namespace Movie_Reservation.Models;

public partial class Movie
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public string? Genre { get; set; }

    public DateTime? ReleaseDate { get; set; }

    public virtual ICollection<Show> Shows { get; set; } = new List<Show>();

    public virtual ICollection<Theater> Theaters { get; set; } = new List<Theater>();
}
