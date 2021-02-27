using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LetsFly.Models
{
    [MetadataType(typeof(AirlineModel))]
    public partial class Airline
    {
        
    }
    public class AirlineModel
    {
        public int AirlineId { get; set; }

        [Required(ErrorMessage = "Please Enter Airline Name")]
        [Display(Name = "Airline Name")]
        public string AirlineName { get; set; }

        [Display(Name = "Airline Image")]
        public string AirlineImg { get; set; }

        [Required(ErrorMessage = "Please Enter Airline Code")]
        [Display(Name = "Airline Code")]
        public string AirlineCode { get; set; }

        [Required(ErrorMessage = "Please Enter Airline Description")]
        [Display(Name = "Airline Description")]
        public string AirlineDescription { get; set; }
    }
}