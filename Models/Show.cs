using System;
using System.Collections.Generic;

namespace Movie_Reservation.Models;

public partial class Show
{
    public int Id { get; set; }

    public int? MovieId { get; set; }

    public int? ScreenId { get; set; }

    public int? AvailableSeats { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual Movie? Movie { get; set; }

    public virtual Screen? Screen { get; set; }
}
