using ManagmentSystem.Core.VModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ManagmentSystem.Core.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        //T GetById(int id);
        Task<IEnumerable<T>> GetAll();
        Task<T> GetByIdAsync(string id);
        Task<T> Add(T entity);
        void Update(T entity);
        void DeleteEntity(T entity);
        int Delete(string id);
        T GetById(string id);
        T Find(Expression<Func<T, bool>> match);
        Task<T> FindAsync(Expression<Func<T, bool>> match);
        T FindByAnyData(Expression<Func<T, bool>> match, string[] includes = null);
        IEnumerable<T> FindAll(Expression<Func<T, bool>> match, string[] includes = null);
        IEnumerable<T> FindAll(Expression<Func<T, bool>> match, int take, int skip);
        IEnumerable<T> FindAll(Expression<Func<T, bool>> match, int? take, int? skip,
            Expression<Func<T, object>> orderBy = null, string orderByDirection = "ASC");
        Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> match, int? take, int? skip, ObjSort orderBy = null);
        IEnumerable<T> AddRange(IEnumerable<T> entities);
        void DeleteRange(IEnumerable<T> entities);
        void Attach(T entity);
        int Count();
        int Count(Expression<Func<T, bool>> match);
        Task<int> CountAsync(Expression<Func<T, bool>> match);
        Task<int> CountAllAsync();
    }
}
