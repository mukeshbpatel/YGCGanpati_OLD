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
    public class CollectionController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Collection
        public ActionResult Index()
        {
            var collections = db.Collections.OrderBy(o=>o.FlatNo);
            return View(collections.ToList());
        }

        // GET: Collection/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Collection collection = db.Collections.Find(id);
            if (collection == null)
            {
                return HttpNotFound();
            }
            return View(collection);
        }

        // GET: Collection/Create
        [Authorize(Roles = "Manager, Admin")]
        public ActionResult Create()
        {            
            return View();
        }

        // POST: Collection/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Manager, Admin")]
        //[ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CollectionID,FirstName,LastName,FlatNo,CollectionDate,Amount")] Collection collection)
        {

            collection.CreatedDate = Common.GetCurrentDate();
            collection.CollectionDate = Common.GetCurrentDate().Date;
            collection.ModifiedDate = collection.CreatedDate;
            collection.UserProfile = db.Users.Find(User.Identity.GetUserId());

            if (ModelState.IsValid)
            {
                db.Collections.Add(collection);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            
            return View(collection);
        }

        // GET: Collection/Edit/5
        [Authorize(Roles = "Manager, Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Collection collection = db.Collections.Find(id);
            if (collection == null)
            {
                return HttpNotFound();
            }            
            return View(collection);
        }

        // POST: Collection/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Manager, Admin")]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CollectionID,FirstName,LastName,FlatNo,CollectionDate,Amount,CreatedDate")] Collection collection)
        {            
            collection.ModifiedDate = Common.GetCurrentDate();
            collection.UserProfile = db.Users.Find(User.Identity.GetUserId());
            if (ModelState.IsValid)
            {
                db.Entry(collection).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }            
            return View(collection);
        }

        // GET: Collection/Delete/5
        [Authorize(Roles = "Manager, Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Collection collection = db.Collections.Find(id);
            if (collection == null)
            {
                return HttpNotFound();
            }
            return View(collection);
        }

        // POST: Collection/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Manager, Admin")]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Collection collection = db.Collections.Find(id);
            db.Collections.Remove(collection);
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
