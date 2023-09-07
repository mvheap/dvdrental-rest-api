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
}