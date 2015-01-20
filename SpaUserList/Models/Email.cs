using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SpaUserList.Models
{
    public class Email
    {
        [Key]
        public string EmailAddress { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }
    }
}
