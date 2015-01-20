using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpaUserList.Models
{
    public class User
    {

        public User()
        {
            Emails = new HashSet<Email>();
            Tags = new HashSet<Tag>();
        }

        public int UserId { get; set; }

        //[Index]
        public string Name { get; set; }

        //[Index]
        public string Surname {get; set;}

        public string Address { get; set; }

        public virtual ICollection<Email> Emails { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }
    }
}