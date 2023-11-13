using MediatR;

namespace Mc2.CrudTest.Application.Contract.Customers.Command;

[Serializable]
public class CreateCustomerCommand : IRequest<Guid>
{
    public CreateCustomerCommand( string firstname, string lastname, DateOnly dateOfBirth, ulong phoneNumber, string email, string? bankAccountNumber)
    {
        Firstname = firstname;
        Lastname = lastname;
        DateOfBirth = dateOfBirth;
        PhoneNumber = phoneNumber;
        Email = email;
        BankAccountNumber = bankAccountNumber;
    }

    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public ulong PhoneNumber { get; set; }
    public string Email { get; set; }
    public string? BankAccountNumber { get; set; }
}