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
    public class HelpRequestsController : ControllerBase
    {
        private readonly PnL1Context _context;

        public HelpRequestsController(PnL1Context context)
        {
            _context = context;
        }

        // GET: api/HelpRequests
        [HttpGet]
        public async Task<ActionResult<BasePagingModel<IEnumerable<HelpRequest>>>> GetHelpRequests([FromQuery] UserSearchModel searchModel, [FromQuery] PagingModel paging)
        {
            if (searchModel is null)
            {
                throw new ArgumentNullException(nameof(searchModel));
            }

            try
            {
                paging = PnLSystem.Utils.PagingUtil.checkDefaultPaging(paging);
                var list = await _context.HelpRequests.Where(o => o.Name.ToString().Contains(searchModel.SearchTerm) || o.Description.ToString().Contains(searchModel.SearchTerm) || o.CreationDate.ToString().Contains(searchModel.SearchTerm) || o.CreationUser.Email.ToString().Contains(searchModel.SearchTerm) || o.CreationUser.DisplayName.ToString().Contains(searchModel.SearchTerm)).ToListAsync();
                if (searchModel.SearchTerm == "")
                {
                    list = await _context.HelpRequests.ToListAsync();
                }
                int totalItem = list.ToList().Count;
                list = list.Skip((paging.PageIndex - 1) * paging.PageSize)
                    .Take(paging.PageSize).ToList();

                var list1 = new List<ResponseDTOs.HelpRequest>();

                foreach (var i in list)
                {
                    list1.Add(new ResponseDTOs.HelpRequest()
                    {
                        Description = i.Description,
                        Id = i.Id,
                        IsDelete = i.IsDelete,
                        Name = i.Name,
                        Status= i.Status,
                        CreationDate = i.CreationDate,
                        CreationUserId = i.CreationUserId
                    });
                }

                var groupUserResult = new BasePagingModel<ResponseDTOs.HelpRequest>()
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

        // GET: api/HelpRequests/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HelpRequest>> GetHelpRequest(int id)
        {
            var helpRequest = await _context.HelpRequests.FindAsync(id);

            if (helpRequest == null)
            {
                return NotFound();
            }

            return helpRequest;
        }

        // PUT: api/HelpRequests/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHelpRequest(int id, HelpRequest helpRequest)
        {
            if (id != helpRequest.Id)
            {
                return BadRequest();
            }

            _context.Entry(helpRequest).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HelpRequestExists(id))
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

        // POST: api/HelpRequests
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<HelpRequest>> PostHelpRequest(HelpRequest helpRequest)
        {
            _context.HelpRequests.Add(helpRequest);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHelpRequest", new { id = helpRequest.Id }, helpRequest);
        }

        // DELETE: api/HelpRequests/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHelpRequest(int id)
        {
            var helpRequest = await _context.HelpRequests.FindAsync(id);
            if (helpRequest == null)
            {
                return NotFound();
            }

            _context.HelpRequests.Remove(helpRequest);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HelpRequestExists(int id)
        {
            return _context.HelpRequests.Any(e => e.Id == id);
        }
    }
}
