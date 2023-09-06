using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using dvd_rental_api.Models;

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
    public async Task<ActionResult<IEnumerable<Film>>> GetFilms(int limit = 200, int year = 0)
    {
        if (year != 0)
        {
            return await _context.Films.Where(f => f.ReleaseYear == year).OrderBy(f => f.Title).ToListAsync();
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
    [HttpGet("{id}/actors")]
    public async Task<ActionResult<IEnumerable<Actor>>> GetFilmActors(short id)
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