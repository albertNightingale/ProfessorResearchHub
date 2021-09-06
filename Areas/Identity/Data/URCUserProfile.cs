using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace URC.Areas.Identity.Data
{
    public class URCUserProfile
    {
        [Key]
        public string URCUserProfileID { get; set; }

        public string Skills { get; set; }

        public string Resume { get; set; }

        public string Interests { get; set; }

        public DateTime GraduationDate { get; set; }

        public string CompletedCourses { get; set; }

        public string DegreePlan { get; set; }

        public string UID { get; set; }

        public string Name { get; set; }

        public double GPA { get; set; }

        public string UserName { get; set; }

        
        public URCUserProfile(string userName, string skills, string resume, string interests, DateTime graduationDate, string completedCourses, string degreePlan, string uID, string name, double gpa)
        {
            UserName = userName; 
            Skills = skills;
            Resume = resume;
            Interests = interests;
            GraduationDate = graduationDate;
            CompletedCourses = completedCourses;
            DegreePlan = degreePlan;
            UID = uID;
            Name = name;
            GPA = gpa;
        }

        public URCUserProfile()
        {

        }
        
    }
}
