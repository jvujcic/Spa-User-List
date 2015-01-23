﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace SpaUserList.Models
{
    public class UserListDbInitializer : DropCreateDatabaseIfModelChanges<UserListDbContext>
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
                        new TelephoneNumber() { Number = "+38595873950" }
                    },
                    Tags = new HashSet<Tag>() {
                        new Tag() { Name="bacvice" }
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
                        new TelephoneNumber() { Number = "+3859182350" },
                        new TelephoneNumber() { Number = "09812232323" }
                    },
                    Tags = new HashSet<Tag>() {
                        new Tag() { Name="hajduk" }
                    },
                    Favorite = false
                }
            };

            var tagMath = new Tag() { Name = "math" };;

            listOfUsers[0].Tags.Add(tagMath);
            listOfUsers[1].Tags.Add(tagMath);
            
            listOfUsers.ForEach(user => context.Users.Add(user));
            base.Seed(context);
        }
    }
}