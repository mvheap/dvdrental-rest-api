using System;
using System.Collections.Generic;
using NpgsqlTypes;

namespace dvd_rental_api.Models;

public partial class Film
{
    public short FilmId { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public int? ReleaseYear { get; set; }

    public short LanguageId { get; set; }

    public short RentalDuration { get; set; }

    public decimal RentalRate { get; set; }

    public short? Length { get; set; }

    public decimal ReplacementCost { get; set; }

    public DateTime LastUpdate { get; set; }

    public string[]? SpecialFeatures { get; set; }

    public NpgsqlTsVector Fulltext { get; set; } = null!;

    public virtual ICollection<FilmActor> FilmActors { get; set; } = new List<FilmActor>();

    public virtual ICollection<FilmCategory> FilmCategories { get; set; } = new List<FilmCategory>();

    public virtual ICollection<Inventory> Inventories { get; set; } = new List<Inventory>();

    public virtual Language Language { get; set; } = null!;
}
