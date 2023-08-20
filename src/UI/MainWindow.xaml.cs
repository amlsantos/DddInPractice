using Logic;
using Persistence;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace UI;

public partial class MainWindow : Window
{
    private readonly ApplicationDbContext _applicationDbContext;

    public MainWindow(ApplicationDbContext applicationDbContext)
    {
        InitializeComponent();

        _applicationDbContext = applicationDbContext;

        var xx = GetSnackMachines();

        //DataContext = new MainViewModel();
    }

    public IEnumerable<Snack> GetSnackMachines()
    {
        return _applicationDbContext.Snacks.ToList();
    }
}