using Azure.Identity;
using ChatApp.Domain.Models.Authentication;
using ChatApp.Hubs;
using ChatApp.Shared.DI;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ChatApp;

public class Startup
{
    private IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSignalR();

        services.AddControllers();

        services.RegisterDependencies();

        services.AddSingleton(new CosmosClient(Configuration.GetValue<string>("CosmosDB:EndpointUri"), new DefaultAzureCredential()));

        services.AddSingleton(new AuthSettings
        {
            Secret = Configuration.GetValue<string>("AuthSettings:Secret")
        });
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

        app.UseCors(builder => builder
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapHub<MessageHub>($"/{nameof(MessageHub)}", options =>
            {
                options.Transports = HttpTransportType.WebSockets;
            });

            endpoints.MapControllers();

            endpoints.MapGet("/", () => "Running!");
        });

        app.UseAuthentication();
    }
}
