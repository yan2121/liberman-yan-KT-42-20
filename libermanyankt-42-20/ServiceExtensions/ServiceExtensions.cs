using libermanyankt_42_20.Interfaces;

namespace libermanyankt_42_20.ServiceInterfaces
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IPrepodService, PrepodService>();
            services.AddScoped<IDegreesService, DegreeService>();

            return services;
        }
    }
}
