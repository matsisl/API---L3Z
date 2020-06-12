using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Common
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<TEntity> GetById(int id);
        Task<IEnumerable<TEntity>> GetAll();
        Task<bool> Add(TEntity entity);
        Task<bool> Delete(TEntity entity);
        Task<bool> Update(TEntity entity);
        Task<IEnumerable<TEntity>> Sort(TypeOfSorting typeOfSorting);
        Task<IEnumerable<TEntity>> Paging(int pageSize, int pageIndex);
        Task<IEnumerable<TEntity>> Filter(string filter);
        Task<IEnumerable<TEntity>> Get(Sorting sorting, Paging paging, Filtering filtering);
    }
}
