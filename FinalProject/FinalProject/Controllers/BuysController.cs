using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FinalProject.Models;
using System.IO;

namespace FinalProject.Controllers
{
	[Authorize]
	public class BuysController : Controller
	{
		private ApplicationDbContext db = new ApplicationDbContext();

		// GET: Buys
		public ActionResult Index(string searchString, string Price)
		{
			var buys = db.Buys.Include(b => b.Products);

			if (!String.IsNullOrEmpty(searchString))
			{
				buys = buys.Where(s => s.Products.ProductName.Contains(searchString));

			}
			if (!String.IsNullOrEmpty(Price))
			{
				int price = Int32.Parse(Price);
				buys = buys.Where(s => s.PriceBought==price);

			}
			
			return View(buys.ToList());
		}
		public ActionResult Statistics()
		{
			var buys = db.Buys.Include(b => b.Products);
			return View(buys.ToList());
		}

		public ActionResult ProductBuys()
		{
			var viewmodel = new ViewModle();
			viewmodel.Buy = db.Buys;
			viewmodel.Product = db.Products;
			return View(viewmodel);
		}
		public ActionResult MostPopular()
		{
			var viewmodel = new ViewModle();
			viewmodel.Product = db.Products.ToList();
			viewmodel.Buy = db.Buys.ToList();
			return View(viewmodel);
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
		public ActionResult Create()
		{
			ViewBag.ProductsID = new SelectList(db.Products, "ID", "ProductName");
			ViewBag.MembersID = new SelectList(db.Users, "ID", "Email");
			return View();
		}

		// GET: Buys/CreateBuy/id
		public ActionResult CreateBuy(int? id)
		{
			var viewmodel = new ViewModle();

			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			viewmodel.Products = db.Products.Find(id);
			if (viewmodel == null)
			{
				return HttpNotFound();
			}
			return View(viewmodel);
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

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult CreateBuy([Bind(Include = "ID,ProductsID,MembersID,PriceBought,DateBought")] Buys buys)
		{
			if (ModelState.IsValid)
			{
				db.Buys.Add(buys);
				db.SaveChanges();
				return RedirectToAction("Index", "Home");
			}

			// ViewBag.ProductsID = new SelectList(db.Products, "ID", "ProductName", buys.ProductsID);
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
		public JsonResult SalesGraph()
		{
			List<Result> salesCount = new List<Result>();

			var myQuery = from c in db.Buys
						  join p in db.Products on c.Products.ID equals p.ID
						  select c;

			var myGroup = from t in myQuery
						  group t by t.Products.ProductName into cat
						  select new
						  {
							  productName = cat.Key,
							  sum = cat.Count()
						  };

			foreach (var item in myGroup)
			{
				Result numReviews = new Result();
				numReviews.State = item.productName;
				numReviews.freq = item.sum;
				salesCount.Add(numReviews);
			}
			return Json(salesCount, JsonRequestBehavior.AllowGet);
		}


		public JsonResult SalesPerbrand()
		{
			List<Result> salesCount = new List<Result>();

			var myQuery = from c in db.Buys
						  join p in db.Products on c.ProductsID equals p.ID
						  select c;

			var myQuery2 = from t in myQuery
						   group t by t.Products.Brand into brand
							select new
						  {
							  Brand = brand.Key,
							  sum = brand.Sum(x => x.Products.Price)
						  };

						

			foreach (var item in myQuery2)
			{
				Result numReviews = new Result();
				numReviews.State = item.Brand;
				numReviews.freq = Convert.ToInt32(item.sum);
				salesCount.Add(numReviews);
			}
			return Json(salesCount, JsonRequestBehavior.AllowGet);
		}
	
}

internal class Result
	{
		public String State { get; set; }
		public int freq { get; set; }

	}
}
