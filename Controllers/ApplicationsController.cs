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
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using Newtonsoft.Json;
using URC.Areas.Identity.Data;
using URC.Data;
using URC.Models;
using WebApplication1.Data;
using WebApplication1.Models;

/**
 * This is the Student application controller
 * 
 **/
namespace URC.Controllers
{
    public class ApplicationsController : Controller
    {
        
        private readonly URC_Context _context;
        private readonly UserManager<URCUser> _userManager;
        private readonly UsersRolesDB _usersDb;
        private List<StudentSummary> summaries = new List<StudentSummary>();

        public ApplicationsController(URC_Context context, UserManager<URCUser> userManager, UsersRolesDB db)
        {
            _context = context;
            _userManager = userManager;
            _usersDb = db;
        }
        /*
         * This generates a list of student summary objects
         */
        public StudentSummary[] GenerateSummaries(URC_Context _context)
        {
            List<Application> apps = _context.Application.ToList<Application>();
            foreach (Application a in apps)
            {
                StudentSummary curr = new StudentSummary();
                curr.Id = a.ID;
                curr.UID = a.UID;
                curr.Name = a.Email;
                curr.GPA = a.GPA;
                curr.Skills = a.Skills;
                if (a.Statement.Length > 100)
                {
                    curr.Statement = a.Statement.Substring(0, 99);
                }
                else
                {
                    curr.Statement = a.Statement;
                }

                summaries.Add(curr);
            }
            return summaries.ToArray();
        }
 
        // GET: Applications
        public async Task<IActionResult> Index()
        {
            var applications = await _context.Application
                .Include(a => a.Opportunity)
                .ToListAsync();
            return View(applications);
        }

        [Authorize(Roles = "Student")]
        public IActionResult Create(int oId, string oName)
        {
            ViewBag.OppID = oId;
            ViewBag.OppName = oName;
            return View();
        }

        // GET: Applications/Details/5
        [Authorize(Roles = "Student, Professor")]
        [Route("Applications/Details/{id}/")]
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var application = await _context.Application
                .Include(a => a.Opportunity)
                .FirstOrDefaultAsync(a => a.ID == id);

            if (application == null)
            {
                return NotFound();
            }

            return View(application);
        }

        // GET: Applications/Create
        [Authorize(Roles = "Student")]
        [Route("Applications/Create/{oId}")]
        public async Task<IActionResult> Create(int oId)
        {
            ViewBag.Opp = await _context.Opportunity.FirstOrDefaultAsync(o => o.OpportunityID == oId);
            ViewBag.Prof = await _usersDb.UserProfiles.FirstOrDefaultAsync(p => p.UserName == _userManager.GetUserName(User));

            if (ViewBag.Opp == null)
            {
                return NotFound();
            }
            return View();
        }

        // This displays the find page for professors
        [Authorize(Roles = "Professor")]

        public async Task<IActionResult> Find()
        {
            return View(await _context.Application.ToListAsync());
        }

        // POST: Applications/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Student")]
        [Route("Applications/Create/{oId}/")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OpportunityID,ID,Skills,Resume,Interests,GraduationDate,CompletedCourses,DegreePlan,UID,Email,GPA,Statement,DateCreated,TimeModified")] Application application)
        {
            if (ModelState.IsValid)
            {
                _context.Add(application);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(application);
        }

        // GET: Applications/Edit/5
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var application = await _context.Application
                .Include(a => a.Opportunity)
                .FirstAsync(a => a.ID == id);

            if (application == null)
            {
                return NotFound();
            }

            return View(application);
        }

        // POST: Applications/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Student")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,UID,GPA,Statement")] Application application)
        {
            if (id != application.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    var old_application = await _context.Application.FindAsync(id);
                    old_application.UID = application.UID;
                    old_application.GPA = application.GPA;
                    old_application.Statement = application.Statement;


                    _context.Update(old_application);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplicationExists(application.ID))
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
            return View(application);
        }

        // GET: Applications/Delete/5
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var application = await _context.Application
                .Include(a => a.Opportunity)
                .FirstOrDefaultAsync(a => a.ID == id);
            if (application == null)
            {
                return NotFound();
            }

            return View(application);
        }

        // POST: Applications/Delete/5
        [Authorize(Roles = "Student")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var application = await _context.Application.FindAsync(id);
            _context.Application.Remove(application);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        // this method finds applications that match the search query and sends the summary object corresponding to the applicaiton back to the front end.
        [HttpPost]
        public async Task<JsonResult> Search_Users(string input)
        {
            List<StudentSummary> res_list = new List<StudentSummary>();
            if (input != null)
            {
                StudentSummary[] sums = GenerateSummaries(_context);
                string[] search = input.Split(", ");

                for (int i = 0; i < sums.Length; i++)
                {

                    StudentSummary s = sums[i];
                    for (int j = 0; j < search.Length; j++)
                    {
                        string curr = search[j];
                        string gpa = s.GPA.ToString();
                        if (s.Name == curr || s.Skills.Contains(curr) || s.Statement.Contains(curr) || gpa == curr || s.UID == curr)
                        {
                            if (!res_list.Contains(s))
                            {
                                res_list.Add(s);
                            }
                        }
                    }
                }

            }

            StudentSummary[] result = res_list.ToArray();
            return Json(result);

        }
        [HttpPost]
        public async Task<IActionResult> UpdateSlotsAndNotify(int oId, int aId)
        {
            // GETTING THE USER WHO MADE THE APPLICATION
            var application = _context.Application.Where(a => a.ID == aId).FirstOrDefault();
            var opp = _context.Opportunity.Where(o => o.OpportunityID == oId).FirstOrDefault();
            var applicant = _usersDb.Users.Where(a => a.Email == application.Email).FirstOrDefault();
            var notificationsController = new NotificationsController(_userManager, _usersDb);
            notificationsController.ControllerContext = ControllerContext;
            opp.Num_Slots = opp.Num_Slots - 1;
            if (opp.Num_Slots <= 0)
            {
                opp.Num_Slots = 0;
            }
            // removing the applicant from the list
            _context.Application.Remove(application);
            ViewData["oSlots"] = opp.Num_Slots;
            try
            {
                _context.SaveChanges();
            }
            catch 
            {
                Console.WriteLine("You done messed up");
                return BadRequest();
                
            }
            notificationsController.CreateNotification(applicant, "You were accepted for the opportunity: " + opp.Name + "!");
            return Ok();
        }
        [HttpPost]
        public async Task<IActionResult> Active(string email, bool toggle_state)
        {

            Console.WriteLine("Hey we are here! :" + email + " toggle state: " + toggle_state.ToString());
            try
            {
                List<Application> list = _context.Application.ToList<Application>();
                foreach (Application a in list)
                {
                    if (a.Email == email)
                    {
                        a.Applying = toggle_state;
                        _context.Application.Update(a);
                        _context.SaveChanges();
                    }
                }

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
        private bool ApplicationExists(int id)
        {
            return _context.Application.Any(e => e.ID == id);
        }

    }
}
