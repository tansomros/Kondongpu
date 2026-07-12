using Kondongpu.Infrastructure.Persistence.Interceptors;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Domain.Entities;

#pragma warning disable CS0618 // ReferenceGroup/ReferenceValue ยังคง DbSet ไว้ แต่ถูกแทนที่ด้วย SmartEnum แล้ว
namespace Kondongpu.Infrastructure.Persistence
{
    public class KondongpuDatabaseContext : DbContext, IKondongpuDatabaseContext
    {
        private readonly AuditableEntitySaveChangesInterceptors _auditableEntitySaveChangesInterceptors;

        public override DatabaseFacade Database { get; }  
        public DbSet<Account> Accounts => Set<Account>();
        public DbSet<Bank> Banks => Set<Bank>();
        public DbSet<BankAccount> BankAccounts => Set<BankAccount>();
        public DbSet<Bill> Bills => Set<Bill>();
        public DbSet<BillDetail> BillDetails => Set<BillDetail>();
        public DbSet<CaneType> CaneTypes => Set<CaneType>();
        public DbSet<Company> Company => Set<Company>();
        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<CustomerCar> CustomerCars => Set<CustomerCar>();
        public DbSet<District> Districts => Set<District>();
        public DbSet<Province> Provinces => Set<Province>();
        public DbSet<SubDistrict> SubDistricts => Set<SubDistrict>();
        public DbSet<Organization> Organizations => Set<Organization>();
        public DbSet<Prefix> Prefixs => Set<Prefix>();
        public DbSet<Price> Prices => Set<Price>();
        public DbSet<Running> Runnings => Set<Running>();
        public DbSet<RunningConfig> RunningConfigs => Set<RunningConfig>();
        public DbSet<User> Users => Set<User>();
        public DbSet<UserRole> UserRoles => Set<UserRole>();

        #region Report Template
        public DbSet<ReportTemplate> ReportTemplates => Set<ReportTemplate>();
        public DbSet<ReportTemplateDetail> ReportTemplateDetails => Set<ReportTemplateDetail>();
        public DbSet<ReportGroup> ReportGroups => Set<ReportGroup>();
        public DbSet<RoleReportTemplate> RoleReportTemplates => Set<RoleReportTemplate>();
        #endregion

        public KondongpuDatabaseContext(
            DbContextOptions<KondongpuDatabaseContext> options,
            AuditableEntitySaveChangesInterceptors auditableEntitySaveChangesInterceptors)
            : base(options)
        {
            Database = base.Database;
            _auditableEntitySaveChangesInterceptors = auditableEntitySaveChangesInterceptors;
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            return base.SaveChangesAsync(cancellationToken);
        }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(KondongpuDatabaseContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
        {
            optionBuilder.AddInterceptors(_auditableEntitySaveChangesInterceptors);
        }
    }
}
