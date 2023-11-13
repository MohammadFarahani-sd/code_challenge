using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;
using System.Net.Mail;
using Mc2.CrudTest.Domain.Exceptions;
using Mc2.CrudTest.Domain.SeedWork;

namespace Mc2.CrudTest.Domain.CustomerAggregate;

[Table("Customers", Schema = "Mc2CodeChallenge")]
public class Customer : Entity, IAggregateRoot
{
    public string FirstName { get; private set; }

    public string LastName { get; private set; }

    public DateOnly DateOfBirth { get; private set; }

    public PhoneNumber Phone { get; private set; } = null!;


    [EmailAddress] 
    public MailAddress Email { get; private set; }

    /// <summary>
    /// The IBAN consist of up to 34 alphanumeric characters  comprising a country code.
    /// </summary>
    public string? BankAccountNumber { get; private set; }

    protected Customer()
    {
    }

    public Customer(string firstname, string lastname, DateOnly dateOfBirth, ulong phoneNumber, string email, string? bankAccountNumber)
    {
        if (string.IsNullOrWhiteSpace(firstname))
            throw new DomainException("invalid firstname");

        if (string.IsNullOrWhiteSpace(lastname))
            throw new DomainException("invalid lastname");

        if (string.IsNullOrWhiteSpace(email))
            throw new DomainException("invalid email");

        if (!MailAddress.TryCreate(email, out var emailItem))
            throw new DomainException("invalid email");


        Id = Guid.NewGuid();
        this.FirstName = firstname;
        this.LastName = lastname;
        this.DateOfBirth = dateOfBirth;
        this.Phone = new PhoneNumber(phoneNumber);
        this.Email = new MailAddress(email);
        this.BankAccountNumber = bankAccountNumber;
    }

    public string GetFirstName() => FirstName;

    public string GetLastName() => LastName;

    public MailAddress GetEmail() => Email;

    public DateOnly GetDateOfBirth() => DateOfBirth;

    public PhoneNumber GetPhoneNumber() => Phone;

    public void Update(string firstname, string lastname, DateOnly dateOfBirth, ulong phoneNumber, string email, string? bankAccountNumber)
    {
        if (string.IsNullOrWhiteSpace(firstname))
            throw new DomainException("invalid firstname");

        if (string.IsNullOrWhiteSpace(lastname))
            throw new DomainException("invalid lastname");

        if (string.IsNullOrWhiteSpace(email))
            throw new DomainException("invalid email");


        if (!MailAddress.TryCreate(email, out var emailItem))
            throw new DomainException("invalid email");
        this.FirstName = firstname;
        this.LastName = lastname;
        this.DateOfBirth = dateOfBirth;
        this.Email = new MailAddress(email);
        this.BankAccountNumber = bankAccountNumber;

        this.Phone.Update(phoneNumber);

    }

}