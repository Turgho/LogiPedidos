using LogiPedidosBackend.LogiPedidos.Api.DTOs.Cliente;
using LogiPedidosBackend.LogiPedidos.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace LogiPedidosBackend.LogiPedidos.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClienteController : ControllerBase
{
    private readonly IClienteServices _clienteService;

    public ClienteController(IClienteServices clienteService)
    {
        _clienteService = clienteService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllClientes()
    {
        var clientes = await _clienteService.GetAllAsync();
        return Ok(clientes);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetClienteById(Guid id)
    {
        var cliente = await _clienteService.GetByIdAsync(id);
        if (cliente == null)
            return NotFound(new { Message = $"Cliente com id {id} não encontrado." });

        return Ok(cliente);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCliente([FromBody] ClienteCreateDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var clienteCriado = await _clienteService.AddAsync(dto);
            return CreatedAtAction(nameof(GetClienteById), new { id = clienteCriado.Id }, clienteCriado);
        }
        catch (Exception ex)
        {
            // Você pode logar o erro aqui se quiser
            return StatusCode(500, new { Message = "Erro ao criar cliente.", Details = ex.Message });
        }
    }

    [HttpPatch("{id:guid}")]
    public async Task<IActionResult> UpdateCliente(Guid id, [FromBody] ClienteUpdateDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (id != dto.Id)
            return BadRequest(new { Message = "ID da URL diferente do ID do corpo da requisição." });

        var clienteAtualizado = await _clienteService.UpdateAsync(dto);
        if (clienteAtualizado == null)
            return NotFound(new { Message = $"Cliente com id {id} não encontrado." });

        return Ok(clienteAtualizado);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteCliente(Guid id)
    {
        var deletado = await _clienteService.DeleteAsync(id);
        if (!deletado)
            return NotFound(new { Message = $"Cliente com id {id} não encontrado." });

        return Ok(new{message = "Cliente deletado com sucesso."});
    }
}
