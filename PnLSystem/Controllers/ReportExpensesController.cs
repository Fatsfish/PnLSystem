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
    public class ReportExpensesController : ControllerBase
    {
        private readonly PnL1Context _context;

        public ReportExpensesController(PnL1Context context)
        {
            _context = context;
        }

        // GET: api/ReportExpenses
        [HttpGet]
        public async Task<ActionResult<BasePagingModel<IEnumerable<ReportExpense>>>> GetReportExpenses([FromQuery] UserSearchModel searchModel, [FromQuery] PagingModel paging)
        {
            if (searchModel is null)
            {
                throw new ArgumentNullException(nameof(searchModel));
            }

            try
            {
                paging = PnLSystem.Utils.PagingUtil.checkDefaultPaging(paging);
                var list = await _context.ReportExpenses.Where(o => o.CreationDate.ToString().Contains(searchModel.SearchTerm) || o.Name.ToString().Contains(searchModel.SearchTerm) || o.Sheet.Brand.ToString().Contains(searchModel.SearchTerm) || o.Value.ToString().Contains(searchModel.SearchTerm)).ToListAsync();
                if (searchModel.SearchTerm == "")
                {
                    list = await _context.ReportExpenses.ToListAsync();
                }
                int totalItem = list.ToList().Count;
                list = list.Skip((paging.PageIndex - 1) * paging.PageSize)
                    .Take(paging.PageSize).ToList();

                var list1 = new List<ResponseDTOs.ReportExpense>();

                foreach (var i in list)
                {
                    list1.Add(new ResponseDTOs.ReportExpense()
                    {
                        Description = i.Description,
                        Id = i.Id,
                        Name = i.Name,
                        SheetId = i.SheetId,
                        CreationDate= i.CreationDate,
                        Value= i.Value
                    });
                }

                var groupUserResult = new BasePagingModel<ResponseDTOs.ReportExpense>()
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
