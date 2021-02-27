using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LetsFly.Models
{
    [MetadataType(typeof(BookingModel))]
    public partial class Booking
    {

    }
    public class BookingModel
    {
        public int BookingNumber { get; set; }

        [Required(ErrorMessage = "Please Enter Price")]
        [Display(Name = "Price")]
        public string Price { get; set; }

        [Required(ErrorMessage = "Please Enter State")]
        [Display(Name = "Status")]
        public string State { get; set; }

        public string UserId { get; set; }
        public int FlightId { get; set; }

        [Required(ErrorMessage = "Please Enter State")]
        [Display(Name = "Booking Date")]
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> BookingDate { get; set; }
    }
}