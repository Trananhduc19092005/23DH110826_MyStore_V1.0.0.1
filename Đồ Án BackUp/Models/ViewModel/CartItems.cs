using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Đồ_Án_BackUp.Models.ViewModel
{
    public class CartItems
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public string ProductImage { get; set; }

        public decimal TotalPrice => Quantity * UnitPrice;
    }
}