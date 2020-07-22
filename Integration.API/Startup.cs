using Integration.API.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;

namespace Integration.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                    .AddJsonOptions(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore)
                    .AddJsonOptions(options => { options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore; });

            services.AddSingleton(Configuration);

            services.Configure<GzipCompressionProviderOptions>(
               o => o.Level = System.IO.Compression.CompressionLevel.Fastest);

            services.AddResponseCompression(
                o => o.Providers.Add<GzipCompressionProvider>());            

            services.AddDependencyInjectionSetup();           

            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "PDA-Integração: Qmais",
                    Description = "API de Integração com o LINX",
                    Contact = new OpenApiContact
                    {
                        Name = "PDA Soluções",
                        Email = "contato@pdasolucoes.com.br",
                        Url = new Uri("http://pdaforms.com.br/")
                    }
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseMvc();
            app.UseResponseCompression();
            app.UseHttpsRedirection();
            app.UseCors(c =>
            {
                c.AllowAnyHeader();
                c.AllowAnyMethod();
                c.AllowAnyOrigin();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "PDA Soluções: QMais API");
            });
        }
    }
}
