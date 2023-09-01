using Logic.Persistence.Repositories;
using UI.Services;

namespace UI.ViewModels;

public class MainViewModel : ViewModel
{
    private readonly IDialogService _dialogService;
    private readonly SnackMachineRepository _repository;

    public MainViewModel(IDialogService dialogService, SnackMachineRepository repository)
    {
        _dialogService = dialogService;
        _repository = repository;

        var snackMachine = _repository.GetById(1);
        var viewModel = new SnackMachineViewModel(snackMachine, repository);

        _dialogService.ShowDialog(viewModel);
    }
}