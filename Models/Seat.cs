using System;
using System.Collections.Generic;

namespace Movie_Reservation.Models;

public partial class Seat
{
    public int Id { get; set; }

    public int ScreenId { get; set; }

    public int? SeatNumber { get; set; }

    public virtual Screen IdNavigation { get; set; } = null!;
}
