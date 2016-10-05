using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LifeLink.Models;

namespace LifeLink.Controllers
{
    public class QuestionnairesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Questionnaires
        public ActionResult Index()
        {
            var questionnaire = db.Questionnaire.Include(q => q.ClientInfo);
            return View(questionnaire.ToList());
        }

        // GET: Questionnaires/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Questionnaire questionnaire = db.Questionnaire.Find(id);
            if (questionnaire == null)
            {
                return HttpNotFound();
            }
            return View(questionnaire);
        }

        // GET: Questionnaires/Create
        public ActionResult Create()
        {
            ViewBag.ClientInfoId = new SelectList(db.ClientInfo, "CientInfoId", "Sex");
            return View();
        }

        // POST: Questionnaires/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "QuestionnaireId,GeneralHealth1,DonationHistory2,VaxOrShots3,Pregnant4,Medications5,Weight6,RiskySex7,TatooOrPiercing8,Jail9,Needles10,ClientInfoId")] Questionnaire questionnaire)
        {
            if (ModelState.IsValid)
            {
                db.Questionnaire.Add(questionnaire);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClientInfoId = new SelectList(db.ClientInfo, "CientInfoId", "Sex", questionnaire.ClientInfoId);
            return View(questionnaire);
        }

        // GET: Questionnaires/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Questionnaire questionnaire = db.Questionnaire.Find(id);
            if (questionnaire == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClientInfoId = new SelectList(db.ClientInfo, "CientInfoId", "Sex", questionnaire.ClientInfoId);
            return View(questionnaire);
        }

        // POST: Questionnaires/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "QuestionnaireId,GeneralHealth1,DonationHistory2,VaxOrShots3,Pregnant4,Medications5,Weight6,RiskySex7,TatooOrPiercing8,Jail9,Needles10,ClientInfoId")] Questionnaire questionnaire)
        {
            if (ModelState.IsValid)
            {
                db.Entry(questionnaire).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClientInfoId = new SelectList(db.ClientInfo, "CientInfoId", "Sex", questionnaire.ClientInfoId);
            return View(questionnaire);
        }

        // GET: Questionnaires/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Questionnaire questionnaire = db.Questionnaire.Find(id);
            if (questionnaire == null)
            {
                return HttpNotFound();
            }
            return View(questionnaire);
        }

        // POST: Questionnaires/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Questionnaire questionnaire = db.Questionnaire.Find(id);
            db.Questionnaire.Remove(questionnaire);
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
