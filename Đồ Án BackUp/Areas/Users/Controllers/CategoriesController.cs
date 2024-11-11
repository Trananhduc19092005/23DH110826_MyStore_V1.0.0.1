using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Đồ_Án_BackUp.Models;

namespace Đồ_Án_BackUp.Areas.Users.Controllers
{
    public class CategoriesController : Controller
    {
        // GET: Users/Categories
        MyStoreEntities db = new MyStoreEntities();

        public ActionResult AddItems()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddItems(ShoppingCart cart)
        {
            int id = 0;
            int productid = 0;
            if (Session["Username"].ToString() != null)
            {
                string Username = Session["Username"].ToString();
                var FindCustomer = db.Customers.Where(x => x.Username.Equals(Username));
                if (FindCustomer != null)
                {
                    var check = FindCustomer.Where(x => x.CustomerID == x.CustomerID).FirstOrDefault();
                    id = check.CustomerID;
                }
                productid = Convert.ToInt32(Session["productid"].ToString());
                
                var Category = new ShoppingCart()
                {
                    CustomerId = id,
                    ProductId = productid,
                    ShoppingCartId = cart.ShoppingCartId,
                };

                db.ShoppingCarts.Add(Category);
                db.SaveChanges();
                return RedirectToAction("Index", "Categories");
            }
            return View(cart);
        }

        public ActionResult Index()
        {
            return View(db.ShoppingCarts.ToList());
        }
    }
}