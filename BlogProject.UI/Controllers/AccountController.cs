using BlogProject.DAL.ORM.Entity;
using BlogProject.UI.Areas.Admin.Controllers;
using BlogProject.UI.Areas.Admin.Models.VM;
using BlogProject.UI.Models.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace BlogProject.UI.Controllers
{
    public class AccountController : BaseController
    {
        public ActionResult Login()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                AppUser user = new AppUser();
                user = service.AppUserService.FindByUserName(HttpContext.User.Identity.Name);
                if (user.Role == DAL.ORM.Enum.Role.Admin)
                {                   
                    return Redirect("~/Admin/Home/Index");
                }
                else if (user.Role == DAL.ORM.Enum.Role.Author)
                {                
                    return Redirect("~/Author/Home/Index");
                }
                else if (user.Role == DAL.ORM.Enum.Role.Member)
                {                 
                    return Redirect("~/Member/Home/Index");
                }

              //  return RedirectToAction("index", "home");
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
                    if (user.Role==DAL.ORM.Enum.Role.Admin)
                    {
                        Session["FullName"] = user.FirstName + ' ' + user.LastName;
                        return Redirect("/Admin/Home/Index");
                    }
                    else if (user.Role==DAL.ORM.Enum.Role.Author)
                    {
                        Session["FullName"] = user.FirstName + ' ' + user.LastName;
                        return Redirect("/Author/Home/Index");
                    }
                    else if (user.Role==DAL.ORM.Enum.Role.Member)
                    {
                        Session["FullName"] = user.FirstName + ' ' + user.LastName;
                        return Redirect("/Member/Home/Index");
                    }
                   
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
            return Redirect("/Account/Login");

        }
    }
}