using System.Linq;
using System.Threading.Tasks;
using DevStore.Billing.API.Models;
using DevStore.Core.Data;
using DevStore.Core.Messages;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;

namespace DevStore.Billing.API.Data;

public sealed class BillingContext : DbContext, IUnitOfWork
{
    public BillingContext(DbContextOptions<BillingContext> options)
        : base(options)
    {
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        ChangeTracker.AutoDetectChangesEnabled = false;
    }

    public DbSet<Payment> Payments { get; set; }
    public DbSet<Transaction> Transactions { get; set; }

    public async Task<bool> Commit()
    {
        return await SaveChangesAsync() > 0;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Ignore<ValidationResult>();
        modelBuilder.Ignore<Event>();

        foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
                     e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
            property.SetColumnType("varchar(100)");

        foreach (var relationship in modelBuilder.Model.GetEntityTypes()
                     .SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BillingContext).Assembly);
    }
}