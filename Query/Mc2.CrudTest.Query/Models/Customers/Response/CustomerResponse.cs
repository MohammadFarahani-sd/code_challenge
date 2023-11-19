using System.ComponentModel.DataAnnotations;
using System.Net.Mail;
using Mc2.CrudTest.Domain.CustomerAggregate;

namespace Mc2.CrudTest.Query.Models.Customers.Response;

[Serializable]
public class CustomerResponse
{
    public Guid Id { get; set; }
    public string PhoneNumber { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public DateOnly DateOfBirth { get; set; }
    
    [EmailAddress]
    public MailAddress Email { get; set; } = null!;

    public static CustomerResponse Build(Customer customer)
    {
        return Build<CustomerResponse>(customer);
    }

    public static T Build<T>(Customer customer) where T : CustomerResponse, new()
    {
        return new T
        {
            Id = customer.Id,
            FirstName = customer.FirstName,
            LastName = customer.LastName,
            DateOfBirth = customer.GetDateOfBirth(),
            Email = new MailAddress(customer.Email),
            PhoneNumber = customer.Phone.Phone.ToString()
        };
    }
}