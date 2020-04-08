using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Task.Models
{
    public class User
    {

        public string UserId { get; set; }
        public string password { get; set; }
        public int roleid { get; set; }
    }

    public class UserDetails
    {
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string username { get; set; }
    }
}