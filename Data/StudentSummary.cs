using Microsoft.AspNetCore.Mvc.ApplicationParts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Data;

namespace URC.Data
{
    /**
     * a object containing information for a student summary
     */
    public class StudentSummary
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public string UID { get; set; }
        public double GPA { get; set; }
        public string Skills { get; set; }
        public string Statement { get; set; }
    
    }
}
