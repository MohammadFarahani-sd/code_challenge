using System.Net.Http.Json;
using Common.Models;
using Mc2.CrudTest.IntegrationTest.BaseTests;
using Mc2.CrudTest.IntegrationTest.Fixtures.Constants;
using Mc2.CrudTest.IntegrationTest.Fixtures.Providers.CustomerProvider;
using Microsoft.Extensions.DependencyInjection;

namespace Mc2.CrudTest.IntegrationTest.CustomerTests;

[Collection("none paralleled collection 3")]
[CollectionDefinition("none paralleled collection 3", DisableParallelization = true)]
public class CustomerTestForCreatingModel : BaseIntegrationTest
{
    [Fact]
    public async Task happy_scenario_for_creating_customer()
    {
        var customerRequest = CustomerProvider.ProvideSomeCustomerTestBuilder();

        var createCustomer = await ClientRequest.PostAsJsonAsync(RouteConstantProvider.CreateCustomer, customerRequest);
        
        var responseOfCreate = await createCustomer.Content.ReadFromJsonAsync<Response<Guid>>();
    }


    [Fact]
    public async Task not_valid_data_for_create_user_when_first_name_is_not_valid()
    {
        var customerRequest = CustomerProvider.ProvideSomeCustomerTestBuilder();
        customerRequest.Firstname = "";
        var createCustomer = await ClientRequest.PostAsJsonAsync(RouteConstantProvider.CreateCustomer, customerRequest);

        var responseOfCreate = await createCustomer.Content.ReadFromJsonAsync<Response<ResponseMeta>>();

        Assert.NotEmpty(responseOfCreate.Meta.Message);
    }



    [Fact]
    public async Task not_valid_data_for_create_user_when_last_name_is_not_valid()
    {
        var customerRequest = CustomerProvider.ProvideSomeCustomerTestBuilder();
        customerRequest.Lastname = "";
        var createCustomer = await ClientRequest.PostAsJsonAsync(RouteConstantProvider.CreateCustomer, customerRequest);

        var responseOfCreate = await createCustomer.Content.ReadFromJsonAsync<Response<ResponseMeta>>();

        Assert.NotEmpty(responseOfCreate.Meta.Message);

    }

}