using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace BookingHotel.Models
{
    public class HotelContext : DbContext
    {
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Purchase> Purchases { get; set; }

        public HotelContext() : base("HotelContext") { }
    }
}