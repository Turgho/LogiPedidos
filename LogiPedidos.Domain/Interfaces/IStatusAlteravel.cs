namespace LogiPedidosBackend.LogiPedidos.Domain.Interfaces;

public interface IStatusAlteravel<TStatus>
{
    bool AlterarStatus(TStatus novoStatus);
}