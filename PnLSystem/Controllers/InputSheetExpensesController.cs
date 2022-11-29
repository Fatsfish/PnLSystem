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
    public class InputSheetExpensesController : ControllerBase
    {
        private readonly PnL1Context _context;

        public InputSheetExpensesController(PnL1Context context)
        {
            _context = context;
        }

        // GET: api/InputSheetExpenses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InputSheetExpense>>> GetInputSheetExpenses()
        {
            return await _context.InputSheetExpenses.ToListAsync();
        }

        // GET: api/InputSheetExpenses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<InputSheetExpense>> GetInputSheetExpense(int id)
        {
            var inputSheetExpense = await _context.InputSheetExpenses.FindAsync(id);

            if (inputSheetExpense == null)
            {
                return NotFound();
            }

            return inputSheetExpense;
        }

        // PUT: api/InputSheetExpenses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInputSheetExpense(int id, InputSheetExpense inputSheetExpense)
        {
            if (id != inputSheetExpense.Id)
            {
                return BadRequest();
            }

            _context.Entry(inputSheetExpense).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InputSheetExpenseExists(id))
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

        // POST: api/InputSheetExpenses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<InputSheetExpense>> PostInputSheetExpense(InputSheetExpense inputSheetExpense)
        {
            _context.InputSheetExpenses.Add(inputSheetExpense);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInputSheetExpense", new { id = inputSheetExpense.Id }, inputSheetExpense);
        }

        // DELETE: api/InputSheetExpenses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInputSheetExpense(int id)
        {
            var inputSheetExpense = await _context.InputSheetExpenses.FindAsync(id);
            if (inputSheetExpense == null)
            {
                return NotFound();
            }

            _context.InputSheetExpenses.Remove(inputSheetExpense);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool InputSheetExpenseExists(int id)
        {
            return _context.InputSheetExpenses.Any(e => e.Id == id);
        }
    }
}
