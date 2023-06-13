using Microsoft.AspNetCore.Mvc.Routing;
using ProjetoClinica.App.Extensions;
using ProjetoClinica.Business.Contracts;
using ProjetoClinica.Business.Services;
using ProjetoClinica.Data.Repository;

namespace ProjetoClinica.App.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddTransient<IAuthenticationRepository, AuthenticationRepository>();
            services.AddTransient<IAuthenticationService, AuthenticationService>();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddTransient<IFisioterapeutaRepository, FisioterapeutaRepository>();
            services.AddTransient<IFisioterapeutaService, FisioterapeutaService>();
            services.AddTransient<IPacienteRepository, PacienteRepository>();
            services.AddTransient<IPacienteService, PacienteService>();

            services.AddSingleton<IUrlHelperFactory, UrlHelperFactory>();
            services.AddScoped<AutenticacaoFilterAttribute>();

            return services;
        }
    }
}