using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PnLSystem.Models;

namespace PnLSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatiosController : ControllerBase
    {
        private readonly PnL1Context _context;

        public RatiosController(PnL1Context context)
        {
            _context = context;
        }

        // GET: api/Ratios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ratio>>> GetRatios()
        {
            return await _context.Ratios.ToListAsync();
        }

        // GET: api/Ratios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Ratio>> GetRatio(int id)
        {
            var ratio = await _context.Ratios.FindAsync(id);

            if (ratio == null)
            {
                return NotFound();
            }

            return ratio;
        }

        // PUT: api/Ratios/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRatio(int id, Ratio ratio)
        {
            if (id != ratio.Id)
            {
                return BadRequest();
            }

            _context.Entry(ratio).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RatioExists(id))
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

        // POST: api/Ratios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Ratio>> PostRatio(Ratio ratio)
        {
            _context.Ratios.Add(ratio);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRatio", new { id = ratio.Id }, ratio);
        }

        // DELETE: api/Ratios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRatio(int id)
        {
            var ratio = await _context.Ratios.FindAsync(id);
            if (ratio == null)
            {
                return NotFound();
            }

            _context.Ratios.Remove(ratio);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RatioExists(int id)
        {
            return _context.Ratios.Any(e => e.Id == id);
        }
    }
}
