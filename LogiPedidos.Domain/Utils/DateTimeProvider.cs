namespace LogiPedidosBackend.LogiPedidos.Domain.Utils;

public static class DateTimeProvider
{
    public static DateTime NowBrasilia()
    {
        return DateTime.UtcNow.AddHours(-3);
    }
}