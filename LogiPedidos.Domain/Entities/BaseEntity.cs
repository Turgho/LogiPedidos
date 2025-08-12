using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LogiPedidosBackend.LogiPedidos.Domain.Entities;

public abstract class BaseEntity
{
    [BsonRepresentation(BsonType.String)]
    public Guid Id { get; set; } = Guid.NewGuid();

    [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
    public DateTime DataCadastro { get; set; } = DateTime.UtcNow;

    [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
    public DateTime? DataAtualizacao { get; set; }

    public bool Ativo { get; set; } = true;

    public virtual void AlterarStatus(bool ativo)
    {
        Ativo = ativo;
        DataAtualizacao = DateTime.UtcNow.AddHours(-3);
    }
}