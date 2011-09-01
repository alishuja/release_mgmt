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

        public ActionResult Create(int id = 0)
        {
            ViewBag.ProjectId = new SelectList(db.Projects, "ID", "Name", id);
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
                return RedirectToAction("Manage", "Project", new { id = release.ProjectId });
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
                return RedirectToAction("Details", "Project", new { id = release.ProjectId });
            }
            ViewBag.ProjectId = new SelectList(db.Projects, "ID", "Name", release.ProjectId);
            return View(release);
        }

        //
        // GET: /Release/Delete/5

        public ActionResult Delete(int id)
        {
            Release release = db.Releases.Find(id);
            
            

            foreach (Story story in release.Stories)
                story.ReleaseId = null;
            foreach (Issue issue in release.Issues)
                issue.ReleaseId = null;
            foreach (ReleaseNote note in release.Notes)
                note.ReleaseId = null;

            db.Releases.Remove(release);
            db.SaveChanges();
            return RedirectToAction("Details", "Project", new { id = release.ProjectId });
        }

        public ActionResult Ajax(int releaseId, int storyId)
        {
            try
            {
                //Release release = db.Releases.Find(releaseId);

                Story story = db.Stories.Find(storyId);
                if (releaseId == -1)
                {
                    story.ReleaseId = null;
                }
                else
                {
                    story.ReleaseId = releaseId;
                }
                db.Entry(story).State = EntityState.Modified;
                db.SaveChanges();
                return Json(new { Message = true });
            }
            catch
            {
                return Json(new { Message = false });
            }
        }


        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}