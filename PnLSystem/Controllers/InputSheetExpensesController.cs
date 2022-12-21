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
    public class InputSheetExpensesController : ControllerBase
    {
        private readonly PnL1Context _context;

        public InputSheetExpensesController(PnL1Context context)
        {
            _context = context;
        }

        // GET: api/InputSheetExpenses
        [HttpGet]
        public async Task<ActionResult<BasePagingModel<IEnumerable<InputSheetExpense>>>> GetInputSheetExpenses([FromQuery] UserSearchModel searchModel, [FromQuery] PagingModel paging)
        {
            if (searchModel is null)
            {
                throw new ArgumentNullException(nameof(searchModel));
            }

            try
            {
                paging = PnLSystem.Utils.PagingUtil.checkDefaultPaging(paging);
                var list = await _context.InputSheetExpenses.Where(o => o.CreationDate.ToString().Contains(searchModel.SearchTerm)
                || o.Name.ToString().Contains(searchModel.SearchTerm) ||
                o.SheetId.ToString().Contains(searchModel.SearchTerm) ||
                o.StockId.ToString().Contains(searchModel.SearchTerm) ||
                o.IsFinished.ToString().Contains(searchModel.SearchTerm) ||
                o.Type.ToString().Contains(searchModel.SearchTerm) ||
                o.Description.ToString().Contains(searchModel.SearchTerm) ||
                o.Id.ToString().Equals(searchModel.SearchTerm) ||
                o.Value.ToString().Equals(searchModel.SearchTerm)).OrderByDescending(o => o.CreationDate).ToListAsync();
                if (searchModel.SearchTerm == "" || searchModel.SearchTerm == null)
                {
                    list = await _context.InputSheetExpenses.ToListAsync();
                }
                int totalItem = list.ToList().Count;
                list = list.Skip((paging.PageIndex - 1) * paging.PageSize)
                    .Take(paging.PageSize).ToList();

                var list1 = new List<ResponseDTOs.InputSheetExpense>();

                foreach (var i in list)
                {
                    list1.Add(new ResponseDTOs.InputSheetExpense()
                    {
                        Description = i.Description,
                        Id = i.Id,
                        Name = i.Name,
                        SheetId= i.SheetId,
                        StockId= i.StockId,
                        CreationDate= i.CreationDate,
                        IsFinished= i.IsFinished,
                        Type= i.Type,
                        Value= i.Value,
                        ImageLink = i.ImageLink
                    });
                }

                var groupUserResult = new BasePagingModel<ResponseDTOs.InputSheetExpense>()
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
