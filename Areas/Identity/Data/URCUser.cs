using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using URC.Models;

namespace URC.Areas.Identity.Data
{
    /*
     * This class is for URCUsers, containing user's information
     */
    public class URCUser : IdentityUser
    {

        public URCUser(string userName) : base (userName)
        {
            Notifications = new HashSet<Notification>();
        }

        public URCUser() : base()
        {

        }

        public virtual ICollection<Notification> Notifications { get; set; }
    }
}
