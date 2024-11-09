using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Đồ_Án_BackUp.Models;

namespace Đồ_Án_BackUp.Areas.Users.Controllers
{
    public class UserOrderInformartionController : Controller
    {
        MyStoreEntities db = new MyStoreEntities();

        //public ActionResult OrderInformation(Order customerorder)
        //{

        //}
        [HttpPost]        
        public ActionResult XacNhanDatHang(Order order)
        {
            int id = 0;
            if (Session["Username"].ToString() != null)
            {
                string Username = Session["Username"].ToString();
                var FindCustomer = db.Customers.Where(x => x.Username.Equals(Username));
                if (FindCustomer != null)
                {
                    var check = FindCustomer.Where(x => x.CustomerID == x.CustomerID).FirstOrDefault();
                    id = check.CustomerID;
                }

                var CustomerOrder = new Order()
                {
                    OrderID = order.OrderID,
                    CustomerID = id,
                    OrderDate = DateTime.Today,
                    TotalAmount = order.TotalAmount,
                    PaymentStatus = order.PaymentStatus,
                    AddressDelivery = order.AddressDelivery,
                };

                db.Orders.Add(CustomerOrder);
                db.SaveChanges();
                return RedirectToAction("Index" , "Home2");
            }
            return View(order);
        }

        public ActionResult XacNhanDatHang()
        {
            return View();
        }

    }
}