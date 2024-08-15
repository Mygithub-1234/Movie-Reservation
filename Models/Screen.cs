using System;
using System.Collections.Generic;

namespace Movie_Reservation.Models;

public partial class Screen
{
    public int Id { get; set; }

    public int? TheaterId { get; set; }

    public int? Capacity { get; set; }

    public virtual Seat? Seat { get; set; }

    public virtual ICollection<Show> Shows { get; set; } = new List<Show>();

    public virtual Theater? Theater { get; set; }
}
