using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SpaUserList.Models
{
    public class User
    {
        public User()
        {
            Emails = new HashSet<Email>(new EmailComparer());
            Tags = new HashSet<Tag>(new TagComparer());
            TelephoneNumbers = new HashSet<TelephoneNumber>(new TelephoneNumberComparer());
            Favorite = false;
        }

        public int UserId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname {get; set;}

        public string Address { get; set; }

        public bool Favorite { get; set; }

        public virtual HashSet<TelephoneNumber> TelephoneNumbers { get; set; }

        public virtual HashSet<Email> Emails { get; set; }

        public virtual HashSet<Tag> Tags { get; set; }
    }
}