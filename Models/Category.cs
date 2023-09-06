using System;
using System.Collections.Generic;

namespace dvd_rental_api.Models;

public partial class Category
{
    public short CategoryId { get; set; }

    public string Name { get; set; } = null!;

    public DateTime LastUpdate { get; set; }

    public virtual ICollection<FilmCategory> FilmCategories { get; set; } = new List<FilmCategory>();
}
