using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using XNVSU0_HFT_202231.Endpoint.Controllers;
using XNVSU0_HFT_202231.Endpoint.Services;
using XNVSU0_HFT_202231.Logic;
using XNVSU0_HFT_202231.Models.TableModels;
using XNVSU0_HFT_202231.Repository;

namespace XNVSU0_HFT_202231.Endpoint
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(o => o.AddPolicy("P", builder =>
            {
                builder
                .WithOrigins("http://localhost:4200")
                .AllowAnyMethod()
                .AllowAnyOrigin()
                .AllowAnyHeader();
            }));

            services.AddTransient<EmployeeDbContext>();

            services.AddTransient<IRepository<Job>, JobRepository>();
            services.AddTransient<IRepository<EventType>, EventTypeRepository>();
            services.AddTransient<IRepository<FixedWageEmployee>, FixedWageEmployeeRepository>();
            services.AddTransient<IRepository<HourlyWageEmployee>, HourlyWageEmployeeRepository>();
            services.AddTransient<IRepository<FixedWageOrder>, FixedWageOrderRepository>();
            services.AddTransient<IRepository<HourlyWageOrder>, HourlyWageOrderRepository>();

            services.AddTransient<IJobLogic, JobLogic>();
            services.AddTransient<IEventTypeLogic, EventTypeLogic>();
            services.AddTransient<IFixedWageEmployeeLogic, FixedWageEmployeeLogic>();
            services.AddTransient<IHourlyWageEmployeeLogic, HourlyWageEmployeeLogic>();
            services.AddTransient<IFixedWageOrderLogic, FixedWageOrderLogic>();
            services.AddTransient<IHourlyWageOrderLogic, HourlyWageOrderLogic>();

            services.AddTransient<RefreshController, RefreshController>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "XNVSU0_202231.Endpoint", Version = "v1" });
            });

            services.AddSignalR(options =>
            {
                options.EnableDetailedErrors = true;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors("P");

            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("../swagger/v1/swagger.json", "XNVSU0_202231.Endpoint v1"));
                app.UseDeveloperExceptionPage();
            }

            app.UseExceptionHandler(c => c.Run(async context =>
            {
                var exception = context.Features
                    .Get<IExceptionHandlerPathFeature>()
                    .Error;
                var response = new { msg = exception.Message };
                await context.Response.WriteAsJsonAsync(response);
            }));

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
                endpoints.MapControllers();
                endpoints.MapHub<SignalRHub>("/hub");
            });
        }
    }
}
