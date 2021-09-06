using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using URC.Models;
/**
* Author:    Richard Hunter Moffat
* Partner:   None
* Date:      9/22/2020
* Course:    CS 4540, University of Utah, School of Computing
* Copyright: CS 4540 and Richard Hunter Moffat
*
* I, Richard Hunter Moffat, certify that I wrote this code from scratch and did 
* not copy it in part or whole from another source.  Any references used 
* in the completion of the assignment are cited in my README file and in
* the appropriate method header.
*
* File Contents
*
* A class for the Opportunity object stored in the database
*         
*/
namespace WebApplication1.Models
{
    public class Opportunity
    {
        

        public int OpportunityID { get; set; }
        public string Name { get; set; }
        public string Professor { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string Mentor { get; set; }
        public DateTime Begin_date { get; set; }
        public DateTime End_date { get; set; }
        public int Pay { get; set; }
        public bool Filled { get; set; }
        public string Required_skills { get; set; }
        public string Search_tags { get; set; }
        public int Num_Slots { get; set; }
        public virtual ICollection<Application> Applications { get; set; }
    }
}
