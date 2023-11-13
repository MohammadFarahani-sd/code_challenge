using Mc2.CrudTest.Domain.CustomerAggregate;
using Mc2.CrudTest.Query.Models.Customers.Response;

namespace Mc2.CrudTest.Query.Models.Customers.Mapping;

public static class Mapper
{
    public static CustomerResponse Map(Customer customer)
    {
        return CustomerResponse.Build(customer);
    }
}