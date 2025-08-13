using LogiPedidosBackend.LogiPedidos.Api.Filters;
using LogiPedidosBackend.LogiPedidos.Api.Mappers.Cliente;
using LogiPedidosBackend.LogiPedidos.Domain.Interfaces.Repository;
using LogiPedidosBackend.LogiPedidos.Domain.Interfaces.Services;
using LogiPedidosBackend.LogiPedidos.Domain.Services;
using LogiPedidosBackend.LogiPedidos.Infrastructure.Data;
using LogiPedidosBackend.LogiPedidos.Infrastructure.Repositories;
using Microsoft.OpenApi.Models;
using Serilog;

string basePath = Path.Combine(Directory.GetCurrentDirectory(), "LogiPedidos.Api");

try
{
    // 1Garante pasta de logs
    EnsureLogFolderExists();

    // Configura logger global (Serilog)
    Log.Logger = new LoggerConfiguration()
        .MinimumLevel.Debug()
        .WriteTo.Console()
        .WriteTo.File(Path.Combine(basePath, "Logs", "log-.txt"),
            rollingInterval: RollingInterval.Day,
            retainedFileCountLimit: 7)
        .CreateLogger();

    Log.Information("Iniciando aplicação...");

    // Carrega configurações
    var config = LoadConfiguration();

    // Cria builder da aplicação
    var builder = WebApplication.CreateBuilder(args);

    // Usa Serilog como logger padrão
    builder.Host.UseSerilog();

    // Substitui config padrão pela carregada manualmente
    builder.Configuration.Sources.Clear();
    builder.Configuration.AddConfiguration(config);
    
    // Configuração dos AutoMappers
    builder.Services.AddAutoMapper(typeof(ClienteMapper).Assembly);

    // Registra infraestrutura e serviços
    builder.Services.AddInfrastructure(builder.Configuration);

    // Configuração de Serviços e Repositories
    builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
    builder.Services.AddScoped<IClienteServices, ClienteServices>();

    builder.Services.AddControllers(options =>
    {
        // Filtro global para tratar exceções nos controllers
        options.Filters.Add<GlobalExceptionFilter>();
        // Filtro para tempo de resposta
        options.Filters.Add<RequestTimingFilter>();
    });

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "LogiPedidosAPI", Version = "v1" });
        c.EnableAnnotations();
    });

    // Monta pipeline da aplicação
    var app = builder.Build();

    // Middleware global para capturar exceções não tratadas
    app.Use(async (context, next) =>
    {
        try
        {
            await next();
        }
        catch (TaskCanceledException)
        {
            Log.Debug("Requisição cancelada pelo cliente.");
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Erro não tratado capturado no middleware");
            context.Response.StatusCode = 500;
            await context.Response.WriteAsync("Erro interno do servidor");
        }
    });

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "LogiPedidosAPI v1");
        });
    }

    app.MapControllers();

    // Roda aplicação
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Erro crítico durante a inicialização da aplicação");
}
finally
{
    Log.CloseAndFlush();
}

return;

// Carrega as configurações da aplicação (appsettings.json) 
// e valida a seção MongoDbSettings.
IConfiguration LoadConfiguration()
{
    var config = new ConfigurationBuilder()
        .SetBasePath(basePath)
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .Build();

    var mongoSettings = config.GetSection("MongoDbSettings").Get<MongoDbSettings>();
    if (mongoSettings == null)
        throw new Exception("Configuração MongoDbSettings não encontrada.");

    return config;
}

void EnsureLogFolderExists()
{
    var logFolder = Path.Combine(basePath, "Logs");
    if (!Directory.Exists(logFolder))
    {
        Directory.CreateDirectory(logFolder);
    }
}
