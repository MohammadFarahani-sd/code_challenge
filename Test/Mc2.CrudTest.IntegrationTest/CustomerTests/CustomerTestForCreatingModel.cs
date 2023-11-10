using System.Net.Http.Json;
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
    public async Task ordering_test_in_create_and_accept()
    {
        var customerRequest = CustomerProvider.ProvideSomeCustomerTestBuilder();


        var createCustomer = await ClientRequest.PostAsJsonAsync(RouteConstantProvider.CreateCustomer, customerRequest);
        
        var responseOfCreate = await createCustomer.Content.ReadFromJsonAsync<Response<Guid>>();
       
        var orders = await OrderProvider.GetZoneOrders(ZoneClient);
        
        dateProvider.SetUtcNow(changeableDateTime.AddMinutes(5));

        AppClient.DefaultRequestHeaders.Add("x-requestid", Guid.NewGuid().ToString());
        
        await RequestToAcceptOrder(AppClient, responseOfCreate.Data);
        
        dateProvider.SetUtcNow(changeableDateTime.AddMinutes(15));

        await RequestToPickUpOrder(AppClient, responseOfCreate.Data);

        dateProvider.SetUtcNow(changeableDateTime.AddMinutes(25));

        await RequestToDeliverOrder(AppClient, responseOfCreate.Data);

        await ZoneClient.DeleteAsync($"{RouteConstantProvider.ZoneOrderRemove}/{responseOfCreate.Data}");

    }

    private static async Task RequestToAcceptOrder(HttpClient client, Guid orderId)
    {
        var acceptOrder = new
        {
            Location = new GeoLocation(20, 30), 
        };

        var rejectByRider =
            await client.PutAsJsonAsync($"{RouteConstantProvider.AcceptOrderByRider}/{orderId}", acceptOrder);

        rejectByRider.EnsureSuccessStatusCode();

    }

    private static async Task RequestToPickUpOrder(HttpClient client, Guid orderId)
    {
        var acceptOrder = new
        {
            Location = new GeoLocation(20, 30),
        };

        var rejectByRider =
            await client.PutAsJsonAsync($"{RouteConstantProvider.PickUpOrderByRider}/{orderId}", acceptOrder);

        rejectByRider.EnsureSuccessStatusCode();

    }


    private static async Task RequestToDeliverOrder(HttpClient client, Guid orderId)
    {
        var acceptOrder = new
        {
            Location = new GeoLocation(20, 30),
        };

        var rejectByRider =
            await client.PutAsJsonAsync($"{RouteConstantProvider.DeliverOrderByRider}/{orderId}", acceptOrder);

        rejectByRider.EnsureSuccessStatusCode();

    }
}