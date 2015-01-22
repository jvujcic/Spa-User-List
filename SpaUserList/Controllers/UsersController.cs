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
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);       
            }

            var userToUpdate = db.Users.Find(id);

            if (id != user.UserId || userToUpdate == null)
            {
                return BadRequest();
            }

            var emailsToRemove = new HashSet<Email>(userToUpdate.Emails);
            emailsToRemove.ExceptWith(user.Emails);
            db.Emails.RemoveRange(emailsToRemove);
            userToUpdate.Emails.IntersectWith(user.Emails);
            userToUpdate.Emails.UnionWith(user.Emails);

            var telephoneNumbersToRemove = new HashSet<TelephoneNumber>(userToUpdate.TelephoneNumbers);
            telephoneNumbersToRemove.ExceptWith(user.TelephoneNumbers);
            db.TelephoneNumbers.RemoveRange(telephoneNumbersToRemove);
            userToUpdate.TelephoneNumbers.IntersectWith(user.TelephoneNumbers);
            userToUpdate.TelephoneNumbers.UnionWith(user.TelephoneNumbers);

            userToUpdate.Tags.IntersectWith(user.Tags);
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
            userToUpdate.Surname = user.Surname;
            userToUpdate.Address = user.Address;
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

            var tagsAllreadyInDb = new List<Tag>();
            foreach(Tag tag in user.Tags)
            {
                Tag tagTemp = db.Tags.SingleOrDefault(t => t.Name == tag.Name);
                if (tagTemp != null)
                {
                    tagTemp.Users.Add(user);
                    tagsAllreadyInDb.Add(tagTemp);
                }
            }
            tagsAllreadyInDb.ForEach(tag => user.Tags.Remove(tag));
            db.Users.Add(user);
            db.SaveChanges();

            return Ok();
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