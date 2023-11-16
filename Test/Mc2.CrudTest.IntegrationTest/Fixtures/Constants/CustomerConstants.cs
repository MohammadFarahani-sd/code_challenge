using System.Net.Mail;

namespace Mc2.CrudTest.IntegrationTest.Fixtures.Constants;

public static class CustomerConstants
{
    
    public const string Firstname = "SampleFirstname";
    
    public const string Lastname = "SampleLastname";

    public const ulong PhoneNumber =00441234567;
    
    public static readonly MailAddress Email =new MailAddress("mohammad@codechalleng.com");

    public static DateTime DateOfBirth =  DateTime.Today;

    public const string BackAccountNumber = "123456789";

    public static readonly Guid Id = new Guid("c1a0a567-6d1f-4864-97fd-abb23023c4fa");

}