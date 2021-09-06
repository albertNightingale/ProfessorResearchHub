using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using URC.Areas.Identity.Data;
using URC.Data;
using URC.Models;

namespace URC.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<URCUser> _userManager;
        private readonly SignInManager<URCUser> _signInManager;
        private readonly UsersRolesDB _userRolesDB;

        public IndexModel(
            UsersRolesDB userRolesDB,
            UserManager<URCUser> userManager,
            SignInManager<URCUser> signInManager)
        {
            _userRolesDB = userRolesDB;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [Display(Name = "User Role")]
        public List<string> UserRoles { get; set; }

        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }

            [Display(Name = "Skills")]
            public IQueryable<Skill> Skills { get; set; }

            [Display(Name = "Resume")]
            public string Resume { get; set; }
            
            [Display(Name = "My Extracurricular Interests")]
            public List<string> Interests { get; set; }

            [Display(Name = "Graduation Date")]
            public DateTime GraduationDate { get; set; }

            [Display(Name = "Completed Courses")]
            public List<string> CompletedCourses { get; set; }

            [Display(Name = "Degree Plan")]
            public string DegreePlan { get; set; }

            [Display(Name = "Name")]
            public string Name { get; set; }

            [Display(Name = "GPA")]
            public double GPA { get; set; }

            [Display(Name = "Utah ID")]
            public string UID { get; set; }
        }

        private async Task LoadAsync(URCUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            var userRoles = await _userManager.GetRolesAsync(user);
            var skills = _userRolesDB.Skills
                .Where(s =>
                    s.Student.Id.Equals(_userManager.GetUserId(User)));

            URCUserProfile profile = _userRolesDB.UserProfiles.First((profile) => profile.UserName == userName);
            var resume = profile.Resume;
            var graduationDate = profile.GraduationDate;
            var degreePlan = profile.DegreePlan;
            var name = profile.Name;
            var gpa = profile.GPA;
            var uid = profile.UID;

            // untouched
            Username = userName;
            UserRoles = new List<string> (userRoles);

            var interests = profile.Interests;
            var completedCourses = profile.CompletedCourses;

            List<string> interestsList = new List<string>(interests.Split(","));
            List<string> completedCoursesList = new List<string>(completedCourses.Split(","));

            Input = new InputModel
            {
                PhoneNumber = phoneNumber,
                Skills = skills,
                Resume = resume,
                Interests = interestsList,
                GraduationDate = graduationDate,
                CompletedCourses = completedCoursesList,
                DegreePlan = degreePlan,
                Name = name,
                GPA = gpa,
                UID = uid
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            // phone number
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber) // phonenumber has been changed
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set phone number.";
                    return RedirectToPage();
                }
            }

            //// user profile changes
            URCUserProfile profile = _userRolesDB.UserProfiles.First((profile) => profile.UserName == user.UserName);
            
            if (Input.UID != profile.UID)
            {
                profile.UID = Input.UID;
            }

            // user profile skills
            //string inputSkill = "";
            //Input.Skills.ForEach((value) => inputSkill += value + ",");
            //Console.WriteLine(inputSkill);
            //if (Input.Skills.Count > 0)
            //    inputSkill = inputSkill.Substring(0, inputSkill.Length - 1);
            //if (inputSkill != profile.Skills)
            //{
            //    profile.Skills = inputSkill;
            //}

            // user profile resume
            if (Input.Resume != profile.Resume)
            {
                profile.Resume = Input.Resume;
            }


            // user profile interests
            string inputInterests = "";
            Input.Interests.ForEach((value) => inputInterests += value + ",");
            if (Input.Interests.Count > 0)
                inputInterests = inputInterests.Substring(0, inputInterests.Length - 1); 
            
            if (inputInterests != profile.Interests)
            {
                profile.Interests = inputInterests;
            }

            // user profile graduationDate
            if (Input.GraduationDate != profile.GraduationDate)
            {
                profile.GraduationDate = Input.GraduationDate;
            }

            // user profile completedCourses 
            string inputCompletedCourses = "";
            Input.CompletedCourses.ForEach((value) => inputCompletedCourses += value + ",");
            if (Input.CompletedCourses.Count > 0)
                inputCompletedCourses = inputCompletedCourses.Substring(0, inputCompletedCourses.Length - 1); 
            
            if (inputCompletedCourses != profile.CompletedCourses)
            {
                profile.CompletedCourses = inputCompletedCourses;
            }

            // profile name
            if (Input.Name != profile.Name)
            {
                profile.Name = Input.Name;
            }

            // user profile DegreePlan
            if (Input.DegreePlan != profile.DegreePlan)
            {
                profile.DegreePlan = Input.DegreePlan;
            }

            // user profile GPA
            if (Input.GPA != profile.GPA)
            {
                profile.GPA = Input.GPA;
            }

            _userRolesDB.UserProfiles.Update(profile);
            _userRolesDB.SaveChanges();

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
