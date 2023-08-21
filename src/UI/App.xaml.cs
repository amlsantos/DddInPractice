using System;
using System.IO;
using Logic;
using Microsoft.Extensions.DependencyInjection;
using Persistence;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using UI.Configurations;
using UI.Services;
using UI.ViewModels;
using UI.Views;

namespace UI;

public partial class App : Application
{
    private IServiceProvider ServiceProvider { get;  }
    private IConfiguration Configuration { get;  }

    public App()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false);
        
        Configuration = builder.Build();

        var serviceCollection = new ServiceCollection();
        ConfigureServices(serviceCollection);
        
        ServiceProvider = serviceCollection.BuildServiceProvider();
    }

    private void ConfigureServices(IServiceCollection services)
    {
        services.Configure<ConnectionStringsOptions>(Configuration.GetSection(nameof(ConnectionStringsOptions.ConnectionStrings)));
        var serverOptions = Configuration.GetSection(nameof(ConnectionStringsOptions.ConnectionStrings)).Get<ConnectionStringsOptions>();
        if (serverOptions is null)
            throw new InvalidOperationException($"Invalid connection string. Please check your appsettings file");

        services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(serverOptions?.SqlServer));

        services.AddSingleton<SnackMachine>();
        services.AddScoped<MainWindow>();
        services.AddScoped<MainViewModel>();
        services.AddScoped<IDialogService, DialogService>();
    }

    private void OnStartup(object sender, StartupEventArgs e)
    {
        var mainWindow = ServiceProvider.GetService<MainWindow>();
        mainWindow.Show();
    }
}