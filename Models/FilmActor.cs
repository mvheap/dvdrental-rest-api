using System;
using System.Collections.Generic;

namespace dvd_rental_api.Models;

public partial class FilmActor
{
    public short ActorId { get; set; }

    public short FilmId { get; set; }

    public DateTime LastUpdate { get; set; }

    public virtual Actor Actor { get; set; } = null!;

    public virtual Film Film { get; set; } = null!;
}
