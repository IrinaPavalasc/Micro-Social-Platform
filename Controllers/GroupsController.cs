using Micro_Social_Platform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Micro_Social_Platform.Controllers
{
    public class GroupsController : Controller
    {
        private Micro_Social_Platform.Models.AppContext db = new Micro_Social_Platform.Models.AppContext(); 
        // GET: Groups
        public ActionResult Index()
        {
            if (TempData.ContainsKey("message"))
            {
                ViewBag.Message = TempData["message"].ToString();
            }
            var groups = from gr in db.Groups
                         orderby gr.Name
                         select gr;
            ViewBag.Groups = groups;
            return View();
        }
        public ActionResult Show(int id)
        {
            Group group = db.Groups.Find(id);
            if (id != null)
            {
                var posts = from pst in db.Posts
                            where pst.GroupId == id
                            select pst;
                ViewBag.Posts = posts;
            }
            return View(group);
        }
        public ActionResult New()
        {
            Group group = new Group();
            return View(group);
        }

        [HttpPost]
        public ActionResult New(Group gr)
        {
            gr.Date = DateTime.Now;
            try
            {
                if (ModelState.IsValid)
                {
                    db.Groups.Add(gr);
                    db.SaveChanges();
                    TempData["message"] = "The Group has been added";
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(gr);
                }
            }
            catch (Exception e)
            {
                return View(gr);
            }
        }
        public ActionResult Edit(int id)
        {
            Group group = db.Groups.Find(id);
            return View(group);
        }
        [HttpPut]
        public ActionResult Edit(int id, Group requestGroup)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Group group = db.Groups.Find(id);

                    if (TryUpdateModel(group))
                    {
                        group.Name = requestGroup.Name;
                        group.Description = requestGroup.Description;
                        db.SaveChanges();
                        TempData["message"] = "Group was Edited!";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return View(requestGroup);
                    }
                }
                else
                {
                    return View(requestGroup);
                }
            }
            catch (Exception e)
            {
                return View(requestGroup);
            }
        }
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            Group group = db.Groups.Find(id);
            foreach(Post post in db.Posts)
            {
                if(post.GroupId == id)
                {
                    db.Posts.Remove(post);
                }
            }
            db.Groups.Remove(group);
            db.SaveChanges();
            TempData["message"] = "Grupul a fost sters";
            return RedirectToAction("Index");
        }
    }
}