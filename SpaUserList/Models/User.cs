using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using FluentValidation.Attributes;

namespace SpaUserList.Models
{
    //[Validator(typeof(Validators.UserValidator))]
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

        //[Required(ErrorMessage="TestName")]
        public string Name { get; set; }

        //[Required(ErrorMessage="TestSur")]
        public string Surname {get; set;}

        public string Address { get; set; }

        public bool Favorite { get; set; }

        public virtual HashSet<TelephoneNumber> TelephoneNumbers { get; set; }

        public virtual HashSet<Email> Emails { get; set; }

        public virtual HashSet<Tag> Tags { get; set; }
    }
}