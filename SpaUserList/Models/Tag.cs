using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpaUserList.Models
{
    public class Tag
    {
        public string name { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
