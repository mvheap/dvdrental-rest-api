using System;
using System.Collections.Generic;

namespace dvd_rental_api.Models;

public partial class Language
{
    public short LanguageId { get; set; }

    public string Name { get; set; } = null!;

    public DateTime LastUpdate { get; set; }

    public virtual ICollection<Film> Films { get; set; } = new List<Film>();
}
