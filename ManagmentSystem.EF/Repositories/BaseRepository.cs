using ManagmentSystem.Core.Interfaces;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ManagmentSystem.Core.Models;
using ManagmentSystem.Core.VModels;

namespace ManagmentSystem.EF.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected ApplicationDBContext _context;

        public BaseRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(string id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<T> Add(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            //_context.SaveChanges(); For added UnitOfWork
            return entity;
        }

        public async Task<T> Update(T entity)
        {
            _context.Set<T>().Update(entity);
            return entity;
        }

        public void DeleteEntity(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
        }

        public int Delete(string id)
        {
            if (id != "")
            {
                var entity = GetById(id);
                var result = _context.Remove(entity);
                if (result != null)
                {
                    return 1;
                }
            }
            return 0;
        }

        public T GetById(string id)
        {
            return _context.Set<T>().Find(id);
        }


        public IEnumerable<T> AddRange(IEnumerable<T> entities)
        {
            _context.Set<T>().AddRange(entities);
            //_context.SaveChanges(); For added UnitOfWork
            return entities;
        }

        public void Attach(T entity)
        {
            _context.Set<T>().Attach(entity);
        }

        public int Count()
        {
            return _context.Set<T>().Count();
        }

        public int Count(Expression<Func<T, bool>> match)
        {
            return _context.Set<T>().Count(match);
        }

        public async Task<T> Find(Expression<Func<T, bool>> match)
        {
            return await _context.Set<T>().SingleOrDefaultAsync(match);
        }

        public async Task<T> FindAsync(Expression<Func<T, bool>> match)
        {
            return await _context.Set<T>().SingleOrDefaultAsync(match);
        }

        public IEnumerable<T> FindAll(Expression<Func<T, bool>> match, string[] includes = null)
        {
            IQueryable<T> query = _context.Set<T>();
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            return query.Where(match).ToList();
        }

        public IEnumerable<T> FindAll(Expression<Func<T, bool>> match, int take, int skip)
        {
            return _context.Set<T>().Where(match).Skip(skip).Take(take).ToList();
        }

        public IEnumerable<T> FindAll(Expression<Func<T, bool>> match, int? take, int? skip, Expression<Func<T, object>> orderBy = null, string orderByDirection = "ASC")
        {
            IQueryable<T> query = _context.Set<T>().Where(match);
            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }
            if (skip.HasValue)
            {
                query = query.Skip(skip.Value);
            }
            if (orderBy != null)
            {
                if (orderByDirection == "ASC")
                {
                    query = query.OrderBy(orderBy);
                }
                else
                {
                    query = query.OrderByDescending(orderBy);
                }
            }

            return query.ToList();
        }

        public T FindByAnyData(Expression<Func<T, bool>> match, string[] includes = null)
        {
            IQueryable<T> query = _context.Set<T>();
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            return query.SingleOrDefault(match);
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> match)
        {
            return await _context.Set<T>().CountAsync(match);
        }
        
        public async Task<int> CountAllAsync()
        {
            return await _context.Set<T>().CountAsync();
        }

        public async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> match, int? take, int? skip, ObjSort sort = null)
        {
            IQueryable<T> query = _context.Set<T>().Where(match);
            if (skip.HasValue)
            {
                query = query.Skip(skip.Value);
            }
            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }

            // Apply sorting if sort fields are provided
            if (sort != null && sort?.column != null && sort?.column != "")
            {
                IOrderedQueryable<T> orderedQuery = null;
                var propertyName = sort.column;
                var sortOrder = sort.state.Length > 1 && sort.state.ToLower() == "desc" ? "desc" : "asc";

                // Create property access expression dynamically
                var parameter = Expression.Parameter(typeof(T), "x");
                var property = Expression.Property(parameter, propertyName);
                var lambda = Expression.Lambda(property, parameter);

                orderedQuery = sortOrder == "desc"
                                        ? Queryable.OrderByDescending(query, (dynamic)lambda)
                                        : Queryable.OrderBy(query, (dynamic)lambda);

                query = orderedQuery ?? query;
            }

            return await query.AsNoTracking().ToListAsync();
        }

        public IQueryable<T> GetQuery(Expression<Func<T, bool>> filter = null, List<string> sortFields = null)
        {
            IQueryable<T> query = _context.Set<T>(); // Get the DbSet of the entity

            // Apply the filter if provided
            if (filter != null)
            {
                query = query.Where(filter);
            }

            // Apply sorting if sort fields are provided
            if (sortFields != null && sortFields.Count > 0)
            {
                IOrderedQueryable<T> orderedQuery = null;
                for (int i = 0; i < sortFields.Count; i++)
                {
                    var sortField = sortFields[i];
                    var parts = sortField.Split(':'); // Split into field and order (e.g., "field:asc" or "field:desc")
                    var propertyName = parts[0];
                    var sortOrder = parts.Length > 1 && parts[1].ToLower() == "desc" ? "desc" : "asc";

                    // Apply sorting dynamically
                    if (i == 0) // First field uses OrderBy/OrderByDescending
                    {
                        orderedQuery = sortOrder == "desc"
                            ? query.OrderByDescending(e => Microsoft.EntityFrameworkCore.EF.Property<object>(e, propertyName))
                            : query.OrderBy(e => Microsoft.EntityFrameworkCore.EF.Property<object>(e, propertyName));
                    }
                    else // Subsequent fields use ThenBy/ThenByDescending
                    {
                        orderedQuery = sortOrder == "desc"
                            ? orderedQuery.ThenByDescending(e => Microsoft.EntityFrameworkCore.EF.Property<object>(e, propertyName))
                            : orderedQuery.ThenBy(e => Microsoft.EntityFrameworkCore.EF.Property<object>(e, propertyName));
                    }
                }

                query = orderedQuery;
            }

            return query; // Return the queryable object
        }
    }
}
