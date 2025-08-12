using LogiPedidosBackend.LogiPedidos.Domain.Entities;
using LogiPedidosBackend.LogiPedidos.Domain.Interfaces.Repository;
using LogiPedidosBackend.LogiPedidos.Infrastructure.Data;
using MongoDB.Driver;

namespace LogiPedidosBackend.LogiPedidos.Infrastructure.Repositories;

public class ClienteRepository : IClienteRepository
{
    private readonly MongoDbContext _context;

    public ClienteRepository(MongoDbContext context)
    {
        _context = context;
    }
    
    public async Task<Cliente> AddAsync(Cliente cliente)
    {
        await _context.Clientes.InsertOneAsync(cliente);
        return cliente;
    }

    public async Task<Cliente?> GetByIdAsync(Guid id)
    {
        return await _context.Clientes.Find(c => c.Id == id).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Cliente>> GetAllAsync()
    {
        return await _context.Clientes.Find(c => true).ToListAsync();
    }

    public async Task<bool> UpdateAsync(Cliente cliente)
    {
        // Retorna exceção se nulo
        ArgumentNullException.ThrowIfNull(cliente);

        var update = Builders<Cliente>.Update
            .Set(c => c.Nome, cliente.Nome)
            .Set(c => c.CpfCnpj, cliente.CpfCnpj)
            .Set(c => c.Telefone, cliente.Telefone)
            .Set(c => c.Email, cliente.Email)
            .Set(c => c.SenhaHash, cliente.SenhaHash)
            .Set(c => c.Endereco, cliente.Endereco)
            .Set(c => c.DataNascimento, cliente.DataNascimento)
            .Set(c => c.Ativo, cliente.Ativo)
            .Set(c => c.DataAtualizacao, DateTime.UtcNow.AddHours(-3));

        var result = await _context.Clientes.UpdateOneAsync(
            c => c.Id == cliente.Id,
            update
        );

        return result.IsAcknowledged && result.ModifiedCount > 0;
    }
    
    public async Task<bool> DeleteAsync(Guid id)
    {
        var result = await _context.Clientes.DeleteOneAsync(c => c.Id == id);
        return result.IsAcknowledged && result.DeletedCount > 0;
    }
}