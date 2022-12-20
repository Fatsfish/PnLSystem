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
    public class InputSheetRevenuesController : ControllerBase
    {
        private readonly PnL1Context _context;

        public InputSheetRevenuesController(PnL1Context context)
        {
            _context = context;
        }

        // GET: api/InputSheetRevenues
        [HttpGet]
        public async Task<ActionResult<BasePagingModel<IEnumerable<InputSheetRevenue>>>> GetInputSheetRevenues([FromQuery] UserSearchModel searchModel, [FromQuery] PagingModel paging)
        {
            if (searchModel is null)
            {
                throw new ArgumentNullException(nameof(searchModel));
            }

            try
            {
                paging = PnLSystem.Utils.PagingUtil.checkDefaultPaging(paging);
                var list = await _context.InputSheetRevenues.ToListAsync();
                int totalItem = list.ToList().Count;
                list = list.Skip((paging.PageIndex - 1) * paging.PageSize)
                    .Take(paging.PageSize).ToList();

                var list1 = new List<ResponseDTOs.InputSheetRevenue>();

                foreach (var i in list)
                {
                    list1.Add(new ResponseDTOs.InputSheetRevenue()
                    {
                        Description = i.Description,
                        Id = i.Id,
                        Name = i.Name,
                        SheetId = i.SheetId,
                        CreationDate = i.CreationDate,
                        IsFinished = i.IsFinished,
                        ProductId = i.ProductId,
                        Type = i.Type,
                        Value = i.Value,
                        ImageLink = i.ImageLink
                    });
                }

                var groupUserResult = new BasePagingModel<ResponseDTOs.InputSheetRevenue>()
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
