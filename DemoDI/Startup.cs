﻿using System;
using DemoDI.Cases;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DemoDI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            #region Lifecycle

            services.AddTransient<IOperacaoTransient, Operacao>();  // Abre uma instancia de um objeto a cada chamada.
            services.AddScoped<IOperacaoScoped, Operacao>();        // Abre apenas uma instancia do objeto até o final da requisição, finalizou a requisição, na proxima irá criar uma nova instancia. - Mais utilizado para aplicações web.
            services.AddSingleton<IOperacaoSingleton, Operacao>();  // Abre apenas uma instancia do objeto até finalizar a aplicação, usa a mesma instancia de um objeto para todos as requisições - Tomar cuidado ao usar.
            services.AddSingleton<IOperacaoSingletonInstance>(new Operacao(Guid.Empty));
            services.AddTransient<OperacaoService>();

            #endregion

            #region VidaReal

            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<IClienteServices, ClienteServices>();

            #endregion

            #region Generics

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));  // Todas vez que registrar um repositório genérico, é necessário usar o "typeof".

            #endregion

            #region MultiplasClasses

            services.AddTransient<ServiceA>();
            services.AddTransient<ServiceB>();
            services.AddTransient<ServiceC>();
            services.AddTransient<Func<string, IService>>(serviceProvider => key =>
            {
                switch (key)
                {
                    case "A":
                        return serviceProvider.GetService<ServiceA>();
                    case "B":
                        return serviceProvider.GetService<ServiceB>();
                    case "C":
                        return serviceProvider.GetService<ServiceC>();
                    default:
                        return null;
                }
            });

            #endregion

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseStaticFiles();
            
            app.UseMvc(routes =>
            {
                routes.MapRoute(name: "default", "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
