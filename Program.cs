using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using FHG.AccessControl;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureServices(services => {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();
        services.AddDbContext<ApplicationDbContext>(options => 
            options.UseSqlServer(Environment.GetEnvironmentVariable("ConnectionStrings:DefaultConnection"))
            .EnableDetailedErrors(true));
    })
    .Build();
host.Run();
