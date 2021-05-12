using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations.Sql;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Threading.Tasks;
using LetsFly.Models;
using Newtonsoft.Json;

namespace LetsFly.Utils
{

    public class EmailSender
    {

        private const String API_KEY = "API_KEY";

        //code help from tutorial and send grid documentation
        public void Send(String toEmail, String subject, String contents)
        {
            var client = new SendGridClient(API_KEY);
            var from = new EmailAddress("noreply@LetsFly.com", "Lets Fly Admin");
            var to = new EmailAddress(toEmail, "");
            var plainTextContent = contents;
            var htmlContent = "<p>" + contents + "</p>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = client.SendEmailAsync(msg);
        }

        public void SendAttachment(String toEmail, String subject, String contents, String attachment, String fileName)
        {
            var client = new SendGridClient(API_KEY);
            var from = new EmailAddress("noreply@LetsFly.com", "Lets Fly!");
            var to = new EmailAddress(toEmail, "");
            var plainTextContent = contents;
            var htmlContent = "<p>" + contents + "</p>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var bytes = File.ReadAllBytes(attachment);
            var file = Convert.ToBase64String(bytes);
            msg.AddAttachment(fileName, file);
            var response = client.SendEmailAsync(msg);
        }

        public void SendBooking(Flight model, string toEmail)
        {
            
                DateTime date = model.DepartureDate;
                string stringDate = date.ToString("dd MM yyyy");
                
                var client = new SendGridClient(API_KEY);
                var msg = new SendGridMessage();
            
                msg.SetFrom(new EmailAddress("noreply@LetsFly.com", "Lets Fly Admin"));
                
                msg.AddTo(new EmailAddress(toEmail));
           
                msg.SetTemplateId("d-e70373776ffb4924a3f106f316c353d5");
                

                var num = GenerateRandomNo().ToString();


                var dynamicTemplateData = new BookTemp
                {
                    BookingNo = num,
                    FlightNo = model.FlightNumber,
                    DepTemp = model.DepartureAirport,
                    ArrivalTemp = model.ArrivalAirport,
                    DateTemp = stringDate,
                    PriceTemp = model.Price
                };
                msg.SetTemplateData(dynamicTemplateData);
                var response = client.SendEmailAsync(msg);
        }
        public int GenerateRandomNo()
        {
            int _min = 1000;
            int _max = 9999;
            Random _rdm = new Random();
            return _rdm.Next(_min, _max);
        }

        private class BookTemp
        {
            [JsonProperty("bookno")]
            public string BookingNo { get; set; }

            [JsonProperty("flightno")]
            public string FlightNo { get; set; }

            [JsonProperty("departure")]
            public string DepTemp { get; set; }

            [JsonProperty("arrival")]
            public string ArrivalTemp { get; set; }

            [JsonProperty("Date")]
            public string DateTemp { get; set; }

            [JsonProperty("price")]
            public string PriceTemp { get; set; }

        }
    }
}
