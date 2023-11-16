using System.Net.Mail;
using Mc2.CrudTest.IntegrationTest.Fixtures.Constants;
using Mc2.CrudTest.IntegrationTest.ReflectionTools;
using Mc2.CrudTest.Presentation.Server.Controllers.Models;

namespace Mc2.CrudTest.IntegrationTest.Fixtures.TestBuilders.Customers;

public class UpdateCustomerRequestTestBuilder : ReflectionBuilder<UpdateCustomerRequest, UpdateCustomerRequestTestBuilder>
{
    private readonly UpdateCustomerRequestTestBuilder _builderInstance;

    public string Firstname { get; set; } = CustomerConstants.Firstname;
    public string Lastname { get; set; } = CustomerConstants.Lastname;
    public ulong PhoneNumber { get; set; } = CustomerConstants.PhoneNumber;
    public DateTime DateOfBirth { get; set; } = CustomerConstants.DateOfBirth;
    public MailAddress Email { get; set; } = CustomerConstants.Email;
    public string? BankAccountNumber { get; set; } = CustomerConstants.BackAccountNumber;
    public UpdateCustomerRequestTestBuilder()
    {
        _builderInstance = this;
    }

    public override UpdateCustomerRequest Build()
    {
        var customer = new UpdateCustomerRequest
        {
            BankAccountNumber = BankAccountNumber,
            DateOfBirth = DateOfBirth,
            Email = Email.ToString(),
            Firstname = Firstname,
            Lastname = Lastname,
            PhoneNumber = PhoneNumber.ToString()
        };

        return customer;
    }

}