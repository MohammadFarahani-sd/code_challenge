namespace Mc2.CrudTest.IntegrationTest.Fixtures.Constants;

public static class RouteConstantProvider
{
    public const string CreateCustomer = "/api/customers";
    public static string UpdateCustomer(Guid id) => $"/api/customers/{id}";
    public const string CustomerRemove = "/api/customers";
    public const string GetCustomers = "/api/customers";
    public const string GetCustomer = "/api/customers";
}