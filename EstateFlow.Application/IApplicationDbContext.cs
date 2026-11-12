using EstateFlow.Domain;
using Microsoft.EntityFrameworkCore;

namespace EstateFlow.Application;

public interface IApplicationDbContext
{
    DbSet<City> Cities { get; }
    DbSet<Property> Properties { get; }
    DbSet<Tenant> Tenants { get; }
    DbSet<Rental> Rentals { get; }
    DbSet<Contract> Contracts { get; }
    DbSet<Invoice> Invoices { get; }
    DbSet<Payment> Payments { get; }
    DbSet<Expense> Expenses { get; }
    DbSet<Meter> Meters { get; }
    DbSet<MeterReading> MeterReadings { get; }
    DbSet<Provider> Providers { get; }
    DbSet<UtilityType> UtilityTypes { get; }
    DbSet<PropertyImage> PropertyImages { get; }

    Task<int> SaveChangesAsync(
        CancellationToken cancellationToken = default);
}