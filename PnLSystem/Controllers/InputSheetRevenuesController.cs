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
    public class InputSheetRevenuesController : ControllerBase
    {
        private readonly PnL1Context _context;

        public InputSheetRevenuesController(PnL1Context context)
        {
            _context = context;
        }

        // GET: api/InputSheetRevenues
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InputSheetRevenue>>> GetInputSheetRevenues()
        {
            return await _context.InputSheetRevenues.ToListAsync();
        }

        // GET: api/InputSheetRevenues/5
        [HttpGet("{id}")]
        public async Task<ActionResult<InputSheetRevenue>> GetInputSheetRevenue(int id)
        {
            var inputSheetRevenue = await _context.InputSheetRevenues.FindAsync(id);

            if (inputSheetRevenue == null)
            {
                return NotFound();
            }

            return inputSheetRevenue;
        }

        // PUT: api/InputSheetRevenues/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInputSheetRevenue(int id, InputSheetRevenue inputSheetRevenue)
        {
            if (id != inputSheetRevenue.Id)
            {
                return BadRequest();
            }

            _context.Entry(inputSheetRevenue).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InputSheetRevenueExists(id))
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

        // POST: api/InputSheetRevenues
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<InputSheetRevenue>> PostInputSheetRevenue(InputSheetRevenue inputSheetRevenue)
        {
            _context.InputSheetRevenues.Add(inputSheetRevenue);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInputSheetRevenue", new { id = inputSheetRevenue.Id }, inputSheetRevenue);
        }

        // DELETE: api/InputSheetRevenues/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInputSheetRevenue(int id)
        {
            var inputSheetRevenue = await _context.InputSheetRevenues.FindAsync(id);
            if (inputSheetRevenue == null)
            {
                return NotFound();
            }

            _context.InputSheetRevenues.Remove(inputSheetRevenue);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool InputSheetRevenueExists(int id)
        {
            return _context.InputSheetRevenues.Any(e => e.Id == id);
        }
    }
}
