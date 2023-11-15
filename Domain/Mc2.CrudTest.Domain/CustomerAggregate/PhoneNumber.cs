using System.ComponentModel.DataAnnotations.Schema;
using Mc2.CrudTest.Domain.SeedWork;

namespace Mc2.CrudTest.Domain.CustomerAggregate;

[Serializable]
[Table("PhoneNumber" ,Schema = "Mc2CodeChallenge")]
public class PhoneNumber : ValueObject
{
    public ulong Phone { get;  set; }

    protected PhoneNumber()
    {

    }
    public PhoneNumber(ulong phone)
    {
        this.Phone = phone;
    }

    public void Update(ulong phone)
    {
        this.Phone = phone;
    }


    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Phone;
    }
}
