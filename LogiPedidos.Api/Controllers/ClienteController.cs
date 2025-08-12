using LogiPedidosBackend.LogiPedidos.Domain.Entities;
using LogiPedidosBackend.LogiPedidos.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LogiPedidosBackend.LogiPedidos.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClienteController : ControllerBase
{
    private readonly ClienteRepository _clienteRepository;

    public ClienteController(ClienteRepository clienteRepository)
    {
        _clienteRepository = clienteRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllClientes()
    {
        var clientes = await _clienteRepository.GetAllAsync();
        return Ok(clientes);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCliente([FromBody] Cliente cliente)
    {
        var clienteCriado = await _clienteRepository.AddAsync(cliente);
        return CreatedAtAction(nameof(CreateCliente), new { id = clienteCriado.Id }, clienteCriado);
    }
}