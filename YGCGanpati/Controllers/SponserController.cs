using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using YGCGanpati.Models;
using Microsoft.AspNet.Identity;
namespace YGCGanpati.Controllers
{
    [Authorize]
    public class SponserController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Sponser        
        public ActionResult Index()
        {
            var sponsers = db.Sponsers;
            return View(sponsers.ToList());
        }

        // GET: Sponser/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sponser sponser = db.Sponsers.Find(id);
            if (sponser == null)
            {
                return HttpNotFound();
            }
            return View(sponser);
        }

        // GET: Sponser/Create
        [Authorize(Roles = "Manager, Admin")]
        public ActionResult Create()
        {            
            return View();
        }

        // POST: Sponser/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Manager, Admin")]
        //[ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SponserID,FirstName,LastName,FlatNo,SponserDate,Amount,Details")] Sponser sponser)
        {
            sponser.SponserDate = Common.GetCurrentDate().Date;
            sponser.CreatedDate = Common.GetCurrentDate();
            sponser.ModifiedDate = sponser.CreatedDate;
            sponser.UserProfile = db.Users.Find(User.Identity.GetUserId());
            if (ModelState.IsValid)
            {
                db.Sponsers.Add(sponser);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            
            return View(sponser);
        }

        // GET: Sponser/Edit/5
        [Authorize(Roles = "Manager, Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sponser sponser = db.Sponsers.Find(id);
            if (sponser == null)
            {
                return HttpNotFound();
            }            
            return View(sponser);
        }

        // POST: Sponser/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Manager, Admin")]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SponserID,FirstName,LastName,FlatNo,SponserDate,Amount,Details,CreatedDate")] Sponser sponser)
        {
            sponser.ModifiedDate = Common.GetCurrentDate();
            sponser.UserProfile = db.Users.Find(User.Identity.GetUserId());
            if (ModelState.IsValid)
            {
                db.Entry(sponser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }            
            return View(sponser);
        }

        // GET: Sponser/Delete/5
        [Authorize(Roles = "Manager, Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sponser sponser = db.Sponsers.Find(id);
            if (sponser == null)
            {
                return HttpNotFound();
            }
            return View(sponser);
        }

        // POST: Sponser/Delete/5
        [Authorize(Roles = "Manager, Admin")]
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sponser sponser = db.Sponsers.Find(id);
            db.Sponsers.Remove(sponser);
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
