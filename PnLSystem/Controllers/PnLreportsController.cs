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
    public class PnLreportsController : ControllerBase
    {
        private readonly PnL1Context _context;

        public PnLreportsController(PnL1Context context)
        {
            _context = context;
        }

        // GET: api/PnLreports
        [HttpGet]
        public async Task<ActionResult<BasePagingModel<IEnumerable<PnLreport>>>> GetPnLreports([FromQuery] UserSearchModel searchModel, [FromQuery] PagingModel paging)
        {
            if (searchModel is null)
            {
                throw new ArgumentNullException(nameof(searchModel));
            }

            try
            {
                paging = PnLSystem.Utils.PagingUtil.checkDefaultPaging(paging);
                var list = await _context.PnLreports.Where(o => o.EndDate.ToString().Contains(searchModel.SearchTerm) || o.StartDate.ToString().Contains(searchModel.SearchTerm) || o.UpdateDate.ToString().Contains(searchModel.SearchTerm) || o.CreationDate.ToString().Contains(searchModel.SearchTerm) || o.Store.Name.ToString().Contains(searchModel.SearchTerm) || o.Brand.Name.ToString().Contains(searchModel.SearchTerm)).ToListAsync();
                if (searchModel.SearchTerm == "")
                {
                    list = await _context.PnLreports.ToListAsync();
                }
                int totalItem = list.ToList().Count;
                list = list.Skip((paging.PageIndex - 1) * paging.PageSize)
                    .Take(paging.PageSize).ToList();

                var list1 = new List<ResponseDTOs.PnLreport>();

                foreach (var i in list)
                {
                    list1.Add(new ResponseDTOs.PnLreport()
                    {
                        Id = i.Id,
                        StoreId= i.StoreId,
                        StartDate= i.StartDate,
                        BrandId=i.BrandId,
                        CreationDate= i.CreationDate,
                        EndDate= i.EndDate,
                        TotalLost= i.TotalLost,
                        TotalProfit= i.TotalProfit,
                        UpdateDate= i.UpdateDate
                    });
                }

                var groupUserResult = new BasePagingModel<ResponseDTOs.PnLreport>()
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

        // GET: api/PnLreports/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PnLreport>> GetPnLreport(int id)
        {
            var pnLreport = await _context.PnLreports.FindAsync(id);

            if (pnLreport == null)
            {
                return NotFound();
            }

            return pnLreport;
        }

        // PUT: api/PnLreports/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPnLreport(int id, PnLreport pnLreport)
        {
            if (id != pnLreport.Id)
            {
                return BadRequest();
            }

            _context.Entry(pnLreport).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PnLreportExists(id))
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

        // POST: api/PnLreports
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PnLreport>> PostPnLreport(PnLreport pnLreport)
        {
            _context.PnLreports.Add(pnLreport);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPnLreport", new { id = pnLreport.Id }, pnLreport);
        }

        // DELETE: api/PnLreports/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePnLreport(int id)
        {
            var pnLreport = await _context.PnLreports.FindAsync(id);
            if (pnLreport == null)
            {
                return NotFound();
            }

            _context.PnLreports.Remove(pnLreport);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PnLreportExists(int id)
        {
            return _context.PnLreports.Any(e => e.Id == id);
        }
    }
}
