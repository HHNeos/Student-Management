using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Management.Models;

namespace Management.Controllers
{
    public class RegistraitonController : Controller
    {
        private StudentEntities db = new StudentEntities();

        // GET: Registraiton
        public ActionResult Index()
        {
            var registraitons = db.registraitons.Include(r => r.batch).Include(r => r.course);
            return View(registraitons.ToList());
        }

        // GET: Registraiton/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            registraiton registraiton = db.registraitons.Find(id);
            if (registraiton == null)
            {
                return HttpNotFound();
            }
            return View(registraiton);
        }

        // GET: Registraiton/Create
        public ActionResult Create()
        {
            ViewBag.batch_id = new SelectList(db.batches, "id", "batch1");
            ViewBag.course_id = new SelectList(db.courses, "id", "course1");
            return View();
        }

        // POST: Registraiton/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,firstname,lastname,nic,course_id,batch_id,telno")] registraiton registraiton)
        {
            if (ModelState.IsValid)
            {
                db.registraitons.Add(registraiton);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.batch_id = new SelectList(db.batches, "id", "batch1", registraiton.batch_id);
            ViewBag.course_id = new SelectList(db.courses, "id", "course1", registraiton.course_id);
            return View(registraiton);
        }

        // GET: Registraiton/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            registraiton registraiton = db.registraitons.Find(id);
            if (registraiton == null)
            {
                return HttpNotFound();
            }
            ViewBag.batch_id = new SelectList(db.batches, "id", "batch1", registraiton.batch_id);
            ViewBag.course_id = new SelectList(db.courses, "id", "course1", registraiton.course_id);
            return View(registraiton);
        }

        // POST: Registraiton/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,firstname,lastname,nic,course_id,batch_id,telno")] registraiton registraiton)
        {
            if (ModelState.IsValid)
            {
                db.Entry(registraiton).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.batch_id = new SelectList(db.batches, "id", "batch1", registraiton.batch_id);
            ViewBag.course_id = new SelectList(db.courses, "id", "course1", registraiton.course_id);
            return View(registraiton);
        }

        // GET: Registraiton/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            registraiton registraiton = db.registraitons.Find(id);
            if (registraiton == null)
            {
                return HttpNotFound();
            }
            return View(registraiton);
        }

        // POST: Registraiton/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            registraiton registraiton = db.registraitons.Find(id);
            db.registraitons.Remove(registraiton);
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
