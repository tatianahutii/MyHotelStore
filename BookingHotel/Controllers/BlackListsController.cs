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
    public class BlackListsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: BlackLists
        public async Task<ActionResult> Index()
        {
            var blackLists = db.BlackLists.Include(b => b.Hotel).Include(b => b.User);
            return View(await blackLists.ToListAsync());
        }

        // GET: BlackLists/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlackList blackList = await db.BlackLists.FindAsync(id);
            if (blackList == null)
            {
                return HttpNotFound();
            }
            return View(blackList);
        }

        // GET: BlackLists/Create
        public ActionResult Create()
        {
            ViewBag.HotelsID = new SelectList(db.Hotels, "ID", "Name");
            ViewBag.UserId = new SelectList(db.Users, "UserId", "Name");
            return View();
        }

        // POST: BlackLists/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "BlackListId,HotelsID,UserId")] BlackList blackList)
        {
            if (ModelState.IsValid)
            {
                db.BlackLists.Add(blackList);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.HotelsID = new SelectList(db.Hotels, "ID", "Name", blackList.HotelsID);
            ViewBag.UserId = new SelectList(db.Users, "UserId", "Name", blackList.UserId);
            return View(blackList);
        }

        // GET: BlackLists/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlackList blackList = await db.BlackLists.FindAsync(id);
            if (blackList == null)
            {
                return HttpNotFound();
            }
            ViewBag.HotelsID = new SelectList(db.Hotels, "ID", "Name", blackList.HotelsID);
            ViewBag.UserId = new SelectList(db.Users, "UserId", "Name", blackList.UserId);
            return View(blackList);
        }

        // POST: BlackLists/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "BlackListId,HotelsID,UserId")] BlackList blackList)
        {
            if (ModelState.IsValid)
            {
                db.Entry(blackList).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.HotelsID = new SelectList(db.Hotels, "ID", "Name", blackList.HotelsID);
            ViewBag.UserId = new SelectList(db.Users, "UserId", "Name", blackList.UserId);
            return View(blackList);
        }

        // GET: BlackLists/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlackList blackList = await db.BlackLists.FindAsync(id);
            if (blackList == null)
            {
                return HttpNotFound();
            }
            return View(blackList);
        }

        // POST: BlackLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            BlackList blackList = await db.BlackLists.FindAsync(id);
            db.BlackLists.Remove(blackList);
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
