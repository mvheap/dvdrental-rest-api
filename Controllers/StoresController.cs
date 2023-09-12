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

    // GET stores/{id}/staff

}