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
    public class ClientInfoesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ClientInfoes
        public ActionResult Index()
        {
            var clientInfo = db.ClientInfo.Include(c => c.AspNetUsers);
            return View(clientInfo.ToList());
        }

        // GET: ClientInfoes/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClientInfo clientInfo = db.ClientInfo.Find(id);
            if (clientInfo == null)
            {
                return HttpNotFound();
            }
            return View(clientInfo);
        }

        // GET: ClientInfoes/Create
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", "LanguageCode");
            return View();
        }

        public ActionResult CreateSP()
        {
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email");//removed "LanguageCode" Weird that it was included here.
            return View();
        }

        // POST: ClientInfoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CientInfoId,BloodType,Approved,UserId")] ClientInfo clientInfo)
        {


            string UserId = User.Identity.GetUserId();
            var userObject = (from x in db.Users where (x.Id == UserId) select x).FirstOrDefault();

            if (ModelState.IsValid)
            {
                clientInfo.AspNetUsers = userObject;

                db.ClientInfo.Add(clientInfo);
                db.SaveChanges();
                
            }

            //ViewBag.UserId = new SelectList(db.ApplicationUsers, "Id", clientInfo.UserId);    //removed "LanguageCode" Weird that it was included here.
            return RedirectToAction("Create", "Addresses");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateSP([Bind(Include = "CientInfoId,BloodType,Approved,UserId")] ClientInfo clientInfo)
        {


            string UserId = User.Identity.GetUserId();
            var userObject = (from x in db.Users where (x.Id == UserId) select x).FirstOrDefault();

            if (ModelState.IsValid)
            {
                clientInfo.AspNetUsers = userObject;

                db.ClientInfo.Add(clientInfo);
                db.SaveChanges();

            }

            //ViewBag.UserId = new SelectList(db.ApplicationUsers, "Id", clientInfo.UserId);    //removed "LanguageCode" Weird that it was included here.
            return RedirectToAction("CreateSP", "Addresses");
                   
        }

        // GET: ClientInfoes/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClientInfo clientInfo = db.ClientInfo.Find(id);
            if (clientInfo == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.ApplicationUsers, "Id", clientInfo.UserId);    //removed "LanguageCode" Weird that it was included here.
            return View(clientInfo);
        }

        // POST: ClientInfoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CientInfoId,DateOfBirth,Sex,BloodType,height,weight,Approved,UserId")] ClientInfo clientInfo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(clientInfo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
           // ViewBag.UserId = new SelectList(db.ApplicationUsers, "Id", clientInfo.UserId);   //removed "LanguageCode" Weird that it was included here.
            return View(clientInfo);
        }

        // GET: ClientInfoes/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClientInfo clientInfo = db.ClientInfo.Find(id);
            if (clientInfo == null)
            {
                return HttpNotFound();
            }
            return View(clientInfo);
        }

        // POST: ClientInfoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ClientInfo clientInfo = db.ClientInfo.Find(id);
            db.ClientInfo.Remove(clientInfo);
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
