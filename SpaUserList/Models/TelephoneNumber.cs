using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace SpaUserList.Models
{
    public class TelephoneNumber
    {   
        [JsonIgnore]
        public int TelephoneNumberId { get; set; }

        //[Required]
        public string Number { get; set; }

        [JsonIgnore]
        public int UserId { get; set; }

        [JsonIgnore]
        public virtual User Uuser { get; set; }
    }

    public class TelephoneNumberComparer : IEqualityComparer<TelephoneNumber>
    {
        public bool Equals(TelephoneNumber t1, TelephoneNumber t2)
        {
            return t1.Number == t2.Number;
        }
        public int GetHashCode(TelephoneNumber t)
        {
            return t.GetHashCode();
        }
    }
}
