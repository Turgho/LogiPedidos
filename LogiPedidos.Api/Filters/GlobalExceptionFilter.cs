using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;

namespace LogiPedidosBackend.LogiPedidos.Api.Filters;

public class GlobalExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        var exception = context.Exception;

        // Log detalhado da exceção
        Log.Error(exception, "Exceção não tratada capturada no filtro global");

        // Monta resposta de erro
        var errorResponse = new
        {
            status = (int)HttpStatusCode.InternalServerError,
            message = "Ocorreu um erro interno no servidor.",
            details = exception.Message // Em produção, pode ser omitido por segurança
        };

        // Retorna JSON para o cliente
        context.Result = new ObjectResult(errorResponse)
        {
            StatusCode = (int)HttpStatusCode.InternalServerError
        };

        context.ExceptionHandled = true;
    }
}
