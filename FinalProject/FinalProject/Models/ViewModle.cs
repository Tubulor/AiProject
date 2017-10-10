using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalProject.Models
{
    public class ViewModle
    {

        public IEnumerable<Products> Product { get; set; }
        public IEnumerable<ApplicationUser> ApplicationUser { get; set; }
        public Buys Buys { get; set; }

        public Products Products { get; set; }
    }
}