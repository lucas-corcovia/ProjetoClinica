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
            services.AddScoped<IAuthenticationRepository, AuthenticationRepository>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();

            
            services.AddScoped<IFisioterapeutaRepository, FisioterapeutaRepository>();
            services.AddScoped<IFisioterapeutaService, FisioterapeutaService>();
            services.AddScoped<IPacienteRepository, PacienteRepository>();
            services.AddScoped<IPacienteService, PacienteService>();
            services.AddScoped<IEnderecoRepository, EnderecoRepository>();            

            services.AddSingleton<IUrlHelperFactory, UrlHelperFactory>();
            services.AddScoped<AutenticacaoFilterAttribute>();

            return services;
        }
    }
}