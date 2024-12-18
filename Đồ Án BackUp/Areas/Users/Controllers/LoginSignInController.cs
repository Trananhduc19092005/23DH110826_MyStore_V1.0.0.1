﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Đồ_Án_BackUp.Models;
using Microsoft.Ajax.Utilities;

namespace Đồ_Án_BackUp.Areas.Users.Controllers
{
    public class LoginSignInController : Controller
    {
        MyStoreEntities db = new MyStoreEntities();
        // GET: Users/LoginSignIn
        public User user_customer = new User();

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
                    UserRole = "U"
                };

                // Lưu thông tin user 

                user_customer.UserRole = "U";
                user_customer.Password = user.Password;
                user_customer.Username = user.Username;

                if (ModelState.IsValid)
                {
                    db.Users.Add(users);
                    db.SaveChanges();
                    return RedirectToAction("Login", "LoginSignIn");
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
                Session["Role"] = user.UserRole;
                Session["Username"] = user.Username;
                if (db.Customers.Any(x => x.Username.Equals(user.Username)))
                {
                    return RedirectToAction("Index", "Home2");
                }
                else
                {
                    return RedirectToAction("CreateCustomer", "Customer");
                }
            }
            return View(user);
        }

        public ActionResult Login()
        {
            return View();
        }

        public bool CheckLogin(User user)
        {
            user.UserRole = "U";
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
            return RedirectToAction("Login", "LoginSignIn");
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
                user.UserRole = "U";
                db.Entry(user).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("UserHomePage", "LoginSignIn");
            }
            return View(user);
        }


    }
}