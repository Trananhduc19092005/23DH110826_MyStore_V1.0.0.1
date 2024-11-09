using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Đồ_Án_BackUp.Models;

namespace Đồ_Án_BackUp.Areas.Users.Controllers
{
    public class CustomerController : Controller
    {
        MyStoreEntities db = new MyStoreEntities();

        //Create customer
        public ActionResult CreateCustomer()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateCustomer(Customer customer)
        {
            if (ModelState.IsValid)
            {
                var new_Customer = new Customer()
                {
                    CustomerID = customer.CustomerID,
                    CustomerName = customer.CustomerName,
                    CustomerAddress = customer.CustomerAddress,
                    CustomerEmail = customer.CustomerEmail,
                    CustomerPhone = customer.CustomerPhone,
                    Username = Session["Username"].ToString(),
                };
                db.Customers.Add(new_Customer);
                db.SaveChanges();
                return RedirectToAction("Index", "Home2");
            }
            return View();
        }

        // Detail Each Users Customer Info

        public ActionResult DetailsCustomer()
        {
            return View(db.Customers.ToList());
        }

        // Edit Customer

        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = await db.Customers.FindAsync(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            ViewBag.Username = new SelectList(db.Users, "Username", "Password", customer.Username);
            return View(customer);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "CustomerID,CustomerName,CustomerPhone,CustomerEmail,CustomerAddress,Username")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                customer.Username = Session["Username"].ToString();
                db.Entry(customer).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("DetailsCustomer" , "Customer");
            }
            ViewBag.Username = new SelectList(db.Users, "Username", "Password", customer.Username);
            return View(customer);
        }
    }
}