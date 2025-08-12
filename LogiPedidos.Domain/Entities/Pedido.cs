using LogiPedidosBackend.LogiPedidos.Domain.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LogiPedidosBackend.LogiPedidos.Domain.Entities
{
    public class Pedido : BaseEntity
    {
        public Pedido()
        {
            Data = DateTime.UtcNow.AddHours(-3);
        }

        public DateTime Data { get; set; }

        [BsonRepresentation(BsonType.String)]
        public StatusPedido Status { get; set; }

        [BsonRepresentation(BsonType.String)]
        public Guid ClienteId { get; set; }

        public List<ItemPedido> Itens { get; set; } = new();

        [BsonIgnore] // Não persiste no MongoDB
        public decimal PrecoTotal => Itens.Sum(i => i.PrecoUnitario * i.Quantidade);
    }

    public class ItemPedido
    {
        [BsonRepresentation(BsonType.String)]
        public Guid ProdutoId { get; set; }

        public string NomeProduto { get; set; } = string.Empty;

        public int Quantidade { get; set; }

        public decimal PrecoUnitario { get; set; }
    }
}