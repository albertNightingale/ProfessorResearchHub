using WebApplication1.Models;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore.Internal;
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
* This class seeds the Opportunities database with 8 opportunities
*         
*/
namespace WebApplication1.Data
{   
    /*
     * A class that seeds the opportunities database
     */

    public static class OpportunitySeeding
    {
        /**
         * This method seeds that opportunities database with 8 opportunities and 2 applications
         **/
        public static void Initialize(URC_Context context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Opportunity.Any())
            {
                return;   // DB has been seeded
            }

            var opportunities = new Opportunity[]
            {
               
                new Opportunity{Name = "5G Testbed Research", Description="Help us research new ways to communicate", Professor="Kobus", 
                    Begin_date = new DateTime(2020, 9, 6), End_date = new DateTime(2020, 10, 30), Filled = false, Image = "5g.png", Mentor="Kobus", Pay= 1500, Search_tags= "", Num_Slots = 5},
                new Opportunity{Name = "Compiler Fuzzer Research", Description="Help us improve C Smith", Professor="Eide",
                    Begin_date =  new DateTime(2020, 5, 21), End_date =  new DateTime(2020, 12, 30), Filled = false, Image = "csmith.png", Mentor="Richard", Pay= 5000, Search_tags= "", Num_Slots = 5},
                new Opportunity{Name = "Corona Virus Social Impact", Description="Research the socialogical impact of the Corona Virus Pandemic", Professor="Cole",
                    Begin_date = new DateTime(2020, 7, 11), End_date = new DateTime(2020, 12, 15), Filled = false, Image = "S.jpg", Mentor="Cole", Pay= 3000,  Search_tags= "", Num_Slots = 3},
                new Opportunity{Name = "Water System Irrigation", Description="Research new ways to securely irrigate water", Professor="Barber",
                    Begin_date = new DateTime(2020, 1, 1), End_date = new DateTime(2021, 12, 31), Filled = false, Image = "WA.jpg", Mentor="Radomski", Pay= 2700,  Search_tags= "", Num_Slots = 6},
                new Opportunity{Name = "Market Impact of Corona Virus", Description="Research the market impact of the corona virus", Professor="Bakhsheshy", 
                    Begin_date = new DateTime(2020, 8, 4), End_date = new DateTime(2021, 5, 15), Filled = false, Image = "M.jpg", Mentor="Werzer", Pay= 3500,  Search_tags= "", Num_Slots = 1},
                new Opportunity{Name = "Educational Game Development", Description="Research the effectiveness of educational games", Professor="Zagal", 
                    Begin_date = new DateTime(2020, 2, 14), End_date = new DateTime(2021, 11, 11), Filled = false, Image = "EG.jpg", Mentor="Steur", Pay= 1300,  Search_tags= "", Num_Slots = 2},
                new Opportunity{Name = "Is sitting the new smoking?", Description="Research the health impact of sitting for long periods of time", Professor="Botkin",
                    Begin_date = new DateTime(2020, 1, 10), End_date = new DateTime(2021, 1, 10), Filled = false, Image = "Chair.jpg", Mentor="Hull", Pay= 4500,  Search_tags= "", Num_Slots = 3},
                new Opportunity{Name = "Earthquake detection", Description="Research new ways to predict earthquakes", Professor="Cerling",
                    Begin_date = new DateTime(2020, 3, 8), End_date = new DateTime(2021, 4, 20), Filled = false, Image = "EQ.jpg", Mentor="Hesketh", Pay= 100,  Search_tags= "", Num_Slots = 4 }
            };
            foreach (Opportunity o in opportunities)
            {
                context.Opportunity.Add(o);
            }

            // seeding the applications 
            var applications = new Application[]
                {
                    new Application{Opportunity = opportunities[0], UID ="u0000001", GPA = (float) 2.1, Applying = true, Email = "u0000001@utah.edu", Statement = "This is a dummy application", Interests = "skiing, hiking, drawing",
                    CompletedCourses = "CS2420, CS3100, CS1410", DegreePlan="CS", Skills="Programming, learning, C#, Visual Studio", Resume = "resume1.txt",
                   DateCreated= new DateTime(2020, 3, 1, 7, 0, 0), TimeModified = new DateTime(2020, 3, 1, 7, 0, 0), GraduationDate = new DateTime(2021, 5, 5, 7, 0, 0)},
                    new Application{Opportunity = opportunities[3], UID ="u0000002", GPA = (float) 3.0, Applying = true, Email = "u0000002@utah.edu", Statement = "This is a dummy application", Interests = "Lifting, baseball, food",
                    CompletedCourses = "ART1010, ECE1000, ECE2000", DegreePlan="ECE", Skills="Electronics, ASP", Resume = "resume2.txt",
                       DateCreated= new DateTime(2020, 3, 1, 7, 0, 0), TimeModified = new DateTime(2020, 3, 1, 7, 0, 0), GraduationDate = new DateTime(2022, 5, 5, 7, 0, 0) },
                    new Application{Opportunity = opportunities[4], UID ="u0000004", GPA = (float) 3.41, Applying = true, Email = "u0000004@utah.edu", Statement = "This is a dummy application, that is way longer than it should be and should be cut off when the summary is created haha cool.", Interests = "Lifting, baseball, food",
                    CompletedCourses = "ART1010, CS4230, CS1410", DegreePlan="ECE", Skills="Ruby, Java, Netbeans", Resume = "resume4.txt",
                       DateCreated= new DateTime(2008, 3, 1, 7, 0, 0), TimeModified = new DateTime(2020, 5, 1, 7, 0, 0), GraduationDate = new DateTime(2021, 5, 5, 7, 0, 0) }
                };

            foreach (Application o in applications)
            {
                Console.WriteLine("Adding Applications to Database");
                context.Application.Add(o);
            }

            context.SaveChanges();

            
        }
    }
}