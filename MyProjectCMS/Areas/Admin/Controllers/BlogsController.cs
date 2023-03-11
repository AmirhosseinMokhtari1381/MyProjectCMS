using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DataLayer.Context;
using DataLayer.Models;
using DataLayer.Models.ViewModels;
using DataLayer.Repositories;
using DataLayer.Services;

namespace MyProjectCMS.Areas.Admin.Controllers
{
    public class BlogsController : Controller
    {
        private IBlogRepository blogRepository;
        private IBlogGroupRepository blogGroupRepository;


        private MyProjectContext db = new MyProjectContext();
        public BlogsController()
        {
            blogRepository = new BlogRepository(db);
            blogGroupRepository = new BlogGroupRepository(db);
        }

        // GET: Admin/Blogs
        public ActionResult Index()
        {
            var getList = blogRepository.GetAll();
            return View(getList);
        }

        // GET: Admin/Blogs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog blog = blogRepository.GetById(id.Value);

            if (blog == null)
            {
                return HttpNotFound();
            }
            return View(blog);
        }

        // GET: Admin/Blogs/Create
        public ActionResult Create()
        {
            ViewBag.GroupId = new SelectList(blogGroupRepository.GetAll(), "GroupId", "GroupTitle");
            return View();
        }

        // POST: Admin/Blogs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create(BlogViewModel blogVM)
        {
            if (ModelState.IsValid)
            {
                Blog blog = new Blog()
                {
                    BlogId = blogVM.BlogId,
                    GroupId = blogVM.GroupId,
                    Title = blogVM.Title,
                    ShortDescription = blogVM.ShortDescription,
                    BlogTexe = blogVM.BlogTexe,
                    ImageName = blogVM.ImageName,
                    CreateDate = blogVM.CreateDate,
                    Visit = blogVM.Visit

                };
                blog.Visit = 0;
                blog.CreateDate = DateTime.Now;
                if (blogVM.ImageUpload != null)
                {
                    blog.ImageName = Guid.NewGuid() + Path.GetExtension(blogVM.ImageUpload.FileName);
                    blogVM.ImageUpload.SaveAs(Server.MapPath("/Images/" + blog.ImageName));
                }


                blogRepository.Create(blog);
                blogRepository.Save();


                return RedirectToAction("Index");
            }

            ViewBag.GroupId = new SelectList(db.BlogGroups, "GroupId", "GroupTitle", blogVM.GroupId);
            return View(blogVM);
        }

        // GET: Admin/Blogs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogViewModel blog = blogRepository.GetByIdForVM(id.Value);

            if (blog == null)
            {
                return HttpNotFound();
            }

            ViewBag.GroupId = new SelectList(blogGroupRepository.GetAll(), "GroupId", "GroupTitle", blog.GroupId);
            return View(blog);
        }

        // POST: Admin/Blogs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BlogViewModel blogVM)
        {
            if (ModelState.IsValid)
            {
                Blog blog = new Blog()
                {
                    BlogId = blogVM.BlogId,
                    GroupId = blogVM.GroupId,
                    Title = blogVM.Title,
                    ShortDescription = blogVM.ShortDescription,
                    BlogTexe = blogVM.BlogTexe,
                    ImageName = blogVM.ImageName,
                    CreateDate = blogVM.CreateDate,
                    Visit = blogVM.Visit

                };

                if (blogVM.ImageUpload != null)
                {
                    if (blog.ImageName != null)
                    {
                        System.IO.File.Delete(Server.MapPath("/Images/" + blog.ImageName));
                    }
                    blog.ImageName = Guid.NewGuid() + Path.GetExtension(blogVM.ImageUpload.FileName);
                    blogVM.ImageUpload.SaveAs(Server.MapPath("/Images/" + blog.ImageName));
                }


                blogRepository.Edit(blog);
                blogRepository.Save();
                return RedirectToAction("Index");
            }
            ViewBag.GroupId = new SelectList(blogGroupRepository.GetAll(), "GroupId", "GroupTitle", blogVM.GroupId);
            return View(blogVM);
        }

        // GET: Admin/Blogs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog blog = blogRepository.GetById(id.Value);
            if (blog == null)
            {
                return HttpNotFound();
            }
            return View(blog);
        }

        // POST: Admin/Blogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Blog blog = blogRepository.GetById(id);
            if (blog.ImageName != null)
            {
                System.IO.File.Delete(Server.MapPath("/Images/" + blog.ImageName));
            }
            blogRepository.DeleteById(id);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                blogGroupRepository.Dispose();
                blogRepository.Dispose();
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
