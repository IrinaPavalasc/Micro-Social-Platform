using Micro_Social_Platform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Micro_Social_Platform.Controllers
{
    public class PostsController : Controller
    {
        private Micro_Social_Platform.Models.AppContext db = new Micro_Social_Platform.Models.AppContext();
        // GET: Posts
        public ActionResult Index()
        {
            var posts = db.Posts.Include("Group");
            ViewBag.Posts = posts;
            if (TempData.ContainsKey("message"))
            {
                ViewBag.Message = TempData["message"];
            }
            return View();
        }

        public ActionResult Show(int id)
        {
            Post post = db.Posts.Find(id);
            return View(post);

        }
        public ActionResult New(int? id)
        {
         
            Post post = new Post();
            post.GroupId = id;
            return View(post);
        }

        [HttpPost]
        public ActionResult New(Post post)
        {
            post.Date = DateTime.Now;
            try
            {
                if (ModelState.IsValid)
                {
                    db.Posts.Add(post);
                    db.SaveChanges();
                    TempData["message"] = "The post has been added.";
                    return RedirectToAction("Index");

                }
                else
                {
                    return View(post);
                }
            }
            catch (Exception e)
            {
                post.Title = e.InnerException.Message;
                post.Title = e.InnerException.ToString();
                return View(post);
            }
        }
        public ActionResult Edit(int id)
        {

            Post post = db.Posts.Find(id);
            return View(post);
        }

        [HttpPut]
        public ActionResult Edit(int id, Post requestPost)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Post post = db.Posts.Find(id);

                    if (TryUpdateModel(post))
                    {
                        //post = requesPost;
                        post.Title = requestPost.Title;
                        post.Content = requestPost.Content;
                        db.SaveChanges();
                        TempData["message"] = "The post has been edited.";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return View(requestPost);
                    }
                }
                else
                {
                    return View(requestPost);
                }
            }
            catch (Exception e)
            {
                return View(requestPost);
            }
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            Post post = db.Posts.Find(id);
            db.Posts.Remove(post);
            db.SaveChanges();
            TempData["message"] = "The post has been deleted.";
            return RedirectToAction("Index");
        }

    }
}