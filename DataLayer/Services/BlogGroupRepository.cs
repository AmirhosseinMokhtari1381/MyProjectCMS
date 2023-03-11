using DataLayer.Context;
using DataLayer.Models;
using DataLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;


namespace DataLayer.Services
{
    public class BlogGroupRepository : IBlogGroupRepository
    {

        // MyProjectContext db = new MyProjectContext(); DELETE..

        //start Dependeny Injection


        //این نمونه سازی دیگر نیست

        private MyProjectContext db;

        //هر وقت این کلاس صدا زده شد بیا اجرا کن کد زیررو
        public BlogGroupRepository(MyProjectContext context)
        {
            this.db = context;
        }



        //end Dependeny Injection

        public IEnumerable<BlogGroup> GetAll()
        {
            var list = db.BlogGroups.ToList();
            return list;
        }

        public BlogGroup GetById(int groupId)
        {
            var GetId = db.BlogGroups.Find(groupId);
            return GetId;
        }

        public bool Create(BlogGroup blogGroup)
        {
            try
            {
                var AddToBlogGroup = db.BlogGroups.Add(blogGroup);
                return true;
                //اگه تونستی اد کنی ترو بر میگردونی اگه نه میری کش
            }
            catch
            {
                throw new Exception("عملیات افزودن گروه با خطا مواجه شد");

            }
        }



        public bool Edit(BlogGroup blogGroup)
        {
            try
            {
                var EditBlogGroup = db.Entry(blogGroup).State = EntityState.Modified;

                return true;
                //اگه تونستی ادیت کنی ترو بر میگردونی اگه نه میری کش
            }
            catch
            {
                throw new Exception("عملیات ویرایش گروه با خطا مواجه شد");

            }
        }




        public bool DeleteById(int GroupId)
        {
            try
            {
                var GetId2 = GetById(GroupId);

                db.BlogGroups.Remove(GetId2);

                return true;

            }
            catch
            {
                throw new Exception("عملیات حذف گروه با خطا مواجه شد");

            }
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
