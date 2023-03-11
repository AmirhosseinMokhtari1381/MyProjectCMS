using DataLayer.Context;
using DataLayer.Models;
using DataLayer.Models.ViewModels;
using DataLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Services
{

    public class BlogRepository : IBlogRepository
    {

        //هر وقت کسی از این کلاس نمونه سازی کرد اون وقت بیا کانکث رو بیار 
        //ولی قبلا رو این هم نمونه سازی میکرد

        //start Dependeny Injection

            //این یعین هر وقت کسی از این کلاس نمونه سازی کرد این کانتکث رو هم با خودش ببره


        private MyProjectContext db;

        public BlogRepository(MyProjectContext context)
        {
            this.db = context;
        }

        //End Dependeny Injection

        public IEnumerable<Blog> GetAll()
        {
            var getList = db.Blogs.ToList();
            return getList;
        }

        public Blog GetById(int BlogId)
        {
            var getID = db.Blogs.Find(BlogId);
            return getID;
        }

        public bool Create(Blog blog)
        {
            try
            {
                var AddToBlog=db.Blogs.Add(blog);
                return true;

            }
            catch
            {
                throw new Exception("عملیات افزودن وبلاگ با خطا مواجه شد");

            }
        }

        public bool Edit(Blog blog)
        {
            try
            {
                var EditBlog = db.Entry(blog).State = EntityState.Modified;

                return true;
                //اگه تونستی ادیت کنی ترو بر میگردونی اگه نه میری کش
            }
            catch
            {
                throw new Exception("عملیات ویرایش وبلاگ با خطا مواجه شد");

            }
        }


        public bool DeleteById(int blogId)
        {
            try
            {
                var GetId2 = GetById(blogId);

                db.Blogs.Remove(GetId2);

                return true;

            }
            catch
            {
                throw new Exception("عملیات حذف وبلاگ با خطا مواجه شد");

            }
        }


        public BlogViewModel GetByIdForVM(int BlogId)
        {
            var getId = db.Blogs.Where(x => x.BlogId == BlogId).Select(x => new BlogViewModel()
            {
                BlogId = x.BlogId,
                GroupId = x.GroupId,
                Title = x.Title,
                ShortDescription = x.ShortDescription,
                BlogTexe = x.BlogTexe,
                ImageName = x.ImageName,
                CreateDate = x.CreateDate,
                Visit = x.Visit,
            }).FirstOrDefault();

            return getId;
        }


        public void Save()
        {
           db.SaveChanges();
        }

        public void Dispose()
        {
            db.Dispose();
        }

      
    }
}
