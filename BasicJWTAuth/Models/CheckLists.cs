using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasicJWTAuth.Models
{
    public class CheckLists
    {
        public List<CheckList> users { get; set; }
    }

    public class CheckList
    {
        public int listid { get; set; }

        public string listname { get; set; }

        public string area { get; set; }
    }
}
