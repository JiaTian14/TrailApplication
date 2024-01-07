using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrailsController : ControllerBase
    {
        private readonly Comp2001malTjiatianContext _context;

        public TrailsController(Comp2001malTjiatianContext context)
        {
            _context = context;
        }

        // GET: api/Trails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Trail>>> GetTrails()
        {
            return await _context.Trails.ToListAsync();
        }

        // GET: api/Trails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Trail>> GetTrail(int id)
        {
            var trail = await _context.Trails.FindAsync(id);

            if (trail == null)
            {
                return NotFound();
            }

            return trail;
        }

        // PUT: api/Trails/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTrail(int id, Trail trail)
        {
            if (id != trail.TrailId)
            {
                return BadRequest();
            }

            _context.Entry(trail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TrailExists(id))
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

        // POST: api/Trails
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Trail>> PostTrail(Trail trail)
        {
            _context.Trails.Add(trail);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTrail", new { id = trail.TrailId }, trail);
        }

        // DELETE: api/Trails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrail(int id)
        {
            var trail = await _context.Trails.FindAsync(id);
            if (trail == null)
            {
                return NotFound();
            }

            _context.Trails.Remove(trail);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TrailExists(int id)
        {
            return _context.Trails.Any(e => e.TrailId == id);
        }
    }
}
