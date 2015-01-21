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
        public virtual HashSet<User> Users { get; set; }
    }

    public class TagComparer : IEqualityComparer<Tag>
    {
        public bool Equals(Tag t1, Tag t2)
        {
            return t1.Name == t2.Name;
        }
        public int GetHashCode(Tag tag)
        {
            return tag.Name.GetHashCode();
        }
    }
}
