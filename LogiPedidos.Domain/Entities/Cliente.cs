using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace LogiPedidosBackend.LogiPedidos.Domain.Entities;

public class Cliente : BaseEntity
{
    [Required, MaxLength(200)]
    public string Nome { get; set; } = string.Empty;

    [Required, MaxLength(18)]
    public string CpfCnpj { get; set; } = string.Empty;

    [Required, MaxLength(20)]
    public string Telefone { get; set; } = string.Empty;

    [Required, EmailAddress, MaxLength(200)]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string SenhaHash { get; set; } = string.Empty;

    [Required]
    public Endereco Endereco { get; set; } = new();

    [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
    public DateTime DataNascimento { get; set; }
}