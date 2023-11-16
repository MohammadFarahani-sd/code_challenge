using System.Net.Http.Json;
using System.Net.Mail;
using Common.Models;
using Mc2.CrudTest.IntegrationTest.BaseTests;
using Mc2.CrudTest.IntegrationTest.Fixtures.Constants;
using Mc2.CrudTest.IntegrationTest.Fixtures.Providers.CustomerProvider;
using Microsoft.Extensions.DependencyInjection;

namespace Mc2.CrudTest.IntegrationTest.CustomerTests;

[Collection("none paralleled collection 3")]
[CollectionDefinition("none paralleled collection 3", DisableParallelization = true)]
public class CustomerTestForUpdatingModel : BaseIntegrationTest
{
    [Fact]
    public async Task happy_scenario_for_update_customer()
    {
        var customerRequest = CustomerProvider.ProvideSomeCustomerTestBuilder();

        var createCustomer = await ClientRequest.PostAsJsonAsync(RouteConstantProvider.CreateCustomer, customerRequest);

        var responseOfCreate = await createCustomer.Content.ReadFromJsonAsync<Response<Guid>>();

        var updateCustomer = CustomerProvider.UpdateSomeCustomer("Mohammad", "farahani", new MailAddress("mfarahan8575@gmail.com"),
            004412345678, "1234567890123456789", DateTime.Today.AddYears(-36));

        var updateCustomerResponse = await ClientRequest.PostAsJsonAsync(RouteConstantProvider.UpdateCustomer(responseOfCreate.Data), updateCustomer);

        var responseOfUpdate = await updateCustomerResponse.Content.ReadFromJsonAsync<Response<Guid>>();

    }


    [Fact]
    public async Task not_valid_data_for_update_user_when_first_name_is_not_valid()
    {
        var customerRequest = CustomerProvider.ProvideSomeCustomerTestBuilder();

        var createCustomer = await ClientRequest.PostAsJsonAsync(RouteConstantProvider.CreateCustomer, customerRequest);

        var responseOfCreate = await createCustomer.Content.ReadFromJsonAsync<Response<Guid>>();

        var updateCustomer = CustomerProvider.UpdateSomeCustomer("", "farahani", new MailAddress("mfarahan8575@gmail.com"), 004412345678,
            "1234567890123456789", DateTime.Today.AddYears(-36));

        var updateCustomerResponse = await ClientRequest.PostAsJsonAsync(RouteConstantProvider.UpdateCustomer(responseOfCreate.Data), updateCustomer);

        var responseOfUpdate = await updateCustomerResponse.Content.ReadFromJsonAsync<Response<ResponseMeta>>();

        Assert.NotEmpty(responseOfUpdate.Meta.Message);
    }
}