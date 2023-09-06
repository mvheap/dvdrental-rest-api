using System;
using System.Collections.Generic;

namespace dvd_rental_api.Models;

public partial class Inventory
{
    public int InventoryId { get; set; }

    public short FilmId { get; set; }

    public short StoreId { get; set; }

    public DateTime LastUpdate { get; set; }

    public virtual Film Film { get; set; } = null!;

    public virtual ICollection<Rental> Rentals { get; set; } = new List<Rental>();
}
