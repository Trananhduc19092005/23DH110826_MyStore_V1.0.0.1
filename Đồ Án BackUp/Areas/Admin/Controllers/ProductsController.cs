using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Đồ_Án_BackUp.Models;
using System.Web.UI.WebControls;
using System.Net.Http.Headers;
using Đồ_Án_BackUp.Models.ViewModel;
using PagedList;

namespace Đồ_Án_BackUp.Areas.Admin.Controllers
{
    public class ProductsController : Controller
    {
        private MyStoreEntities db = new MyStoreEntities();

        //GET: Admin/Products
        //public async Task<ActionResult> Index()
        //{
        //    var products = db.Products.Include(p => p.Category);
        //    return View(await products.ToListAsync());
        //}

        //[HttpPost]
        //public ActionResult Index(int ?id)
        //{
        //    var search = db.Products.Find(id);
        //    if (search != null)
        //    {
        //        return View();
        //    }
        //    return RedirectToAction("Search");
        //}


        public ActionResult Index(string FindProduct, string OrderBy , int ? MinPrice , int ? MaxPrice , int ? page)
        {
            Product products = new Product();
            ProductSearchVM searchVM = new ProductSearchVM();
            var product = db.Products.AsQueryable();

            if (FindProduct != null)
            {
                searchVM.FindProducts = FindProduct;
                product = product.Where(x => x.ProductName.Contains(FindProduct));
            }

            if (MinPrice.HasValue)
            {
                searchVM.MinPrice = MinPrice;
                product = product.Where(x => x.ProductPrice >= MinPrice);
            }

            if (MaxPrice.HasValue)
            {
                searchVM.MaxPrice = MaxPrice;
                product = product.Where(x => x.ProductPrice <= MaxPrice);
            }

            switch (OrderBy)
            {
                case "name_asc": 
                    product = product.OrderBy(p => p.ProductID);
                    break;
                case "name_desc": 
                    product = product.OrderByDescending(p => p.ProductID); 
                    break;
                case "price_asc":
                    product = product.OrderBy(p => p.ProductPrice); 
                    break;
                case "price_desc":
                    product = product.OrderByDescending(p => p.ProductPrice); 
                    break;
                default:
                    product = product.OrderBy(p => p.ProductID);
                    break;
            }

            int PageNumber = page ?? 1;
            int PageSize = 2;

            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName");


            searchVM.OrderBy = OrderBy;

            searchVM.Products = product.ToPagedList(PageNumber, PageSize);
            return View(searchVM);
        }

        // GET: Admin/Products/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = await db.Products.FindAsync(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Admin/Products/Create
        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName");
            return View();
        }

        // POST: Admin/Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ProductID,CategoryID,ProductName,ProductDescription,ProductPrice,ProductImage")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", product.CategoryID);
            return View(product);
        }

        // GET: Admin/Products/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = await db.Products.FindAsync(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", product.CategoryID);
            return View(product);
        }

        // POST: Admin/Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ProductID,CategoryID,ProductName,ProductDescription,ProductPrice,ProductImage")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", product.CategoryID);
            return View(product);
        }

        // GET: Admin/Products/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = await db.Products.FindAsync(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Admin/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Product product = await db.Products.FindAsync(id);
            db.Products.Remove(product);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }




        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult Home()
        {
            return View();
        }

        
    }
}
