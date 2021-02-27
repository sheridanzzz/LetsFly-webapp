using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LetsFly.Models
{
    [MetadataType(typeof(FlightModel))]
    public partial class Flight
    {

    }
    public class FlightModel
    {
        public List<Airport> Airport { get; set; }
        public int FlightId { get; set; }

        [Required(ErrorMessage = "Please Enter Flight Number")]
        [Display(Name = "Flight Number")]
        public string FlightNumber { get; set; }

        [Required(ErrorMessage = "Please Enter Departure Date")]
        [Display(Name = "Departure Date")]
        [DataType(DataType.DateTime)]
        public System.DateTime DepartureDate { get; set; }

        [Required(ErrorMessage = "Please Enter Arrival Date")]
        [Display(Name = "Arrival Date")]
        [DataType(DataType.DateTime)]
        public System.DateTime ArrivalDate { get; set; }

        [Required(ErrorMessage = "Please Enter Price")]
        [Display(Name = "Price")]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid price")]
        public string Price { get; set; }

        [Required(ErrorMessage = "Please Enter Capacity")]
        [Display(Name = "Capacity")]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid Capacity")]
        public int Capacity { get; set; }

        [Required(ErrorMessage = "Please Enter Duration")]
        [Display(Name = "Duration")]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid Duration")]
        public string Duration { get; set; }

        public int AirlineId { get; set; }

        [Required(ErrorMessage = "Please Select Arrival Airport")]
        [Display(Name = "Arrival Airport")]
        public string ArrivalAirport { get; set; }

        [Required(ErrorMessage = "Please Select Departure Airport")]
        [Display(Name = "Departure Airport")]
        public string DepartureAirport { get; set; }
        public int AirportId { get; set; }
    }
}