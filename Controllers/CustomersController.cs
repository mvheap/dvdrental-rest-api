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
        int limit = 200,
        string email = null
    )
    {
        IQueryable<Customer> currentQueried = _context.Customers;

        if (email is not null)
        {
            currentQueried = currentQueried.Where(c => c.Email == email).Take(limit);
        }

        return await currentQueried.OrderBy(c => c.FirstName).Take(limit).ToListAsync();
    }

    // GET customers/{id}
    [HttpGet("{id:int:min(1)}")]
    public async Task<ActionResult<Customer>> GetCustomerById(short id)
    {
        var customer = await _context.Customers.FindAsync(id);

        if (customer is null)
        {
            return NotFound();
        }

        return customer;
    }

    // GET customers/{id}/rental
    [HttpGet("{id:int:min(1)}/rental")]
    public async Task<ActionResult<IEnumerable<Film>>> GetRentedFilms(short id)
    {
        var customer = await _context.Customers.FindAsync(id);

        if (customer is null)
        {
            return NotFound();
        }

        var query = from c in _context.Customers
                    join r in _context.Rentals on c.CustomerId equals r.CustomerId
                    join i in _context.Inventories on r.InventoryId equals i.InventoryId
                    join f in _context.Films on i.FilmId equals f.FilmId
                    where c.CustomerId.Equals(id)
                    orderby f.Title ascending
                    select f;

        var films = await query.ToListAsync();
        return films;
    }
}