using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LetsFly.Models;
using Microsoft.AspNet.Identity;
using LetsFly.Utils;
using Rotativa;


namespace LetsFly.Controllers
{
    [Authorize]
    public class BookingsController : Controller
    {
        private LetsFlyModelContainer db = new LetsFlyModelContainer();

        // GET: Bookings
        public ActionResult Index()
        {
            //get bookings for the current user
            string currentUserId = User.Identity.GetUserId();
            var bookings = db.Bookings.Include(b => b.User);

            var guid = new Guid(currentUserId);

            var count = db.Bookings.Where(m => m.UserId == guid).ToList();

            if (count.Count == 0)
            {
                ViewBag.Result = "No Bookings Found!";
            }

            return View(db.Bookings.Where(m => m.UserId == guid).ToList());
        }

        public ActionResult view()
        {
           

            return View(db.Bookings.ToList());
        }

        [AllowAnonymous]
        // GET: Bookings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        //Generating PDF
        public ActionResult PrintInvoice(int? bookid)
        {
            return new ActionAsPdf(
                    "Details",
                    new { id = bookid })
                { FileName = "Invoice.pdf" };
        }

        // GET: Bookings/Create
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.Users, "UserId", "FirstName");
            return View();
        }

        // POST: Bookings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BookingNumber,Price,State,UserId")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                booking.BookingDate = DateTime.Now;
                db.Bookings.Add(booking);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.Users, "UserId", "FirstName", booking.UserId);
            return View(booking);
        }

        // GET: Bookings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.Users, "UserId", "FirstName", booking.UserId);
            return View(booking);
        }

        // POST: Bookings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BookingNumber,Price,State,UserId")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                booking.BookingDate = booking.BookingDate;
                db.Entry(booking).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.Users, "UserId", "FirstName", booking.UserId);
            return View(booking);
        }

        // GET: Bookings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        // POST: Bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Booking booking = db.Bookings.Find(id);
            var flightId = booking.Flight.FlightId;
            Flight flight = db.Flights.Find(flightId);

            // Capactiy will based on the Capacity of Flight
            var capacity = flight.Capacity;

            // A list to store number of bookings available at the current time
            List<Booking> confirmedBooking = db.Bookings.Where(r => r.State == "Confirmed").ToList(); // store the booking list which is confirmed


            if (confirmedBooking.Count() < capacity)
            {
                //Add date to booking
                List<Booking> pendingBooking = db.Bookings.Where(r => r.State == "Pending").ToList();
                pendingBooking = pendingBooking.OrderBy(e => e.BookingDate).ToList();
                
                if (pendingBooking.Any())
                {
                    //changes from pending to confirmed
                    pendingBooking[0].State = "Confirmed";

                    EmailSender es = new EmailSender();

                    var user = pendingBooking[0].User;
                    var toEmail = user.Email;

                    es.Send(toEmail, "Booking Confirmed!", "Booking status has changed to Confirmed");
                    es.SendBooking(flight, toEmail);
                    // Change the Status of that pendingBooking[0] to "Confirmed" and send Email

                }

            }
            db.Bookings.Remove(booking);
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
