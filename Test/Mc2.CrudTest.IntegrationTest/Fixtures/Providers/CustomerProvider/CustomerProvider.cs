using System.Net.Mail;
using Mc2.CrudTest.IntegrationTest.Fixtures.Constants;
using Mc2.CrudTest.IntegrationTest.Fixtures.TestBuilders.Customers;
using Mc2.CrudTest.Presentation.Server.Controllers.Models;

namespace Mc2.CrudTest.IntegrationTest.Fixtures.Providers.CustomerProvider;

public static class CustomerProvider
{
    public static CreateCustomerRequestTestBuilder ProvideSomeCustomerTestBuilder()
    {
        var builder = new CreateCustomerRequestTestBuilder()
            .With(x => x.Firstname, CustomerConstants.Firstname)
            .With(x => x.Lastname, CustomerConstants.Lastname)
            .With(x => x.Email, CustomerConstants.Email.ToString())
            .With(x => x.PhoneNumber, CustomerConstants.PhoneNumber.ToString())
            .With(x => x.DateOfBirth, CustomerConstants.DateOfBirth)
            .With(x => x.BankAccountNumber, CustomerConstants.BackAccountNumber);

        return builder;
    }

    public static UpdateCustomerRequestTestBuilder UpdateCustomerTestBuilder(string firstName, string lastname, MailAddress email, ulong phoneNumber, string bankAccount, DateTime dateOfBirth)
    {
        var builder = new UpdateCustomerRequestTestBuilder()
            .With(x => x.Firstname, firstName)
            .With(x => x.Lastname, lastname)
            .With(x => x.Email, email.ToString())
            .With(x => x.DateOfBirth, dateOfBirth)
            .With(x => x.PhoneNumber, phoneNumber.ToString())
            .With(x => x.BankAccountNumber, bankAccount);

        return builder;
    }
    public static CreateCustomerRequest ProvideSomeCustomer()
    {
        return ProvideSomeCustomerTestBuilder().Build();
    }

    public static UpdateCustomerRequest UpdateSomeCustomer(string firstName, string lastname, MailAddress email, ulong phoneNumber, string bankAccount, DateTime dateOfBirth)
    {
        return UpdateCustomerTestBuilder(firstName, lastname, email, phoneNumber, bankAccount, dateOfBirth).Build();
    }
}