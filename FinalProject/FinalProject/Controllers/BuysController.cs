using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FinalProject.Models;

namespace FinalProject.Controllers
{
    [Authorize]
    public class BuysController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Buys
        public ActionResult Index()
        {
            var buys = db.Buys.Include(b => b.Products);
            return View(buys.ToList());
        }

        // GET: Buys/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Buys buys = db.Buys.Find(id);
            if (buys == null)
            {
                return HttpNotFound();
            }
            return View(buys);
        }

        // GET: Buys/Create
        public ActionResult Create()
        {
            ViewBag.ProductsID = new SelectList(db.Products, "ID", "ProductName");
            ViewBag.MembersID = new SelectList(db.Users, "ID", "MemberID");
            return View();
        }

        // POST: Buys/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ProductsID,MembersID,PriceBought,DateBought")] Buys buys)
        {
            if (ModelState.IsValid)
            {
                
                db.Buys.Add(buys);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProductsID = new SelectList(db.Products, "ID", "ProductName", buys.ProductsID);
            return View(buys);
        }


        // GET: Buys/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Buys buys = db.Buys.Find(id);
            if (buys == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductsID = new SelectList(db.Products, "ID", "ProductName", buys.ProductsID);
            return View(buys);
        }

        // POST: Buys/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ProductsID,MembersID,PriceBought,DateBought")] Buys buys)
        {
            if (ModelState.IsValid)
            {
                db.Entry(buys).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductsID = new SelectList(db.Products, "ID", "ProductName", buys.ProductsID);
            return View(buys);
        }

        // GET: Buys/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Buys buys = db.Buys.Find(id);
            if (buys == null)
            {
                return HttpNotFound();
            }
            return View(buys);
        }

        // POST: Buys/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Buys buys = db.Buys.Find(id);
            db.Buys.Remove(buys);
            db.SaveChanges();
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
