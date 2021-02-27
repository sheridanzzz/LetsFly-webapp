using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LetsFly.Models;

namespace LetsFly.Controllers
{
    [Authorize]
    public class FlightsController : Controller
    {
        private LetsFlyModelContainer db = new LetsFlyModelContainer();

        // GET: Flights
        public ActionResult Index()
        {
            var flights = db.Flights.Include(f => f.Airline);
            return View(flights.ToList());
        }

        // GET: Flights/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Flight flight = db.Flights.Find(id);
            if (flight == null)
            {
                return HttpNotFound();
            }
            return View(flight);
        }

        [Authorize(Roles = "Administrator")]
        // GET: Flights/Create
        public ActionResult Create()
        {
            ViewBag.AirlineId = new SelectList(db.Airlines, "AirlineId", "AirlineName");

            var airport = db.Airports.ToList();

            ViewBag.MyList = airport;

            return View();
        }

        // POST: Flights/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FlightId,FlightNumber,DepartureDate,ArrivalDate,Price,Capacity,Duration,AirlineId,ArrivalAirport,DepartureAirport")] Flight flight)
        {
            if (ModelState.IsValid)
            {

                var airportId = db.Airports.Where(e => e.AirportLocationName == flight.DepartureAirport).Select(e=>e.AirportId).First();
                flight.AirportId = airportId;
                db.Flights.Add(flight);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AirlineId = new SelectList(db.Airlines, "AirlineId", "AirlineName", flight.AirlineId);
            return View(flight);
        }

        [Authorize(Roles = "Administrator")]
        // GET: Flights/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Flight flight = db.Flights.Find(id);
            if (flight == null)
            {
                return HttpNotFound();
            }
            ViewBag.AirlineId = new SelectList(db.Airlines, "AirlineId", "AirlineName", flight.AirlineId);
            return View(flight);
        }

        // POST: Flights/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FlightId,FlightNumber,DepartureDate,ArrivalDate,Price,Capacity,Duration,AirlineId,ArrivalAirport,DepartureAirport")] Flight flight)
        {
            if (ModelState.IsValid)
            {
                db.Entry(flight).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AirlineId = new SelectList(db.Airlines, "AirlineId", "AirlineName", flight.AirlineId);
            return View(flight);
        }

        [Authorize(Roles = "Administrator")]
        // GET: Flights/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Flight flight = db.Flights.Find(id);
            if (flight == null)
            {
                return HttpNotFound();
            }
            return View(flight);
        }

        // POST: Flights/Delete/5
        [Authorize(Roles = "Administrator")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Flight flight = db.Flights.Find(id);
            db.Flights.Remove(flight);
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
