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
    public class IssueController : Controller
    {
        private ProjectDBContext db = new ProjectDBContext();

        //
        // GET: /Issue/

        public ViewResult Index(int id = 0)
        {

            var issues = id > 0 ? db.Issues.Where(r => r.ReleaseId == id) : db.Issues.Include(r => r.Release);
    
            return View(issues.ToList());
        }

        //
        // GET: /Issue/Details/5

        public ViewResult Details(int id = 0)
        {
            Issue issue = db.Issues.Find(id);
            return View(issue);
        }

        //
        // GET: /Issue/Create

        public ActionResult Create(int id = 0)
        {
            ViewBag.ReleaseId = new SelectList(db.Releases, "ID", "Name", id);
            return View();
        } 

        //
        // POST: /Issue/Create

        [HttpPost]
        public ActionResult Create(Issue issue)
        {
            if (ModelState.IsValid)
            {
                db.Issues.Add(issue);
                db.SaveChanges();
                return RedirectToAction("Details", "Release", new { id = issue.ReleaseId });  
            }

            ViewBag.ReleaseId = new SelectList(db.Releases, "ID", "Name", issue.ReleaseId);
            return View(issue); 

        }
        
        //
        // GET: /Issue/Edit/5
 
        public ActionResult Edit(int id = 0)
        {
            Issue issue = db.Issues.Find(id);
            ViewBag.ReleaseId = new SelectList(db.Releases, "ID", "Name", issue.ReleaseId);
            return View(issue);
        }

        //
        // POST: /Issue/Edit/5

        [HttpPost]
        public ActionResult Edit(Issue issue)
        {
            if (ModelState.IsValid)
            {
                db.Entry(issue).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", "Release", new { id = issue.ReleaseId }); 
            }
            ViewBag.ReleaseId = new SelectList(db.Releases, "ID", "Name", issue.ReleaseId);
            return View(issue);
        }

        //
        // GET: /Issue/Delete/5
 
        public ActionResult Delete(int id = 0 )
        {
            Issue issue = db.Issues.Find(id);
            return View(issue);
        }

        //
        // POST: /Issue/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Issue issue = db.Issues.Find(id);
            db.Issues.Remove(issue);
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