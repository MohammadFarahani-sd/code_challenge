using Mc2.CrudTest.Query.Models.Customers.Response;
using MediatR;

namespace Mc2.CrudTest.Query.Queries.Customers;

[Serializable]
public class GetCustomerQuery : IRequest<CustomerResponse?>
{
    public GetCustomerQuery(Guid customerId)
    {
        CustomerId = customerId;
    }

    public Guid  CustomerId { get; set; }
}