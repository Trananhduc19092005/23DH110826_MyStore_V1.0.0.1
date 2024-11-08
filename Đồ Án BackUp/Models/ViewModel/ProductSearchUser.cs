using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Đồ_Án_BackUp.Models.ViewModel
{
    public class ProductSearchUser
    {
        public string FindProducts { get; set; }
        public string OrderBy { get; set; }
        //public List<Product> Products { get; set; }

        public int? MinPrice { get; set; }

        public int? MaxPrice { get; set; }

        public int? CategoryId { get; set; }

        public List<Product> products { get; set; }
    }
}