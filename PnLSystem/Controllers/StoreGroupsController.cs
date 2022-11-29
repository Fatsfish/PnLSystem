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
    public class StoreGroupsController : ControllerBase
    {
        private readonly PnL1Context _context;

        public StoreGroupsController(PnL1Context context)
        {
            _context = context;
        }

        // GET: api/StoreGroups
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StoreGroup>>> GetStoreGroups()
        {
            return await _context.StoreGroups.ToListAsync();
        }

        // GET: api/StoreGroups/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StoreGroup>> GetStoreGroup(int id)
        {
            var storeGroup = await _context.StoreGroups.FindAsync(id);

            if (storeGroup == null)
            {
                return NotFound();
            }

            return storeGroup;
        }

        // PUT: api/StoreGroups/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStoreGroup(int id, StoreGroup storeGroup)
        {
            if (id != storeGroup.Id)
            {
                return BadRequest();
            }

            _context.Entry(storeGroup).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StoreGroupExists(id))
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

        // POST: api/StoreGroups
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<StoreGroup>> PostStoreGroup(StoreGroup storeGroup)
        {
            _context.StoreGroups.Add(storeGroup);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStoreGroup", new { id = storeGroup.Id }, storeGroup);
        }

        // DELETE: api/StoreGroups/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStoreGroup(int id)
        {
            var storeGroup = await _context.StoreGroups.FindAsync(id);
            if (storeGroup == null)
            {
                return NotFound();
            }

            _context.StoreGroups.Remove(storeGroup);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StoreGroupExists(int id)
        {
            return _context.StoreGroups.Any(e => e.Id == id);
        }
    }
}
