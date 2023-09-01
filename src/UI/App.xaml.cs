using System;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using Logic.Persistence;
using Logic.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using UI.Configurations;
using UI.Services;
using UI.ViewModels;
using UI.Views;

namespace UI;

public partial class App : Application
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IConfiguration _configuration;

    public App()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false);
        
        _configuration = builder.Build();

        var serviceCollection = new ServiceCollection();
        ConfigureServices(serviceCollection);
        
        _serviceProvider = serviceCollection.BuildServiceProvider();
    }

    private void ConfigureServices(IServiceCollection services)
    {
        services.Configure<ConnectionStringsOptions>(_configuration.GetSection(nameof(ConnectionStringsOptions.ConnectionStrings)));
        var serverOptions = _configuration.GetSection(nameof(ConnectionStringsOptions.ConnectionStrings)).Get<ConnectionStringsOptions>();
        if (serverOptions is null || string.IsNullOrEmpty(serverOptions.SqlServer))
            throw new InvalidOperationException($"Invalid connection string. Please check your app-settings file");

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
        var mainWindow = _serviceProvider.GetService<MainWindow>();
        mainWindow?.Show();
    }
}