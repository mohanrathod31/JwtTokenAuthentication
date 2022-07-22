using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasicJWTAuth.Models
{
    public class Users
    {
        public string Name { get; set; }
        public string Password { get; set; }
    }

    public class User
    {
        public List<UserModel> users { get; set; }
    }

    public class UserModel
    {
        public string userid { get; set; }

        public string password { get; set; }

        public string role { get; set; }

    }
}
