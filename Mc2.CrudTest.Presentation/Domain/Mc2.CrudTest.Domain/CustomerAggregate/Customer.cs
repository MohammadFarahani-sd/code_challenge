using System.ComponentModel.DataAnnotations.Schema;
using Mc2.CrudTest.Domain.Exceptions;
using Mc2.CrudTest.Domain.SeedWork;

namespace Mc2.CrudTest.Domain.CustomerAggregate;

[Table("Customers", Schema = "Mc2CodeChallenge")]
public class Customer : Entity, IAggregateRoot
{
    private string firstname;

    private string lastname;

    private DateTimeOffset dateOfBirth;

    private string phoneNumber;

    private string email;

    private string? bankAccountNumber;

    protected Customer()
    {
    }

    public Customer(string firstname, string lastname, DateTimeOffset dateOfBirth, string phoneNumber, string email, string? bankAccountNumber)
    {
        if (string.IsNullOrWhiteSpace(firstname))
            throw new DomainException("invalid firstname");

        if (string.IsNullOrWhiteSpace(lastname))
            throw new DomainException("invalid lastname");

        if (string.IsNullOrWhiteSpace(email))
            throw new DomainException("invalid email");


        Id = Guid.NewGuid();
        this.firstname = firstname;
        this.lastname = lastname;
        this.dateOfBirth = dateOfBirth;
        this.phoneNumber = phoneNumber;
        this.email = email;
        this.bankAccountNumber = bankAccountNumber;
    }

    public string GetFirstName() => firstname;

    public string GetLastName() => lastname;

    public string GetEmail() => email;

    public DateTimeOffset GetDateOfBirth() => dateOfBirth.Date;

    public string GetPhoneNumber() => phoneNumber;

    public void Update(string firstname, string lastname, DateTimeOffset dateOfBirth, string phoneNumber, string email, string bankAccountNumber)
    {
        if (string.IsNullOrWhiteSpace(firstname))
            throw new DomainException("invalid firstname");

        if (string.IsNullOrWhiteSpace(lastname))
            throw new DomainException("invalid lastname");

        if (string.IsNullOrWhiteSpace(email))
            throw new DomainException("invalid email");

        this.firstname = firstname;
        this.lastname = lastname;
        this.dateOfBirth = dateOfBirth;
        this.phoneNumber = phoneNumber;
        this.email = email;
        this.bankAccountNumber = bankAccountNumber;
    }

}