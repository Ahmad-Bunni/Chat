
using ChatApp.Data.Repository;
using ChatApp.Domain.Interface;
using ChatApp.Domain.Serivce;
using ChatApp.Hubs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ChatApp;

public class Startup
{
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSignalR();

        services.AddControllers();

        services.AddSingleton(typeof(ICosmosRepository<>), typeof(CosmosRepository<>));

        services.AddScoped(typeof(IUserService), typeof(UserService));

        var authSettingsSection = Configuration.GetSection("AuthSettings");
    }

    public void Configure(IApplicationBuilder app, IHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }

        app.UseRouting();

        app.UseAuthorization();

        app.UseHttpsRedirection();

        app.UseStaticFiles();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapHub<MessageHub>($"/{nameof(MessageHub)}", options =>
            {
                options.Transports = HttpTransportType.WebSockets;
            });
        });

        app.UseAuthentication();

        app.UseCors(builder => builder.AllowAnyOrigin()
                              .AllowAnyMethod()
                              .AllowAnyHeader());

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
