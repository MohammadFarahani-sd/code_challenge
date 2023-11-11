using Mc2.CrudTest.Domain.CustomerAggregate;
using Mc2.CrudTest.Domain.SeedWork;
using Mc2.CrudTest.Infrastructure.EntityConfigurations.Customers;
using Microsoft.EntityFrameworkCore;

namespace Mc2.CrudTest.Infrastructure.Persistence;

public class CustomerDbContext : DbContext,  IUnitOfWork
{
    public CustomerDbContext(DbContextOptions<CustomerDbContext> options) : base(options)
    {
    }

    protected CustomerDbContext(DbContextOptions options) : base(options)
    {
    }


    public DbSet<Customer> Customers { get; set; }
    
    public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
    {
        await base.SaveChangesAsync(cancellationToken);

        return true;
    }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new CustomerEntityTypeConfiguration());

        builder.Entity<Customer>(customer =>
        {
            customer.OwnsOne(e => e.Phone,
                b =>
                {
                    b.ToTable("PhoneNumber");
                    b.Property<Guid>("CustomerId");
                    b.Property(c=> c.Phone)
                        .IsRequired();
                });
            customer.Navigation(e => e.Phone).IsRequired();
        });
    }
}
