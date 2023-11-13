using MediatR;

namespace Mc2.CrudTest.Application.Contract.Customers.Command;

[Serializable]
public class DeleteCustomerCommand : IRequest<Guid>
{
    public DeleteCustomerCommand(Guid id)
    {
        Id = id;
    }
    public Guid Id { get; set; }
}