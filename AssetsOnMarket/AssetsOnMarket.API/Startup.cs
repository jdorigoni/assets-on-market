using AssetsOnMarket.Api.Configurations;
using AssetsOnMarket.Infrastructure.Data.Context;
using AssetsOnMarket.Infrastructure.IoC;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AssetsOnMarket.Api
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
            services.AddDbContext<AssetsOnMarketDBContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("AssetsOnMarketDBConnection"));
                //options.UseSqlServer(Configuration.GetConnectionString("AssetsOnMarketDBConnection")
                //    .Replace("{{DB_ENDPOINT}}", Configuration.GetValue<string>("DB_ENDPOINT")));
            });
                    
            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "Assets available on Market API",
                    Version = "v1"
                });
            });

            services.AddMediatR(typeof(Startup));

            services.RegisterAutoMapper();

            RegisterServices(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // UpdateDatabase(app);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Assets available on Market API v1");
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private static void RegisterServices(IServiceCollection services)
        {
            DependencyContainer.RegisterServices(services);
        }


        // Run Database migrations on the dockerized database
        public void UpdateDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                using (var _context = serviceScope.ServiceProvider.GetService<AssetsOnMarketDBContext>())
                {

                    if (Configuration.GetValue<bool>("DB_MIGRATE") == true)
                        _context.Database.Migrate();

                }
            }
        }

    }
}
