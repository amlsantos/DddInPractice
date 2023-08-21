using UI.Services;

namespace UI.ViewModels;

public class MainViewModel : ViewModel
{
    private readonly IDialogService _dialogService;
    private readonly SnackMachineViewModel _viewModel;

    public MainViewModel(IDialogService dialogService, SnackMachineViewModel viewModel)
    {
        _dialogService = dialogService;
        _viewModel = viewModel;

        _dialogService.ShowDialog(_viewModel);
    }
}