using Mc2.CrudTest.Application.Contract.Customers.Command;
using Mc2.CrudTest.Domain.CustomerAggregate;
using Mc2.CrudTest.Domain.Exceptions;
using MediatR;

namespace Mc2.CrudTest.Application.Command.Customers;

public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, Guid>
{
    private readonly ICustomerRepository _repository;

    public DeleteCustomerCommandHandler(ICustomerRepository repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {

        var customer = await _repository.GetCustomerAsync(request.Id, cancellationToken);

        if (customer == null)
            throw new DomainException("Item Not Found");


        _repository.Delete(customer);

        await _repository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        return request.Id;
    }
}