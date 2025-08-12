namespace LogiPedidosBackend.LogiPedidos.Domain.Enums;

public enum StatusPedido
{
    Aberto,           // Pedido criado, mas não confirmado (ex: carrinho)
    Confirmado,       // Pedido confirmado pelo cliente
    Processando,      // Pedido em preparo/separação/logística
    Enviado,          // Pedido despachado para entrega
    Entregue,         // Pedido entregue ao cliente
    Cancelado,        // Pedido cancelado antes da entrega
    Devolvido,        // Pedido devolvido após entrega
    AguardandoPagamento // Pedido criado, aguardando confirmação do pagamento
}
