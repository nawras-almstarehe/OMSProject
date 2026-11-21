using ManagmentSystem.Core.Interfaces;
using ManagmentSystem.Core.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;

namespace ManagmentSystem.Core.UnitOfWorks
{
    public interface IUnitOfWork : IDisposable
    {
        //IBaseRepository<Category> Categories { get; } After add spicial method
        ICategoriesRepository Categories { get; } // After add spicial method
        IProductesRepository Productes { get; }
        ICategoryProductesRepository CategoryProductes { get; } 
        IUsersRepository Users { get; }
        IUserPositionsRepository UserPositions { get; }
        IImagesRepository Images { get; }
        IDepartmentsRepository Departments { get; }
        IPositionsRepository Positions { get; }
        IRolesRepository Roles { get; }
        IPrivilegesRepository Privileges { get; }
        int Complete();
        Task<int> CompleteAsync();
        bool HasChanges();
        void BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
        //Task<IDbContextTransaction> BeginTransactionAsync();
        //Task<IDbContextTransaction> CommitTransactionAsync();
        //Task<IDbContextTransaction> RollbackTransactionAsync();
    }
}
