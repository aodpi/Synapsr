using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Synapsr.Models;

namespace Synapsr.Controllers
{
    public class VaseaController : Controller
    {
        private DatabaseStore db = new DatabaseStore();

        // GET: Vasea
        public ActionResult Index()
        {
            var users = db.Users.Include(u => u.Elevation).Include(u => u.Specialitate);
            return View(users.ToList());
        }

        // GET: Vasea/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Vasea/Create
        public ActionResult Create()
        {
            ViewBag.ElevationId = new SelectList(db.Elevations, "Id", "ElevationName");
            ViewBag.IdSpecialitate = new SelectList(db.Specialities, "Id", "Name");
            return View();
        }

        // POST: Vasea/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,Email,UserName,Password,IdSpecialitate,ElevationId,avatar_uri")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ElevationId = new SelectList(db.Elevations, "Id", "ElevationName", user.ElevationId);
            ViewBag.IdSpecialitate = new SelectList(db.Specialities, "Id", "Name", user.IdSpecialitate);
            return View(user);
        }

        // GET: Vasea/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.ElevationId = new SelectList(db.Elevations, "Id", "ElevationName", user.ElevationId);
            ViewBag.IdSpecialitate = new SelectList(db.Specialities, "Id", "Name", user.IdSpecialitate);
            return View(user);
        }

        // POST: Vasea/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,Email,UserName,Password,IdSpecialitate,ElevationId,avatar_uri")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ElevationId = new SelectList(db.Elevations, "Id", "ElevationName", user.ElevationId);
            ViewBag.IdSpecialitate = new SelectList(db.Specialities, "Id", "Name", user.IdSpecialitate);
            return View(user);
        }

        // GET: Vasea/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Vasea/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
