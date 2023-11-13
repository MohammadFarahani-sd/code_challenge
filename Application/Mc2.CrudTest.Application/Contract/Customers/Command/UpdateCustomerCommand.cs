using MediatR;

namespace Mc2.CrudTest.Application.Contract.Customers.Command;

[Serializable]
public class UpdateCustomerCommand : IRequest<Guid>
{
    public UpdateCustomerCommand(Guid id, string firstname, string lastname, DateOnly dateOfBirth, ulong phoneNumber, string email, string? bankAccountNumber)
    {
        Id = id;
        Firstname = firstname;
        Lastname = lastname;
        DateOfBirth = dateOfBirth;
        PhoneNumber = phoneNumber;
        Email = email;
        BankAccountNumber = bankAccountNumber;
    }

    public Guid Id { get; set; }
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public ulong PhoneNumber { get; set; }
    public string Email { get; set; }
    public string? BankAccountNumber { get; set; }
}