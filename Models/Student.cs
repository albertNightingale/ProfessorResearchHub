using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace URC.Models
{
    /*
     * OUT DATED PLEASE SEE APPLICATION.CS
     */
    public class Student
    {
        // Student Skills -> comma separated string 
        public string Skills { get; set; }
        // Resume - the name of the file
        public string Resume { get; set; }
        // Interests -> comma separated string 
        public string Interests { get; set; }
        // Expected Graduation Date
        public DateTime GraduationDate { get; set; }
        // Completed Courses -> comma separated string
        public string CompletedCourses { get; set; }
        // Degree Plan
        public string DegreePlan { get; set; }
        // GPA of student
        public int GPA { get; set; }
        // UID of Student
        public string Uid { get; set; }
        // Hours per week you have available
        public int HoursPerWeek { get; set; }
        // Student's Personal Statement
        public string PersonalStatement { get; set; }
        // Student's Aplication Date -> should be filled in by the system and not the student
        public DateTime ApplicationDate { get; set; }
        // Is the Student actively looking for a position
        public bool Active { get; set; }
    }
}
