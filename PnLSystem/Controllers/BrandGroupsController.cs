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
    public class BrandGroupsController : ControllerBase
    {
        private readonly PnL1Context _context;

        public BrandGroupsController(PnL1Context context)
        {
            _context = context;
        }

        // GET: api/BrandGroups
        [HttpGet]
        public async Task<ActionResult<BasePagingModel<IEnumerable<BrandGroup>>>> GetBrandGroups([FromQuery] UserSearchModel searchModel, [FromQuery] PagingModel paging)
        {
            if (searchModel is null)
            {
                throw new ArgumentNullException(nameof(searchModel));
            }

            try
            {
                paging = PnLSystem.Utils.PagingUtil.checkDefaultPaging(paging);
                var list = await _context.BrandGroups.Where(o => o.GroupId.ToString().Contains(searchModel.SearchTerm) ||
                o.UserId.ToString().Contains(searchModel.SearchTerm)||
                o.Id.ToString().Contains(searchModel.SearchTerm)).OrderByDescending(o=> o.Id).ToListAsync();
                if (searchModel.SearchTerm == "" || searchModel.SearchTerm == null)
                {
                    list = await _context.BrandGroups.ToListAsync();
                }
                int totalItem = list.ToList().Count;
                list = list.Skip((paging.PageIndex - 1) * paging.PageSize)
                    .Take(paging.PageSize).ToList();

                var list1 = new List<ResponseDTOs.BrandGroup>();

                foreach (var i in list)
                {
                    list1.Add(new ResponseDTOs.BrandGroup()
                    {
                        Id = i.Id,
                        GroupId = i.GroupId,
                        IsAdmin = i.IsAdmin,
                        UserId = i.UserId
                    });
                }

                var groupUserResult = new BasePagingModel<ResponseDTOs.BrandGroup>()
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

        // GET: api/BrandGroups/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BrandGroup>> GetBrandGroup(int id)
        {
            var brandGroup = await _context.BrandGroups.FindAsync(id);

            if (brandGroup == null)
            {
                return NotFound();
            }

            return brandGroup;
        }

        // PUT: api/BrandGroups/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBrandGroup(int id, BrandGroup brandGroup)
        {
            if (id != brandGroup.Id)
            {
                return BadRequest();
            }

            _context.Entry(brandGroup).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BrandGroupExists(id))
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

        // POST: api/BrandGroups
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BrandGroup>> PostBrandGroup(BrandGroup brandGroup)
        {
            _context.BrandGroups.Add(brandGroup);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBrandGroup", new { id = brandGroup.Id }, brandGroup);
        }

        // DELETE: api/BrandGroups/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBrandGroup(int id)
        {
            var brandGroup = await _context.BrandGroups.FindAsync(id);
            if (brandGroup == null)
            {
                return NotFound();
            }

            _context.BrandGroups.Remove(brandGroup);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BrandGroupExists(int id)
        {
            return _context.BrandGroups.Any(e => e.Id == id);
        }
    }
}
