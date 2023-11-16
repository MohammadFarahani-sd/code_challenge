using Mc2.CrudTest.Application.Contract.Customers.Command;
using Mc2.CrudTest.Domain.CustomerAggregate;
using Mc2.CrudTest.Domain.Exceptions;
using MediatR;

namespace Mc2.CrudTest.Application.Command.Customers;

public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, Guid>
{
    private readonly ICustomerRepository _repository;

    public UpdateCustomerCommandHandler(ICustomerRepository repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        var isDuplicatedEmail =
            await _repository.IsUniqueEmail(request.Id, request.Email);

        if (!isDuplicatedEmail)
            throw new DomainException("customer with this email is exist");

        var isDuplicatedData =
            await _repository.IsUniqueValidationPassed(request.Id, request.Firstname, request.Lastname, DateOnly.FromDateTime(request.DateOfBirth));

        if (!isDuplicatedData)
            throw new DomainException("customer with this data is duplicated");

        var customer = await _repository.GetCustomerAsync(request.Id, cancellationToken);

        if (customer == null)
            throw new DomainException("item not found");

        customer.Update(request.Firstname, request.Lastname, DateOnly.FromDateTime(request.DateOfBirth), request.PhoneNumber, request.Email, request.BankAccountNumber);

        await _repository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        return customer.Id;
    }
}