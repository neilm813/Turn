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
  public class NotesController : ApiController
  {
    private CRM_Data_ModelContainer db = new CRM_Data_ModelContainer();

    public NotesController()
    {
      db.Configuration.ProxyCreationEnabled = false;
    }

    // GET: api/Notes
    public IQueryable<Note> GetNotes()
    {
      return db.Notes.OrderByDescending(n => n.DateAdded);
    }

    // GET: api/Notes?customerId=varName
    public IQueryable<Note> GetNotesForCustomer(int customerId)
    {
      return db.Notes
        .Where(n => n.CustomerId == customerId)
        .OrderByDescending(n => n.DateAdded);
    }

    // GET: api/Notes/5
    [ResponseType(typeof(Note))]
    public IHttpActionResult GetNote(int id)
    {
      Note note = db.Notes.Find(id);
      if (note == null)
      {
        return NotFound();
      }

      return Ok(note);
    }

    // PUT: api/Notes/5
    [ResponseType(typeof(void))]
    public IHttpActionResult PutNote(int id, Note note)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      if (id != note.Id)
      {
        return BadRequest();
      }

      db.Entry(note).State = EntityState.Modified;

      try
      {
        db.SaveChanges();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!NoteExists(id))
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

    // POST: api/Notes
    [ResponseType(typeof(Note))]
    public IHttpActionResult PostNote(Note note)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      db.Notes.Add(note);
      db.SaveChanges();

      return CreatedAtRoute("DefaultApi", new { id = note.Id }, note);
    }

    // DELETE: api/Notes/5
    [ResponseType(typeof(Note))]
    public IHttpActionResult DeleteNote(int id)
    {
      Note note = db.Notes.Find(id);
      if (note == null)
      {
        return NotFound();
      }

      db.Notes.Remove(note);
      db.SaveChanges();

      return Ok(note);
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        db.Dispose();
      }
      base.Dispose(disposing);
    }

    private bool NoteExists(int id)
    {
      return db.Notes.Count(e => e.Id == id) > 0;
    }
  }
}