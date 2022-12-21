using System;
using System.Collections;
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
    public class UserRolesController : ControllerBase
    {
        private readonly PnL1Context _context;

        public UserRolesController(PnL1Context context)
        {
            _context = context;
        }

        // GET: api/UserRoles
        [HttpGet]
        public async Task<ActionResult<BasePagingModel<IEnumerable<UserRole>>>> GetUserRoles([FromQuery] UserSearchModel searchModel, [FromQuery] PagingModel paging)
        {
            if (searchModel is null)
            {
                throw new ArgumentNullException(nameof(searchModel));
            }

            try
            {
                paging = PnLSystem.Utils.PagingUtil.checkDefaultPaging(paging);
                var list = await _context.UserRoles.ToListAsync();
                int totalItem = list.ToList().Count;
                list = list.Skip((paging.PageIndex - 1) * paging.PageSize)
                    .Take(paging.PageSize).ToList();

                var list1 = new List<ResponseDTOs.UserRole>();

                foreach (var i in list)
                {
                    list1.Add(new ResponseDTOs.UserRole()
                    {
                        Id = i.Id,
                        UserId = i.UserId,
                        RoleId = i.RoleId
                    });
                }

                var groupUserResult = new BasePagingModel<ResponseDTOs.UserRole>()
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

        // GET: api/UserRoles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PnLSystem.ResponseDTOs.UserRole>> GetUserRole(int id)
        {
            var userRole = await _context.UserRoles.FindAsync(id);

            if (userRole == null)
            {
                return NotFound();
            }
            PnLSystem.ResponseDTOs.UserRole i = new ResponseDTOs.UserRole();
            i.Id = userRole.Id;
            i.UserId = userRole.UserId;
            i.RoleId = userRole.RoleId;
            return i;
        }

        // GET: api/UserRoles/5
        [HttpGet("Email")]
        public async Task<ActionResult<IEnumerable<string>>> GetUserRole(string email)
        {
            var user = await _context.Users.Where(o => o.Email.ToLower().Equals(email.ToLower())).FirstOrDefaultAsync();
            if (user == null) { return NotFound(); }
            int id = user.Id;
            var list = await _context.UserRoles.Where(o => o.UserId == id).Include(o=>o.Role).ToListAsync();
            if (list == null)
            {
                return NotFound();
            }
            List<string> list1 = new List<string>();
            foreach (var item in list) {
                PnLSystem.ResponseDTOs.UserRoleAuth i = new ResponseDTOs.UserRoleAuth();
                i.Name=item.Role.Name;
                list1.Add(i.Name);
            }
            return list1;
        }

        // PUT: api/UserRoles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserRole(int id, PnLSystem.ResponseDTOs.UserRole userRole)
        {
            if (id != userRole.Id)
            {
                return BadRequest();
            }

            _context.Entry(userRole).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserRoleExists(id))
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

        // POST: api/UserRoles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PnLSystem.ResponseDTOs.UserRole>> PostUserRole(PnLSystem.ResponseDTOs.UserRole userRole)
        {
            PnLSystem.Models.UserRole i = new Models.UserRole();
            i.Id = userRole.Id;
            i.UserId = userRole.UserId;
            i.RoleId = userRole.RoleId;
            if (_context.UserRoles.Any(o => o.UserId == i.UserId && o.RoleId == i.RoleId))
            {
                return BadRequest(error: "User's already had this role!");
            }
            _context.UserRoles.Add(i);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserRole", new { id = i.Id }, userRole);
        }

        // DELETE: api/UserRoles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserRole(int id)
        {
            var userRole = await _context.UserRoles.FindAsync(id);
            if (userRole == null)
            {
                return NotFound();
            }

            _context.UserRoles.Remove(userRole);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserRoleExists(int id)
        {
            return _context.UserRoles.Any(e => e.Id == id);
        }
    }
}
