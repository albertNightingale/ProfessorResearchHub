using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using URC.Areas.Identity.Data;
using URC.Data;
using URC.Models;

namespace URC.Controllers
{
    public class NotificationsController : Controller
    {
        private readonly UserManager<URCUser> _userManager;
        private readonly UsersRolesDB _db;

        public NotificationsController(UserManager<URCUser> userManager, UsersRolesDB usersDB)
        {
            _userManager = userManager;
            _db = usersDB;
        }
        public IActionResult Index()
        {

            var notifications = _db.Notifications
                .Where(n =>
                    n.User.Id.Equals(_userManager.GetUserId(User))
                    && n.IsRead == false)
                .OrderByDescending(n => n.CreatedTime);

            return View(notifications);
        }

        public void CreateNotification(URCUser user, string notificationMessage)
        {
            _ = _db.Add(_ = new Notification
            {
                NotificationMessage = notificationMessage,
                IsRead = false,
                User = user
            });
            try
            {
                _db.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error creating notification: " + e);
            }
        }

        public void CreateNotification(List<URCUser> users, string notificationMessage)
        {
            foreach (var user in users)
            {
                _ = _db.Add(_ = new Notification
                {
                    NotificationMessage = notificationMessage,
                    IsRead = false,
                    User = user
                });
            }
            try
            {
                _db.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error creating notification: " + e);
            }
        }

        [HttpPost]
        public async Task<IActionResult> MarkRead(int notificationId)
        {
            try
            {
                Notification notification = await _db.Notifications.FindAsync(notificationId);
                notification.IsRead = !notification.IsRead;
                _db.SaveChanges();
                return Ok();
            } 
            catch (Exception e)
            {
                Console.WriteLine("Error in notification MarkRead(): " + e);
                return BadRequest();
            }
        }
    }
}
