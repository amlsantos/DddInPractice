using System.Windows;

namespace UI.Common;

public partial class CustomWindow : Window
{
    public CustomWindow(ViewModel model)
    {
        InitializeComponent();

        DataContext = model;
    }
}