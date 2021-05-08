using accounts.Models;
using accounts.viewmodel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace accounts.Controllers
{
    public class UsersController : Controller
    {
        private Mydbcontext db = new Mydbcontext();
        // GET: Users
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(Users user)
        {
            if (!ModelState.IsValid)
                return View("Register", user);
            if (db.users.Where(u => u.Email == user.Email).Any())
            {
                ModelState.AddModelError("Email","This Email already exists");
                return View("Register", user);
            }
            db.users.Add(user);
            db.SaveChanges();
            return Content("Your registration ok");
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Loginviewmodel user)
        {
            if (!ModelState.IsValid)
                return View("Login", user);

            var loginuser = db.users.Where(u => u.Name == user.Name && u.Password == user.Password && u.isActive == true).FirstOrDefault();
            if(loginuser==null)
            {
                ModelState.AddModelError("Name", "User name and password are wrong please try with correct user name password");
                return View("Login", user);
            }
            else
            {
                Session["Name"] = loginuser.Name;
                return RedirectToAction("Index", "Accounts");
            }
            return View();
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Login");
        }
    }
}