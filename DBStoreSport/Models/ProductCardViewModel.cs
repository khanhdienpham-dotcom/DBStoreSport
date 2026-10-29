using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DBStoreSport.Models
{
    public class ProductCardViewModel
    {
        public int ProductID { get; set; }
        public string NamePro { get; set; }
        public string CategoryName { get; set; }
        public decimal? Price { get; set; }
        public string ImagePro { get; set; }
        public string ShortDescription { get; set; }
    }
}