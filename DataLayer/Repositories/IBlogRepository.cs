using DataLayer.Models;
using DataLayer.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories
{
    public interface IBlogRepository:IDisposable
    {

        IEnumerable<Blog> GetAll();

        Blog GetById(int BlogId);
        BlogViewModel GetByIdForVM(int BlogId);
        
        bool Create(Blog blog);

        bool Edit(Blog blog);

        bool DeleteById(int blogId);

        void Save();

    }
}
