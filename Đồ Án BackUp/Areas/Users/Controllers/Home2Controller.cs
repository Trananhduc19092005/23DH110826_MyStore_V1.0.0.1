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
using Đồ_Án_BackUp.Models.ViewModel;

namespace Đồ_Án_BackUp.Areas.Users.Controllers
{
    public class Home2Controller : Controller
    {
        private MyStoreEntities db = new MyStoreEntities();

        public async Task<ActionResult> Index()
        {
            var products = db.Products.Include(p => p.Category);
            return View(await products.ToListAsync());
        }

        // GET: Users/Home2/Details/5
        public ActionResult Details(int? id)
        {
            return View(db.Products.Find(id));
        }     
        
        

        public ActionResult AoHomePage(string OrderByPrice)
        {
            var product = db.Products.AsQueryable();
            product = product.Where(x => x.Category.CategoryName == "Quần Áo");
            if (product.Where(x => x.Category.CategoryName == "Quần Áo") != null)
            {
                if (OrderByPrice != null)
                {
                    switch (OrderByPrice)
                    {
                        case "price_asc":
                            product = product.OrderBy(x => x.ProductPrice); break;
                        case "price_desc":
                            product = product.OrderByDescending(x => x.ProductPrice); break;
                        default:
                            product = product.OrderBy(x => x.ProductID); break;
                    }
                }
            }
            return View(product.ToList());
        }
        public ActionResult QuanHomePage(string OrderByPrice)
        {
            var product = db.Products.AsQueryable();
            product = product.Where(x => x.Category.CategoryName == "Quần");
            if (product.Where(x => x.Category.CategoryName == "Quần") != null)
            {
                if (OrderByPrice != null)
                {
                    switch (OrderByPrice)
                    {
                        case "price_asc":
                            product = product.OrderBy(x => x.ProductPrice); break;
                        case "price_desc":
                            product = product.OrderByDescending(x => x.ProductPrice); break;
                        default:
                            product = product.OrderBy(x => x.ProductID); break;
                    }
                }
            }
            return View(product.ToList());
        }

        public ActionResult PhuKienHomePage(string OrderByPrice)
        {
            var product = db.Products.AsQueryable();
            product = product.Where(x => x.Category.CategoryName == "Phụ Kiện");
            if (product.Where(x => x.Category.CategoryName == "Phụ Kiện") != null)
            {
                if (OrderByPrice != null)
                {
                    switch (OrderByPrice)
                    {
                        case "price_asc":
                            product = product.OrderBy(x => x.ProductPrice); break;
                        case "price_desc":
                            product = product.OrderByDescending(x => x.ProductPrice); break;
                        default:
                            product = product.OrderBy(x => x.ProductID); break;
                    }
                }
            }
            return View(product.ToList());
        }

        public ActionResult GiayDepHomePage(string OrderByPrice)
        {
            var product = db.Products.AsQueryable();
            product = product.Where(x => x.Category.CategoryName == "Giày dép");
            if (product.Where(x => x.Category.CategoryName == "Giày dép") != null)
            {
                if (OrderByPrice != null)
                {
                    switch (OrderByPrice)
                    {
                        case "price_asc":
                            product = product.OrderBy(x => x.ProductPrice); break;
                        case "price_desc":
                            product = product.OrderByDescending(x => x.ProductPrice); break;
                        default:
                            product = product.OrderBy(x => x.ProductID); break;
                    }
                }
            }
            return View(product.ToList());
        }

     
        public ActionResult Search(string ProductName , string OrderBy , int ? MinPrice , int ? MaxPrice)
        {
            ProductSearchUser model = new ProductSearchUser();
            var product = db.Products.AsQueryable();

            if (ProductName != null)
            {
                model.FindProducts = ProductName;
                product = product.Where(x => x.ProductName.Contains(ProductName));
            }

            if (OrderBy != null)
            {
                model.OrderBy = OrderBy;
                switch (OrderBy)
                {
                    case "name_asc": product = product.OrderBy(x => x.ProductName); break;
                    case "name_desc": product = product.OrderByDescending(x => x.ProductName); break;
                    case "price_asc":
                        product = product.OrderBy(x => x.ProductPrice); break;
                    case "price_desc":
                        product = product.OrderByDescending(x => x.ProductPrice); break;
                    default:
                        product = product.OrderBy(x => x.ProductID); break;
                }
            }

            if (MinPrice.HasValue)
            {
                model.MinPrice = MinPrice;
                product = product.Where(x => x.ProductPrice >= MinPrice);
            }

            if (MaxPrice.HasValue)
            {
                model.MaxPrice = MaxPrice;
                product = product.Where(x => x.ProductPrice <= MaxPrice);
            }

            model.products = product.ToList();
            return View(model);
        }


    }
}
