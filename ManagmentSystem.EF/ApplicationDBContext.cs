using System;
using Microsoft.EntityFrameworkCore;
using ManagmentSystem.Core.Models;
using ManagmentSystem.Core.VModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace ManagmentSystem.EF
{
    public partial class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // In case options are not already configured (unlikely, but just in case)
                optionsBuilder.UseSqlServer("DefaultConnection");
            }

            // Enable sensitive data logging and log to the console
            optionsBuilder.EnableSensitiveDataLogging();
            optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>(eb =>
            {
                eb.HasKey(x => x.Id);
                eb.Property(x => x.Id)
                    .HasColumnType("VARCHAR(36)").IsRequired();
                eb.Property(x => x.AName).HasColumnType("varchar(200)").IsRequired();
                eb.Property(x => x.EName).HasColumnType("varchar(200)").IsRequired();
                eb.Property(x => x.LastAccessed).HasColumnType("DateTime").ValueGeneratedOnAddOrUpdate();
            });
            modelBuilder.Entity<Privilege>(eb =>
            {
                eb.HasKey(x => x.Id);
                eb.Property(x => x.Id)
                    .HasColumnType("VARCHAR(36)").IsRequired();
                eb.Property(x => x.Code).HasColumnType("float");
                eb.Property(x => x.AName).HasColumnType("varchar(200)");
                eb.Property(x => x.EName).HasColumnType("varchar(200)");
                eb.Property(x => x.Type).HasColumnType("int");
                eb.Property(x => x.ADescription).HasColumnType("varchar(4000)");
                eb.Property(x => x.EDescription).HasColumnType("varchar(4000)");
                eb.Property(x => x.LastAccessed).HasColumnType("DateTime").ValueGeneratedOnAddOrUpdate();
            });
            modelBuilder.Entity<RolePrivilege>(eb =>
            {
                eb.HasKey(x => x.Id);
                eb.Property(x => x.Id)
                    .HasColumnType("VARCHAR(36)").IsRequired();
                eb.Property(x => x.AddedOn).HasColumnType("DateTime");
                eb.Property(x => x.LastAccessed).HasColumnType("DateTime").ValueGeneratedOnAddOrUpdate();
                eb.Property(x => x.RoleId).HasColumnType("VARCHAR(36)");
                eb.Property(x => x.PrivilegeId).HasColumnType("VARCHAR(36)");
            });
            modelBuilder.Entity<User>(eb =>
            {
                eb.HasKey(x => x.Id);
                eb.Property(x => x.Id)
                    .HasColumnType("VARCHAR(36)").IsRequired();
                eb.Property(x => x.UserName).HasColumnType("varchar(200)").IsRequired();
                eb.Property(x => x.AFirstName).HasColumnType("varchar(200)").IsRequired();
                eb.Property(x => x.EFirstName).HasColumnType("varchar(200)").IsRequired();
                eb.Property(x => x.ALastName).HasColumnType("varchar(200)").IsRequired();
                eb.Property(x => x.ELastName).HasColumnType("varchar(200)").IsRequired();
                eb.Property(x => x.Email).HasColumnType("varchar(200)").IsRequired();
                eb.Property(x => x.Password).HasColumnType("varchar(200)").IsRequired();
                eb.Property(x => x.PhoneNumber).HasColumnType("varchar(200)").IsRequired();
                eb.Property(x => x.IsBlocked).HasColumnType("bit");
                eb.Property(x => x.IsAdmin).HasColumnType("bit").HasDefaultValue(false);
                eb.Property(x => x.BlockedType).HasColumnType("int");
                eb.Property(x => x.UserType).HasColumnType("int");
                eb.Property(x => x.LastAccessed).HasColumnType("DateTime").ValueGeneratedOnAddOrUpdate();
            });
            modelBuilder.Entity<Department>(eb =>
            {
                eb.HasKey(x => x.Id);
                eb.Property(x => x.Id)
                    .HasColumnType("VARCHAR(36)").IsRequired();
                eb.Property(x => x.AName).HasColumnType("varchar(200)").IsRequired();
                eb.Property(x => x.EName).HasColumnType("varchar(200)").IsRequired();
                eb.Property(x => x.DepartmentParentId).HasColumnType("VARCHAR(36)");
                eb.Property(x => x.DepartmentType).HasColumnType("int");
                eb.Property(x => x.Code).HasColumnType("varchar(200)").IsRequired();
                eb.Property(x => x.DepCode).HasColumnType("varchar(200)");
                eb.Property(x => x.IsActive).HasColumnType("bit");
                eb.Property(x => x.LastAccessed).HasColumnType("DateTime").ValueGeneratedOnAddOrUpdate();
            });
            modelBuilder.Entity<Position>(eb =>
            {
                eb.HasKey(x => x.Id);
                eb.Property(x => x.Id)
                    .HasColumnType("VARCHAR(36)").IsRequired();
                eb.Property(x => x.DepartmentId).HasColumnType("VARCHAR(36)").IsRequired();
                eb.Property(x => x.AName).HasColumnType("varchar(200)").IsRequired();
                eb.Property(x => x.EName).HasColumnType("varchar(200)").IsRequired();
                eb.Property(x => x.IsActive).HasColumnType("bit");
                eb.Property(x => x.IsLeader).HasColumnType("bit");
                eb.Property(x => x.LastAccessed).HasColumnType("DateTime").ValueGeneratedOnAddOrUpdate();
            });
            modelBuilder.Entity<UserPosition>(eb =>
            {
                eb.HasKey(x => x.Id);
                eb.Property(x => x.Id)
                    .HasColumnType("VARCHAR(36)").IsRequired();
                eb.Property(x => x.UserId).HasColumnType("VARCHAR(36)").IsRequired();
                eb.Property(x => x.UserName).HasColumnType("varchar(200)").IsRequired();
                eb.Property(x => x.IsActive).HasColumnType("bit");
                eb.Property(x => x.Type).HasColumnType("int");
                eb.Property(x => x.AddedOn).HasColumnType("DateTime");
                eb.Property(x => x.StartDate).HasColumnType("DateTime").IsRequired();
                eb.Property(x => x.EndDate).HasColumnType("DateTime").IsRequired();
                eb.Property(x => x.PositionId).HasColumnType("VARCHAR(36)").IsRequired();
                eb.Property(x => x.LastAccessed).HasColumnType("DateTime").ValueGeneratedOnAddOrUpdate();
            });
            modelBuilder.Entity<UserPositionRole>(eb =>
            {
                eb.HasKey(x => x.Id);
                eb.Property(x => x.Id)
                    .HasColumnType("VARCHAR(36)").IsRequired();
                eb.Property(x => x.AddedOn).HasColumnType("DateTime");
                eb.Property(x => x.RoleId).HasColumnType("VARCHAR(36)");
                eb.Property(x => x.UserPositionId).HasColumnType("VARCHAR(36)");
                eb.Property(x => x.LastAccessed).HasColumnType("DateTime").ValueGeneratedOnAddOrUpdate();
            });
            modelBuilder.Entity<UserProfile>(eb =>
            {
                eb.HasKey(x => x.Id);
                eb.Property(x => x.Id)
                    .HasColumnType("VARCHAR(36)").IsRequired();
                eb.Property(x => x.UserId).HasColumnType("VARCHAR(36)").IsRequired();
                eb.Property(x => x.UserName).HasColumnType("varchar(200)").IsRequired();
                eb.Property(x => x.ImagePath).HasColumnType("varchar(200)");
                eb.Property(x => x.Language).HasColumnType("int").HasDefaultValue(VMUserProfile.Enum_Language.English);
                eb.Property(x => x.Theme).HasColumnType("int").HasDefaultValue(VMUserProfile.Enum_Theme.Lighte);
                eb.Property(x => x.LastAccessed).HasColumnType("DateTime").ValueGeneratedOnAddOrUpdate();
            });
            modelBuilder.Entity<AccessListPrivilege>(eb =>
            {
                eb.HasKey(x => x.Id);
                eb.Property(x => x.Id)
                    .HasColumnType("VARCHAR(36)").IsRequired();
                eb.Property(x => x.Code).HasColumnType("float");
                eb.Property(x => x.UserPositionId).HasColumnType("VARCHAR(36)");
                eb.Property(x => x.LastAccessed).HasColumnType("DateTime").ValueGeneratedOnAddOrUpdate();
            });
            modelBuilder.Entity<Product>(eb =>
            {
                eb.HasKey(x => x.Id);
                eb.Property(x => x.Id)
                    .HasColumnType("VARCHAR(36)").IsRequired();
                eb.Property(x => x.AName).HasColumnType("varchar(200)").IsRequired();
                eb.Property(x => x.EName).HasColumnType("varchar(200)").IsRequired();
                eb.Property(x => x.Description).HasColumnType("varchar(4000)");
                eb.Property(x => x.Quantity).HasColumnType("float").HasDefaultValue(0);
                eb.Property(x => x.Status).HasColumnType("bit");
                eb.Property(x => x.IsDiscount).HasColumnType("bit");
                eb.Property(x => x.LastAccessed).HasColumnType("DateTime").ValueGeneratedOnAddOrUpdate();
            });
            modelBuilder.Entity<Category>(eb =>
            {
                eb.HasKey(x => x.Id);
                eb.Property(x => x.Id)
                    .HasColumnType("VARCHAR(36)").IsRequired();
                eb.Property(x => x.AName).HasColumnType("varchar(200)").IsRequired();
                eb.Property(x => x.EName).HasColumnType("varchar(200)").IsRequired();
                eb.Property(x => x.Description).HasColumnType("varchar(4000)");
                eb.Property(x => x.LastAccessed).HasColumnType("DateTime").ValueGeneratedOnAddOrUpdate();
            });
            modelBuilder.Entity<CategoryProduct>(eb =>
            {
                eb.HasKey(x => x.Id);
                eb.Property(x => x.Id)
                    .HasColumnType("VARCHAR(36)").IsRequired();
                eb.Property(x => x.CategoryId).HasColumnType("VARCHAR(36)");
                eb.Property(x => x.ProductId).HasColumnType("VARCHAR(36)");
                eb.Property(x => x.AddedOn).HasColumnType("DateTime");
                eb.Property(x => x.LastAccessed).HasColumnType("DateTime").ValueGeneratedOnAddOrUpdate();
            });
            modelBuilder.Entity<ImageFolder>(eb =>
            {
                eb.HasKey(x => x.Id);
                eb.Property(x => x.Id)
                    .HasColumnType("VARCHAR(36)").IsRequired();
                eb.Property(x => x.ImagePath).HasColumnType("varchar(4000)");
                eb.Property(x => x.ProductId).HasColumnType("VARCHAR(36)");
                eb.Property(x => x.CategoryId).HasColumnType("VARCHAR(36)");
                eb.Property(x => x.LastAccessed).HasColumnType("DateTime").ValueGeneratedOnAddOrUpdate();
            });
            modelBuilder.Entity<Customer>(eb =>
            {
                eb.HasKey(x => x.Id);
                eb.Property(x => x.Id)
                    .HasColumnType("VARCHAR(36)").IsRequired();
                eb.Property(x => x.AName).HasColumnType("varchar(200)").IsRequired();
                eb.Property(x => x.EName).HasColumnType("varchar(200)").IsRequired();
                eb.Property(x => x.Address).HasColumnType("varchar(200)").IsRequired();
                eb.Property(x => x.UserId).HasColumnType("VARCHAR(36)").IsRequired();
                eb.Property(x => x.LastAccessed).HasColumnType("DateTime").ValueGeneratedOnAddOrUpdate();
            });
            modelBuilder.Entity<Producer>(eb =>
            {
                eb.HasKey(x => x.Id);
                eb.Property(x => x.Id)
                    .HasColumnType("VARCHAR(36)").IsRequired();
                eb.Property(x => x.Address).HasColumnType("varchar(200)").IsRequired();
                eb.Property(x => x.AName).HasColumnType("varchar(200)").IsRequired();
                eb.Property(x => x.EName).HasColumnType("varchar(200)").IsRequired();
                eb.Property(x => x.UserId).HasColumnType("VARCHAR(36)").IsRequired();
                eb.Property(x => x.LastAccessed).HasColumnType("DateTime").ValueGeneratedOnAddOrUpdate();
            });
            modelBuilder.Entity<Invoice>(eb =>
            {
                eb.HasKey(x => x.Id);
                eb.Property(x => x.Id)
                    .HasColumnType("VARCHAR(36)").IsRequired();
                eb.Property(x => x.Code).HasColumnType("varchar(200)");
                eb.Property(x => x.AddedOn).HasColumnType("DateTime");
                eb.Property(x => x.TotalCost).HasColumnType("decimal(18,2)");
                eb.Property(x => x.TotalCostAfterDiscount).HasColumnType("decimal(18,2)");
                eb.Property(x => x.TotalCostWithoutDiscount).HasColumnType("decimal(18,2)");
                eb.Property(x => x.SpecialDiscount).HasColumnType("decimal(18,2)");
                eb.Property(x => x.Note).HasColumnType("varchar(4000)");
                eb.Property(x => x.ProcessType).HasColumnType("varchar(20)");
                eb.Property(x => x.Status).HasColumnType("varchar(20)");
                eb.Property(x => x.LastAccessed).HasColumnType("DateTime").ValueGeneratedOnAddOrUpdate();
            });            
            modelBuilder.Entity<Process>(eb =>
            {
                eb.HasKey(x => x.Id);
                eb.Property(x => x.Id)
                    .HasColumnType("VARCHAR(36)").IsRequired();
                eb.Property(x => x.Code).HasColumnType("varchar(200)");
                eb.Property(x => x.Quantity).HasColumnType("float").IsRequired();
                eb.Property(x => x.AddedOn).HasColumnType("DateTime");
                eb.Property(x => x.Price).HasColumnType("decimal(18,2)");
                eb.Property(x => x.PriceItem).HasColumnType("decimal(18,2)");
                eb.Property(x => x.Discount).HasColumnType("decimal(18,2)");
                eb.Property(x => x.Note).HasColumnType("varchar(4000)");
                eb.Property(x => x.Status).HasColumnType("int").HasDefaultValue((int)VNProcess.Enum_Status.Request);
                eb.Property(x => x.ProcessType).HasColumnType("varchar(20)");
                eb.Property(x => x.InvoiceId).HasColumnType("VARCHAR(36)");
                eb.Property(x => x.ProductId).HasColumnType("VARCHAR(36)");
                eb.Property(x => x.CustomerId).HasColumnType("VARCHAR(36)");
                eb.Property(x => x.ProducerId).HasColumnType("VARCHAR(36)");
                eb.Property(x => x.LastAccessed).HasColumnType("DateTime").ValueGeneratedOnAddOrUpdate();
            });
            modelBuilder.Entity<SessionInfo>(eb =>
            {
                eb.HasKey(x => x.Id);
                eb.Property(x => x.Id)
                    .HasColumnType("VARCHAR(36)").IsRequired();
                eb.Property(x => x.SessionId).HasColumnType("VARCHAR(36)");
                eb.Property(x => x.UserId).HasColumnType("VARCHAR(36)");
                eb.Property(x => x.UserName).HasColumnType("varchar(200)");
                eb.Property(x => x.IsAdmin).HasColumnType("bit");
                eb.Property(x => x.Language).HasColumnType("int");
                eb.Property(x => x.ExpiresAt).HasColumnType("DateTime");
                eb.Property(x => x.LastAccessed).HasColumnType("DateTime").ValueGeneratedOnAddOrUpdate();
            });
            /////////
            /////////
            ///
            modelBuilder.Entity<Position>()
                .HasMany(x => x.Users)
                .WithMany(y => y.Positions)
                .UsingEntity<UserPosition>(
                    z => z
                            .HasOne(u => u.User)
                            .WithMany(t => t.UserPositions)
                            .HasForeignKey(pt => pt.UserId),
                    w => w
                            .HasOne(up => up.Position)
                            .WithMany(t => t.UserPositions)
                            .HasForeignKey(up => up.PositionId),
                    j =>
                    {
                        j.Property(up => up.AddedOn).HasDefaultValueSql("NOW()");
                        j.HasKey(t => new { t.UserId, t.PositionId });
                    }
            );

            modelBuilder.Entity<UserPosition>(entity =>
            {
                entity.HasKey(up => up.Id);
                entity.HasOne(up => up.AccessListPrivilege)
                      .WithOne(alp => alp.UserPosition)
                      .HasForeignKey<AccessListPrivilege>(alp => alp.UserPositionId);
            });

            modelBuilder.Entity<AccessListPrivilege>(entity =>
            {
                entity.HasKey(alp => alp.Id);
            });

            modelBuilder.Entity<Privilege>()
                .HasMany(x => x.Roles)
                .WithMany(y => y.Privileges)
                .UsingEntity<RolePrivilege>(
                    z => z
                            .HasOne(u => u.Role)
                            .WithMany(t => t.RolePrivileges)
                            .HasForeignKey(pt => pt.RoleId),
                    w => w
                            .HasOne(up => up.Privilege)
                            .WithMany(t => t.RolePrivileges)
                            .HasForeignKey(up => up.PrivilegeId),
                    j =>
                    {
                        j.Property(up => up.AddedOn).HasDefaultValueSql("NOW()");
                        j.HasKey(t => new { t.RoleId, t.PrivilegeId });
                    }
            );

            modelBuilder.Entity<Department>()
                .HasMany(x => x.Positions)
                .WithOne();

            modelBuilder.Entity<Position>()
                .HasOne(x => x.Department)
                .WithMany(y => y.Positions)
                .HasForeignKey(z => z.DepartmentId);

            modelBuilder.Entity<Process>()
                .HasOne(x => x.Invoice)
                .WithMany(y => y.Processes)
                .HasForeignKey(z => z.InvoiceId);

            modelBuilder.Entity<Process>()
                .HasOne(x => x.Product)
                .WithMany(y => y.Processes)
                .HasForeignKey(z => z.ProductId);
            
            modelBuilder.Entity<Process>()
                .HasOne(x => x.Customer)
                .WithMany(y => y.Processes)
                .HasForeignKey(z => z.CustomerId);
            
            modelBuilder.Entity<Process>()
                .HasOne(x => x.Producer)
                .WithMany(y => y.Processes)
                .HasForeignKey(z => z.ProducerId);

            modelBuilder.Entity<User>()
                .HasOne(x => x.Customer)
                .WithOne(y => y.User)
                .HasForeignKey<Customer>(z => z.UserId);

            modelBuilder.Entity<User>()
                .HasOne(x => x.Producer)
                .WithOne(y => y.User)
                .HasForeignKey<Producer>(z => z.UserId);

            modelBuilder.Entity<User>()
                .HasOne(x => x.UserProfile)
                .WithOne(y => y.User)
                .HasForeignKey<UserProfile>(z => z.UserId);

            modelBuilder.Entity<Category>()
                .HasMany(x => x.Productes)
                .WithMany(y => y.Categories)
                .UsingEntity<CategoryProduct>(
                    z => z
                            .HasOne(cp => cp.Product)
                            .WithMany(t => t.CategoriesProductes)
                            .HasForeignKey(pt => pt.ProductId),
                    w => w
                            .HasOne(up => up.Category)
                            .WithMany(t => t.CategoriesProductes)
                            .HasForeignKey(up => up.CategoryId),
                    j =>
                    {
                        j.Property(up => up.AddedOn).HasDefaultValueSql("NOW()");
                        j.HasKey(t => new { t.ProductId, t.CategoryId });
                    }
                );

            modelBuilder.Entity<Category>()
                .HasMany(x => x.ImageFolders)
                .WithOne();

            modelBuilder.Entity<ImageFolder>()
                .HasOne(x => x.Category)
                .WithMany(y => y.ImageFolders)
                .HasForeignKey(z => z.CategoryId);

            modelBuilder.Entity<Product>()
                .HasMany(x => x.ImageFolders)
                .WithOne();

            modelBuilder.Entity<ImageFolder>()
                .HasOne(x => x.Product)
                .WithMany(y => y.ImageFolders)
                .HasForeignKey(z => z.ProductId);
            ///////////////////////////
            ///
            modelBuilder.Entity<Category>()
                .HasIndex(x => x.Id).IsUnique();
            modelBuilder.Entity<CategoryProduct>()
                .HasIndex(x => x.Id).IsUnique();
            modelBuilder.Entity<Customer>()
                .HasIndex(x => x.Id).IsUnique();
            modelBuilder.Entity<Department>()
                .HasIndex(x => new { x.Id, x.Code }).IsUnique();
            modelBuilder.Entity<Invoice>()
                .HasIndex(x => x.Id).IsUnique();
            modelBuilder.Entity<Position>()
                .HasIndex(x => x.Id).IsUnique();
            modelBuilder.Entity<Privilege>()
                .HasIndex(x => x.Id).IsUnique();
            modelBuilder.Entity<Process>()
                .HasIndex(x => x.Id).IsUnique();
            modelBuilder.Entity<Producer>()
                .HasIndex(x => x.Id).IsUnique();
            modelBuilder.Entity<Product>()
                .HasIndex(x => x.Id).IsUnique();
            modelBuilder.Entity<Role>()
                .HasIndex(x => x.Id).IsUnique();
            modelBuilder.Entity<RolePrivilege>()
                .HasIndex(x => x.Id).IsUnique();
            modelBuilder.Entity<User>()
                .HasIndex(x => new {x.Id, x.UserName, x.Email}).IsUnique();
            modelBuilder.Entity<UserPosition>()
                .HasIndex(x => x.Id).IsUnique();
            modelBuilder.Entity<UserPositionRole>()
                .HasIndex(x => x.Id).IsUnique();
            modelBuilder.Entity<ImageFolder>()
                .HasIndex(x => x.Id).IsUnique();
            modelBuilder.Entity<UserProfile>()
                .HasIndex(x => x.Id).IsUnique();
            modelBuilder.Entity<AccessListPrivilege>()
                .HasIndex(x => x.Id).IsUnique();

            ///////Seed data
            ///
            var hasher = new PasswordHasher<User>();
            var privilegeId = Guid.NewGuid().ToString();
            var userId = Guid.NewGuid().ToString();
            var roleId = Guid.NewGuid().ToString();
            var rolePrivilegeId = Guid.NewGuid().ToString();
            var accessListPrivilegeId = Guid.NewGuid().ToString();
            var departmentId = Guid.NewGuid().ToString();
            var positionId = Guid.NewGuid().ToString();
            var userPositionId = Guid.NewGuid().ToString();
            var userPositionRoleId = Guid.NewGuid().ToString();
            modelBuilder.Entity<Role>()
                .HasData(new Role
                {
                    Id = roleId,
                    AName = "دور مدير النظام",
                    EName = "Role system manager"
                });

            modelBuilder.Entity<Privilege>()
                .HasData(new Privilege
                {
                    Id = privilegeId,
                    AName = "مدير النظام",
                    EName = "SystemManager",
                    ADescription = "مدير النظام",
                    EDescription = "System manager",
                    Code = (double)VMPrivilege.Enum_Privilege.SystemManager,
                    Type = (int)VMPrivilege.Enum_Privilege_Type.None,
                });

            modelBuilder.Entity<RolePrivilege>()
                .HasData(new RolePrivilege
                {
                    Id = rolePrivilegeId,
                    RoleId = roleId,
                    PrivilegeId = privilegeId,
                });

            modelBuilder.Entity<User>()
                .HasData(new User
                {
                    Id = userId,
                    UserName = "admin",
                    AFirstName = "مدير",
                    EFirstName = "System",
                    ALastName = "النظام",
                    ELastName = "Manager",
                    Email = "nawras4mstarehe@gmail.com",
                    Password = hasher.HashPassword(null, "admin"),
                    PhoneNumber = "0953244518",
                    IsBlocked = false,
                    IsAdmin = true,
                    BlockedType = (int)VMUser.Enum_User_Blocked_Type.None,
                    UserType = (int)VMUser.Enum_User_Type.Employee,
                });
            modelBuilder.Entity<Department>()
                .HasData(new Department
                {
                    Id = departmentId,
                    AName = "الإدارة العامة",
                    EName = "General department",
                    Code = "GD",
                    DepCode = "0001",
                    DepartmentParentId = "",
                    IsActive = true,
                    DepartmentType = (int)VMDepartment.Enum_Department_Type.GeneralDepartment,
                });
            modelBuilder.Entity<Position>()
                .HasData(new Position
                {
                    Id = positionId,
                    AName = "المدير العام",
                    EName = "Manager General",
                    DepartmentId = departmentId,
                    IsActive = true,
                    IsLeader = true,
                    
                });
            modelBuilder.Entity<UserPosition>()
                .HasData(new UserPosition
                {
                    Id = userPositionId,
                    UserName = "admin",
                    UserId = userId,
                    IsActive = true,
                    PositionId = positionId,
                    Type = (int)VMUserPosition.Enum_UserPosition_Type.HR,
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddMonths(12),

                });
            modelBuilder.Entity<AccessListPrivilege>()
                .HasData(new AccessListPrivilege
                {
                    Id = accessListPrivilegeId,
                    UserPositionId = userPositionId,
                    Code = (double)(VMPrivilege.Enum_Privilege.None | VMPrivilege.Enum_Privilege.SystemManager)
                });
            modelBuilder.Entity<UserPositionRole>()
                .HasData(new UserPositionRole
                {
                    Id = userPositionRoleId,
                    UserPositionId = userPositionId,
                    RoleId = roleId
                });
        }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Privilege> Privileges { get; set; }
        public DbSet<RolePrivilege> RolePrivileges { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Department> Departmentes { get; set; }
        public DbSet<Position> Positiones { get; set; }
        public DbSet<UserPosition> UsersPositions { get; set; }
        public DbSet<UserPositionRole> UserPositionRoles { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<AccessListPrivilege> AccessListPrivileges { get; set; }
        public DbSet<Product> Productes { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryProduct> CategoryProductes { get; set; }
        public DbSet<ImageFolder> ImageFolderes { get; set; }
        public DbSet<Customer> Customeres { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Producer> Produceres { get; set; }
        public DbSet<Process> Processes { get; set; }
        public DbSet<SessionInfo> Sessions { get; set; }
    
    }
}
