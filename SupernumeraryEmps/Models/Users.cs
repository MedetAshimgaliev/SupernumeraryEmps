using System;
using System.Collections.Generic;

namespace SupernumeraryEmps.Models
{
    public partial class Users
    {
        public Users()
        {
            UserRoles = new HashSet<UserRoles>();
        }

        public int Uid { get; set; }
        public string FullName { get; set; }

        public virtual ICollection<UserRoles> UserRoles { get; set; }
    }
}
