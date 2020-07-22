using Integration.Service.Interfaces;
using Integration.Service.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Integration.Infra.CrossCutting.IoC
{
    public class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IOrderService, OrderService>();
        }
    }
}
