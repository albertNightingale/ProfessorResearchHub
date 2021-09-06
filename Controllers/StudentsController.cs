using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    /**
     * [OUT DATED PLEASE SEE APPLICATIONSCONTROLLER] 
     */
    public class StudentsController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public StudentsController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public IActionResult Application()
        {
            return View();
        }
        public IActionResult Logout()
        {
            return View();
        }
        public IActionResult Manage_Account()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
