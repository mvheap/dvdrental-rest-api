using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using dvd_rental_api.Models;

namespace dvd_rental_api.Controllers;

[Route("[controller]")]
[ApiController]
public class StoresController : ControllerBase
{
    private readonly DvdrentalContext _context;

    public StoresController(DvdrentalContext context)
    {
        _context = context;
    }

    // GET stores/
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Store>>> GetStores(int limit = 200)
    {
        return await _context.Stores.OrderBy(s => s.StoreId).Take(limit).ToListAsync();
    }

    // GET stores/{id}/staff

}