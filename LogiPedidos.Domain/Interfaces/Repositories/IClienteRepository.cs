using LogiPedidosBackend.LogiPedidos.Domain.Entities;

namespace LogiPedidosBackend.LogiPedidos.Domain.Interfaces.Repository;

public interface IClienteRepository
{
    // Principais
    Task<Cliente> AddAsync(Cliente cliente);
    Task<Cliente?> GetByIdAsync(Guid id);
    Task<IEnumerable<Cliente>> GetAllAsync();
    Task<bool> UpdateAsync(Cliente cliente);
    Task<bool> DeleteAsync(Guid id);

    // // Consultas específicas
    // Task<Cliente?> GetByEmailAsync(string email);
    // Task<Cliente?> GetByCpfCnpjAsync(string documento);
    // Task<IEnumerable<Cliente>> SearchByNameAsync(string nome);
    // Task<IEnumerable<Cliente>> GetActiveClientsAsync();
    // Task<IEnumerable<Cliente>> GetInactiveClientsAsync();
    //
    // // Utilitários
    // Task<long> CountAsync();
    // Task<bool> ExistsAsync(string id);
    // Task<bool> ExistsByEmailAsync(string email);
    //
    // // Operações de status
    // Task<bool> DeactivateAsync(string id);
    // Task<bool> ActivateAsync(string id);
}