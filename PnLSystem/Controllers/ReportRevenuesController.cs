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
        public async Task<ActionResult<BasePagingModel<IEnumerable<ReportRevenue>>>> GetReportRevenues([FromQuery] UserSearchModel searchModel, [FromQuery] PagingModel paging)
        {
            if (searchModel is null)
            {
                throw new ArgumentNullException(nameof(searchModel));
            }

            try
            {
                paging = PnLSystem.Utils.PagingUtil.checkDefaultPaging(paging);
                var list = await _context.ReportRevenues.Where(o => o.CreationDate.ToString().Contains(searchModel.SearchTerm) || o.Name.ToString().Contains(searchModel.SearchTerm) || o.Sheet.Brand.ToString().Contains(searchModel.SearchTerm) || o.Value.ToString().Contains(searchModel.SearchTerm)).ToListAsync();
                if (searchModel.SearchTerm == "")
                {
                    list = await _context.ReportRevenues.ToListAsync();
                }
                int totalItem = list.ToList().Count;
                list = list.Skip((paging.PageIndex - 1) * paging.PageSize)
                    .Take(paging.PageSize).ToList();

                var list1 = new List<ResponseDTOs.ReportRevenue>();

                foreach (var i in list)
                {
                    list1.Add(new ResponseDTOs.ReportRevenue()
                    {
                        Description = i.Description,
                        Id = i.Id,
                        Name = i.Name,
                        SheetId= i.SheetId,
                        CreationDate= i.CreationDate,
                        Value= i.Value
                    });
                }

                var groupUserResult = new BasePagingModel<ResponseDTOs.ReportRevenue>()
                {
                    PageIndex = paging.PageIndex,
                    PageSize = paging.PageSize,
                    TotalItem = totalItem,
                    TotalPage = (int)Math.Ceiling((decimal)totalItem / (decimal)paging.PageSize),
                    Data = list1
                };
                return Ok(groupUserResult);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(error: ex.Message);
            }
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
