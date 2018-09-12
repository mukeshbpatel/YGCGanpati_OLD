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

        // GET: QuizController
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Play()
        {
            string sql = "select quizquestions.QuestionID,Question,ImgURL,Option1,Option2,Option3,Option4,quizquestions.Answer,DisplayDate,AnswerID,quizanswers.Answer,AnswerDate,case WHEN AnswerID IS NULL THEN 0 WHEN quizquestions.Answer = quizanswers.Answer THEN 2 ELSE -1 END Points,TIME_TO_SEC(TIMEDIFF(DisplayDate, AnswerDate)) TimeTaken,UserProfile_ID from quizquestions left OUTER JOIN quizanswers on quizquestions.QuestionID = quizanswers.QuestionID and quizanswers.UserProfile_ID='" + User.Identity.GetUserId() + "' where DisplayDate <= NOW() ORDER BY DisplayDate Desc,QuestionID";
            var QList = db.Database.SqlQuery<UserAnswer>(sql).ToList();
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