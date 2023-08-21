using Logic;
using UI.Services;
using UI.ViewModels.Common;

namespace UI.ViewModels;

public class MainViewModel : ViewModel
{
    private readonly SnackMachine _snackMachine;
    private readonly IDialogService _dialogService;

    public MainViewModel(SnackMachine snackMachine, IDialogService dialogService)
    {
        _snackMachine = snackMachine;
        _dialogService = dialogService;

        var viewModel = new SnackMachineViewModel(_snackMachine);
        _dialogService.ShowDialog(viewModel);
    }
}