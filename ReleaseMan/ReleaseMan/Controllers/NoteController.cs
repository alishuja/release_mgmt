using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ReleaseMan.Models;

namespace ReleaseMan.Controllers
{ 
    public class NoteController : Controller
    {
        private ProjectDBContext db = new ProjectDBContext();

        //
        // GET: /Note/

        public ViewResult Index(int id = 0)
        {
            var releasenotes = id > 0 ? db.ReleaseNotes.Where(r => r.ReleaseId == id) : db.ReleaseNotes.Include(r => r.Release);

            return View(releasenotes.ToList());
        }

        //
        // GET: /Note/Details/5

        public ViewResult Details(int id = 0)
        {
            ReleaseNote releasenote = db.ReleaseNotes.Find(id);
            return View(releasenote);
        }

        //
        // GET: /Note/Create

        public ActionResult Create()
        {
            ViewBag.ReleaseId = new SelectList(db.Releases, "ID", "Name");
            return View();
        } 

        //
        // POST: /Note/Create

        [HttpPost]
        public ActionResult Create(ReleaseNote releasenote)
        {
            if (ModelState.IsValid)
            {
                db.ReleaseNotes.Add(releasenote);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.ReleaseId = new SelectList(db.Releases, "ID", "Name", releasenote.ReleaseId);
            return View(releasenote);
        }
        
        //
        // GET: /Note/Edit/5
 
        public ActionResult Edit(int id = 0)
        {
            ReleaseNote releasenote = db.ReleaseNotes.Find(id);
            ViewBag.ReleaseId = new SelectList(db.Releases, "ID", "Name", releasenote.ReleaseId);
            return View(releasenote);
        }

        //
        // POST: /Note/Edit/5

        [HttpPost]
        public ActionResult Edit(ReleaseNote releasenote)
        {
            if (ModelState.IsValid)
            {
                db.Entry(releasenote).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ReleaseId = new SelectList(db.Releases, "ID", "Name", releasenote.ReleaseId);
            return View(releasenote);
        }

        //
        // GET: /Note/Delete/5
 
        public ActionResult Delete(int id = 0)
        {
            ReleaseNote releasenote = db.ReleaseNotes.Find(id);
            return View(releasenote);
        }

        //
        // POST: /Note/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            ReleaseNote releasenote = db.ReleaseNotes.Find(id);
            db.ReleaseNotes.Remove(releasenote);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}