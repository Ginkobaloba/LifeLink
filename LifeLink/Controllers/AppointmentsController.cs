using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LifeLink.Models;
using Microsoft.AspNet.Identity;

namespace LifeLink.Controllers
{
    public class AppointmentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Appointments
        public ActionResult Index()
        {
            var appointment = db.Appointment.Include(a => a.AspNetUsers).Include(a => a.Location).Select(a => new AppointmentViewModel()
            {
                id = a.id,
                title = a.title,
                start = a.start,
                end = a.end,
                Status = a.Status,
                LocationName = a.Location.Name,
                Username = a.AspNetUsers.UserName
            });
            return View(appointment.ToList());
        }

        // GET: Appointments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment appointment = db.Appointment.Find(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }
            return View(appointment);
        }

        // GET: Appointments/Create
        public ActionResult Create()
        {
            
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email");
            ViewBag.LocationId = new SelectList(db.Location, "LocationId", "LocationId");

            var UserId = User.Identity.GetUserId();
            var clientInfo = (from c in db.ClientInfo where (c.UserId == UserId) select c).FirstOrDefault();

            if (clientInfo.Approved != true)
            {
                return RedirectToAction("Denial", "Questionnaires");
            }
            return View();
        }

        // POST: Appointments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,title,start,end,Status,LocationId,UserId")] Appointment appointment)
        {
            var UserId = User.Identity.GetUserId();

            var clientInfo = (from c in db.ClientInfo where (c.UserId == appointment.UserId) select c).FirstOrDefault();

            if (clientInfo.Approved != true)
            {
                RedirectToAction("Denial", "Questionnaires");
            }


            if (ModelState.IsValid)
            {
                db.Appointment.Add(appointment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", appointment.UserId);
            ViewBag.LocationId = new SelectList(db.Location, "LocationId", "LocationId", appointment.LocationId);
            return View(appointment);
        }

        // GET: Appointments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment appointment = db.Appointment.Find(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", appointment.UserId);
            ViewBag.LocationId = new SelectList(db.Location, "LocationId", "LocationId", appointment.LocationId);
            return View(appointment);
        }

        // POST: Appointments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,title,start,end,Status,LocationId,UserId")] Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(appointment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", appointment.UserId);
            ViewBag.LocationId = new SelectList(db.Location, "LocationId", "LocationId", appointment.LocationId);
            return View(appointment);
        }

        // GET: Appointments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment appointment = db.Appointment.Find(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }
            return View(appointment);
        }

        // POST: Appointments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Appointment appointment = db.Appointment.Find(id);
            db.Appointment.Remove(appointment);
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
