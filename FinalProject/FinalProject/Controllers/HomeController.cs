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