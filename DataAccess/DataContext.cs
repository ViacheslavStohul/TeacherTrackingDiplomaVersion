using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Win32.SafeHandles;
using System.Runtime.InteropServices;

namespace DataAccess
{
    public partial class DataContext : DbContext
    {
        private bool disposed;
        private readonly SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);

        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        ~DataContext()
        {
            this.Dispose(false);
        }

        #region DeclaringTables
        public DbSet<CompleteMigration> CompleteMigrations { get; set; }

        public DbSet<Log> Logs { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<UserInfo> UserInfos { get; set; }

        public DbSet<AccessLevel> AccessLevels { get; set; }

        public DbSet<Chair> Chairs { get; set; }

        public DbSet<Commission> Commissions { get; set; }

        public DbSet<Department> Departments { get; set; }

        public DbSet<OrganizationalWork> OrganizationalWorks { get; set; }

        public DbSet<Rank> Ranks { get; set; }

        public DbSet<WorkType> WorkTypes { get; set; }

        public DbSet<CommissionHead> ComissionHeads { get; set; }

        public DbSet<ChairHead> ChairHeads { get; set; }

        public DbSet<BanLog> BanLogs { get; set; }
        #endregion

        #region FluentAPI
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<User>()
                .HasKey(u => u.IdUser);

            modelBuilder.Entity<UserInfo>()
                .HasOne(u => u.User)
                .WithOne(ui => ui.UserInfo)
                .HasForeignKey<User>(ui => ui.IdUser);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }


        }

        #endregion

        #region ServiceMethods
        public virtual async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return await this.Database.BeginTransactionAsync(cancellationToken);
        }
        public virtual async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }

        public override void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (this.disposed)
            {
                return;
            }

            if (disposing)
            {
                this.handle.Dispose();
            }

            base.Dispose();
        }
        #endregion
    }
}