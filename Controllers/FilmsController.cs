using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using dvd_rental_api.Models;
using dvd_rental_api.Utils;

namespace dvd_rental_api.Controllers;

[Route("[controller]")]
[ApiController]
public class FilmsController : ControllerBase
{
    private readonly DvdrentalContext _context;

    public FilmsController(DvdrentalContext context)
    {
        _context = context;
    }

    // GET films/
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Film>>> GetFilms(int limit = 200,
    int relaseYear = 0,
    string language = null,
    int minDuration = 0, int maxDuration = 0)
    {
        if (relaseYear != 0)
        {
            return await _context.Films
            .Where(f => f.ReleaseYear == relaseYear)
            .OrderBy(f => f.Title)
            .Take(limit)
            .ToListAsync();
        }

        if (language is not null)
        {
            language = language.ToLower();
            if (Utilities.languageIds.ContainsKey(language))
            {
                return await _context.Films
                .Where(f => f.Language.LanguageId == Utilities.languageIds[language])
                .OrderBy(f => f.Title)
                .Take(limit)
                .ToListAsync();
            }

            return NotFound();
        }

        // the two of the limits of the range have to be provided
        if (minDuration == 0 ^ maxDuration == 0)
        {
            return BadRequest();
        }

        if (minDuration != 0 && maxDuration != 0)
        {
            return await _context.Films
            .Where(f => f.Length >= minDuration && f.Length <= maxDuration)
            .OrderBy(f => f.Title)
            .Take(limit)
            .ToListAsync();
        }

        return await _context.Films.OrderBy(f => f.Title).Take(limit).ToListAsync();
    }

    // GET films/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Film>> GetFilm(short id)
    {
        var film = await _context.Films.FindAsync(id);

        if (film == null)
        {
            return NotFound();
        }

        return film;
    }

    // GET films/{id}/actors
    [HttpGet("{id:int}/actors")]
    public async Task<ActionResult<IEnumerable<Actor>>> GetFilmCast(short id)
    {
        var film = await _context.Films.FindAsync(id);

        if (film == null)
        {
            return NotFound();
        }

        var query = from a in _context.Actors
                    join fa in _context.FilmActors on a.ActorId equals fa.ActorId
                    join f in _context.Films on fa.FilmId equals f.FilmId
                    where f.Title.Equals(film.Title)
                    orderby a.FirstName ascending
                    select a;

        var actors = await query.ToListAsync();
        return actors;
    }
}