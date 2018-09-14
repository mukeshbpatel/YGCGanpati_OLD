using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YGCGanpati.Models;
using Microsoft.AspNet.Identity;

namespace YGCGanpati.Controllers
{
    [Authorize]
    public class QuizController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();        

        public ActionResult Index()
        {            
            var time = DateTime.Now;
            var usrid = User.Identity.GetUserId();
            var questions = from c in db.QuizQuestions
                            where c.DisplayDate < time &&
                            !(from o in db.QuizAnswers
                              where o.UserProfile.Id == usrid
                              select o.QuestionID).Contains(c.QuestionID)
                            select c;
            return View(questions.OrderBy(o=>o.DisplayDate));
        }

        [HttpPost]
        public ActionResult Index(FormCollection collection)
        {
            var usrid = User.Identity.GetUserId();
            var time = DateTime.Now;
            foreach (var item in collection.AllKeys)
            {
                if (item.StartsWith("radio_"))
                {
                    var ans = int.Parse(collection[item].Split('_')[1]);
                    var qns = int.Parse(item.Split('_')[1]);
                    var userans = db.QuizAnswers.Where(u => u.UserProfile.Id == usrid && u.QuestionID == qns).SingleOrDefault();
                    if(userans == null)
                    {
                        userans = new QuizAnswer();
                        userans.UserProfile = db.Users.Find(usrid);
                        userans.QuestionID = qns;
                        userans.Answer = ans;
                        userans.AnswerDate = time;                        
                        db.QuizAnswers.Add(userans);
                        db.SaveChanges();
                    }
                }
            }
            return RedirectToAction("Result");
        }


        public ActionResult Result(string id)
        {
            string userid = User.Identity.GetUserId();
            if(!string.IsNullOrEmpty(id))
            {
                userid = id;
            }
            string sql = "select quizquestions.QuestionID,Question,ImgURL,Option1,Option2,Option3,Option4,quizquestions.Answer,DisplayDate,AnswerID,quizanswers.Answer Answers,AnswerDate,case WHEN AnswerID IS NULL THEN 0 WHEN quizquestions.Answer = quizanswers.Answer THEN 2 ELSE -1 END Points,TIME_TO_SEC(TIMEDIFF(DisplayDate, AnswerDate)) TimeTaken,UserProfile_ID from quizquestions INNER JOIN quizanswers on quizquestions.QuestionID = quizanswers.QuestionID and quizanswers.UserProfile_ID='" + userid + "' where DisplayDate <= NOW() ORDER BY DisplayDate";
            var QList = db.Database.SqlQuery<UserAnswer>(sql).ToList();
            return View(QList);
        }

        public ActionResult Winner()
        {
            string sql = @"SELECT Id,Name,FlatNo,SUM(Points) Points,AVG(TimeTaken) TimeTaken
                            FROM (
                            select 
                            aspnetusers.Id,aspnetusers.Name,aspnetusers.FlatNo,
                            case WHEN AnswerID IS NULL THEN 0 WHEN quizquestions.Answer = quizanswers.Answer THEN 2 ELSE -1 END Points,TIME_TO_SEC(TIMEDIFF(AnswerDate,DisplayDate)) TimeTaken
                            from quizquestions INNER JOIN quizanswers on quizquestions.QuestionID = quizanswers.QuestionID
                            INNER JOIN aspnetusers ON aspnetusers.Id = quizanswers.UserProfile_Id) TBL
                            GROUP BY Id,Name,FlatNo
                            ORDER By Points desc, TimeTaken";
            var QList = db.Database.SqlQuery<UserWinner>(sql).ToList();
            return View(QList);
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