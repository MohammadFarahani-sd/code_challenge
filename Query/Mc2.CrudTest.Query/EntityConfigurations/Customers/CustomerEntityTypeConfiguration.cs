using Mc2.CrudTest.Domain.CustomerAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mc2.CrudTest.Query.EntityConfigurations.Customers;

public class CustomerEntityTypeConfiguration : EntityTypeConfiguration<Customer>
{
    public override void ConfigureDerived(EntityTypeBuilder<Customer> configuration)
    {
        configuration.ToTable("Customer", "Mc2CodeChallenge");

        configuration.Property(o => o.Id).HasColumnName("Id");

        configuration.HasKey(o => o.Id);

        configuration.HasIndex(o => new { o.DateOfBirth, o.FirstName, o.LastName });

        configuration.HasIndex(p => p.Email).IsUnique();

        configuration
            .OwnsOne(o => o.Phone, a =>
            {
                a.Property<Guid>("CustomerId");
                a.Property(c => c.Phone).IsRequired();
                a.WithOwner();
            });

        configuration
            .Property(c => c.FirstName)
            .HasMaxLength(128)
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .IsRequired();


        configuration
            .Property(c => c.LastName)
            .HasMaxLength(128)
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .IsRequired();


        configuration
            .Property(c => c.Email)
            .HasMaxLength(128)
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .IsRequired();

        configuration
            .Property(c => c.DateOfBirth)
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .IsRequired();

        configuration
            .Property(c => c.BankAccountNumber)
            .UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}