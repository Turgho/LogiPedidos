using LogiPedidosBackend.LogiPedidos.Domain.Entities;
using LogiPedidosBackend.LogiPedidos.Infrastructure.Data;
using MongoDB.Driver;

namespace LogiPedidosBackend.LogiPedidos.Infrastructure.Repositories;

public class ClienteRepository
{
    private readonly MongoDbContext _context;

    public ClienteRepository(MongoDbContext context)
    {
        _context = context;
    }

    public async Task<List<Cliente>> GetAllAsync()
    {
        return await _context.Clientes.Find(_ => true).ToListAsync();
    }

    public async Task<Cliente> AddAsync(Cliente cliente)
    {
        await _context.Clientes.InsertOneAsync(cliente);
        return cliente;
    } 
}