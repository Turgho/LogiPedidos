namespace LogiPedidosBackend.LogiPedidos.Domain.Enums;

public enum FormaPagamento
{
    Nenhum,         // Nenhuma forma de pagamento selecionada
    CartaoCredito,  // Pagamento com cartão de crédito
    CartaoDebito,   // Pagamento com cartão de débito
    Pix,            // Pagamento via Pix
    Boleto          // Pagamento via boleto bancário
}