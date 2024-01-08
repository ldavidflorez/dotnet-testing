using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MyApp.Namespace
{
    [Route("api/[controller]")]
    [ApiController]
    public class BeersController : ControllerBase
    {
        private readonly StoreContex _context; // Replace YourDbContext with the actual name of your DbContext

        public BeersController(StoreContex context)
        {
            _context = context;
        }

        // GET: api/beers
        [HttpGet]
        public ActionResult<IEnumerable<Beer>> GetBeers()
        {
            return _context.Beers.Include(b => b.Brand).ToList();
        }
    }
}
