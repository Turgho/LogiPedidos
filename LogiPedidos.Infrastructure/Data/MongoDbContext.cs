using LogiPedidosBackend.LogiPedidos.Domain.Entities;
using MongoDB.Driver;

namespace LogiPedidosBackend.LogiPedidos.Infrastructure.Data;

public class MongoDbContext
{
    private IMongoDatabase Database { get; }

    public MongoDbContext(MongoDbSettings settings)
    {
        var client = new MongoClient(settings.ConnectionString);
        Database = client.GetDatabase(settings.DatabaseName);
    }
    
    // Collections
    private const string ClientesCollection = "Clientes";
    private const string ProdutosCollection = "Produtos";
    private const string PedidosCollection = "Pedidos";
    private const string PagamentosCollection = "Pagamentos";

    public IMongoCollection<Cliente> Clientes => Database.GetCollection<Cliente>(ClientesCollection);
    public IMongoCollection<Produto> Produtos => Database.GetCollection<Produto>(ProdutosCollection);
    public IMongoCollection<Pedido> Pedidos => Database.GetCollection<Pedido>(PedidosCollection);
    public IMongoCollection<Pagamento> Pagamentos => Database.GetCollection<Pagamento>(PagamentosCollection);
}