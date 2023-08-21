using System.Windows;
using UI.ViewModels.Common;

namespace UI.Common;

public partial class CustomWindow : Window
{
    public CustomWindow(ViewModel viewModel)
    {
        InitializeComponent();

        DataContext = viewModel;
    }
}