﻿using System;
using System.Collections.Generic;

namespace dvd_rental_api.Models;

public partial class Customer
{
    public short CustomerId { get; set; }

    public short StoreId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? Email { get; set; }

    public short AddressId { get; set; }

    public bool? Activebool { get; set; }

    public DateOnly CreateDate { get; set; }

    public DateTime? LastUpdate { get; set; }

    public int? Active { get; set; }

    public virtual Address Address { get; set; } = null!;

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual ICollection<Rental> Rentals { get; set; } = new List<Rental>();
}
