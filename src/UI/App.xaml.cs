#region

using System;
using System.IO;
using System.Windows;
using Logic.Common;
using Logic.SnackMachines;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UI.Common;
using UI.Utils;

#endregion

namespace UI;

public partial class App : Application
{
    private readonly IConfiguration _configuration;
    private readonly IServiceProvider _provider;

    public App()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", false, false);

        _configuration = builder.Build();

        var serviceCollection = new ServiceCollection();
        ConfigureServices(serviceCollection);

        _provider = serviceCollection.BuildServiceProvider();
    }

    private void ConfigureServices(IServiceCollection services)
    {
        services.Configure<ConnectionStringsOptions>(
            _configuration.GetSection(nameof(ConnectionStringsOptions.ConnectionStrings)));
        var serverOptions = _configuration.GetSection(nameof(ConnectionStringsOptions.ConnectionStrings))
            .Get<ConnectionStringsOptions>();
        if (serverOptions is null || string.IsNullOrEmpty(serverOptions.SqlServer))
            throw new InvalidOperationException("Invalid connection string. Please check your app-settings file");

        // persistence
        services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(serverOptions?.SqlServer));

        // services
        services.AddScoped<IDialogService, DialogService>();
        services.AddScoped<SnackMachineRepository>();
        services.AddScoped<SnackRepository>();

        // views
        services.AddScoped<MainWindow>();
        services.AddScoped<CustomWindow>();

        // view models
        services.AddScoped<MainViewModel>();
    }

    private void OnStartup(object sender, StartupEventArgs e)
    {
        var mainWindow = _provider.GetService<MainWindow>();
        mainWindow?.Show();
    }
}