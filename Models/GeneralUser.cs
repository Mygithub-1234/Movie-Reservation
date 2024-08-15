using System;
using System.Collections.Generic;

namespace Movie_Reservation.Models;

public partial class GeneralUser
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }
}
