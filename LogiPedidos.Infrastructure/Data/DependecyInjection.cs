namespace LogiPedidosBackend.LogiPedidos.Infrastructure.Data
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var mongoSettings = configuration
                .GetSection("MongoDbSettings")
                .Get<MongoDbSettings>();

            if (mongoSettings == null)
                throw new ArgumentNullException(nameof(mongoSettings), "Configuração MongoDbSettings não encontrada.");

            services.AddSingleton(mongoSettings);
            services.AddSingleton<MongoDbContext>();

            return services;
        }
    }
}