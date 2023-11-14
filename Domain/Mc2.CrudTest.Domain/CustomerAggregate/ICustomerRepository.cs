using System.Net.Mail;
using Mc2.CrudTest.Domain.SeedWork;

namespace Mc2.CrudTest.Domain.CustomerAggregate;

public interface ICustomerRepository : IRepository<Customer>
{
    Customer Add(Customer customer);

    void Delete(Customer entity);

    Task<Customer?> GetCustomerAsync(Guid id, CancellationToken cancellationToken);

    Task<bool> IsUniqueValidationPassed(string firstname, string lastname, DateOnly dateOfBirth);

    Task<bool> IsUniqueValidationPassed(Guid id, string firstname, string lastname, DateOnly dateOfBirth);

    Task<bool> IsUniqueEmail(MailAddress email);

    Task<bool> IsUniqueEmail(Guid id, MailAddress email);
}
