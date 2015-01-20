using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

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

        [JsonIgnore]
        public virtual ICollection<User> Users { get; set; }
    }
}
