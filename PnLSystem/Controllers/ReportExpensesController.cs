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
    public class ReportExpensesController : ControllerBase
    {
        private readonly PnL1Context _context;

        public ReportExpensesController(PnL1Context context)
        {
            _context = context;
        }

        // GET: api/ReportExpenses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReportExpense>>> GetReportExpenses()
        {
            return await _context.ReportExpenses.ToListAsync();
        }

        // GET: api/ReportExpenses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReportExpense>> GetReportExpense(int id)
        {
            var reportExpense = await _context.ReportExpenses.FindAsync(id);

            if (reportExpense == null)
            {
                return NotFound();
            }

            return reportExpense;
        }

        // PUT: api/ReportExpenses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReportExpense(int id, ReportExpense reportExpense)
        {
            if (id != reportExpense.Id)
            {
                return BadRequest();
            }

            _context.Entry(reportExpense).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReportExpenseExists(id))
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

        // POST: api/ReportExpenses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ReportExpense>> PostReportExpense(ReportExpense reportExpense)
        {
            _context.ReportExpenses.Add(reportExpense);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReportExpense", new { id = reportExpense.Id }, reportExpense);
        }

        // DELETE: api/ReportExpenses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReportExpense(int id)
        {
            var reportExpense = await _context.ReportExpenses.FindAsync(id);
            if (reportExpense == null)
            {
                return NotFound();
            }

            _context.ReportExpenses.Remove(reportExpense);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ReportExpenseExists(int id)
        {
            return _context.ReportExpenses.Any(e => e.Id == id);
        }
    }
}
