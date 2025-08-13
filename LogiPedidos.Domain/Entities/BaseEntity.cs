using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using LogiPedidosBackend.LogiPedidos.Domain.Utils;

namespace LogiPedidosBackend.LogiPedidos.Domain.Entities;

public abstract class BaseEntity
{
    [BsonRepresentation(BsonType.String)]
    public Guid Id { get; set; } = Guid.NewGuid();

    [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
    public DateTime DataCadastro { get; set; } = DateTimeProvider.NowBrasilia();

    [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
    public DateTime DataAtualizacao { get; set; } = DateTimeProvider.NowBrasilia();

    public bool Ativo { get; set; } = true;

    public virtual void AlterarStatus(bool ativo)
    {
        Ativo = ativo;
        DataAtualizacao = DateTime.UtcNow.AddHours(-3);
    }
}