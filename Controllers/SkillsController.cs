using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using URC.Areas.Identity.Data;
using URC.Data;
using URC.Models;

namespace URC.Controllers
{
    public class SkillsController : Controller
    {
        private readonly UserManager<URCUser> _userManager;
        private readonly UsersRolesDB _db;
        private readonly ILogger _logger;

        public SkillsController(UserManager<URCUser> userManager, UsersRolesDB db, ILogger<SkillsController> logger)
        {
            _userManager = userManager;
            _db = db;
            _logger = logger;
        }


        // GET: SkillsController
        public ActionResult Index()
        {
            string studentID = _userManager.GetUserId(User);
            var skills = _db.Skills
                .Where(s =>
                    s.Student.Id.Equals(_userManager.GetUserId(User)));

            return View(skills);
        }

        [HttpPost]
        public IActionResult CreateSkill(string skillText)
        {
            var student = _db.Users.Where(s => s.Id == _userManager.GetUserId(User)).FirstOrDefault();
            _db.Skills.Add(new Skill
            {
                SkillText = skillText,
                Student = student
            });
            try
            {
                _db.SaveChanges();
            }
            catch (Exception e)
            {
                _logger.LogDebug("error adding skill: " + e);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Update(int skillId, string skillText)
        {
            var skill = _db.Skills.Where(s => s.Id == skillId).FirstOrDefault();
            skill.SkillText = skillText;
            try
            {
                _db.SaveChanges();
            } catch (Exception e)
            {
                _logger.LogDebug("error updating skill: " + e);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int skillId)
        {
            _db.Skills.Remove(_db.Skills.Where(s => s.Id == skillId).FirstOrDefault());
            try
            {
                _db.SaveChanges();
            } catch (Exception e)
            {
                _logger.LogDebug("error delete skill: " + e);
            }

            return RedirectToAction("Index");
        }
    }
}

