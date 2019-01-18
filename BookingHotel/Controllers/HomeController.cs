using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookingHotel.Models;

namespace BookingHotel.Controllers
{
    public class HomeController : Controller
    {
        HotelContext db = new HotelContext();

        public ActionResult Index()
        {
            // получаем из бд все объекты Book
            IEnumerable<Hotel> hotels = db.Hotels;
            // передаем все объекты в динамическое свойство Books в ViewBag
            ViewBag.Hotels = hotels;
            // возвращаем представление
            return View();
        }

        public ActionResult ModalPopUp()
        {
            return View();
        }
    }
}