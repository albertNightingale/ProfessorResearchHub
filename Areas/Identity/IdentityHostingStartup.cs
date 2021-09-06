using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using URC.Areas.Identity.Data;
using URC.Areas.Identity.Services;
using URC.Data;
using WebPWrecover.Services;

[assembly: HostingStartup(typeof(URC.Areas.Identity.IdentityHostingStartup))]
namespace URC.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
         
        public void Configure(IWebHostBuilder builder)
        {
            _ = builder.ConfigureServices((context, services) =>
            {
                services.AddDbContext<UsersRolesDB>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("UsersRolesDBConnection")));

                services.AddDefaultIdentity<URCUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddRoles<IdentityRole>()   
                    .AddEntityFrameworkStores<UsersRolesDB>();
                services.AddTransient<IEmailSender, EmailSender>();
                services.Configure<AuthMessageSenderOptions>(context.Configuration);
            });
        }
    }
}