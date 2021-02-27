using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.EnterpriseServices;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LetsFly.Models;
using LetsFly.Utils;
using Microsoft.AspNet.Identity;

namespace LetsFly.Controllers
{
    [Authorize]
    public class RatingsController : Controller
    {
        private LetsFlyModelContainer db = new LetsFlyModelContainer();

        // GET: Ratings
        public ActionResult Index()
        {
            var ratings = db.Ratings.Include(r => r.User).Include(r => r.Airline);

            var count = db.Ratings.ToList();

            //checks if user has ratings
            if (count.Count == 0)
            {
                ViewBag.Result = "No Ratings Found!";
            }
            return View(ratings.ToList());
        }

        // GET: Ratings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rating rating = db.Ratings.Find(id);
            if (rating == null)
            {
                return HttpNotFound();
            }

            return View(rating);
        }

        // GET: Ratings/Create
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.Users, "UserId", "FirstName");
            ViewBag.AirlineId = new SelectList(db.Airlines, "AirlineId", "AirlineName");
            return View();
        }

        // POST: Ratings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RatingId,RatingNumber,RatingImg,RatingDescription,AirlineId")] Rating rating)
        {
            if (ModelState.IsValid)
            {
                var guid = new Guid(User.Identity.GetUserId());
                rating.UserId = guid;
                rating.RatingDate = DateTime.Now;
                db.Ratings.Add(rating);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.Users, "UserId", "FirstName", rating.UserId);
            ViewBag.AirlineId = new SelectList(db.Airlines, "AirlineId", "AirlineName", rating.AirlineId);
            return View(rating);
        }

        // GET: Ratings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rating rating = db.Ratings.Find(id);
            if (rating == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.Users, "UserId", "FirstName", rating.UserId);
            ViewBag.AirlineId = new SelectList(db.Airlines, "AirlineId", "AirlineName", rating.AirlineId);
            return View(rating);
        }

        // POST: Ratings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RatingId,RatingNumber,RatingImg,RatingDate,RatingDescription,UserId,AirlineId")] Rating rating)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rating).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.Users, "UserId", "FirstName", rating.UserId);
            ViewBag.AirlineId = new SelectList(db.Airlines, "AirlineId", "AirlineName", rating.AirlineId);
            return View(rating);
        }

        // GET: Ratings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rating rating = db.Ratings.Find(id);
            if (rating == null)
            {
                return HttpNotFound();
            }
            return View(rating);
        }

        // POST: Ratings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Rating rating = db.Ratings.Find(id);
            db.Ratings.Remove(rating);
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
