using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace DapperRepository.Core
{
    public interface IBaseRepository<T> where T: class
    {
         List<T> List ();
         void  Delete(int  id);
         T Find (int  id);
         int  SaveRange(IEnumerable<T> list);
         void  Update(T t,string tablename, int id);
         void   Insert(T t, string tablename);
         List<PropertyInfo> GetProperties();
    }
}
