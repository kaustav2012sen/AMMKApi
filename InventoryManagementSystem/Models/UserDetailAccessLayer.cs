using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Models
{
    public class UserDetailAccessLayer
    {
        public UserDetails GetUserDetails()
        {
            try
            {
                UserDetails ud = new UserDetails();
                ud.UserID = "2413";
                ud.UserName = "DevAdmin";
                ud.UserRole = "ADMIN";

                return ud;

            }
            catch
            {
                throw;
            }
        }
    }
}
