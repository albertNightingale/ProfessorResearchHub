using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace URC.Areas.Identity.Data
{
    /*
     * This class is for URCRoles and is currently unused.
     */
    public class URCRole : IdentityRole
    {
        public URCRole(string roleName) : base(roleName)
        {

        }
    }
}
