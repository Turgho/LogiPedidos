using System.ComponentModel.DataAnnotations;

namespace LogiPedidosBackend.LogiPedidos.Domain.Entities;

public class Endereco
{
    [Required, MaxLength(200)]
    public string Rua { get; set; } = string.Empty;

    [Required, MaxLength(10)]
    public string Numero { get; set; } = string.Empty;

    [MaxLength(100)]
    public string Complemento { get; set; } = string.Empty;

    [Required, MaxLength(100)]
    public string Bairro { get; set; } = string.Empty;

    [Required, MaxLength(100)]
    public string Cidade { get; set; } = string.Empty;

    [Required, MaxLength(2)]
    public string Estado { get; set; } = string.Empty;

    [Required, MaxLength(9)]
    public string Cep { get; set; } = string.Empty;
}