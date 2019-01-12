using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookingHotel.Models
{
    public class Hotel
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string City { get; set; }

        public int Price { get; set; }
    }
}