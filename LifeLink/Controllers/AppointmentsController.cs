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

        // GET: Events 
        public ActionResult Index()
        {
            var events = db.Event.Include(b => b.AspNetUsers).Include(b => b.Location);
            return View(events.ToList());
            
        }

        // GET: Events/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment @event = db.Event.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        // GET: Events/Create
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.ApplicationUsers, "Id", "Email");
            ViewBag.LocationId = new SelectList(db.Location, "LocationId", "LocationId");
            return View();
        }

        // POST: Events/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EventID,EventDate,LocationId,UserId")] Appointment @event)
        {
            var UserId = User.Identity.GetUserId();
            var user = db.Questionnaire.Include(q => q.ClientInfo);

           var userNameObject = (from x in db.Address where (x.UserId == UserId) select x).FirstOrDefault();
            var userEmailObject = (from z in db.Users where (z.Id == UserId) select z).FirstOrDefault();

            if (ModelState.IsValid)
            {
                db.Event.Add(@event);
                db.SaveChanges();
                string message = CreateAppointmentMessage(userNameObject.FirstName);
                RedirectToAction("SendSimpleMessage", "Addresses", new { userEmailObject.Email, userNameObject.FirstName, message });
                
                return RedirectToAction("Index");//Needs to go to a thank you page.
            }

            ViewBag.UserId = new SelectList(db.ApplicationUsers, "Id", "Email", @event.UserId);
            ViewBag.LocationId = new SelectList(db.Location, "LocationId", "LocationId", @event.LocationId);
            return View(@event);
        }



        string CreateAppointmentMessage(string name)
        {
            string message = string.Format("Dear, {0},\nThank you for booking your appointment through LifeLink.As a member you will earn reward"+
                                            " points and recognition based on your donation behavior and blood typing. Bonus points can"+
                                            " be earned through referring qualified friends to join LifeLink and sharing your experience"+
                                            " via social media. Reward points can be redeemed for t-shirts, coffee mugs, gift cards and"+
                                            " even recognition on our Hall of Fame, for our most valuable members.\n\nBest Regards,\n\n"+
                                            "The LifeLink Team", name);
            return message;
        }

        string CreateUpcomingAppointmentMessage(string name)
        {

            string message = string.Format("Dear, {0},\nYour LifeLink blood donation center appointment is coming up soon! LifeLink,"+
                                            " its blood recipients and your fellow donors are excited to have you join us in our cause."+
                                            " We believe that heros like you should get rewarded for your actions. Make sure to arrive 15"+
                                            " minutes prior to your scheduled time to ensure proper registration can be completed.\n\nYour"+
                                            " appointment time is XXX on XXX date.\n\nWarm Regards,\n\nThe LifeLink Team", name);
            return message;
        }

        
        // GET: Events/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment @event = db.Event.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.ApplicationUsers, "Id", "Email", @event.UserId);
            ViewBag.LocationId = new SelectList(db.Location, "LocationId", "LocationId", @event.LocationId);
            return View(@event);
        }

        // POST: Events/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EventID,EventDate,LocationId,UserId")] Appointment @event)
        {
            if (ModelState.IsValid)
            {
                db.Entry(@event).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.ApplicationUsers, "Id", "Email", @event.UserId);
            ViewBag.LocationId = new SelectList(db.Location, "LocationId", "LocationId", @event.LocationId);
            return View(@event);
        }

        // GET: Events/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment @event = db.Event.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Appointment @event = db.Event.Find(id);
            db.Event.Remove(@event);
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
