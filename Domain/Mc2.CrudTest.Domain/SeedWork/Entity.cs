namespace Mc2.CrudTest.Domain.SeedWork;

[Serializable]
public abstract class Entity
{
    public Guid Id { get; protected set; }
}
