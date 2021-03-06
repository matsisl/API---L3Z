﻿using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Common
{
    public interface IService<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> Get();
        Task<TEntity> GetById(int id);
        Task<bool> Add(TEntity entity);
        Task<bool> Update(TEntity entity);
        Task<bool> Delete(TEntity entity);
        Task<IEnumerable<TEntity>> Sort(TypeOfSorting typeOfSorting);
        Task<IEnumerable<TEntity>> Filter(string filter);
        Task<IEnumerable<TEntity>> Paging(int pageSize, int pageIndex);
    }
}
