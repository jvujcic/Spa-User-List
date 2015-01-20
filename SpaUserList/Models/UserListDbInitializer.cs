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
                    } 
                }
            };

            listOfUsers.ForEach(user => context.Users.Add(user));
            base.Seed(context);
        }
    }
}