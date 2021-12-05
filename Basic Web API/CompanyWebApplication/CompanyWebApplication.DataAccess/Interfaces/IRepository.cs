using System;
using System.Collections.Generic;
using System.Text;

namespace CompanyWebApplication.DataAccess.Interfaces
{
    public interface IRepository<T>
    {
        List<T> Get();
        T GetById(int id);
        int Create(T entity);
        void Update(T entity);
        void Delete(int id);

    }
}
