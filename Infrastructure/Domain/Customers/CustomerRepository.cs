using Mc2.CrudTest.Domain.CustomerAggregate;
using Mc2.CrudTest.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Mc2.CrudTest.Infrastructure.Domain.Customers;

public class CustomerRepository : RepositoryBase<Customer>, ICustomerRepository
{
    public CustomerRepository(CustomerDbContext dbContext) : base(dbContext)
    {
    }

    public Customer Add(Customer entity)
    {
        return DbContext.Customers.Add(entity).Entity;
    }

    public void Delete(Customer entity)
    {
        DbContext.Customers.Remove(entity);
    }


    public async Task<Customer?> GetCustomerAsync(Guid id, CancellationToken cancellationToken)
    {
        return await DbContext.Customers.FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
    }

    public async Task<bool> IsUniqueValidationPassed(string firstname, string lastname, DateOnly dateOfBirth)
    {
        return await DbContext.Customers.AnyAsync(q =>
            q.FirstName == firstname && q.LastName == lastname && q.DateOfBirth == dateOfBirth);
    }

    public async Task<bool> IsUniqueEmail(string email)
    {
        return await DbContext.Customers.AnyAsync(q => q.Email.ToString() == email);
    }
}