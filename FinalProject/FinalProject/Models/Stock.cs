using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalProject.Models
{
    public class Stock
    {
        public int Id { get; set; }
        public string Letters { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public double Price { get; set; }
        public double ChangeDbl { get; set; }
        public double ChangePct { get; set; }
    }
}