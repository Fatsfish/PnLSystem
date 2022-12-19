﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PnLSystem.Models;

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
        public async Task<ActionResult<IEnumerable<PnLSystem.ResponseDTOs.UserRole>>> GetUserRoles()
        {
            var list = await _context.UserRoles.ToListAsync();
            List<PnLSystem.ResponseDTOs.UserRole> list1 = new List<PnLSystem.ResponseDTOs.UserRole>();
            foreach (var item in list)
            {
                PnLSystem.ResponseDTOs.UserRole i = new ResponseDTOs.UserRole();
                i.Id = item.Id;
                i.UserId = item.UserId;
                i.RoleId = item.RoleId;
                list1.Add(i);
            }
            return list1;
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
        [HttpPost("Email")]
        public async Task<ActionResult<IEnumerable<PnLSystem.ResponseDTOs.UserRole>>> GetUserRole(string email)
        {
            var user = await _context.Users.Where(o => o.Email.ToLower().Equals(email.ToLower())).FirstOrDefaultAsync();
            if (user == null) { return NotFound(); }
            int id = user.Id;
            var list = await _context.UserRoles.Where(o => o.UserId == id).ToListAsync();
            if (list == null)
            {
                return NotFound();
            }
            List<PnLSystem.ResponseDTOs.UserRole> list1 = new List<PnLSystem.ResponseDTOs.UserRole>();
            foreach (var item in list) {
                PnLSystem.ResponseDTOs.UserRole i = new ResponseDTOs.UserRole();
                i.Id=item.Id;
                i.UserId = item.UserId;
                i.RoleId = item.RoleId;
                list1.Add(i);
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
