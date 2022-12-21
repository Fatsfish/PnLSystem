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
    public class StoreGroupsController : ControllerBase
    {
        private readonly PnL1Context _context;

        public StoreGroupsController(PnL1Context context)
        {
            _context = context;
        }

        // GET: api/StoreGroups
        [HttpGet]
        public async Task<ActionResult<BasePagingModel<IEnumerable<StoreGroup>>>> GetStoreGroups([FromQuery] UserSearchModel searchModel, [FromQuery] PagingModel paging)
        {
            if (searchModel is null)
            {
                throw new ArgumentNullException(nameof(searchModel));
            }

            try
            {
                paging = PnLSystem.Utils.PagingUtil.checkDefaultPaging(paging);
                var list = await _context.StoreGroups.Where(o => o.User.Email.ToString().Contains(searchModel.SearchTerm) || o.User.DisplayName.ToString().Contains(searchModel.SearchTerm) || o.Group.Name.ToString().Contains(searchModel.SearchTerm)|| o.IsAdmin.ToString().Contains(searchModel.SearchTerm)).ToListAsync();
                if (searchModel.SearchTerm == "")
                {
                    list = await _context.StoreGroups.ToListAsync();
                }
                int totalItem = list.ToList().Count;
                list = list.Skip((paging.PageIndex - 1) * paging.PageSize)
                    .Take(paging.PageSize).ToList();

                var list1 = new List<ResponseDTOs.StoreGroup>();

                foreach (var i in list)
                {
                    list1.Add(new ResponseDTOs.StoreGroup()
                    {
                        Id = i.Id,
                        IsAdmin= i.IsAdmin,
                        GroupId= i.Id,
                        UserId= i.UserId
                    });
                }

                var groupUserResult = new BasePagingModel<ResponseDTOs.StoreGroup>()
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

        // GET: api/StoreGroups/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StoreGroup>> GetStoreGroup(int id)
        {
            var storeGroup = await _context.StoreGroups.FindAsync(id);

            if (storeGroup == null)
            {
                return NotFound();
            }

            return storeGroup;
        }

        // PUT: api/StoreGroups/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStoreGroup(int id, StoreGroup storeGroup)
        {
            if (id != storeGroup.Id)
            {
                return BadRequest();
            }

            _context.Entry(storeGroup).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StoreGroupExists(id))
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

        // POST: api/StoreGroups
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<StoreGroup>> PostStoreGroup(StoreGroup storeGroup)
        {
            _context.StoreGroups.Add(storeGroup);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStoreGroup", new { id = storeGroup.Id }, storeGroup);
        }

        // DELETE: api/StoreGroups/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStoreGroup(int id)
        {
            var storeGroup = await _context.StoreGroups.FindAsync(id);
            if (storeGroup == null)
            {
                return NotFound();
            }

            _context.StoreGroups.Remove(storeGroup);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StoreGroupExists(int id)
        {
            return _context.StoreGroups.Any(e => e.Id == id);
        }
    }
}
