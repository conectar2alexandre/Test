using Microsoft.AspNetCore.Builder;
using src.Repository;
using Test.Interfaces.Repository;
using Test.Interfaces.Services;
using Test.Middlewares;
using Test.Repository;
using Test.Services;

namespace Test
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddScoped<IDbConnectionFactory, DbConnectionFactory>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IContextAcessor, ContextAcessor>();


            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<IUserService, UserService>();
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", $"Minha API v1 " + Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT"));
            });

            app.UseMiddleware<ExceptionHandlingMiddleware>();
            app.UseMiddleware<ConnectionManagerMiddleware>();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
