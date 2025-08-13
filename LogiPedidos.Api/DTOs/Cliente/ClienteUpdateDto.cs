using LogiPedidosBackend.LogiPedidos.Api.DTOs.Endereco;

namespace LogiPedidosBackend.LogiPedidos.Api.DTOs.Cliente;

public class ClienteUpdateDto
{
    public Guid Id { get; set; }
    public string? Nome { get; set; }
    public string? Telefone { get; set; }
    public string? Email { get; set; }
    public string? SenhaHash { get; set; }
    public EnderecoDto? Endereco { get; set; }
    public DateTime? DataNascimento { get; set; }
}