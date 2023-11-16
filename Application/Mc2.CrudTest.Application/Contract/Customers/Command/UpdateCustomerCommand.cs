using System.Net.Mail;
using MediatR;

namespace Mc2.CrudTest.Application.Contract.Customers.Command;

[Serializable]
public class UpdateCustomerCommand : IRequest<Guid>
{
    public UpdateCustomerCommand(Guid id, string firstname, string lastname, DateTime dateOfBirth, ulong phoneNumber, MailAddress email, string? bankAccountNumber)
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
    public DateTime DateOfBirth { get; set; }
    public ulong PhoneNumber { get; set; }
    public MailAddress Email { get; set; }
    public string? BankAccountNumber { get; set; }
}