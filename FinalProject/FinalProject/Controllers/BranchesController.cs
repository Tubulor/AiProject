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
    public class BranchesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Branches
        public ActionResult Index()
        {
            return View(db.Branches.ToList());
        }
		public PartialViewResult BranchSearch(String branchname,String country,bool saturday)
		{

			IQueryable<Branches> model = db.Branches;

			if (!String.IsNullOrEmpty(branchname))
			{
				model = model.Where(x => x.BranchName.Contains(branchname));
			}
			if (!String.IsNullOrEmpty(country))
			{
				model = model.Where(x => x.Country.Contains(country));
			}
			if (saturday)
			{
				model = model.Where(x => x.Saturday.Equals(true));
			}
			else
			{
				model = model.Where(x => x.Saturday.Equals(false));
			}
			
			var result = model.ToList();
			return PartialView("BranchSearch",result);
		}

		// GET: Branches/Details/5
		public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Branches branches = db.Branches.Find(id);
            if (branches == null)
            {
                return HttpNotFound();
            }
            return View(branches);
        }

        // GET: Branches/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Branches/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,BranchName,Country,City,HouseNumber,Street,PhoneNumber,Saturday,BranchNumber,Image")] Branches branches)
        {
            if (ModelState.IsValid)
            {
                db.Branches.Add(branches);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(branches);
        }

        // GET: Branches/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Branches branches = db.Branches.Find(id);
            if (branches == null)
            {
                return HttpNotFound();
            }
            return View(branches);
        }

        // POST: Branches/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,BranchName,Country,City,HouseNumber,Street,PhoneNumber,Saturday,BranchNumber,Image")] Branches branches)
        {
            if (ModelState.IsValid)
            {
                db.Entry(branches).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(branches);
        }

        // GET: Branches/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Branches branches = db.Branches.Find(id);
            if (branches == null)
            {
                return HttpNotFound();
            }
            return View(branches);
        }

        // POST: Branches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Branches branches = db.Branches.Find(id);
            db.Branches.Remove(branches);
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
