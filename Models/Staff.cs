using System;
using System.Collections.Generic;

namespace dvd_rental_api.Models;

public partial class Staff
{
    public short StaffId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public short AddressId { get; set; }

    public string? Email { get; set; }

    public short StoreId { get; set; }

    public bool? Active { get; set; }

    public string Username { get; set; } = null!;

    public string? Password { get; set; }

    public DateTime LastUpdate { get; set; }

    public byte[]? Picture { get; set; }

    public virtual Address Address { get; set; } = null!;

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual ICollection<Rental> Rentals { get; set; } = new List<Rental>();

    public virtual Store? Store { get; set; }
}
