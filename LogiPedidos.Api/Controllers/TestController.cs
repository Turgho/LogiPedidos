using Microsoft.AspNetCore.Mvc;

namespace LogiPedidosBackend.LogiPedidos.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TestController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        await Task.Delay(10);
        return Ok(new{Message = "Test com sucesso"});
    }
}