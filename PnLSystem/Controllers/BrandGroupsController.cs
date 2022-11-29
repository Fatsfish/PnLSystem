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
    public class BrandGroupsController : ControllerBase
    {
        private readonly PnL1Context _context;

        public BrandGroupsController(PnL1Context context)
        {
            _context = context;
        }

        // GET: api/BrandGroups
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BrandGroup>>> GetBrandGroups()
        {
            return await _context.BrandGroups.ToListAsync();
        }

        // GET: api/BrandGroups/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BrandGroup>> GetBrandGroup(int id)
        {
            var brandGroup = await _context.BrandGroups.FindAsync(id);

            if (brandGroup == null)
            {
                return NotFound();
            }

            return brandGroup;
        }

        // PUT: api/BrandGroups/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBrandGroup(int id, BrandGroup brandGroup)
        {
            if (id != brandGroup.Id)
            {
                return BadRequest();
            }

            _context.Entry(brandGroup).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BrandGroupExists(id))
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

        // POST: api/BrandGroups
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BrandGroup>> PostBrandGroup(BrandGroup brandGroup)
        {
            _context.BrandGroups.Add(brandGroup);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBrandGroup", new { id = brandGroup.Id }, brandGroup);
        }

        // DELETE: api/BrandGroups/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBrandGroup(int id)
        {
            var brandGroup = await _context.BrandGroups.FindAsync(id);
            if (brandGroup == null)
            {
                return NotFound();
            }

            _context.BrandGroups.Remove(brandGroup);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BrandGroupExists(int id)
        {
            return _context.BrandGroups.Any(e => e.Id == id);
        }
    }
}
