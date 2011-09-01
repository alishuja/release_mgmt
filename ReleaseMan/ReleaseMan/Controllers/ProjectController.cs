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
    public class ProjectController : Controller
    {
        private ProjectDBContext db = new ProjectDBContext();

        //
        // GET: /Project/

        [Authorize]
        public ViewResult Index()
        {
            return View(db.Projects.ToList());
        }

        //
        // GET: /Project/Details/5

        public ViewResult Details(int id = 0)
        {
            Project project = db.Projects.Find(id);
            if (project == null) {

                }
            return View(project);
        }

        //
        // GET: /Project/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Project/Create

        [HttpPost]
        public ActionResult Create(Project project)
        {
            if (ModelState.IsValid)
            {
                db.Projects.Add(project);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(project);
        }
        
        //
        // GET: /Project/Edit/5
 
        public ActionResult Edit(int id = 0)
        {
            Project project = db.Projects.Find(id);
            return View(project);
        }

        //
        // POST: /Project/Edit/5

        [HttpPost]
        public ActionResult Edit(Project project)
        {
            if (ModelState.IsValid)
            {
                db.Entry(project).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(project);
        }

        //
        // GET: /Project/Delete/5
 
        public ActionResult Delete(int id = 0)
        {
            Project project = db.Projects.Find(id);
            db.Projects.Remove(project);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Manage(int id = 0)
        {
            Project project = db.Projects.Find(id);
            return View(project);

        }

        public ActionResult Fixture()
        {
            ProjectDBContext db = new ProjectDBContext(); Random rnd = new Random(Environment.TickCount);

            for (int i = 0; i < 3; i++)
                db.Projects.Add(new Project() { Name = String.Format("Project {0}", i), Description = "Project Description" }); db.SaveChanges();
            foreach (Project p in db.Projects)
                for (int i = 0; i < 3; i++)
                    db.Releases.Add(new Release() { Name = String.Format("Release {0}", i), ProjectId = p.ID, Capacity = rnd.Next(50, 100) }); db.SaveChanges();
            foreach (Release r in db.Releases)
            {
                for (int i = 0; i < 3; i++)
                    db.Issues.Add(new Issue() { Name = String.Format("Issue {0}", i), Description = "issue Description", ReleaseId = r.ID }); db.SaveChanges();
                for (int i = 0; i < 3; i++)
                    db.Stories.Add(new Story() { Name = String.Format("Story {0}", i), Description = "Story Description", ProjectId = rnd.Next(1, 3), Estimate = rnd.Next(20) }); db.SaveChanges();
                for (int i = 0; i < 3; i++)
                    db.ReleaseNotes.Add(new ReleaseNote() { Name = String.Format("ReleaseNote {0}", i), Description = "ReleaseNote Description", ReleaseId = r.ID }); db.SaveChanges();
            }
            db.Projects.Add(new Project() { Name = "Empty", Description = "empty project description" }); db.SaveChanges();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}