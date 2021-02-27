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
    [Authorize]
    public class AirportsController : Controller
    {
        private LetsFlyModelContainer db = new LetsFlyModelContainer();

        // GET: Airports
        public ActionResult Index()
        {
            return View(db.Airports.ToList());
        }

        // GET: Airports/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Airport airport = db.Airports.Find(id);
            if (airport == null)
            {
                return HttpNotFound();
            }
            return View(airport);
        }

        [Authorize(Roles = "Administrator")]
        // GET: Airports/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: Airports/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AirportId,AirportName,AirportCode,AirportLocationName,AirportLong,AirportLat")] Airport airport)
        {
            if (ModelState.IsValid)
            {
                db.Airports.Add(airport);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(airport);
        }

        [Authorize(Roles = "Administrator")]
        // GET: Airports/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Airport airport = db.Airports.Find(id);
            if (airport == null)
            {
                return HttpNotFound();
            }
            
            return View(airport);
        }

        // POST: Airports/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AirportId,AirportName,AirportCode,AirportLocationName,AirportLong,AirportLat")] Airport airport)
        {
            if (ModelState.IsValid)
            {
                db.Entry(airport).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
           
            return View(airport);
        }

        [Authorize(Roles = "Administrator")]
        // GET: Airports/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Airport airport = db.Airports.Find(id);
            if (airport == null)
            {
                return HttpNotFound();
            }
            return View(airport);
        }

        // POST: Airports/Delete/5
        [Authorize(Roles = "Administrator")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Airport airport = db.Airports.Find(id);
            db.Airports.Remove(airport);
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
