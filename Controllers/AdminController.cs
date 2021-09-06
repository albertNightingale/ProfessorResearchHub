using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using URC.Areas.Identity.Data;
using URC.Data;
/**
 * This controller is for the administrator and currently only handles the admin role manager page.
 */
namespace URC.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AdminController : Controller
    {
       
        private readonly UserManager<URCUser> _userManager;
        private readonly UsersRolesDB _context;

        public AdminController(UsersRolesDB context, UserManager<URCUser> um)
        {
            _context = context;
            _userManager = um;
        }
        public IActionResult Roles_and_Users()
        {
            URCUser[] users = _userManager.Users.ToArray();
            return View("Role_Manager", users);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateRole(string user, string role, bool toggle_state)
        {

            Console.WriteLine("Hey we are here! :" + user + " " + role + " toggle state: " + toggle_state.ToString());
            try
            {
                URCUser u = await _userManager.FindByNameAsync(user);
                if (!await _userManager.IsInRoleAsync(u, role) && toggle_state)
                {
                    await _userManager.AddToRoleAsync(u, role);
                }
                if (await _userManager.IsInRoleAsync(u, role) && !toggle_state)
                {
                    await _userManager.RemoveFromRoleAsync(u, role);
                }
                return Ok();
            }
            catch(Exception e)
            {
                return BadRequest();
            }
        }

    }
}
