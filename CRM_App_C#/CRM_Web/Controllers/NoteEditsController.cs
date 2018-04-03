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
  public class NoteEditsController : ApiController
  {
    private CRM_Data_ModelContainer db = new CRM_Data_ModelContainer();

    public NoteEditsController()
    {
      db.Configuration.ProxyCreationEnabled = false;
    }

    // GET: api/NoteEdits
    public IQueryable<NoteEdits> GetNoteEdits()
    {
      return db.NoteEdits;
    }

    // GET: api/NoteEdits/5
    [ResponseType(typeof(NoteEdits))]
    public IHttpActionResult GetNoteEdits(int id)
    {
      NoteEdits noteEdits = db.NoteEdits.Find(id);
      if (noteEdits == null)
      {
        return NotFound();
      }

      return Ok(noteEdits);
    }

    // GET: api/NoteEdits?noteId=varName
    public IQueryable<NoteEdits> GetEditHistory(int noteId)
    {
      return db.NoteEdits
        .Where(n => n.NoteId == noteId)
        .OrderByDescending(n => n.DateEdited);
    }

    // PUT: api/NoteEdits/5
    [ResponseType(typeof(void))]
    public IHttpActionResult PutNoteEdits(int id, NoteEdits noteEdits)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      if (id != noteEdits.Id)
      {
        return BadRequest();
      }

      db.Entry(noteEdits).State = EntityState.Modified;

      try
      {
        db.SaveChanges();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!NoteEditsExists(id))
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

    // POST: api/NoteEdits
    [ResponseType(typeof(NoteEdits))]
    public IHttpActionResult PostNoteEdits(NoteEdits noteEdits)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      db.NoteEdits.Add(noteEdits);
      db.SaveChanges();

      return CreatedAtRoute("DefaultApi", new { id = noteEdits.Id }, noteEdits);
    }

    // DELETE: api/NoteEdits/5
    [ResponseType(typeof(NoteEdits))]
    public IHttpActionResult DeleteNoteEdits(int id)
    {
      NoteEdits noteEdits = db.NoteEdits.Find(id);
      if (noteEdits == null)
      {
        return NotFound();
      }

      db.NoteEdits.Remove(noteEdits);
      db.SaveChanges();

      return Ok(noteEdits);
    }

    // DELETE note edits by noteId: api/NoteEdits?noteId=varName
    public IHttpActionResult DeleteNotes (int NoteId)
    {
      var notes = db.NoteEdits.Where(n => n.NoteId == NoteId).ToList();

      for (int i = 0; i < notes.Count(); i++)
      {
        db.NoteEdits.Remove(notes[i]);
        db.SaveChanges();
      }

      return Ok();
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        db.Dispose();
      }
      base.Dispose(disposing);
    }

    private bool NoteEditsExists(int id)
    {
      return db.NoteEdits.Count(e => e.Id == id) > 0;
    }
  }
}