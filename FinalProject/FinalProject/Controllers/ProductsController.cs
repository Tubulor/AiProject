using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FinalProject.Models;
using Accord.Controls;
using Accord.MachineLearning.VectorMachines.Learning;
using Accord.Math.Optimization.Losses;
using Accord.Statistics;
using Accord.Statistics.Kernels;
using Accord.Statistics.Models.Fields.Functions;
using Accord.Statistics.Models.Fields;
using Accord.Statistics.Models.Fields.Learning;

namespace FinalProject.Controllers
{
	public class ProductsController : Controller
	{
		private ApplicationDbContext db = new ApplicationDbContext();
		

		// GET: Products
		public ActionResult Index(string searchString, string Brand, string Inches)
		{
			var products = from p in db.Products
						   select p;

			if (!String.IsNullOrEmpty(searchString))
			{
				products = products.Where(s => s.ProductName.Contains(searchString));

			}
			if (!String.IsNullOrEmpty(Brand))
			{
				products = products.Where(s => s.Brand.Contains(Brand));

			}
			if (!String.IsNullOrEmpty(Inches))
			{
				products = products.Where(s => s.Inches.Equals(Inches));

			}


			return View(products.ToList());
		}


		public PartialViewResult ProductSearch(String ProductName, String Brand, String Inches)
		{

			IQueryable<Products> model = db.Products;

			if (!String.IsNullOrEmpty(ProductName))
			{
				model = model.Where(x => x.ProductName.Contains(ProductName));
			}
			if (!String.IsNullOrEmpty(Brand))
			{
				model = model.Where(x => x.Brand.Contains(Brand));
			}
			if (!String.IsNullOrEmpty(Inches))
			{
				model = model.Where(x => x.Inches.Contains(Inches));
			}

			var result = model.ToList();
			return PartialView("ProductSearch", result);
		}

		// GET: Products/Details/5
		public ActionResult Details(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Products products = db.Products.Find(id);
			if (products == null)
			{
				return HttpNotFound();
			}
			return View(products);
		}

		// GET: Products/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: Products/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create([Bind(Include = "ID,ProductName,Price,Description,Brand,Inches,Resolution,RefreshRate,Image,Panel")] Products products)
		{
			if (ModelState.IsValid)
			{
				db.Products.Add(products);
				db.SaveChanges();
				return RedirectToAction("Index");
			}

			return View(products);
		}

		// GET: Products/Edit/5
		public ActionResult Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Products products = db.Products.Find(id);
			if (products == null)
			{
				return HttpNotFound();
			}
			return View(products);
		}

		// POST: Products/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit([Bind(Include = "ID,ProductName,Price,Description,Brand,Inches,Resolution,RefreshRate,Image,Panel")] Products products)
		{
			if (ModelState.IsValid)
			{
				db.Entry(products).State = EntityState.Modified;
				db.SaveChanges();
				return RedirectToAction("Index");
			}
			return View(products);
		}

		// GET: Products/Delete/5
		public ActionResult Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Products products = db.Products.Find(id);
			if (products == null)
			{
				return HttpNotFound();
			}
			return View(products);
		}

		// POST: Products/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(int id)
		{
			Products products = db.Products.Find(id);
			db.Products.Remove(products);
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
	
		

	
