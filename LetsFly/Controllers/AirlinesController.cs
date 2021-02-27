using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LetsFly.Models;

namespace LetsFly.Controllers
{
    //[Authorize(Roles = "Administrator")]
    [Authorize]
    public class AirlinesController : Controller
    {
        
        private LetsFlyModelContainer db = new LetsFlyModelContainer();

        // GET: Airlines
        public ActionResult Index()
        {

            return View(db.Airlines.ToList());
        }

        // GET: Airlines/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Airline airline = db.Airlines.Find(id);

            if (airline == null)
            {
                return HttpNotFound();
            }

            var ratings = airline.Ratings;
            if (ratings.Count > 0)
            {
                //pass data to view
                var ratingSum = ratings.Sum(d=>d.RatingNumber);
                ViewBag.RatingSum = ratingSum;
                var ratingCount = ratings.Count();
                ViewBag.RatingCount = ratingCount;
            }
            else
            {
                ViewBag.RatingSum = 0;
                ViewBag.RatingCount = 0;
            }

            return View(airline);
        }

        // GET: Airlines/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Airlines/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AirlineId,AirlineName,AirlineCode,AirlineDescription")] Airline airline, HttpPostedFileBase
            postedFile)
        {
            ModelState.Clear();
            var myUniqueFileName = string.Format(@"{0}", Guid.NewGuid());
            airline.AirlineImg = myUniqueFileName;
            TryValidateModel(airline);
            if (ModelState.IsValid)
            {
                if (postedFile != null)
                {
                    string serverPath = Server.MapPath("~/Uploads/");
                    string fileExtension = Path.GetExtension(postedFile.FileName);
                    string filePath = airline.AirlineImg + fileExtension;
                    airline.AirlineImg = filePath;
                    postedFile.SaveAs(serverPath + airline.AirlineImg);
                    db.Airlines.Add(airline);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

            }

            return View(airline);
        }


        // GET: Airlines/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Airline airline = db.Airlines.Find(id);
            if (airline == null)
            {
                return HttpNotFound();
            }
            return View(airline);
        }

        // POST: Airlines/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AirlineId,AirlineName,AirlineImg,AirlineCode,AirlineDescription")] Airline airline)
        {
            if (ModelState.IsValid)
            {
                db.Entry(airline).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(airline);
        }

        // GET: Airlines/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Airline airline = db.Airlines.Find(id);
            if (airline == null)
            {
                return HttpNotFound();
            }
            return View(airline);
        }

        // POST: Airlines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Airline airline = db.Airlines.Find(id);
            db.Airlines.Remove(airline);
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
