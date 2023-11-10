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
            .With(x => x.Email, CustomerConstants.Email)
            .With(x => x.DateOfBirth, CustomerConstants.DateOfBirth)
            .With(x => x.BankAccountNumber, CustomerConstants.BackAccountNumber);

        return builder;
    }

    public static CreateCustomerRequest ProvideSomeCustomer()
    {
        return ProvideSomeCustomerTestBuilder().Build();
    }
}