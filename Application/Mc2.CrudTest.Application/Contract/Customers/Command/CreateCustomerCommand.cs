using System.Net.Mail;
using MediatR;

namespace Mc2.CrudTest.Application.Contract.Customers.Command;

[Serializable]
public class CreateCustomerCommand : IRequest<Guid>
{
    public CreateCustomerCommand( string firstname, string lastname, DateTime dateOfBirth, ulong phoneNumber, MailAddress email, string? bankAccountNumber)
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
    public DateTime DateOfBirth { get; set; }
    public ulong PhoneNumber { get; set; }
    public MailAddress Email { get; set; }
    public string? BankAccountNumber { get; set; }
}