using LetsFly.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LetsFly.Models;
using System.IO;
using System.Net;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using System.Web.Security.AntiXss;
using IronPdf;

namespace LetsFly.Controllers
{
   
    [RequireHttps]
    public class HomeController : Controller
    {
        //Admin email - Admin@letsfly.com password: Abc123!
        private LetsFlyModelContainer db = new LetsFlyModelContainer();

        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult About()
        {
            ViewBag.Message = "About";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Contact";

            return View();
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult Send_Email()
        {
            return View(new SendEmails());
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public ActionResult Send_Email(SendEmails model, HttpPostedFileBase fileUploader)
        {
            if (ModelState.IsValid)
            {
                if(fileUploader != null)
                {
                    //email for attachments 
                    string serverPath = Server.MapPath("~/Uploads/");
                    string fileName = Path.GetFileName(fileUploader.FileName);
                    string filePath = serverPath + fileName;
                    model.Attachment = filePath;
                    fileUploader.SaveAs(serverPath + fileUploader.FileName);

                    String toEmail = model.ToEmail;
                    String subject = model.Subject;
                    String contents = model.Contents;
                    String attachment = model.Attachment;

                    EmailSender es = new EmailSender();
                    es.SendAttachment(toEmail, subject, contents, attachment, fileName);

                    ViewBag.Result = "Email with attachment has been send.";


                    ModelState.Clear();

                    return View(new SendEmails());
                }
                else if(fileUploader == null)
                {
                    String toEmail = model.ToEmail;
                    String subject = model.Subject;
                    String contents = model.Contents;

                    EmailSender es = new EmailSender();
                    es.Send(toEmail, subject, contents);

                    ViewBag.Result = "Email has been send.";


                    ModelState.Clear();

                    return View(new SendEmails());
                }
            }

            return View();
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult Send_BulkEmails()
        {
            var data = db.Users.Select(e => e.Email).ToList();

            var model = new SendBulkEmail();
            if (TempData["shortMessage"] != null)
            {
                ViewBag.Result = TempData["shortMessage"].ToString();
                TempData.Remove("shortMessage");
            }
            

            model.EmailList = data;

            return View(model);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public ActionResult Send_BulkEmails(string[] data, SendBulkEmail model, HttpPostedFileBase fileUploader)
        {
            if (ModelState.IsValid)
            {
                foreach (var emailX in data)
                {
                    //bulk emails
                    string serverPath = Server.MapPath("~/Uploads/");
                    string fileName = Path.GetFileName(fileUploader.FileName);
                    string filePath = serverPath + fileName;
                    model.Attachment = filePath;
                    fileUploader.SaveAs(serverPath + fileUploader.FileName);

                    String toEmail = emailX;
                    String subject = model.Subject;
                    String contents = model.Contents;
                    String attachment = model.Attachment;

                    EmailSender es = new EmailSender();
                    es.SendAttachment(toEmail, subject, contents, attachment, fileName);
                }


                ModelState.Clear();

                TempData["shortMessage"] = "Successfully Sent!";
                
                return RedirectToAction("Send_BulkEmails");
            }

            return View();
        }

        [Authorize]
        public ActionResult Chart()
        {

            List<ChartModel.DataPoint> dataPoints1 = new List<ChartModel.DataPoint>();
            List<ChartModel.DataPoint> dataPoints2 = new List<ChartModel.DataPoint>();
            List<ChartModel.DataPoint> dataPoints3 = new List<ChartModel.DataPoint>();

            dataPoints1.Add(new ChartModel.DataPoint("Jan", 72));
            dataPoints1.Add(new ChartModel.DataPoint("Feb", 67));
            dataPoints1.Add(new ChartModel.DataPoint("Mar", 55));
            dataPoints1.Add(new ChartModel.DataPoint("Apr", 42));
            dataPoints1.Add(new ChartModel.DataPoint("May", 40));
            dataPoints1.Add(new ChartModel.DataPoint("Jun", 35));

            dataPoints2.Add(new ChartModel.DataPoint("Jan", 48));
            dataPoints2.Add(new ChartModel.DataPoint("Feb", 56));
            dataPoints2.Add(new ChartModel.DataPoint("Mar", 50));
            dataPoints2.Add(new ChartModel.DataPoint("Apr", 47));
            dataPoints2.Add(new ChartModel.DataPoint("May", 65));
            dataPoints2.Add(new ChartModel.DataPoint("Jun", 69));

            dataPoints3.Add(new ChartModel.DataPoint("Jan", 38));
            dataPoints3.Add(new ChartModel.DataPoint("Feb", 46));
            dataPoints3.Add(new ChartModel.DataPoint("Mar", 55));
            dataPoints3.Add(new ChartModel.DataPoint("Apr", 70));
            dataPoints3.Add(new ChartModel.DataPoint("May", 77));
            dataPoints3.Add(new ChartModel.DataPoint("Jun", 91));

            ViewBag.DataPoints1 = JsonConvert.SerializeObject(dataPoints1);
            ViewBag.DataPoints2 = JsonConvert.SerializeObject(dataPoints2);
            ViewBag.DataPoints3 = JsonConvert.SerializeObject(dataPoints3);

            return View();
        }

        [Authorize]
        public ActionResult PopularDestinations()
        {
            //chart implementation some documentation from canvasjs
            List<PopChartModel.DataPoint> dataPoints = new List<PopChartModel.DataPoint>();

            dataPoints.Add(new PopChartModel.DataPoint(519960, "Eastern Australia", "#E7823A"));
            dataPoints.Add(new PopChartModel.DataPoint(363040, "Western Australia", "#546BC1"));

            ViewBag.NewVsReturningVisitors = JsonConvert.SerializeObject(dataPoints);

            dataPoints = new List<PopChartModel.DataPoint>();
            dataPoints.Add(new PopChartModel.DataPoint(1547078400000, 33000));
            dataPoints.Add(new PopChartModel.DataPoint(1549756800000, 35960));
            dataPoints.Add(new PopChartModel.DataPoint(1552176000000, 42160));
            dataPoints.Add(new PopChartModel.DataPoint(1554854400000, 42240));
            dataPoints.Add(new PopChartModel.DataPoint(1557446400000, 43200));
            dataPoints.Add(new PopChartModel.DataPoint(1560124800000, 40600));

            ViewBag.NewVisitors = JsonConvert.SerializeObject(dataPoints);

            dataPoints = new List<PopChartModel.DataPoint>();
            dataPoints.Add(new PopChartModel.DataPoint(1547078400000, 22000));
            dataPoints.Add(new PopChartModel.DataPoint(1549756800000, 26040));
            dataPoints.Add(new PopChartModel.DataPoint(1552176000000, 25840));
            dataPoints.Add(new PopChartModel.DataPoint(1554854400000, 23760));
            dataPoints.Add(new PopChartModel.DataPoint(1557446400000, 28800));
            dataPoints.Add(new PopChartModel.DataPoint(1560124800000, 29400));
           

            ViewBag.ReturningVisitors = JsonConvert.SerializeObject(dataPoints);

            return View();
        }

        
        [Authorize]
       // GET: Flights/Create
        public ActionResult Flight()
        {
            //ViewBag.AirlineId = new SelectList(db.Airlines, "AirlineId", "AirlineName");

            var airport = db.Airports.ToList();

            var viewModel = new FlightViewModel { Airport = airport };

            ViewBag.Message = "Flights";

            return View(viewModel);
        }

      
       [Authorize]
        // POST: Flights/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Flight(FlightViewModel model)
        {
            if (ModelState.IsValid)
            {
                //searching flights 
                var origin = model.Origin;
                var destination = model.Destination;
                DateTime flightDate = model.FlightDate.Date;
                var date = flightDate;

                var data = db.Flights.Where(e => e.DepartureAirport == origin && e.ArrivalAirport == destination && DbFunctions.TruncateTime(e.DepartureDate) == date).ToList();

                int num = data.Count();
                if (num == 0)
                {
                    ViewBag.Result = "No Flights Found! Try Again!";
                }

                List<Airport> airportList = new List<Airport>();

                var originAirport = (from u in db.Airports
                    where u.AirportLocationName == origin
                                     select u).FirstOrDefault();

                var destinationAirport = (from u in db.Airports
                    where u.AirportLocationName == destination
                                          select u).FirstOrDefault();

                airportList.Insert (0, originAirport);
                airportList.Insert(1, destinationAirport);
                model.Airport = airportList;
                //add 2 lists search and then add to airports list
                model.Flight = data;
            }

            ModelState.Clear();

            //ViewBag.Result = "Flight has been found.";

            return View("Flight_list", model);
        }

        [Authorize]
        // GET: Flights/Create
        public ActionResult Flight_list(FlightViewModel model)
        {

            ViewBag.Message = "Flights";

            return View(model);
        }

        [Authorize]
        // GET: 
        public ActionResult Book(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Flight flight = db.Flights.Find(id);

            string currentUserId = User.Identity.GetUserId();
            var guid = new Guid(currentUserId);

            var user = db.Users.Find(guid);

            //checking if user has 5 bookings 
            if (user.Bookings.Count >= 5)
            {
                ViewBag.Result = "Booking Limit Exceeded (cant have more than 5 bookings)";
            }


            if (flight == null)
            {
                return HttpNotFound();
            }

            return View(flight);
        }

        [Authorize]
        // POST:
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Book(Flight flight)
        {
            
                Flight flightX = db.Flights.Find(flight.FlightId);
                string currentUserId = User.Identity.GetUserId();
                var guid = new Guid(currentUserId);

                var user = db.Users.Find(guid);

                

                var capacity = flightX.Capacity;
                List<Booking> confirmedBooking = db.Bookings.Where(r => r.State == "Confirmed").ToList();

                //pending and confirmation emails and status of booking
                if (confirmedBooking.Count() >= capacity)
                {
                    var booking = new Booking();

                    booking.Price = flightX.Price;
                    booking.Flight = flightX;
                    booking.UserId = guid;
                    booking.BookingDate = DateTime.Now;
                    booking.State = "Pending";
                    booking.User = user;

                    String toEmail = user.Email;
                    String subject = "Booking Pending!";
                    String contents = "Thank you for booking with lets Fly!";

                    EmailSender es = new EmailSender();
                    es.Send(toEmail, subject, contents);

                    db.Bookings.Add(booking);
                    db.SaveChanges();
                    ViewBag.Result = "Booking pending.";
                }
                else
                {

                    var booking = new Booking();

                    booking.Price = flightX.Price;
                    booking.Flight = flightX;
                    booking.UserId = guid;
                    booking.BookingDate = DateTime.Now;
                    booking.State = "Confirmed";
                    booking.User = user;

                    var write = booking.Price + " " + booking.State + " " + flightX.FlightNumber;

                    String toEmail = user.Email;
                    EmailSender es = new EmailSender();

                   

                    //es.Send(toEmail, "Booking Confirmed!", "Booking is Confirmed");
                    es.SendBooking(flightX, toEmail);
                    db.Bookings.Add(booking);
                    db.SaveChanges();
                    ViewBag.Result = "Booking Confirmed.";
                }

                return RedirectToAction("Index","Bookings");
        }


    }
}


    