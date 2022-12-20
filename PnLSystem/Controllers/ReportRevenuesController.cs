using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PnLSystem.Models;
using PnLSystem.ResponseDTOs.PagingModel;
using PnLSystem.ResponseDTOs.SearchModel;

namespace PnLSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportRevenuesController : ControllerBase
    {
        private readonly PnL1Context _context;

        public ReportRevenuesController(PnL1Context context)
        {
            _context = context;
        }

        // GET: api/ReportRevenues
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReportRevenue>>> GetReportRevenues([FromQuery] UserSearchModel searchModel, [FromQuery] PagingModel paging
        {
            return await _context.ReportRevenues.ToListAsync();
        }

        // GET: api/ReportRevenues/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReportRevenue>> GetReportRevenue(int id)
        {
            var reportRevenue = await _context.ReportRevenues.FindAsync(id);

            if (reportRevenue == null)
            {
                return NotFound();
            }

            return reportRevenue;
        }

        // PUT: api/ReportRevenues/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReportRevenue(int id, ReportRevenue reportRevenue)
        {
            if (id != reportRevenue.Id)
            {
                return BadRequest();
            }

            _context.Entry(reportRevenue).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReportRevenueExists(id))
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

        // POST: api/ReportRevenues
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ReportRevenue>> PostReportRevenue(ReportRevenue reportRevenue)
        {
            _context.ReportRevenues.Add(reportRevenue);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReportRevenue", new { id = reportRevenue.Id }, reportRevenue);
        }

        // DELETE: api/ReportRevenues/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReportRevenue(int id)
        {
            var reportRevenue = await _context.ReportRevenues.FindAsync(id);
            if (reportRevenue == null)
            {
                return NotFound();
            }

            _context.ReportRevenues.Remove(reportRevenue);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ReportRevenueExists(int id)
        {
            return _context.ReportRevenues.Any(e => e.Id == id);
        }
    }
}
