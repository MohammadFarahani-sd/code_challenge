using Mc2.CrudTest.Domain.SeedWork;
using Mc2.CrudTest.Query.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Mc2.CrudTest.Query.EntityConfigurations;

public abstract class EntityTypeConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : Entity
{
    private static readonly JsonSerializer Serializer = new()
    {
        DefaultValueHandling = DefaultValueHandling.Ignore,
        ContractResolver = new DefaultContractResolver()
    };

    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.Property(x => x.Id);

        ConfigureDerived(builder);
    }

    public abstract void ConfigureDerived(EntityTypeBuilder<TEntity> builder);

    internal static string Serialize<T>(T obj)
    {
        using (StringWriter writer = new StringWriter())
        {
            Serializer.Serialize(writer, obj);
            return writer.ToString();
        }
    }

    internal static T Deserialize<T>(string json)
    {
        using (StringReader reader = new StringReader(json))
        using (JsonTextReader jsonReader = new JsonTextReader(reader))
        {
            return Serializer.Deserialize<T>(jsonReader)!;
        }
    }
}