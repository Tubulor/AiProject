using FinalProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinalProject.Controllers
{
    public class HomeController : Controller
    {
		private ApplicationDbContext db = new ApplicationDbContext();
		public ActionResult Index()
        {
			var viewmodel = new ViewModle();
			viewmodel.Product = db.Products.ToList();
			viewmodel.Buy = db.Buys.ToList();
			return View(viewmodel);
		}
		public ActionResult Facebook()
		{
			return View();
		}


		/*	[HttpPost]
			public string Index(string brand, string inches, string resolution, bool notUsed)
			{
				return "From [HttpPost]Index: filter on " + resolution;
			}

			// GET: Products
			public async Task<ActionResult> Index(string brand, string inches, string resolution)
			{
				var products = from p in db.Products
							   select p;

				if (!String.IsNullOrEmpty(brand))
				{
					products = products.Where(s => s.Brand.Equals(brand));
				}
				if (!String.IsNullOrEmpty(inches))
				{
					products = products.Where(s => s.Inches.Equals(brand));
				}
				if (!String.IsNullOrEmpty(resolution))
				{
					products = products.Where(s => s.Resolution.Equals(resolution));
				}

				return View(await products.ToListAsync());
			}
			*/

		public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}