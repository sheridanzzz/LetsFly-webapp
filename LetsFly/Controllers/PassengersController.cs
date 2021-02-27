﻿using System;
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
    public class PassengersController : Controller
    {
        private LetsFlyModelContainer db = new LetsFlyModelContainer();

        // GET: Passengers
        public ActionResult Index()
        {
            var passengers = db.Passengers.Include(p => p.Booking);
            return View(passengers.ToList());
        }

        // GET: Passengers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Passenger passenger = db.Passengers.Find(id);
            if (passenger == null)
            {
                return HttpNotFound();
            }
            return View(passenger);
        }

        // GET: Passengers/Create
        public ActionResult Create()
        {
            ViewBag.BookingNumber = new SelectList(db.Bookings, "BookingNumber", "Price");
            return View();
        }

        // POST: Passengers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PassengerId,Email,FirstName,LastName,PassportNo,BookingNumber")] Passenger passenger)
        {
            if (ModelState.IsValid)
            {
                db.Passengers.Add(passenger);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BookingNumber = new SelectList(db.Bookings, "BookingNumber", "Price", passenger.BookingNumber);
            return View(passenger);
        }

        // GET: Passengers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Passenger passenger = db.Passengers.Find(id);
            if (passenger == null)
            {
                return HttpNotFound();
            }
            ViewBag.BookingNumber = new SelectList(db.Bookings, "BookingNumber", "Price", passenger.BookingNumber);
            return View(passenger);
        }

        // POST: Passengers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PassengerId,Email,FirstName,LastName,PassportNo,BookingNumber")] Passenger passenger)
        {
            if (ModelState.IsValid)
            {
                db.Entry(passenger).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BookingNumber = new SelectList(db.Bookings, "BookingNumber", "Price", passenger.BookingNumber);
            return View(passenger);
        }

        // GET: Passengers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Passenger passenger = db.Passengers.Find(id);
            if (passenger == null)
            {
                return HttpNotFound();
            }
            return View(passenger);
        }

        // POST: Passengers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Passenger passenger = db.Passengers.Find(id);
            db.Passengers.Remove(passenger);
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
