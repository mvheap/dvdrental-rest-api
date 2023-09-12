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
        // Here will be saved the query result because
        // the user can specify multiple values to search
        IQueryable<Film> currentQueried = _context.Films;

        // the two of the limits of the range have to be provided
        if ((minDuration < 0 ^ maxDuration < 0) || minDuration > maxDuration)
        {
            return BadRequest();
        }

        if (relaseYear != 0)
        {
            currentQueried = currentQueried
            .Where(f => f.ReleaseYear == relaseYear)
            .Take(limit);
        }

        if (language is not null)
        {
            language = language.ToLower();
            if (Utilities.languageIds.ContainsKey(language))
            {
                currentQueried = currentQueried
                .Where(f => f.Language.LanguageId == Utilities.languageIds[language])
                .Take(limit);
            }

            return NotFound();
        }

        if (minDuration != 0 && maxDuration != 0)
        {
            currentQueried = currentQueried
            .Where(f => f.Length >= minDuration && f.Length <= maxDuration)
            .Take(limit);
        }

        return await currentQueried.OrderBy(f => f.Title).Take(limit).ToListAsync();
    }

    // GET films/{id}
    [HttpGet("{id:int:min(1)}")]
    public async Task<ActionResult<Film>> GetFilm(short id)
    {
        var film = await _context.Films.FindAsync(id);

        if (film is null)
        {
            return NotFound();
        }

        return film;
    }

    // GET films/{id}/actors
    [HttpGet("{id:int:min(1)}/actors")]
    public async Task<ActionResult<IEnumerable<Actor>>> GetFilmCast(short id)
    {
        var film = await _context.Films.FindAsync(id);

        if (film is null)
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

    // GET films/{id}/genre
    [HttpGet("{id:int:min(1)}/genre")]
    public async Task<ActionResult<Category>> GetFilmGenre(short id)
    {
        var film = await _context.Films.FindAsync(id);

        if (film is null)
        {
            return NotFound();
        }

        var query = from fc in _context.FilmCategories
                    join f in _context.Films on fc.FilmId equals f.FilmId
                    join c in _context.Categories on fc.CategoryId equals c.CategoryId
                    where f.FilmId.Equals(id)
                    select c;

        var genre = await query.SingleAsync();

        return genre;
    }
}