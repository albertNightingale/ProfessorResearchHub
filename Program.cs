using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using URC.Areas.Identity.Data;
using URC.Data;
using WebApplication1.Data;

namespace WebApplication1
{
    
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            CreateDbIfNotExists(host);
            host.Run();
        }
        /*
         * Creates a database if the database does not exist.
         */
        private static void CreateDbIfNotExists(IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    UserManager<URCUser> um = services.GetRequiredService<UserManager<URCUser>>();
                    RoleManager<IdentityRole> rm = services.GetRequiredService<RoleManager<IdentityRole>>();
                    var context_URC = services.GetRequiredService<URC_Context>();
                    var context_Users = services.GetRequiredService<UsersRolesDB>();
                    OpportunitySeeding.Initialize(context_URC);
                    SeedUsersRolesDB.Initialize(context_Users, um, rm);
                } 
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);                   
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred creating the DB.");
                }
            }
        }
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
