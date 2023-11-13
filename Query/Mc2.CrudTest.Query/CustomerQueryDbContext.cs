using Mc2.CrudTest.Domain.CustomerAggregate;
using Mc2.CrudTest.Query.EntityConfigurations.Customers;
using Microsoft.EntityFrameworkCore;

namespace Mc2.CrudTest.Query;

public class CustomerQueryDbContext : DbContext
{
    public CustomerQueryDbContext(DbContextOptions<CustomerQueryDbContext> options) : base(options)
    {
    }

    protected CustomerQueryDbContext(DbContextOptions options) : base(options)
    {
    }


    public DbSet<Customer> Customers { get; set; }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new CustomerEntityTypeConfiguration());
    }
}