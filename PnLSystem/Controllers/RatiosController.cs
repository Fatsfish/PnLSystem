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
    public class RatiosController : ControllerBase
    {
        private readonly PnL1Context _context;

        public RatiosController(PnL1Context context)
        {
            _context = context;
        }

        // GET: api/Ratios
        [HttpGet]
        public async Task<ActionResult<BasePagingModel<IEnumerable<Ratio>>>> GetRatios([FromQuery] UserSearchModel searchModel, [FromQuery] PagingModel paging)
        {
            if (searchModel is null)
            {
                throw new ArgumentNullException(nameof(searchModel));
            }

            try
            {
                paging = PnLSystem.Utils.PagingUtil.checkDefaultPaging(paging);
                var list = await _context.Ratios.ToListAsync();
                int totalItem = list.ToList().Count;
                list = list.Skip((paging.PageIndex - 1) * paging.PageSize)
                    .Take(paging.PageSize).ToList();

                var list1 = new List<ResponseDTOs.Ratio>();

                foreach (var i in list)
                {
                    list1.Add(new ResponseDTOs.Ratio()
                    {
                        Id = i.Id,
                        StockId= i.StockId,
                        CreationDate= i.CreationDate,
                        IsActive= i.IsActive,
                        ProductId= i.ProductId,
                        Value= i.Value
                    });
                }

                var groupUserResult = new BasePagingModel<ResponseDTOs.Ratio>()
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

        // GET: api/Ratios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Ratio>> GetRatio(int id)
        {
            var ratio = await _context.Ratios.FindAsync(id);

            if (ratio == null)
            {
                return NotFound();
            }

            return ratio;
        }

        // PUT: api/Ratios/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRatio(int id, Ratio ratio)
        {
            if (id != ratio.Id)
            {
                return BadRequest();
            }

            _context.Entry(ratio).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RatioExists(id))
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

        // POST: api/Ratios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Ratio>> PostRatio(Ratio ratio)
        {
            _context.Ratios.Add(ratio);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRatio", new { id = ratio.Id }, ratio);
        }

        // DELETE: api/Ratios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRatio(int id)
        {
            var ratio = await _context.Ratios.FindAsync(id);
            if (ratio == null)
            {
                return NotFound();
            }

            _context.Ratios.Remove(ratio);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RatioExists(int id)
        {
            return _context.Ratios.Any(e => e.Id == id);
        }
    }
}
