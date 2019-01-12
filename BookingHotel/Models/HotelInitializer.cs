using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using BookingHotel.Models;

namespace BookStore.Models
{
    public class HotelDbInitializer : DropCreateDatabaseAlways<HotelContext>
    {
        protected override void Seed(HotelContext db)
        {
            db.Hotels.Add(new Hotel { Name = "Война и мир", City = "Л. Толстой", Price = 220 });
            db.Hotels.Add(new Hotel { Name = "Отцы и дети", City = "И. Тургенев", Price = 180 });
            db.Hotels.Add(new Hotel { Name = "Чайка", City = "А. Чехов", Price = 150 });

            base.Seed(db);
        }
    }
}