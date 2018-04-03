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

namespace CRM_Web.Controllers
{
  public class CustomersController : ApiController
  {
    private CRM_Data_ModelContainer db = new CRM_Data_ModelContainer();

    public CustomersController()
    {
      db.Configuration.ProxyCreationEnabled = false;
    }

    // GET: api/Customers
    public IQueryable<Customer> GetCustomers()
    {
      return db.Customers.OrderByDescending(c => c.DateAdded);
    }

    // GET: api/Customers?userId=varName
    public IQueryable<Customer> GetCustomersByUser(int userId)
    {
      return db.Customers
        .Where(c => c.UserId == userId)
        .OrderBy(c => c.FirstName);
    }

    // GET: api/Customers?searchFor=varName
    // search can contain partial string for: first name, or last name, or phone, or email
    public IQueryable<Customer> GetCustomersBySearch(string searchFor)
    {
      return db.Customers
        .Where(c => c.FirstName.Contains(searchFor) 
        || c.LastName.Contains(searchFor) 
        || c.Phone.Contains(searchFor) 
        || c.Email.Contains(searchFor));
    }


    // GET: api/Customers/5
    [ResponseType(typeof(Customer))]
    public IHttpActionResult GetCustomer(int id)
    {
      Customer customer = db.Customers.Find(id);
      if (customer == null)
      {
        return NotFound();
      }

      return Ok(customer);
    }

    // PUT: api/Customers/5
    [ResponseType(typeof(void))]
    public IHttpActionResult PutCustomer(int id, Customer customer)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      if (id != customer.Id)
      {
        return BadRequest();
      }

      db.Entry(customer).State = EntityState.Modified;

      try
      {
        db.SaveChanges();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!CustomerExists(id))
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

    // POST: api/Customers
    [ResponseType(typeof(Customer))]
    public IHttpActionResult PostCustomer(Customer customer)
    {
            customer.DateAdded = DateTime.Now;
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      db.Customers.Add(customer);
      db.SaveChanges();

      return Ok(customer);
    }

    // DELETE: api/Customers/5
    [ResponseType(typeof(Customer))]
    public IHttpActionResult DeleteCustomer(int id)
    {
      Customer customer = db.Customers.Find(id);
      if (customer == null)
      {
        return NotFound();
      }

      var customerNotes = db.Notes.Where(n => n.CustomerId == id).ToList();

      foreach (Note note in customerNotes)
      {
        var noteEdits = db.NoteEdits.Where(n => n.NoteId == note.Id).ToList();

        foreach (NoteEdits noteEdit in noteEdits)
        {
          db.NoteEdits.Remove(noteEdit);
          db.SaveChanges();
        }
        db.Notes.Remove(note);
        db.SaveChanges();
      }
      db.Customers.Remove(customer);
      db.SaveChanges();
      return Ok(customer);

      //Customer customer = db.Customers.Find(id);
      //if (customer == null)
      //{
      //  return NotFound();
      //}

      //db.Customers.Remove(customer);
      //db.SaveChanges();

      //return Ok(customer);
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        db.Dispose();
      }
      base.Dispose(disposing);
    }

    private bool CustomerExists(int id)
    {
      return db.Customers.Count(e => e.Id == id) > 0;
    }
  }
}