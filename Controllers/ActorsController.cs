using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using dvd_rental_api.Models;

namespace dvd_rental_api.Controllers;

[Route("[controller]")]
[ApiController]
public class ActorsController : ControllerBase
{
    private readonly DvdrentalContext _context;

    public ActorsController(DvdrentalContext context)
    {
        _context = context;
    }

    // GET actors/
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Actor>>> GetActors(int limit = 200)
    {
        return await _context.Actors.OrderBy(a => a.FirstName).Take(limit).ToListAsync();
    }

    // GET actors/{id}
    [HttpGet("{id:int:min(1)}")]
    public async Task<ActionResult<Actor>> GetActor(short id)
    {
        var actor = await _context.Actors.FindAsync(id);

        if (actor == null)
        {
            return NotFound();
        }

        return actor;
    }

    // GET actors/{id}/films
    [HttpGet("{id:int:min(1)}/films")]
    public async Task<ActionResult<IEnumerable<Film>>> GetMoviesOfActor(short id)
    {
        var actor = await _context.Actors.FindAsync(id);

        if (actor == null)
        {
            return NotFound();
        }

        var query = from a in _context.Actors
                    join fa in _context.FilmActors on a.ActorId equals fa.ActorId
                    join f in _context.Films on fa.FilmId equals f.FilmId
                    where a.FirstName.Equals(actor.FirstName) && a.LastName.Equals(actor.LastName)
                    orderby f.Title ascending
                    select f;

        var films = await query.ToListAsync();
        return films;
    }
}