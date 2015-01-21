﻿using System;
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

            foreach (Email email in user.Emails)
            {
                Email emailTemp = db.Emails.SingleOrDefault(e => e.EmailAddress == email.EmailAddress);
                if(emailTemp != null && emailTemp.UserId != user.UserId)
                {
                    return BadRequest("koristena email adresa");
                }
            }
           /*
            * Moze i efikasnije (bez da sve izbrisen i updejtan)
            */         

            foreach (Email email in userToUpdate.Emails.ToArray())
            {
                db.Emails.Remove(email);
            }

            userToUpdate.Emails.Clear();
            userToUpdate.Tags.Clear();

            foreach (Email email in user.Emails)
            {
                userToUpdate.Emails.Add(new Email() { EmailAddress = email.EmailAddress });
            }

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

            foreach (Email email in user.Emails)
            {
                if (db.Emails.SingleOrDefault(e => e.EmailAddress == email.EmailAddress) != null)
                {
                    return BadRequest("koristena email adresa");
                }
            }

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