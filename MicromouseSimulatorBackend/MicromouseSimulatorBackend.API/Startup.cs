using MicromouseSimulatorBackend.BLL.RepositoryInterfaces;
using MicromouseSimulatorBackend.BLL.ServiceInterfaces;
using MicromouseSimulatorBackend.BLL.Services;
using MicromouseSimulatorBackend.DATA.Config;
using MicromouseSimulatorBackend.DATA.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

namespace MicromouseSimulatorBackend.API
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
            // DB settings
            services.Configure<MicromouseDatabaseSettings>(
                Configuration.GetSection(nameof(MicromouseDatabaseSettings)));

            services.AddSingleton<IMicromouseDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<MicromouseDatabaseSettings>>().Value);

            // DB repository
            services.AddSingleton(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddSingleton<ISimulationRepository, SimulationRepository>();

            // Services
            services.AddScoped<IAlgorithmService, AlgorithmService>();
            services.AddScoped<IMazeService, MazeService>();
            services.AddScoped<IMouseService, MouseService>();
            services.AddScoped<ISimulationService, SimulationService>();
            services.AddScoped<ISimulationFileService, SimulationFileService>();
            services.AddScoped<ISimulationDockerService, SimulationDockerService>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MicromouseSimulatorBackend.API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MicromouseSimulatorBackend.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
