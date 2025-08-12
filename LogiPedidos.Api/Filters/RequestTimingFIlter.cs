using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;
using System.Diagnostics;

namespace LogiPedidosBackend.LogiPedidos.Api.Filters
{
    public class RequestTimingFilter : IActionFilter
    {
        private Stopwatch _stopwatch = null!;

        public void OnActionExecuting(ActionExecutingContext context)
        {
            _stopwatch = Stopwatch.StartNew();
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            _stopwatch.Stop();

            var elapsedMs = _stopwatch.ElapsedMilliseconds;
            var request = context.HttpContext.Request;
            var response = context.HttpContext.Response;

            Log.Information("Tempo de resposta: {Elapsed} ms | {Method} {Path} | Status: {StatusCode}",
                elapsedMs, request.Method, request.Path, response.StatusCode);
        }
    }
}