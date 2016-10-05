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
        public ActionResult Create([Bind(Include = "QuestionnaireId,GeneralHealth1,DonationHistory2,VaxOrShots3,Pregnant4,Medications5,Weight6,"+
                                                    "RiskySex7,TatooOrPiercing8,Jail9,Needles10,ClientInfoId")] Questionnaire questionnaire)
        {
            var UserId = User.Identity.GetUserId();
            var user = db.Questionnaire.Include(q => q.ClientInfo);

            var userNameObject = (from x in db.Address where (x.UserId == UserId) select x).FirstOrDefault();
            var userEmailObject = (from z in db.Users where (z.Id == questionnaire.ClientInfo.UserId) select z).FirstOrDefault();

            

           
            
            
            if (ModelState.IsValid)
            {
                db.Questionnaire.Add(questionnaire);
                db.SaveChanges();


                if (questionnaire.GeneralHealth1 && questionnaire.DonationHistory2 && questionnaire.VaxOrShots3 &&
                    questionnaire.Pregnant4 && questionnaire.Medications5 && questionnaire.Weight6 && questionnaire.RiskySex7 &&
                    questionnaire.TatooOrPiercing8 && questionnaire.Jail9 && questionnaire.Needles10)
                {
                    string message = CreateApprovalMessage(userNameObject.FirstName);
                    RedirectToAction("SendSimpleMessage", "Addresses", new { userEmailObject.Email, userNameObject.FirstName, message });
                    return RedirectToAction("Details", "Addresses");
                }

                else CreateDenialMessage(userNameObject.FirstName);



                return RedirectToAction("Index");
            }

            ViewBag.ClientInfoId = new SelectList(db.ClientInfo, "CientInfoId", "Sex", questionnaire.ClientInfoId);
            return View(questionnaire);
        }


        string CreateApprovalMessage(string name)
        {
            string message = string.Format("Thank you, {0}, for taking our basic blood donor eligibility questionnaire. You are approved to make an appointment" +
                            " at a LifeLink donation center of your choice. If you have not already made your appointment, feel free to log" +
                            " in to your LifeLink account at any time to schedule your next donation. You may be asked additional questions" +
                            " on the day of your appointment. Once again, we thank you for choosing to donate through LifeLink!", name);

            return message;

        }        

        string CreateDenialMessage(string name)
        {

            string message = string.Format("Thank you, {0}, for taking our basic blood donor eligibility questionnaire. Unfortunately, based on your answers"+
                            ", we cannot schedule an appointment for you today. Not every hero makes it into battle. Donor eligibility is a "+
                            "process by which we determine the best fit for our recipient patients. In some cases a basic survey is not complete"+
                            "enough to determine your eligibility. We invite you to contact your nearest LifeLink donation center by phone to find"+
                            "out how or when you might become eligible.", name);
            return message;


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
