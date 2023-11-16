using System.Net.Mail;
using Mc2.CrudTest.Domain.CustomerAggregate;

namespace Mc2.CrudTest.UnitTest.Customers;

public class CustomerTestBuilder
{
    private string firstname;

    private string lastname;

    private DateOnly dateOfBirth;

    private ulong phoneNumber;

    private MailAddress email;

    private string? bankAccountNumber;


    public CustomerTestBuilder()
    {
        var currentId = Guid.NewGuid();
        firstname = Guid.NewGuid().ToString();
        lastname = Guid.NewGuid().ToString();
        dateOfBirth = DateOnly.FromDateTime(DateTime.Today);
        phoneNumber = ulong.Parse("+989128986248");
        email = new MailAddress($"{Guid.NewGuid()}@example.com");
        bankAccountNumber = "123456789";
        WithId(currentId);

    }

    public Guid Id { get; set; }


    internal CustomerTestBuilder WithId(Guid id)
    {
        Id = id;

        return this;
    }

    internal CustomerTestBuilder WithFirstname(string firstname)
    {
        this.firstname = firstname;

        return this;
    }


    internal CustomerTestBuilder WithLastname(string lastname)
    {
        this.lastname = lastname;

        return this;
    }


    internal CustomerTestBuilder WithEmail(MailAddress email)
    {
        this.email = email;

        return this;
    }


    public Customer CreateEntity()
    {
        var entity = new Customer(firstname, lastname, dateOfBirth, phoneNumber, email, bankAccountNumber);
        return entity;
    }

}