using System.Net.Mail;
using Mc2.CrudTest.Domain.CustomerAggregate;
using Mc2.CrudTest.Domain.Exceptions;
using Xunit;

namespace Mc2.CrudTest.UnitTest.Customers;

public class CustomerTests
{
    [Fact]
    public void The_customer_created_properly_when_all_business_rules_are_valid()
    {
        //Arrange
        CustomerTestBuilder builder = new CustomerTestBuilder();

        //Act
        Customer entity = builder.CreateEntity();

        //Assert
        Assert.NotNull(entity);

        Assert.NotEqual(default, entity.Id);
    }


    [Fact]
    public void customer_creating_properly_when_firstname_is_invalid()
    {
        //Arrange
        CustomerTestBuilder? builder = new CustomerTestBuilder();

        builder.WithFirstname(string.Empty);

        //Act
        Customer Action()
        {
            return builder.CreateEntity();
        }

        DomainException ex = Assert.Throws<DomainException>((Func<Customer>)Action);

        //Assert            
        Assert.NotNull(ex);
        Assert.IsType<DomainException>(ex);
    }


    [Fact]
    public void email_should_be_equal_with_expected_value()
    {
        //Arrange
        var builder = new CustomerTestBuilder();
        var email = new MailAddress("mfarahan8575@gmail.com");
        builder.WithEmail(email);


        //Act
        Customer Action() => builder.CreateEntity();

        // Assert
        Assert.NotEmpty(Action().GetEmail().ToString());
        Assert.Equal(email.ToString(), Action().GetEmail().ToString());
        Assert.NotEqual(email.ToString(), Action().GetFirstName());
    }

    [Fact]
    public void firstname_should_be_equal_with_expected_value()
    {
        //Arrange
        var builder = new CustomerTestBuilder();
        var firstname = "mohammad";
        builder.WithFirstname(firstname);


        //Act
        Customer Action() => builder.CreateEntity();

        // Assert
        Assert.Equal(firstname, Action().GetFirstName());

    }

    
}