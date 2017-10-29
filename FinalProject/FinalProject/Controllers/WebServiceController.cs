//using System;
//using System.Net;
//using System.Net.Http;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using FinalProject.Models;

namespace FinalProject.Controllers
{
    public class WebServiceController : ApiController
    {

        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }

        Stock[] stocks = new Stock[]
        {
            new Stock { Id = 1, Name = "Apple Inc.", Category = "Companies", Price = 163.05, ChangeDbl = 5.64, ChangePct = 3.58, Letters = "AAPL"  }
        };

        public IEnumerable<Stock> GetAllStocks()
        {
            return stocks;
        }

        public IHttpActionResult GetStock(int id)
        {
            var stock = stocks.FirstOrDefault((p) => p.Id == id);
            if (stock == null)
            {
                return NotFound();
            }
            return Ok(stock);
        }
    }
}
