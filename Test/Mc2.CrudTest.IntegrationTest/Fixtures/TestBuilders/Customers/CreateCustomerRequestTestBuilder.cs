using System.Net.Mail;
using Mc2.CrudTest.IntegrationTest.Fixtures.Constants;
using Mc2.CrudTest.IntegrationTest.ReflectionTools;
using Mc2.CrudTest.Presentation.Server.Controllers.Models;

namespace Mc2.CrudTest.IntegrationTest.Fixtures.TestBuilders.Customers;

public class CreateCustomerRequestTestBuilder : ReflectionBuilder<CreateCustomerRequest, CreateCustomerRequestTestBuilder>
{
    private readonly CreateCustomerRequestTestBuilder _builderInstance;

    public Guid Id { get; set; } = CustomerConstants.Id;
    public string Firstname { get; set; } = CustomerConstants.Firstname;
    public string Lastname { get; set; } = CustomerConstants.Lastname;
    public ulong PhoneNumber { get; set; } = CustomerConstants.PhoneNumber;
    public DateTime DateOfBirth { get; set; } = CustomerConstants.DateOfBirth;
    public MailAddress Email { get; set; } = CustomerConstants.Email;
    public string? BankAccountNumber { get; set; } = CustomerConstants.BackAccountNumber;
    public CreateCustomerRequestTestBuilder()
    {
        _builderInstance = this;
    }

    public override CreateCustomerRequest Build()
    {
        var customer = new CreateCustomerRequest
        {
            BankAccountNumber = BankAccountNumber,
            DateOfBirth = DateOfBirth,
            Email = Email.ToString(),
            Firstname = Firstname,
            Lastname = Lastname,
            PhoneNumber = PhoneNumber.ToString(),
        };

        return customer;
    }
}