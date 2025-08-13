using LogiPedidosBackend.LogiPedidos.Api.DTOs.Endereco;

namespace LogiPedidosBackend.LogiPedidos.Api.DTOs.Cliente;

public class ClienteCreateDto
{
    public string Nome { get; set; } = null!;
    public string CpfCnpj { get; set; } = null!;
    public string Telefone { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string SenhaHash { get; set; } = null!;
    public EnderecoDto Endereco { get; set; } = null!;
    public DateTime DataNascimento { get; set; }
}