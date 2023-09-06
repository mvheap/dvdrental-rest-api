using System;
using System.Collections.Generic;

namespace dvd_rental_api.Models;

public partial class FilmCategory
{
    public short FilmId { get; set; }

    public short CategoryId { get; set; }

    public DateTime LastUpdate { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual Film Film { get; set; } = null!;
}
