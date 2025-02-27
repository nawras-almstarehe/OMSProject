using ManagmentSystem.Core.Interfaces;
using ManagmentSystem.Core.IServices;
using ManagmentSystem.Core.Models;
using ManagmentSystem.Core.UnitOfWorks;
using ManagmentSystem.EF.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagmentSystem.EF.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDBContext _context;
        //private IDbTransaction _transaction;   للمراجعة
        public UnitOfWork(ApplicationDBContext context)
        {
            _context = context;
            //Categories = new BaseRepository<Category>(_context);//initial // Comment after add spicial method
            Categories = new CategoriesRepository(_context);// After add spicial method
            Users = new UsersRepository(_context);
            UserPositions = new UserPositionsRepository(_context);
            Images = new ImagesRepository(_context);
            Departments = new DepartmentsRepository(_context);
        }

        //public IBaseRepository<Category> Categories { get; private set; } Comment After add spicial method
        public ICategoriesRepository Categories { get; private set; } // After add spicial method
        public IUsersRepository Users { get; private set; }
        public IUserPositionsRepository UserPositions { get; private set; }
        public IImagesRepository Images { get; private set; }
        public IDepartmentsRepository Departments { get; private set; }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public async Task<int> CompleteAsync()
        {
            int Resault = await _context.SaveChangesAsync();
            return Resault;
        }

        public bool HasChanges()
        {
            return _context.ChangeTracker.HasChanges();
        }


        /// <summary>
        /// للمراجعة
        /// </summary>
        /// 
        //public IDbTransaction BeginTransaction()
        //{
        //    if (_transaction != null)
        //    {
        //        throw new InvalidOperationException("A transaction is already in progress.");
        //    }

        //    _transaction = _context.Database.BeginTransaction();

        //    return _transaction;
        //}

        //public void Commit()
        //{
        //    if (_transaction == null)
        //    {
        //        throw new InvalidOperationException("No active transaction.");
        //    }

        //    try
        //    {
        //        SaveChanges(); // Call SaveChanges() before committing to ensure changes are persisted.
        //        _transaction.Commit();
        //        DisposeCurrentTransaction(); // Clean up after successful commit.

        //        Console.WriteLine("Commit successful.");

        //    }
        //    catch (Exception ex)
        //    {
        //        Rollback();
        //        Console.WriteLine($"Error during commit: {ex.Message}");
        //    }
        //}

        //public void Rollback()
        //{
        //    if (_transaction == null)
        //    {
        //        throw new InvalidOperationException("No active transaction.");
        //    }

        //    try
        //    {
        //        DisposeCurrentTransaction(); // Ensure cleanup even on rollback.

        //        Console.WriteLine("Rollback performed due to error or cancellation.");

        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Error during rollback: {ex.Message}");
        //    }
        //    finally
        //    {
        //        DisposeCurrentTransaction();
        //    }
        //}

        //private void DisposeCurrentTransaction()
        //{
        //    if (_transaction != null)
        //    {
        //        try
        //        {
        //            ((IDisposable)_transaction).Dispose();
        //        }
        //        finally
        //        {
        //            _transaction = null;
        //        }
        //    }
        //}

        //public async Task CompleteAsync()
        //{
        //    await SaveChangesAsync().ConfigureAwait(false);
        //}

        //protected virtual async Task<int> SaveChangesAsync()
        //{
        //    return await base.SaveChangesAsync().ConfigureAwait(false);
        //}

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
