using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Đồ_Án_BackUp.Models.ViewModel
{
    public class Category
    {
        private List<CartItems> items = new List<CartItems>();

        public IEnumerable<CartItems> Items => items;

        public void AddItem(int productid, string productimg, string productname,
            decimal unitPrice, int quantity, string category)
        {
            var existeditem = items.FirstOrDefault(i => i.ProductId == productid);
            if (existeditem != null)
            {
                items.Add
                    (new CartItems()
                    {
                        ProductId = productid,
                        ProductImage = productimg,
                        ProductName = productname,
                        UnitPrice = unitPrice,
                        Quantity = quantity,
                    });

            }
            else
            {

            }
        }

        public void RemoveItem(int productid)
        {
            items.RemoveAll(i => i.ProductId == productid);
        }

        public decimal TotalValue()
        {
            return items.Sum(i => i.TotalPrice);
        }

        public void Clear()
        {
            items.Clear();
        }

        public void UpdateQuantity(int productid, int quantity)
        {
            var item = items.FirstOrDefault(i => i.ProductId == productid);
            if (item != null)
            {
                item.Quantity = quantity;
            }
        }
    }
}