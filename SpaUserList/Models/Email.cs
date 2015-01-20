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
}
