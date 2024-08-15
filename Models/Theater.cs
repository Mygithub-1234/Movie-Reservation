using System;
using System.Collections.Generic;

namespace Movie_Reservation.Models;

public partial class Theater
{
    public int Id { get; set; }

    public int? MovieId { get; set; }

    public string? Name { get; set; }

    public string? City { get; set; }

    public string? ContactInfo { get; set; }

    public virtual Movie? Movie { get; set; }

    public virtual ICollection<Screen> Screens { get; set; } = new List<Screen>();
}
