#region

using System.Windows;

#endregion

namespace UI.Common;

public partial class MainWindow : Window
{
    public MainWindow(MainViewModel viewModel)
    {
        InitializeComponent();

        DataContext = viewModel;
    }
}