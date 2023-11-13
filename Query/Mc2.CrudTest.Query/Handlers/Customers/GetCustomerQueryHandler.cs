using Mc2.CrudTest.Query.Models.Customers.Mapping;
using Mc2.CrudTest.Query.Models.Customers.Response;
using Mc2.CrudTest.Query.Queries.Customers;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Mc2.CrudTest.Query.Handlers.Customers;

public class GetCustomerQueryHandler : IRequestHandler<GetCustomerQuery, CustomerResponse?>
{
    private readonly CustomerQueryDbContext _dbContext;

    public GetCustomerQueryHandler(CustomerQueryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<CustomerResponse?> Handle(GetCustomerQuery filter, CancellationToken cancellationToken)
    {
        var customer = await _dbContext.Customers
            .AsNoTracking()
            .Where(c => c.Id == filter.CustomerId)
            .FirstOrDefaultAsync(cancellationToken);
      
        return customer == null ? null : Mapper.Map(customer);
    }
}