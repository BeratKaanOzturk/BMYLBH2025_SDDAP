using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BMYLBH2025_SDDAP.Models
{
    public interface IBaseRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
        void Add(T entity);
        void Update(T entity);
        void Delete(int id);
    }
}