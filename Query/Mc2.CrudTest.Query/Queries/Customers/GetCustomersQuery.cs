using Mc2.CrudTest.Query.Filters;
using Mc2.CrudTest.Query.Models.Customers.Response;
using MediatR;

namespace Mc2.CrudTest.Query.Queries.Customers;

[Serializable]
public class GetCustomersQuery : IRequest<List<CustomerResponse>>
{
    public GetCustomersQuery(CustomerFilter filter)
    {
        Filter = filter;
    }

    public CustomerFilter Filter { get; set; }
}