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

namespace BookingHotel.Controllers
{
    public class HotelsRoomsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: HotelsRooms
        public async Task<ActionResult> Index()
        {
            var hotelsRoomses = db.HotelsRoomses.Include(h => h.Hotel).Include(h => h.Room);
            return View(await hotelsRoomses.ToListAsync());
        }

        // GET: HotelsRooms/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HotelsRooms hotelsRooms = await db.HotelsRoomses.FindAsync(id);
            if (hotelsRooms == null)
            {
                return HttpNotFound();
            }
            return View(hotelsRooms);
        }

        // GET: HotelsRooms/Create
        public ActionResult Create()
        {
            ViewBag.HotelID = new SelectList(db.Hotels, "ID", "Name");
            ViewBag.RoomId = new SelectList(db.Rooms, "Id", "Name");
            return View();
        }

        // POST: HotelsRooms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,HotelID,RoomId,Discount,Count,Earning")] HotelsRooms hotelsRooms)
        {
            if (ModelState.IsValid)
            {
                db.HotelsRoomses.Add(hotelsRooms);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.HotelID = new SelectList(db.Hotels, "ID", "Name", hotelsRooms.HotelID);
            ViewBag.RoomId = new SelectList(db.Rooms, "Id", "Name", hotelsRooms.RoomId);
            return View(hotelsRooms);
        }

        // GET: HotelsRooms/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HotelsRooms hotelsRooms = await db.HotelsRoomses.FindAsync(id);
            if (hotelsRooms == null)
            {
                return HttpNotFound();
            }
            ViewBag.HotelID = new SelectList(db.Hotels, "ID", "Name", hotelsRooms.HotelID);
            ViewBag.RoomId = new SelectList(db.Rooms, "Id", "Name", hotelsRooms.RoomId);
            return View(hotelsRooms);
        }

        // POST: HotelsRooms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,HotelID,RoomId,Discount,Count,Earning")] HotelsRooms hotelsRooms)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hotelsRooms).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.HotelID = new SelectList(db.Hotels, "ID", "Name", hotelsRooms.HotelID);
            ViewBag.RoomId = new SelectList(db.Rooms, "Id", "Name", hotelsRooms.RoomId);
            return View(hotelsRooms);
        }

        // GET: HotelsRooms/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HotelsRooms hotelsRooms = await db.HotelsRoomses.FindAsync(id);
            if (hotelsRooms == null)
            {
                return HttpNotFound();
            }
            return View(hotelsRooms);
        }

        // POST: HotelsRooms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            HotelsRooms hotelsRooms = await db.HotelsRoomses.FindAsync(id);
            db.HotelsRoomses.Remove(hotelsRooms);
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
