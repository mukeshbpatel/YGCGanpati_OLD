using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using YGCGanpati.Models;

namespace YGCGanpati.Controllers
{
    [Authorize(Roles ="Admin")]
    public class QuizQuestionController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: QuizQuestion
        public ActionResult Index()
        {
            return View(db.QuizQuestions.ToList());
        }

        // GET: QuizQuestion/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuizQuestion quizQuestion = db.QuizQuestions.Find(id);
            if (quizQuestion == null)
            {
                return HttpNotFound();
            }
            return View(quizQuestion);
        }

        // GET: QuizQuestion/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: QuizQuestion/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "QuestionID,Question,ImgURL,Option1,Option2,Option3,Option4,Answer,DisplayDate")] QuizQuestion quizQuestion)
        {
            if (ModelState.IsValid)
            {
                db.QuizQuestions.Add(quizQuestion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(quizQuestion);
        }

        // GET: QuizQuestion/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuizQuestion quizQuestion = db.QuizQuestions.Find(id);
            if (quizQuestion == null)
            {
                return HttpNotFound();
            }
            return View(quizQuestion);
        }

        // POST: QuizQuestion/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "QuestionID,Question,ImgURL,Option1,Option2,Option3,Option4,Answer,DisplayDate")] QuizQuestion quizQuestion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(quizQuestion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(quizQuestion);
        }

        // GET: QuizQuestion/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuizQuestion quizQuestion = db.QuizQuestions.Find(id);
            if (quizQuestion == null)
            {
                return HttpNotFound();
            }
            return View(quizQuestion);
        }

        // POST: QuizQuestion/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            QuizQuestion quizQuestion = db.QuizQuestions.Find(id);
            db.QuizQuestions.Remove(quizQuestion);
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
