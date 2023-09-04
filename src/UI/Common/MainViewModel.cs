#region

using Logic.SnackMachines;
using UI.SnackMachines;
using UI.Utils;

#endregion

namespace UI.Common;

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