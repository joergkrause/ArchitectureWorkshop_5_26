using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Configuration;
using System.Data;
using System.Windows;
using Workshop.GrpcServer;
using Workshop.ServiceClients;

namespace Workshop.DesktopClient
{
  /// <summary>
  /// Interaction logic for App.xaml
  /// </summary>
  public partial class App : Application
  {
    private IHost? _host;
    public IServiceProvider Services => _host?.Services ?? throw new InvalidOperationException("Host not initialized");

    protected override async void OnStartup(StartupEventArgs e)
    {
      _host = Host
        .CreateDefaultBuilder()
        .UseContentRoot(AppContext.BaseDirectory)
        .ConfigureAppConfiguration((ctx, cfg) =>
        {
          cfg.SetBasePath(AppContext.BaseDirectory)
             .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
             .AddJsonFile($"appsettings.{ctx.HostingEnvironment.EnvironmentName}.json",
                          optional: true, reloadOnChange: true)
             .AddEnvironmentVariables();
        })
        .ConfigureServices((context, services) =>
          {



            var rest = context.Configuration["Services:RestBaseUrl"]!;
            var grpc = context.Configuration["Services:GrpcBaseUrl"]!;

            // Typed REST client (NSwag-generated RestClient takes HttpClient)
            services.AddHttpClient<RestClient>(c => c.BaseAddress = new Uri(rest));

            // gRPC client factory
            services.AddGrpcClient<CustomerService.CustomerServiceClient>(o =>
            {
              o.Address = new Uri(grpc);
            });

            // Windows / view models
            services.AddSingleton<MainWindow>();
          })
          .Build();

      await _host.StartAsync();

      _host.Services.GetRequiredService<MainWindow>().Show();
      base.OnStartup(e);
    }

    protected override async void OnExit(ExitEventArgs e)
    {
      if (_host != null)
      {
        await _host.StopAsync();
        _host.Dispose();
      }

      base.OnExit(e);
    }


  }

}
