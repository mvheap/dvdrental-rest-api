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

    // GET films/{id}
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
}