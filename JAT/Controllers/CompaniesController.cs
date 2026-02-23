using Microsoft.AspNetCore.Mvc;
using JAT.Data;
using JAT.Models;
using Microsoft.EntityFrameworkCore;

namespace JAT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public CompaniesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Company>>> GetAll()
        {
            return await _context.Companies.ToListAsync();
        }

        [HttpGet("status/{Status}")]
        public async Task<ActionResult<IEnumerable<Company>>> GetByStatus(Status Status)
        {
            return await _context.Companies.Where(c => c.Status == Status).ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Company>> Create([FromBody] Company company)
        {
            try
            {
                company.AppliedDate = DateTime.UtcNow;
                company.Status = Status.Pending;
                _context.Companies.Add(company);
                await _context.SaveChangesAsync();
                return Ok(company);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] Status status)
        {
            var application = await _context.Companies.FindAsync(id);
            if (application == null)
            {
                return NotFound();
            }
            application.Status = status;
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
