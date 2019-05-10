using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookingHotel.Models;
using BookingHotel.Email;
using System.Threading.Tasks;

namespace BookingHotel.Controllers
{
    public class HomeController : Controller
    {

        ApplicationDbContext db = new ApplicationDbContext();

        //public ActionResult Index()
        //{
        //    // получаем из бд все объекты Book
        //    IEnumerable<Hotel> hotels = db.Hotels;
        //    // передаем все объекты в динамическое свойство Books в ViewBag
        //    ViewBag.Hotels = hotels;
        //    // возвращаем представление
        //    return View();
        //}

        public ActionResult Index(int? id)
        {
            int page = id ?? 0;
            if (Request.IsAjaxRequest())
            {
                return PartialView("SearchHotel", GetItemsPage(page));
            }
            return View(GetItemsPage(page));
        }

        

        const int pageSize = 4;

        private List<Hotel> GetItemsPage(int page = 1)
        {
            var itemsToSkip = page * pageSize;

            return db.Hotels.OrderBy(t => t.ID).Skip(itemsToSkip).
                Take(pageSize).ToList();
        }

        //public ActionResult SearchHotel()
        //{
        //    var hotels = db.Hotels.ToList();
        //    if (hotels.Count <= 0)
        //    {
        //        return PartialView();
        //    }
        //    return PartialView(hotels);
        //}

        [HttpPost]
        public ActionResult SearchHotel(string name, string city, string country, string begin, string end)
        {
            DateTime beginDate = DateTime.Parse(begin);
            DateTime endDate = DateTime.Parse(end);
            string room = "";
            var hotels = db.Hotels.Where(a => a.Name.Contains(name)).ToList();
            hotels = hotels.Where(a => a.Country.Contains(country)).ToList();
            hotels = hotels.Where(a => a.City.Contains(city)).ToList();
            foreach(var hotel_ in hotels)
            {
                foreach(var room_ in hotel_.HotelsRooms)
                {
                    var purch = db.Purchases.Where(a => (a.Hotel.Name.Contains(name) &&
                                                         a.Hotel.City.Contains(city) &&
                                                         a.Hotel.Country.Contains(country) &&
                                                         a.Room.Name==room_.Name &&
                                                         a.Room.Name.Contains(room) &&
                                                         ((a.DateBegin <= beginDate && a.DateEnd >= endDate) ||
                                                         (a.DateBegin<=beginDate && a.DateEnd>=beginDate) ||
                                                         (a.DateBegin>=beginDate && a.DateBegin<=endDate)))).ToList();
                    int purchCount = purch.Count;
                    var hotelsroom = db.HotelsRoomses.Where(a => (a.Hotel.Name.Contains(hotel_.Name) && a.Room.Name.Contains(room_.Name))).ToList();
                    int count;
                    if (hotelsroom.Count!=0)
                    {
                        count = hotelsroom.First().Count;
                    }
                    else
                    {
                        count = 0;
                    }                 
                    if (count<=purchCount)
                    {
                        room_.Price = 0;
                    }
                }
            }

            if (hotels.Count <= 0)
            {
                return PartialView();
            }

            return PartialView(hotels);
        }

        public ActionResult Details(int id=0)
        {
            Hotel hotels = db.Hotels.Find(id);
            if (hotels==null)
            {
                return HttpNotFound();
            }
            return View(hotels);
        }

        public ActionResult HotelView()
        {
            return PartialView("_HotelView");
        }
    }
}