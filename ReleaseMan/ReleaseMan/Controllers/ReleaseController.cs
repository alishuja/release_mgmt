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
    public class ReleaseController : Controller
    {
        private ProjectDBContext db = new ProjectDBContext();

        //
        // GET: /Release/

        public ViewResult Index()
        {
            var releases = db.Releases.Include(r => r.Project);
            return View(releases.ToList());
        }

        //
        // GET: /Release/Details/5

        public ViewResult Details(int id)
        {
            Release release = db.Releases.Find(id);
            return View(release);
        }

        //
        // GET: /Release/Create

        public ActionResult Create()
        {
            ViewBag.ProjectId = new SelectList(db.Projects, "ID", "Name");
            return View();
        } 

        //
        // POST: /Release/Create

        [HttpPost]
        public ActionResult Create(Release release)
        {
            if (ModelState.IsValid)
            {
                db.Releases.Add(release);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.ProjectId = new SelectList(db.Projects, "ID", "Name", release.ProjectId);
            return View(release);
        }
        
        //
        // GET: /Release/Edit/5
 
        public ActionResult Edit(int id)
        {
            Release release = db.Releases.Find(id);
            ViewBag.ProjectId = new SelectList(db.Projects, "ID", "Name", release.ProjectId);
            return View(release);
        }

        //
        // POST: /Release/Edit/5

        [HttpPost]
        public ActionResult Edit(Release release)
        {
            if (ModelState.IsValid)
            {
                db.Entry(release).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProjectId = new SelectList(db.Projects, "ID", "Name", release.ProjectId);
            return View(release);
        }

        //
        // GET: /Release/Delete/5
 
        public ActionResult Delete(int id)
        {
            Release release = db.Releases.Find(id);
            return View(release);
        }

        //
        // POST: /Release/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Release release = db.Releases.Find(id);
            db.Releases.Remove(release);
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