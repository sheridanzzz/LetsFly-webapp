using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LetsFly.Models
{
    [MetadataType(typeof(RatingModel))]
    public partial class Rating
    {

    }
    public class RatingModel
    {
        public int RatingId { get; set; }

        [Required(ErrorMessage = "Please Enter Rating Number (1 to 5)")]
        [Display(Name = "Rating Number (1 to 5)")]
        [Range(1, 5, ErrorMessage = "Please Enter Rating Number (1 to 5)")]
        public int RatingNumber { get; set; }

      
        public string RatingImg { get; set; }

        [Required]
        [Display(Name = "Rating Date")]
        [DataType(DataType.Date)]
        public System.DateTime RatingDate { get; set; }

        [AllowHtml]
        [Required(ErrorMessage = "Please Enter Rating Description")]
        [Display(Name = "Rating Description")]
        public string RatingDescription { get; set; }

       
        public int AirlineId { get; set; }

        public string UserId { get; set; }

        public virtual Airline Airline { get; set; }
    }
}