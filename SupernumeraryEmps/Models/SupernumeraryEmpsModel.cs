using System;
using System.Collections.Generic;
using System.Linq;

namespace SupernumeraryEmps.Models
{
    public class SupernumeraryEmpsModel
    {
        public static List<myUsers> getUsers()
        {
            List<myUsers> list = new List<myUsers>();
            using (var entity = new WebPortalContext())
            {
                list = entity.Users.Select(u => new myUsers()
                {
                    Id = u.Uid,
                    FullName = u.FullName
                }).ToList();
            }
            return list;
        }
    }

    public class myUsers
    {
        public int Id { get; set; }
        public string FullName { get; set; }
    }
}
