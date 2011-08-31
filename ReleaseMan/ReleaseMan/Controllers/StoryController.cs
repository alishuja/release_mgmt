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
    public class StoryController : Controller
    {
        private ProjectDBContext db = new ProjectDBContext();

        //
        // GET: /Story/

        public ViewResult Index(int id = 0)
        {
            var stories = id > 0 ? db.Stories.Where(r => r.ReleaseId == id) : db.Stories.Include(r => r.Release);
            return View(stories.ToList());
        }

        //
        // GET: /Story/Details/5

        public ViewResult Details(int id)
        {
            Story story = db.Stories.Find(id);
            return View(story);
        }

        //
        // GET: /Story/Create

        public ActionResult Create(int id)
        {
            ViewBag.ProjectId = new SelectList(db.Projects, "ID", "Name", id);
            return View();
        } 

        //
        // POST: /Story/Create

        [HttpPost]
        public ActionResult Create(Story story)
        {
            if (ModelState.IsValid)
            {
                db.Stories.Add(story);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.ReleaseId = new SelectList(db.Releases, "ID", "Name", story.ReleaseId);
            return View(story);
        }
        
        //
        // GET: /Story/Edit/5
 
        public ActionResult Edit(int id)
        {
            Story story = db.Stories.Find(id);
            ViewBag.ReleaseId = new SelectList(db.Releases, "ID", "Name", story.ReleaseId);
            return View(story);
        }

        //
        // POST: /Story/Edit/5

        [HttpPost]
        public ActionResult Edit(Story story)
        {
            if (ModelState.IsValid)
            {
                db.Entry(story).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ReleaseId = new SelectList(db.Releases, "ID", "Name", story.ReleaseId);
            return View(story);
        }

        //
        // GET: /Story/Delete/5
 
        public ActionResult Delete(int id)
        {
            Story story = db.Stories.Find(id);
            return View(story);
        }

        //
        // POST: /Story/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Story story = db.Stories.Find(id);
            db.Stories.Remove(story);
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