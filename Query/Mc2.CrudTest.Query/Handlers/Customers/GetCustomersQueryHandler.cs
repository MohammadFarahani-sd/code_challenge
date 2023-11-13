using Mc2.CrudTest.Domain.CustomerAggregate;
using Mc2.CrudTest.Query.Models.Customers.Mapping;
using Mc2.CrudTest.Query.Models.Customers.Response;
using Mc2.CrudTest.Query.Queries.Customers;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Mc2.CrudTest.Query.Handlers.Customers;

public class GetCustomersQueryHandler : IRequestHandler<GetCustomersQuery, List<CustomerResponse>>
{
    private readonly CustomerQueryDbContext _dbContext;
    public GetCustomersQueryHandler(CustomerQueryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<CustomerResponse>> Handle(GetCustomersQuery filter, CancellationToken cancellationToken)
    {
        var query = _dbContext.Customers.AsNoTracking()
            .OrderBy(c => c.FirstName)
            .ThenBy(o=> o.LastName).AsQueryable();

        query = await ApplyCriteria(query, filter);

        var customers = await query
            .Skip(filter.Filter.Offset)
            .Take(filter.Filter.Count)
            .Select(c => Mapper.Map(c))
            .ToListAsync(cancellationToken);

        return customers;
    }

    private static Task<IQueryable<Customer>> ApplyCriteria(IQueryable<Customer> query, GetCustomersQuery filter)
    {
        if (!string.IsNullOrWhiteSpace(filter.Filter.Keyword))
            query = query.Where(r => r.FirstName.Contains(filter.Filter.Keyword) || r.LastName.Contains(filter.Filter.Keyword) || r.Email.ToString().Contains(filter.Filter.Keyword));

        return Task.FromResult(query);
    }
}