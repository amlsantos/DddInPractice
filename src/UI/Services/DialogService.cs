﻿using UI.ViewModels;
using UI.Views;

namespace UI.Services;

public class DialogService : IDialogService
{
    private readonly CustomWindow _window;
    
    public DialogService(CustomWindow window) => _window = window;

    public bool? ShowDialog(ViewModel viewModel)
    {
        _window.DataContext = viewModel;
        
        return _window.ShowDialog();
    }
}