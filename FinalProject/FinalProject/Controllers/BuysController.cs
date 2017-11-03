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
using Accord.Statistics.Models.Fields.Functions;
using Accord.Statistics.Models.Fields;
using Accord.Statistics.Models.Fields.Learning;
using Accord.Statistics.Models.Markov;
using Accord.Statistics.Models.Markov.Learning;
using Accord.MachineLearning.VectorMachines.Learning;
using Accord.Statistics.Kernels;

namespace FinalProject.Controllers
{
	[Authorize]
	public class BuysController : Controller
	{
		private ApplicationDbContext db = new ApplicationDbContext();
		public enum Resolution : int { UltraHD, FullHD, HD };
		public enum Inches : int { _55, _49, _43, _42, _37, _32, _27, _24, _22, _21, _19, _17, _15, _14, _13 };
		public enum Panel : int { OLED, LED, LCD, Plasma };

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
		public PartialViewResult RelatedProduct(int id)
		{

			int resolution = 0, inches = 0, panel = 0;
			var viewmodel = new ViewModle();
			double[][] Input = new double[1][];
			
			viewmodel.Products = db.Products.Find(id);
			
			if (Enum.IsDefined(typeof(Resolution), viewmodel.Products.Resolution))
			{
				resolution = (int)Enum.Parse(typeof(Resolution), viewmodel.Products.Resolution);

			}
			if (Enum.IsDefined(typeof(Inches), "_" + viewmodel.Products.Inches))
			{
				inches = (int)Enum.Parse(typeof(Inches), "_" + viewmodel.Products.Inches);
			}
			if (Enum.IsDefined(typeof(Panel), viewmodel.Products.Panel))
			{
				panel = (int)Enum.Parse(typeof(Panel), viewmodel.Products.Panel);
			}

			Input[0] = new double[] { resolution, inches, panel };
			int[] x = Learning(Input,id);

			int prodcutid = x[0];
			//var viewmodel = new ViewModle();
			//viewmodel.Products = db.Products.Find(2);
			var result = db.Products.Where(p => p.ID == prodcutid);
			return PartialView("RelatedProduct", result);
		}

		
		public int[] Learning(double[][] Inputs,int id )
		{
			var products = (from p in db.Products
							select p).ToArray();

			int resolution=0, inches=0, panel=0 ;
            int length = products.Length;
            int lastID = products[length-1].ID;
            double[][] Input = new double[lastID+1][];
            int[] output = new int[lastID+1];
            for (int i = 0; i <= lastID; i++)
			{
                for (int j = 0; j < products.Length; j++)
                {
                    if (products[j].ID == i)
                    {
                        if (products[j].ID != id) //skip the current item
                        {
                            if (Enum.IsDefined(typeof(Resolution), products[j].Resolution))
                            {
                                Console.WriteLine(products[j].Resolution);
                                resolution = (int)Enum.Parse(typeof(Resolution), products[j].Resolution);

                            }
                            if (Enum.IsDefined(typeof(Inches), "_" + products[j].Inches))
                            {
                                inches = (int)Enum.Parse(typeof(Inches), "_" + products[j].Inches);
                            }
                            if (Enum.IsDefined(typeof(Panel), products[j].Panel))
                            {
                                panel = (int)Enum.Parse(typeof(Panel), products[j].Panel);
                            }

                            Input[i] = new double[] { resolution, inches, panel };
                            output[i] = products[j].ID;
                            break;
                        }
                        else
                        { //outlier
                            Input[i] = new double[] { 20, 20, 20 };
                            output[i] = id;
                        }
                    }
                    else
                    {
                        Input[i] = new double[] { 20, 20, 20 };
                        output[i] = i;
                    }
                }
            }
			// Create the Multi-label learning algorithm for the machine
			var teacher = new MulticlassSupportVectorLearning<Linear>()
			{
				Learner = (p) => new SequentialMinimalOptimization<Linear>()
				{
					Complexity = 10000.0 // Create a hard SVM
				}
			};

			// Learn a multi-label SVM using the teacher
			var svm = teacher.Learn(Input, output);


			// Compute the classifier answers for the given inputs
			int []answers =svm.Decide(Inputs);
			
			return answers;
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
