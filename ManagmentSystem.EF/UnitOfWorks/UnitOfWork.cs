using ManagmentSystem.Core.Interfaces;
using ManagmentSystem.Core.IServices;
using ManagmentSystem.Core.Models;
using ManagmentSystem.Core.UnitOfWorks;
using ManagmentSystem.EF.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagmentSystem.EF.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDBContext _context;
        public UnitOfWork(ApplicationDBContext context)
        {
            _context = context;
            //Categories = new BaseRepository<Category>(_context);//initial // Comment after add spicial method
            Categories = new CategoriesRepository(_context);// After add spicial method
            Users = new UsersRepository(_context);
            UserPositions = new UserPositionsRepository(_context);
            Images = new ImagesRepository(_context);
        }

        //public IBaseRepository<Category> Categories { get; private set; } Comment After add spicial method
        public ICategoriesRepository Categories { get; private set; } // After add spicial method
        public IUsersRepository Users { get; private set; }
        public IUserPositionsRepository UserPositions { get; private set; }
        public IImagesRepository Images { get; private set; }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public async Task<int> CompleteAsync()
        {
            int Resault = await _context.SaveChangesAsync();
            return Resault;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
