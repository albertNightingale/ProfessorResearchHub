using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace URC.Models
{
    /*
     * The class with fields for an application 
     */
    public class Application
    {           
        /// <summary>
        /// DB Table ID 
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Student Skills -> comma separated string 
        /// </summary>
        public string Skills { get; set; }
        /// <summary>
        /// Resume - the name of the file
        /// </summary>
        public string Resume { get; set; }
        /// <summary>
        /// Interests -> comma separated string
        /// </summary>
        public string Interests { get; set; }
        /// <summary>
        ///  Expected Graduation Date
        /// </summary>
        public DateTime GraduationDate { get; set; }
        /// <summary>
        /// Completed Courses -> comma separated string
        /// </summary>
        public string CompletedCourses { get; set; }
        /// <summary>
        /// Degree Plan
        /// </summary>
        public string DegreePlan { get; set; }
        // GPA of student
        /// <summary>
        ///  Your Student ID
        /// </summary>
        [RegularExpression(@"^u[0-9]{7}$", ErrorMessage = "Please use th format: u12345678")]
        public string UID { get; set; }
        /// <summary>
        /// Applicant's email address
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        ///  From 0 to 4.0
        /// </summary>
        [Range(0,4)]
        public float GPA { get; set; }
        /// <summary>
        ///  True if the application is "in-play". If false it shouldn't show up anywhere except for individual student to review.
        /// </summary>
        public bool Applying { get; set; }
        /// <summary>
        ///  The applicant's statement
        /// </summary>
        [MaxLength(500, ErrorMessage = "Please limit your statement to 500 characters." )]
        public string Statement { get; set; }
        /// <summary>
        ///  The date this application was initially created
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime DateCreated { get; set; }
        /// <summary>
        ///  The lat time this application was modified
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime TimeModified { get; set; }
        /// <summary>
        ///  Unique number representing changes to field
        /// </summary>
        [ScaffoldColumn(false)]
        [Timestamp]
        public Byte[] VersionNumber { get; set; }

        [ForeignKey("Opportunity")]
        public int OpportunityID { get; set; }
        public Opportunity Opportunity { get; set; }

    }
}
