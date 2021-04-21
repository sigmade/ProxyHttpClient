using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace ProxyHttpClient
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

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ProxyHttpClient", Version = "v1" });
            });
            services.AddHttpClient<ProxysHttpClient>().
                ConfigurePrimaryHttpMessageHandler((c => new HttpClientHandler()
                {
                    Proxy = new WebProxy(Configuration["ProxyOptions:Address"])
                    {
                        Credentials = new NetworkCredential { UserName = Configuration["ProxyOptions:Username"], Password = Configuration["ProxyOptions:Password"] }
                    }
                }));
            services.AddHttpClient<CurrentHttpClient>();
            services.AddHttpClient<ProxysHttpClient>().
                ConfigurePrimaryHttpMessageHandler((c => new HttpClientHandler()
                {
                    Proxy = new WebProxy(Configuration["ProxyOptions:Address"])
                    {
                        Credentials = new NetworkCredential { UserName = Configuration["ProxyOptions:Username"], Password = Configuration["ProxyOptions:Password"] }
                    }
                }));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ProxyHttpClient v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
