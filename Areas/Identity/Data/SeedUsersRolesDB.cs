using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using URC.Data;

namespace URC.Areas.Identity.Data
{
    /*
     * This class seeds the users and roles database with 5 different users with 3 different rolses
     */
    public class SeedUsersRolesDB
    {

        public static void Initialize(UsersRolesDB context, UserManager<URCUser> um, RoleManager<IdentityRole> rm)
        {
            
            context.Database.EnsureCreated();

            if(rm.Roles.ToArray().Count() > 0)
            {
                return;
            }
            // seeding roles
            try
            {
                rm.CreateAsync(new IdentityRole("Student")).Wait();
                rm.CreateAsync(new IdentityRole("Professor")).Wait();
                rm.CreateAsync(new IdentityRole("Administrator")).Wait();
            }
            catch(Exception e)
            {
                Console.WriteLine("Error when seeding roles");
            }
            string pass = "123ABC!@#def";
            // seeding users
            try
            {
                um.CreateAsync(new URCUser { UserName = "admin@utah.edu", Email = "admin@utah.edu", EmailConfirmed = true }, pass).Wait();
                addDefaultProfile(context, "admin@utah.edu");
                um.CreateAsync(new URCUser { UserName = "professor@utah.edu", Email = "professor@utah.edu", EmailConfirmed = true }, pass).Wait();
                addDefaultProfile(context, "professor@utah.edu");
                um.CreateAsync(new URCUser { UserName = "professor_mary@utah.edu", Email = "professor_mary@utah.edu", EmailConfirmed = true }, pass).Wait();
                addDefaultProfile(context, "professor_mary@utah.edu");
                um.CreateAsync(new URCUser { UserName = "u0000000@utah.edu", Email = "u0000000@utah.edu", EmailConfirmed = true }, pass).Wait();
                addDefaultProfile(context, "u0000000@utah.edu");
                um.CreateAsync(new URCUser { UserName = "u1234567@utah.edu", Email = "u1234567@utah.edu", EmailConfirmed = true }, pass).Wait();
                addDefaultProfile(context, "u1234567@utah.edu");
                um.CreateAsync(new URCUser { UserName = "u0000001@utah.edu", Email = "u0000001@utah.edu", EmailConfirmed = true }, pass).Wait();
                addDefaultProfile(context, "u0000001@utah.edu");
                um.CreateAsync(new URCUser { UserName = "u0000002@utah.edu", Email = "u0000002@utah.edu", EmailConfirmed = true }, pass).Wait();
                addDefaultProfile(context, "u0000002@utah.edu");
                um.CreateAsync(new URCUser { UserName = "u0000003@utah.edu", Email = "u0000003@utah.edu", EmailConfirmed = true }, pass).Wait();
                addDefaultProfile(context, "u0000003@utah.edu");
                um.CreateAsync(new URCUser { UserName = "u0000004@utah.edu", Email = "u0000004@utah.edu", EmailConfirmed = true }, pass).Wait();
                addDefaultProfile(context, "u0000004@utah.edu");
                context.SaveChangesAsync().Wait();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error when seeding users");
            }

            // adding roles to users
            URCUser[] users = um.Users.ToArray();
            try
            {
                foreach(URCUser u in users)
                {
                    if (u.UserName == "admin@utah.edu")
                    {
                        Console.WriteLine("adding Admin Role\n");
                        um.AddToRoleAsync(u,"Administrator").Wait();
                    }
                    else if (u.UserName.StartsWith("professor"))
                    {
                        Console.WriteLine("adding Professor Role\n");
                        um.AddToRoleAsync(u, "Professor").Wait();
                    }
                    else
                    {
                        Console.WriteLine("adding Student Role\n");
                        um.AddToRoleAsync(u, "Student").Wait();
                    }
                }
                
            }
            catch (Exception e)
            {
                Console.WriteLine("Error while seeding: adding roles to users");
            }

            AddNotifications(context, um);
            AddSkills(context, um);

        }

        private static void addDefaultProfile(UsersRolesDB context, string userName)
        {
            URCUserProfile defaultProfile = new URCUserProfile(userName, "", "", "", new DateTime(), "", "", "", "", 0.0);
            context.Add(defaultProfile);
        }

        private static void AddNotifications(UsersRolesDB context, UserManager<URCUser> um)
        {

            var user = um.Users.Where(u => u.UserName.Equals("u0000000@utah.edu")).FirstOrDefault();
            context.Add(_ = new Models.Notification
            {
                NotificationMessage = "Test u0000000",
                IsRead = false,
                User = user
            }) ;
            context.Add(_ = new Models.Notification
            {
                NotificationMessage = "Test 2 u0000000",
                IsRead = false,
                User = user
            });
            try
            {
                context.SaveChanges();
            }
            catch (Exception e)
            {
            }

            var user2 = um.Users.Where(u => u.UserName.Equals("u1234567@utah.edu")).FirstOrDefault();
            context.Add(_ = new Models.Notification
            {
                NotificationMessage = "Test u1234567",
                IsRead = false,
                User = user2
            });
            try
            {
                context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private static void AddSkills(UsersRolesDB context, UserManager<URCUser> um)
        {
            var user = um.Users.Where(u => u.UserName.Equals("u0000000@utah.edu")).FirstOrDefault();
            string[] skills = { "Ruby", "Python", "C++", "CSS" };
            foreach(string skill in skills)
            {
                context.Add(_ = new Models.Skill
                {
                    SkillText = skill,
                    Student = user
                });
            }

            try
            {
                context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
