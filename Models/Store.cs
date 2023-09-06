using System;
using System.Collections.Generic;

namespace dvd_rental_api.Models;

public partial class Store
{
    public int StoreId { get; set; }

    public short ManagerStaffId { get; set; }

    public short AddressId { get; set; }

    public DateTime LastUpdate { get; set; }

    public virtual Address Address { get; set; } = null!;

    public virtual Staff ManagerStaff { get; set; } = null!;
}
