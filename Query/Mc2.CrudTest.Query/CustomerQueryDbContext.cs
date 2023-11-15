using System.ComponentModel;
using System.Net.Mail;
using Mc2.CrudTest.Domain.CustomerAggregate;
using Mc2.CrudTest.Query.EntityConfigurations.Customers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

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

        builder.Entity<Customer>(customer =>
        {
            customer.OwnsOne(e => e.Phone,
                b =>
                {
                    b.ToTable("PhoneNumber");
                    b.Property<Guid>("CustomerId");
                    b.Property(c => c.Phone).IsRequired();
                });
            //customer.Navigation(e => e.Phone).IsRequired();
        });

    }
    protected override void ConfigureConventions(ModelConfigurationBuilder builder)
    {
        builder.Properties<DateOnly>()
            .HaveConversion<DateOnlyConverter>()
            .HaveColumnType("date");
        

        builder.Properties<DateOnly?>()
            .HaveConversion<NullableDateOnlyConverter>()
            .HaveColumnType("date");
    }
    
    /// <summary>
    /// Converts <see cref="DateOnly" /> to <see cref="DateTime"/> and vice versa.
    /// </summary>
    public class DateOnlyConverter : ValueConverter<DateOnly, DateTime>
    {
        /// <summary>
        /// Creates a new instance of this converter.
        /// </summary>
        public DateOnlyConverter() : base(
            d => d.ToDateTime(TimeOnly.MinValue),
            d => DateOnly.FromDateTime(d))
        { }
    }
   
    /// <summary>
    /// Converts <see cref="DateOnly?" /> to <see cref="DateTime?"/> and vice versa.
    /// </summary>
    public class NullableDateOnlyConverter : ValueConverter<DateOnly?, DateTime?>
    {
        /// <summary>
        /// Creates a new instance of this converter.
        /// </summary>
        public NullableDateOnlyConverter() : base(
            d => d == null
                ? null
                : new DateTime?(d.Value.ToDateTime(TimeOnly.MinValue)),
            d => d == null
                ? null
                : new DateOnly?(DateOnly.FromDateTime(d.Value)))
        { }
    }
}