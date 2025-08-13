using LogiPedidosBackend.LogiPedidos.Api.DTOs.Cliente;
using LogiPedidosBackend.LogiPedidos.Domain.Entities;

namespace LogiPedidosBackend.LogiPedidos.Domain.Interfaces.Services;

public interface IClienteServices
{
    Task<ClienteReadDto> AddAsync(ClienteCreateDto dto);

    Task<ClienteReadDto?> GetByIdAsync(Guid id);

    Task<IEnumerable<ClienteReadDto>> GetAllAsync();

    Task<ClienteReadDto?> UpdateAsync(ClienteUpdateDto dto);

    Task<bool> DeleteAsync(Guid id);
}
