using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SpaUserList.Models
{
    public class Tag
    {
        public Tag()
        {
            Users = new HashSet<User>();
        }

        [Key]
        public string Name { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
