using Đồ_Án_BackUp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Đồ_Án_BackUp.Areas.Admin.Controllers
{
    public class AdminLogInSignUpController : Controller
    {
        MyStoreEntities db = new MyStoreEntities();
        // GET: Users/LoginSignIn

        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(User user)
        {
            var CheckExistUser = db.Users.Any(x => x.Username.Equals(user.Username));

            if (!CheckExistUser)
            {
                var users = new User()
                {
                    Username = user.Username,
                    Password = user.Password,
                    Repassword = user.Repassword,
                    UserRole = "A"
                };

                if (ModelState.IsValid)
                {
                    db.Users.Add(users);
                    db.SaveChanges();
                    return RedirectToAction("Login", "AdminLogInSignUp");
                }
                else
                {
                    ModelState.AddModelError("Incorrect", "Incorrect UserName Or PassWord");
                }
            }
            return View(user);
        }

        [HttpPost]
        public ActionResult Login(User user)
        {
            if (CheckLogin(user))
            {
                Session["AdminRole"] = user.UserRole;
                Session["AdminUsername"] = user.Username;
                return RedirectToAction("Index", "Products");
            }
            return View(user);
        }

        public ActionResult Login()
        {
            return View();
        }

        public bool CheckLogin(User user)
        {
            user.UserRole = "A";
            if (db.Users.Where(x => (x.Username.Equals(user.Username)) && (x.Password.Equals(user.Password)) && (x.UserRole.Equals(user.UserRole))).FirstOrDefault() != null)
            {
                return true;
            }
            return false;
        }

        // Đăng xuất

        public ActionResult LogOut()
        {
            Session.Clear();
            return RedirectToAction("Login", "AdminLogInSignUp");
        }

        public ActionResult UserHomePage()
        {
            return View(db.Users.ToList());
        }

        public ActionResult EditUserAccount(string UserName)
        {
            if (UserName == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var FindUserByName = db.Users.Where(x => x.Username.Equals(UserName)).FirstOrDefault();
            if (FindUserByName == null)
            {
                return HttpNotFound();
            }
            return View(FindUserByName);
        }


        [HttpPost]
        public async Task<ActionResult> EditUserAccount(User user)
        {
            if (ModelState.IsValid)
            {
                user.UserRole = "A";
                db.Entry(user).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("UserHomePage", "AdminLogInSignUp");
            }
            return View(user);
        }


    }
}