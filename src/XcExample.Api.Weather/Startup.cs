using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XcExample.Api.Weather
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
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "XcExample.Api.Weather", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseSwagger(options =>
                {
                    options.SerializeAsV2 = true;
                });
                app.UseSwaggerUI(options =>
                {
                    options.RoutePrefix = "Swagger";
                    options.SwaggerEndpoint("v1/swagger.json", "XcExample.Api.Weather v1");
                });
            }

            app.UseRouting();

            app.UseAuthorization();

            _ = app.UseEndpoints(endpoints =>
              {
                  endpoints.MapGet("/", async context => await context.Response.WriteAsync("ALIVE!"));
                  var redisConnectionString = Configuration["REDIS_HOST"];
                  endpoints.MapGet("/redis_health", async context =>
                  {
                      await (new Redis(redisConnectionString)).PingAsync();
                      await context.Response.WriteAsync("Redis ALIVE!");
                  });
                  endpoints.MapControllers();
              });
        }
    }
}
