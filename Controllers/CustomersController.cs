using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using dvd_rental_api.Models;

namespace dvd_rental_api.Controllers;

[Route("[controller]")]
[ApiController]
public class CustomersController : ControllerBase
{
    private readonly DvdrentalContext _context;

    public CustomersController(DvdrentalContext context)
    {
        _context = context;
    }

    // GET customers/
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers(
        int limit = 200
    )
    {
        return await _context.Customers.OrderBy(c => c.FirstName).Take(limit).ToListAsync();
    }
}