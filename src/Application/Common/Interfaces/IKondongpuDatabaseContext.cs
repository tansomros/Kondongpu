using Microsoft.EntityFrameworkCore.Infrastructure;
using Kondongpu.Domain.Entities;

#pragma warning disable CS0618
namespace Kondongpu.Application.Common.Interfaces
{
    public interface IKondongpuDatabaseContext
    {
        DatabaseFacade Database { get; }          
        DbSet<Account> Accounts { get; }
        DbSet<Bank> Banks { get; }
        DbSet<BankAccount> BankAccounts { get; }
        DbSet<Bill> Bills { get; }
        DbSet<BillDetail> BillDetails { get; }
        DbSet<CaneType> CaneTypes { get; }
        DbSet<Company> Company { get; }
        DbSet<Customer> Customers { get; }
        DbSet<CustomerCar> CustomerCars { get; }
        DbSet<District> Districts { get; }
        DbSet<Province> Provinces { get; }
        DbSet<SubDistrict> SubDistricts { get; }
        DbSet<Organization> Organizations { get; }
        DbSet<Prefix> Prefixs { get; }
        DbSet<Price> Prices { get; }
        DbSet<Running> Runnings { get; }
        DbSet<RunningConfig> RunningConfigs { get; }
        DbSet<User> Users { get; }
        DbSet<Role> Roles { get; }
        DbSet<ReportTemplate> ReportTemplates { get; }
        DbSet<ReportTemplateDetail> ReportTemplateDetails { get; }
        DbSet<ReportGroup> ReportGroups { get; }
        DbSet<RoleReportTemplate> RoleReportTemplates { get; }
        
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        int SaveChanges();
    }
}
