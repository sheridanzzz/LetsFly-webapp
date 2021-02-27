using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LetsFly.Models
{
    [MetadataType(typeof(AirportModel))]
    public partial class Airport
    {

    }
    public class AirportModel
    {

        public int AirportId { get; set; }

        [Required(ErrorMessage = "Please Enter Airport Name")]
        [Display(Name = "Airport Name")]
        public string AirportName { get; set; }

        [Required(ErrorMessage = "Please Enter Airport Code")]
        [Display(Name = "Airport Code")]
        public string AirportCode { get; set; }

        [Required(ErrorMessage = "Please Enter Airport Location Name")]
        [Display(Name = "Airport Location Name")]
        public string AirportLocationName { get; set; }

        [Required(ErrorMessage = "Please Enter Airport Longitude")]
        [Display(Name = "Airport Longitude")]
        [Range(float.NegativeInfinity, float.MaxValue, ErrorMessage = "Please enter valid Longitude")]
        public string AirportLong { get; set; }

        [Required(ErrorMessage = "Please Enter Airport Latitude")]
        [Display(Name = "Airport Latitude")]
        [Range(float.NegativeInfinity, float.MaxValue, ErrorMessage = "Please enter valid Latitude")]
        public string AirportLat { get; set; }
    }
}