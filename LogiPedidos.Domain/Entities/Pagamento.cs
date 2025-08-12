using LogiPedidosBackend.LogiPedidos.Domain.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace LogiPedidosBackend.LogiPedidos.Domain.Entities
{
    public class Pagamento : BaseEntity
    {
        [BsonRepresentation(BsonType.String)]
        public Guid PedidoId { get; set; } // Referência ao pedido

        [BsonRepresentation(BsonType.String)]
        public Guid ClienteId { get; set; } // Referência ao cliente

        [BsonRepresentation(BsonType.String)]
        public FormaPagamento FormaPagamento { get; set; } // Forma utilizada

        [BsonRepresentation(BsonType.String)]
        public StatusPagamento StatusPagamento { get; set; } // Status atual

        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime DataPagamento { get; set; } = DateTime.UtcNow.AddHours(-3); // Data/hora do pagamento

        [MaxLength(200)]
        public string? TransacaoId { get; set; } // ID da transação do gateway
    }
}