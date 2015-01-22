using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using SpaUserList.Models;
using Newtonsoft.Json;
using FluentValidation.Results;

namespace SpaUserList.Controllers
{
    public class UsersController : ApiController
    {
        private UserListDbContext db = new UserListDbContext();

        // GET: api/Users
        public IQueryable<User> GetUsers()
        {
            return db.Users;
        }

        // GET: api/Users/5
        [ResponseType(typeof(User))]
        public IHttpActionResult GetUser(int id)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // PUT: api/Users/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUser(int id, User user)
        {
            var validator = new Models.Validators.UserValidator();
            ValidationResult result = validator.Validate(user);
            if (!result.IsValid)
            {
                return BadRequest(result.Errors.First().ErrorMessage);
            }
            if (!ModelState.IsValid)
            {
                //var errors = new List<string>();
                //foreach(var state in ModelState)
                //{ errors.Add(state.Value.Errors.First().ErrorMessage); }
                return BadRequest(ModelState);       
            }

            var userToUpdate = db.Users.Find(id);

            if (id != user.UserId || userToUpdate == null)
            {
                return BadRequest();
            }

            user.Emails.RemoveWhere(email => email.EmailAddress == "");

            //foreach (Email email in user.Emails)
            //{
            //    Email emailTemp = db.Emails.SingleOrDefault(e => e.EmailAddress == email.EmailAddress);
            //    if(emailTemp != null && emailTemp.UserId != user.UserId)
            //    {
            //        return BadRequest("koristena email adresa");
            //    }
            //}
           /*
            * Moze i efikasnije (bez da sve izbrisen i updejtan)
            */         

            foreach (Email email in userToUpdate.Emails.ToArray())
            {
                db.Emails.Remove(email);
            }

            user.TelephoneNumbers.RemoveWhere(tel => tel.Number == "");
            foreach (TelephoneNumber tel in userToUpdate.TelephoneNumbers.ToArray())
            {
                db.TelephoneNumbers.Remove(tel);
            }

            userToUpdate.Emails.Clear();
            userToUpdate.Tags.Clear();
            userToUpdate.TelephoneNumbers.Clear();

            foreach (Email email in user.Emails)
            {
                userToUpdate.Emails.Add(new Email() { EmailAddress = email.EmailAddress });
            }
            foreach (TelephoneNumber tel in user.TelephoneNumbers)
            {
                userToUpdate.TelephoneNumbers.Add(new TelephoneNumber() { Number = tel.Number });
            }

            user.Tags.RemoveWhere(tag => tag.Name == "");
            foreach (Tag tag in user.Tags)
            {
                Tag tagTemp = db.Tags.SingleOrDefault(t => t.Name == tag.Name);
                if (tagTemp == null)
                {
                    tagTemp = new Tag() { Name = tag.Name };
                }
                userToUpdate.Tags.Add(tagTemp);
            }

            userToUpdate.Name = user.Name;
            userToUpdate.Address = user.Address;
            userToUpdate.Surname = user.Surname;
            userToUpdate.Favorite = user.Favorite;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Users
        [ResponseType(typeof(User))]
        public IHttpActionResult PostUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            user.Emails.RemoveWhere(email => email.EmailAddress == "");
            foreach (Email email in user.Emails)
            {
                if (db.Emails.SingleOrDefault(e => e.EmailAddress == email.EmailAddress) != null)
                {
                    return BadRequest("koristena email adresa");
                }
            }

            user.Tags.RemoveWhere(tag => tag.Name == "");
            var tagsToRemove = new List<Tag>();
            foreach(Tag tag in user.Tags)
            {
                Tag tagTemp = db.Tags.SingleOrDefault(t => t.Name == tag.Name);
                if (tagTemp != null)
                {
                    tagTemp.Users.Add(user);
                    tagsToRemove.Add(tag);
                }
            }
            tagsToRemove.ForEach(tag => user.Tags.Remove(tag));

            db.Users.Add(user);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = user.UserId }, user);
        }

        // DELETE: api/Users/5
        [ResponseType(typeof(User))]
        public IHttpActionResult DeleteUser(int id)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            db.Users.Remove(user);
            db.SaveChanges();

            return Ok(user);
        }

        // GET: api/Users/Search
        [Route("api/Users/Search/{query}")]
        public List<User> GetSearch(string query)
        {
            if (query == null) return db.Users.ToList();

            var users = (
                    from u in db.Users 
                    where String.Compare(u.Name, query, StringComparison.OrdinalIgnoreCase) == 0
                    || String.Compare(u.Surname, query, StringComparison.OrdinalIgnoreCase)  == 0
                    select u
                )
                .Concat((
                        from t in db.Tags 
                        where String.Compare(t.Name, query, StringComparison.OrdinalIgnoreCase) == 0 
                        select t.Users.ToList()
                    )
                    .SelectMany(v => v)
                )
               .ToList();

            return users.Distinct().ToList();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserExists(int id)
        {
            return db.Users.Count(e => e.UserId == id) > 0;
        }
    }
}