using Logic;
using Microsoft.Extensions.DependencyInjection;
using Persistence;
using System.Windows;

namespace UI;

public partial class App : Application
{
    private readonly ServiceProvider _serviceProvider;

    public App()
    {
        var services = new ServiceCollection();
        ConfigureServices(services);
        _serviceProvider = services.BuildServiceProvider();
    }

    private void ConfigureServices(ServiceCollection services)
    {
        services.AddDbContext<ApplicationDbContext>();
        services.AddSingleton<SnackMachine>();
        services.AddSingleton<MainWindow>();
    }

    private void OnStartup(object sender, StartupEventArgs e)
    {
        var mainWindow = _serviceProvider.GetService<MainWindow>();
        mainWindow.Show();
    }
}