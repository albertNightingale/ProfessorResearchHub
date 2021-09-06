using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using URC.Areas.Identity.Data;
using URC.Controllers;
using URC.Data;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    
    public class OpportunitiesController : Controller
    {
        private readonly UserManager<URCUser> _userManager;
        private readonly URC_Context _context;
        private readonly UsersRolesDB _usersDB;

        public OpportunitiesController(UserManager<URCUser> userManager, URC_Context context, UsersRolesDB usersDB)
        {
            _userManager = userManager;
            _context = context;
            _usersDB = usersDB;
        }

        // GET: Opportunities
        public async Task<IActionResult> Index()
        {
            var opportunitites = await _context.Opportunity
                .Include(o => o.Applications)
                .ToListAsync();
            return View(opportunitites);
        }


        // GET: Opportunities, list
        // for authorization
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> List()
        {
            return View(await _context.Opportunity.ToListAsync());
        }

        // GET: Opportunities/Details/5

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var opportunity = await _context.Opportunity
                .Include(o => o.Applications)
                .FirstOrDefaultAsync(o => o.OpportunityID == id);
            if (opportunity == null)
            {
                return NotFound();
            }

            return View(opportunity);
        }

        // GET: Opportunities/Create
        [Authorize(Roles = "Administrator")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Opportunities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Create([Bind("OpportunityID,Name,Professor,Description,Image,Mentor,Begin_date,End_date,Pay,Filled,Required_skills,Search_tags,Num_Slots")] Opportunity opportunity)
        {
            if (opportunity.Required_skills != null)
            {
                string rawSkills = opportunity.Required_skills;
                List<string> skills = ParseSkills(rawSkills);
                SendSkillNotifications(skills, opportunity.Name);
            }

            if (ModelState.IsValid)
            {
                _context.Add(opportunity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(opportunity);
        }

        // GET: Opportunities/Edit/5
        [Authorize(Roles = "Administrator, Professor")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var opportunity = await _context.Opportunity.FindAsync(id);
            if (opportunity == null)
            {
                return NotFound();
            }
            return View(opportunity);
        }

        // POST: Opportunities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator, Professor")]
        public async Task<IActionResult> Edit(int id, [Bind("OpportunityID,Name,Professor,Description,Image,Mentor,Begin_date,End_date,Pay,Filled,Required_skills,Search_tags,Num_Slots")] Opportunity opportunity)
        {
            if (id != opportunity.OpportunityID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(opportunity);
                    await _context.SaveChangesAsync();

                    if (opportunity.Required_skills != null)
                    {
                        string rawSkills = opportunity.Required_skills;
                        List<string> skills = ParseSkills(rawSkills);
                        SendSkillNotifications(skills, opportunity.Name);
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OpportunityExists(opportunity.OpportunityID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(opportunity);
        }

        // GET: Opportunities/Delete/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var opportunity = await _context.Opportunity
                .FirstOrDefaultAsync(m => m.OpportunityID == id);
            if (opportunity == null)
            {
                return NotFound();
            }

            return View(opportunity);
        }

        // POST: Opportunities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var opportunity = await _context.Opportunity.FindAsync(id);
            _context.Opportunity.Remove(opportunity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OpportunityExists(int id)
        {
            return _context.Opportunity.Any(e => e.OpportunityID == id);
        }

        private List<string> ParseSkills(string rawSkills)
        {
            return rawSkills.Split(',').ToList();
        }

        private void SendSkillNotifications(List<string> skills, string opportunityName)
        {
            List<URCUser> studentsWithMatchingSkills = new List<URCUser>();
            NotificationsController notificationsController = new NotificationsController(_userManager, _usersDB);
            foreach (string skill in skills)
            {
                var skillsInDB = _usersDB.Skills.Where(s => s.SkillText.ToLower().Equals(skill.ToLower())).Include(s => s.Student);
                foreach (var skillInDB in skillsInDB)
                {
                    studentsWithMatchingSkills.Add(skillInDB.Student);
                }
            }
            notificationsController.CreateNotification(studentsWithMatchingSkills, "There is an opportunity that matches your skills: " + opportunityName);
        }
    }
}
