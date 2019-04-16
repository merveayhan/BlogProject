using BlogProject.DAL.ORM.Entity;
using BlogProject.UI.Areas.Author.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogProject.UI.Areas.Author.Controllers
{
    public class ArticleController : BaseController
    {
        private object comment;


        public ActionResult Add()
        {
            return View(service.CategoryService.GetActive());
        }
        [HttpPost]
        public ActionResult Add(Article data)
        {
            data.AppUserID = service.AppUserService.FindByUserName(HttpContext.User.Identity.Name).ID;
            data.AddDate = DateTime.Now;
            data.PublishDate = DateTime.Now;
            service.ArticleService.Add(data);
            return View("/home/index");
        }

        public ActionResult Show(Guid id)
        {
            if (ModelState.IsValid)
            {
                ArticleCommentVM model = new ArticleCommentVM();

                model.Article = service.ArticleService.GetById(id);
                model.Comments = service.CommentService.GetDefault(x => x.ArticleID == id).OrderByDescending(x => x.AddDate).Take(10);
                model.Likes = service.LikeService.GetDefault(x => x.ArticleID == id).Count();

                return View(model);
            }
            else
            {
                return View();
            }

        }

        public ActionResult AddComment(CommentVM data)
        {
            Comment comment = new Comment();

            comment.AppUserID = service.AppUserService.FindByUserName(HttpContext.User.Identity.Name).ID;
            comment.ArticleID = data.ID;
            comment.Header = data.Header;
            comment.Content = data.Content;
            comment.AddDate = DateTime.Now;
            service.CommentService.Add(comment);
            return Redirect("/Article/Show/" + data.ID);
        }

        public JsonResult AddLike(Guid id)
        {
            JsonLikeVM jr = new JsonLikeVM();
            Guid appuserID = service.AppUserService.FindByUserName(HttpContext.User.Identity.Name).ID;

            if (!(service.LikeService.Any(x => x.AppUserID == appuserID && x.ArticleID == id)))
            {
                Like like = new Like();
                like.ArticleID = id;
                like.AppUserID = appuserID;
                service.LikeService.Add(like);

                jr.Likes = service.LikeService.GetDefault(x => x.ArticleID == id).Count();
                jr.userMessage = "";
                jr.isSuccess = true;
                return Json(jr, JsonRequestBehavior.AllowGet);
            }

            jr.isSuccess = false;
            jr.userMessage = "Bu yazıya daha önce beğendiniz!";
            jr.Likes = service.LikeService.GetDefault(x => x.ArticleID == id).Count();
            return Json(jr, JsonRequestBehavior.AllowGet);
        }
    }
}