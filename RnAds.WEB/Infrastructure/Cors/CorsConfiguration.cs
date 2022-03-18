using Microsoft.Extensions.DependencyInjection;

namespace RnAds.WEB.Infrastructure.Cors
{
    /// <summary>
    /// Конфигурация для настройки CORS
    /// </summary>
    public static class CorsConfiguration
    {
        public const string CorsPolicy = "RnCorsPolicy";

        /// <summary>
        /// Метод расширения для конфигурации CORS
        /// </summary>
        /// <param name="serviceCollection">Коллекция сервисов</param>
        public static void ConfigureCors(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddCors(x =>
            {
                x.AddPolicy(CorsPolicy, builder =>
                {
                    builder.WithOrigins("http://localhost:8080").AllowAnyMethod().AllowAnyHeader().AllowCredentials();
                });
            });

        }
    }
}
