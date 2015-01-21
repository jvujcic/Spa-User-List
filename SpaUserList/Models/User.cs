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
            Emails = new HashSet<Email>(new EmailComparer());
            Tags = new HashSet<Tag>(new TagComparer());
        }

        public int UserId { get; set; }

        //[Index]
        public string Name { get; set; }

        //[Index]
        public string Surname {get; set;}

        public string Address { get; set; }

        public virtual HashSet<Email> Emails { get; set; }

        public virtual HashSet<Tag> Tags { get; set; }
    }
}