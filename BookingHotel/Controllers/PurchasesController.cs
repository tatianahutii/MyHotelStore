using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BookingHotel.Models;
using Microsoft.AspNet.Identity;
using System.Security.Claims;
using Newtonsoft.Json;
using BookingHotel.Email;

namespace BookingHotel.Controllers
{
    public class PurchasesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Purchases
        public async Task<ActionResult> Index()
        {
            if (User.IsInRole("admin"))
            {
                return View(await db.Purchases.ToListAsync());
            }
            else
            {
                var claim = User.Identity as ClaimsIdentity;
                var userIdClaim = claim.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
                var userIdValue = userIdClaim.Value;
                return View(db.Purchases.Where(a => a.UserId.Contains(userIdValue)));
            }
        }

        //[Authorize(Roles = "admin")]
        public ActionResult PurchTable(string beginPurch,string endPurch,string beginTimePurch,string endTimePurch,string userLogin, State userState)
        {
            if (User.IsInRole("user"))
            {
                userLogin = User.Identity.Name;
            }
            DateTime beginPurch_ = new DateTime();
            DateTime endPurch_ = DateTime.Now;
            DateTime beginTimePurch_ = new DateTime();
            DateTime endTimePurch_= DateTime.Now;
            if (beginPurch!="")
            {
                beginPurch_ = DateTime.Parse(beginPurch);
            }
            if (endPurch != "")
            {
                endPurch_ = DateTime.Parse(endPurch);
            }
            if (beginTimePurch != "")
            {
                beginTimePurch_ = DateTime.Parse(beginTimePurch);
            }
            if (endTimePurch != "")
            {
                endTimePurch_ = DateTime.Parse(endTimePurch);
            }            
            var purch = db.Purchases.Where(a => (a.DateBegin >= beginPurch_ &&
                                              a.DateEnd <= endPurch_ &&
                                              a.TimeOfPurch >= beginTimePurch_ &&
                                              a.TimeOfPurch <= endTimePurch_)).ToList();
            var result = purch.Where(a=>a.User.Email.Contains(userLogin) && a.PurchState==userState).ToList();
            return PartialView(result);
        }

        // GET: Purchases/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Purchase purchase = await db.Purchases.FindAsync(id);
            if (purchase == null)
            {
                return HttpNotFound();
            }
            return View(purchase);
        }

        // GET: Purchases/Create
        public ActionResult Create()
        {
            return View();
        }

        [Route("Purchase")]
        public string Purchase(string HotelId,string RoomId, string BeginDate,string EndDate)
        {
            DateTime beginDate= DateTime.Now; ;
            DateTime endDate= DateTime.Now;
            if (BeginDate!="" && EndDate!="")
            {
                beginDate = DateTime.Parse(BeginDate);
                endDate = DateTime.Parse(EndDate);
            }
            if (BeginDate=="")
            {
                beginDate = endDate;
            }
            if (EndDate=="")
            {
                endDate = beginDate;
            }

            int ID = Int32.Parse(HotelId);
            int IDR = Int32.Parse(RoomId);
            var claim = User.Identity as ClaimsIdentity;
            var userIdClaim = claim.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
            var userIdValue = userIdClaim.Value;
            var  model = new Purchase{ HotelsID = ID, UserId = userIdValue, Hotel = db.Hotels.Find(ID),RoomId=IDR, Room=db.Rooms.Find(IDR), User = db.Users.Find(userIdValue), DateBegin = beginDate, DateEnd = endDate};
            return JsonConvert.SerializeObject(model);
        }

        // POST: Purchases/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpGet]
        [Route("Create")]
        public async Task<ActionResult> Create(string HotelId,string RoomId, string Price, DateTime BeginDate, DateTime EndDate)
        {
            Purchase purchase = new Purchase();
            var claim = User.Identity as ClaimsIdentity;
            int ID = Int32.Parse(HotelId);
            int RoomId_ = Int32.Parse(RoomId);
            int price_ = Int32.Parse(Price);
            if(claim!=null)
            {
                var userIdClaim = claim.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
                if (userIdClaim != null)
                {
                    var userIdValue = userIdClaim.Value;
                    if (ModelState.IsValid)
                    {
                        var hotel = db.HotelsRoomses.Where(b => b.HotelID == ID && b.RoomId == RoomId_).FirstOrDefault();
                        int earning = hotel.Earning;
                        purchase = new Purchase { HotelsID = ID, TimeOfPurch=DateTime.Now, UserId = userIdValue, RoomId=RoomId_,Room=db.Rooms.Find(RoomId_), Price=price_, Hotel=db.Hotels.Find(ID), User=db.Users.Find(userIdValue), DateBegin=BeginDate, DateEnd=EndDate,Earning=earning };
                        db.Purchases.Add(purchase);
                         db.SaveChanges();
                        await SendMessage(purchase.User.Email);
                        return RedirectToAction("Index");
                    }
                }
            }

            
            return View(purchase);
        }

        public async Task<ActionResult> SendMessage(string email)
        {
            EmailService1 emailService = new EmailService1();
            await emailService.SendEmailAsync(email, "Купівля товару", "Дякуємо за покупку!!!");
            return RedirectToAction("Index");
        }

        // GET: Purchases/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Purchase purchase = await db.Purchases.FindAsync(id);
            if (purchase == null)
            {
                return HttpNotFound();
            }
            return View(purchase);
        }

        // POST: Purchases/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "PurchaseId,PurchState")] Purchase purchase )
        {
            if (ModelState.IsValid)
            {
                Purchase newPurcahse = db.Purchases.Where(i => i.PurchaseId == purchase.PurchaseId).FirstOrDefault();
                newPurcahse.PurchState = purchase.PurchState;
                db.Entry(newPurcahse).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(purchase);
        }

        // GET: Purchases/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Purchase purchase = db.Purchases.Find(id);
            if (purchase == null)
            {
                return HttpNotFound();
            }
            return View(purchase);
        }

        // POST: Purchases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Purchase purchase = await db.Purchases.FindAsync(id);
            db.Purchases.Remove(purchase);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
