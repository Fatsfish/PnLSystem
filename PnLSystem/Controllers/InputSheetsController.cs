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
    public class InputSheetsController : ControllerBase
    {
        private readonly PnL1Context _context;

        public InputSheetsController(PnL1Context context)
        {
            _context = context;
        }

        // GET: api/InputSheets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InputSheet>>> GetInputSheets()
        {
            return await _context.InputSheets.ToListAsync();
        }

        // GET: api/InputSheets/5
        [HttpGet("{id}")]
        public async Task<ActionResult<InputSheet>> GetInputSheet(int id)
        {
            var inputSheet = await _context.InputSheets.FindAsync(id);

            if (inputSheet == null)
            {
                return NotFound();
            }

            return inputSheet;
        }

        // PUT: api/InputSheets/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInputSheet(int id, InputSheet inputSheet)
        {
            if (id != inputSheet.Id)
            {
                return BadRequest();
            }

            _context.Entry(inputSheet).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InputSheetExists(id))
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

        // POST: api/InputSheets
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<InputSheet>> PostInputSheet(InputSheet inputSheet)
        {
            _context.InputSheets.Add(inputSheet);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInputSheet", new { id = inputSheet.Id }, inputSheet);
        }

        // DELETE: api/InputSheets/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInputSheet(int id)
        {
            var inputSheet = await _context.InputSheets.FindAsync(id);
            if (inputSheet == null)
            {
                return NotFound();
            }

            _context.InputSheets.Remove(inputSheet);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool InputSheetExists(int id)
        {
            return _context.InputSheets.Any(e => e.Id == id);
        }
    }
}
