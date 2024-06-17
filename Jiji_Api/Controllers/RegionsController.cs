using Jiji_Api.Data;
using Jiji_Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Jiji_Api.Controllers
{
    
    
        [Route("api/[controller]")]
        [ApiController]
        public class RegionsController : ControllerBase
        {
            private readonly JijiDbContext _context;

            public RegionsController(JijiDbContext context)
            {
                _context = context;
            }

            // GET: api/Regions
            [HttpGet]
            public async Task<ActionResult<IEnumerable<Regions>>> GetRegions()
            {
                return await _context.Regions.ToListAsync();
            }

            // GET: api/Regions/5
            [HttpGet("{id}")]
            public async Task<ActionResult<Regions>> GetRegion(int id)
            {
                var region = await _context.Regions.FindAsync(id);

                if (region == null)
                {
                    return NotFound();
                }

                return region;
            }

            // PUT: api/Regions/5
            [HttpPut("{id}")]
            public async Task<IActionResult> PutRegion(int id, Regions region)
            {
                if (id != region.Id)
                {
                    return BadRequest();
                }

                _context.Entry(region).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RegionExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return NoContent();
            }

            // POST: api/Regions
            [HttpPost]
            public async Task<ActionResult<Regions>> PostRegion(Regions region)
            {
                _context.Regions.Add(region);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetRegion), new { id = region.Id }, region);
            }

            // DELETE: api/Regions/5
            [HttpDelete("{id}")]
            public async Task<IActionResult> DeleteRegion(int id)
            {
                var region = await _context.Regions.FindAsync(id);
                if (region == null)
                {
                    return NotFound();
                }

                _context.Regions.Remove(region);
                await _context.SaveChangesAsync();

                return NoContent();
            }

            private bool RegionExists(int id)
            {
                return _context.Regions.Any(e => e.Id == id);
            }

        }
}



