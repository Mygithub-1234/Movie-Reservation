using System;
using System.Collections.Generic;

namespace Movie_Reservation.Models;

public partial class Booking
{
    public int Id { get; set; }

    public int? ShowId { get; set; }

    public int? NoOfTickets { get; set; }

    public int? Amount { get; set; }

    public int? BookedByUser { get; set; }

    public virtual Show? Show { get; set; }
}
