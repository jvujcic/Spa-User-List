using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace SpaUserList.Models
{
    public class Email
    {
        [Key]
        public string EmailAddress { get; set; }

        [JsonIgnore]
        public int UserId { get; set; }

        [JsonIgnore]
        public virtual User User { get; set; }
    }

    public class EmailComparer : IEqualityComparer<Email>
    {
        public bool Equals(Email e1, Email e2)
        {
            return e1.EmailAddress == e2.EmailAddress;
        }
        public int GetHashCode(Email e)
        {
            return e.EmailAddress.GetHashCode();
        }
    }
}
