using BlogProject.DAL.ORM.Entity;
using BlogProject.UI.Areas.Author.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace BlogProject.UI.Areas.Author.Controllers
{
    public class AccountController : BaseController
    {
        public ActionResult Login()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("index", "home");
            }
            TempData["class"] = "custom-hide";
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Login(LoginVM credential)
        {
            if (ModelState.IsValid)
            {
                if (service.AppUserService.CheckCredentials(credential.UserName, credential.Password))
                {
                    AppUser user = service.AppUserService.FindByUserName(credential.UserName);
                    string cookie = user.UserName;
                    FormsAuthentication.SetAuthCookie(cookie, true);
                    return RedirectToAction("index", "home");
                }
                else
                {
                    ViewData["error"] = "Kullanıcı adı veya şifre Hatalı";
                    return View();
                }
            }
            TempData["class"] = "custom-show";
            return View();
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return Redirect("/home/index");

        }
    }
}