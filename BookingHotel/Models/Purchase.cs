using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookingHotel.Models
{
    public class Purchase
    {
        public int PurchaseId { get; set; }

        public string Person { get; set; }

        public int HotelId { get; set; }

        public DateTime DateBegin { get; set; }

        public DateTime DateEnd { get; set; }
    }
}