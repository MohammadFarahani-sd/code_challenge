using Mc2.CrudTest.Application.Contract.Customers.Command;
using Mc2.CrudTest.Domain.CustomerAggregate;
using MediatR;

namespace Mc2.CrudTest.Application.Command.Customers;

public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, Guid>
{
    private readonly ICustomerRepository _repository;

    public CreateCustomerCommandHandler(ICustomerRepository repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var entity = new Customer(request.Firstname, request.Lastname, request.DateOfBirth, request.PhoneNumber, request.Email, request.BankAccountNumber);

        _repository.Add(entity);

        await _repository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        return entity.Id;
    }
}