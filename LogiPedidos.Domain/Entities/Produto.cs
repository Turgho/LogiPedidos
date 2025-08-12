using System.ComponentModel.DataAnnotations;

namespace LogiPedidosBackend.LogiPedidos.Domain.Entities;

public class Produto : BaseEntity
{
    [Required, MaxLength(200)]
    public string Nome { get; set; } = string.Empty;

    [MaxLength(1000)]
    public string Descricao { get; set; } = string.Empty;

    [Range(0, double.MaxValue)]
    public decimal Preco { get; set; }

    [Range(0, int.MaxValue)]
    public int Estoque { get; set; }
}