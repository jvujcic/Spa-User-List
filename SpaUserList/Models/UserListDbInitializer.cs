using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace SpaUserList.Models
{
    public class UserListDbInitializer : DropCreateDatabaseAlways<UserListDbContext>
    {
        protected override void Seed(UserListDbContext context)
        {
            var listOfUsers = new List<User>() {
                new User() { 
                    Name = "Josip", 
                    Surname = "Vujcic", 
                    Address = "Stara Knezija 12", 
                    Emails = new HashSet<Email>() {
                        new Email() { EmailAddress = "jvujcic@gmail.com" },
                        new Email() { EmailAddress = "jvujcic@math.hr" }
                    },
                    TelephoneNumbers = new HashSet<TelephoneNumber>() {
                        new TelephoneNumber() { Number = "+385 95 873950" }
                    },
                    Favorite = true
                },
                new User() { 
                    Name = "Mate", 
                    Surname = "Matic", 
                    Address = "Imotski", 
                    Emails = new HashSet<Email>() {
                        new Email() { EmailAddress = "mate@gmail.com" },
                        new Email() { EmailAddress = "mate@math.hr" }
                    },
                    TelephoneNumbers = new HashSet<TelephoneNumber>() {
                        new TelephoneNumber() { Number = "+385 91 82350" },
                        new TelephoneNumber() { Number = "09812232323" }
                    },
                    Favorite = false
                }
            };

            var tagBacvice = new Tag() { Name = "bacvice" };
            var tagMath = new Tag() { Name = "math" };
            var tagHajduk = new Tag() { Name = "hajduk" };

            listOfUsers[0].Tags.Add(tagBacvice);
            listOfUsers[0].Tags.Add(tagMath);
            listOfUsers[1].Tags.Add(tagMath);
            listOfUsers[1].Tags.Add(tagHajduk);
            
            listOfUsers.ForEach(user => context.Users.Add(user));
            base.Seed(context);
        }
    }
}