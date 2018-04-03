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
using CRM_App;
using System.Net.Mail;

namespace CRM_Web.Controllers
{
    public class UsersController : ApiController
    {
        private CRM_Data_ModelContainer db = new CRM_Data_ModelContainer();

        public UsersController()
        {
            db.Configuration.ProxyCreationEnabled = false;
        }

        // GET: api/Users
        public IQueryable<User> GetUsers()
        {
            return db.Users.OrderBy(u => u.FirstName);
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

        //GET: api/Users?email=string&password=string
        public IHttpActionResult GetUser(string email, string password)
        {
            User user = db.Users.FirstOrDefault(u => u.Email == email && u.Password == password);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

    public IHttpActionResult GetUser(string email)
    {
      User user = db.Users.FirstOrDefault(u => u.Email == email);
      if (user == null)
      {
        return NotFound();
      }
      MailMessage mail = new MailMessage("wereturn@gmx.com", user.Email);
      SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
      client.Credentials = new NetworkCredential("wereturn@gmx.com", "TurnOneTwoThree!23");
      client.EnableSsl = true;
      mail.Subject = "Your password verification.";
      mail.Body = "Your password is: " + user.Password;
      client.Send(mail);
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

            if (id != user.Id)
            {
                return BadRequest();
            }

            db.Entry(user).State = EntityState.Modified;

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

            db.Users.Add(user);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = user.Id }, user);
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
            return db.Users.Count(e => e.Id == id) > 0;
        }
    }
}