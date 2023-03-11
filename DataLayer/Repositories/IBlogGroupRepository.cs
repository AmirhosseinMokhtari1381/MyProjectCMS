using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories
{
    public interface IBlogGroupRepository:IDisposable
    {
        //اینترفیس میاد اون قوانین و انتظاراتی که داریم رو پیاده سازی میکنیم
        //میگیم باید چه قوانینی داشته باشی

        IEnumerable<BlogGroup> GetAll();

        BlogGroup GetById(int GroupId);

        bool Create(BlogGroup blogGroup);

        bool Edit(BlogGroup blogGroup);

        bool DeleteById(int GroupId);

        void Save();


    }
}
