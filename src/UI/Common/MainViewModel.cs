#region

using Logic.Atms;
using Logic.SnackMachines;
using UI.Atms;
using UI.Utils;

#endregion

namespace UI.Common;

public class MainViewModel : ViewModel
{
    private readonly AtmRepository _atmRepository;
    private readonly IDialogService _dialogService;
    private readonly IPaymentGateway _gateway;
    private readonly SnackMachineRepository _snackMachineRepository;

    public MainViewModel(AtmRepository atmRepository,
        IDialogService dialogService,
        IPaymentGateway gateway,
        SnackMachineRepository snackMachineRepository)
    {
        _atmRepository = atmRepository;
        _dialogService = dialogService;
        _gateway = gateway;
        _snackMachineRepository = snackMachineRepository;

        // var snackMachine = _snackMachineRepository.GetById(existingEntity);
        // var viewModel = new SnackMachineViewModel(snackMachine, snackMachineRepository);

        const int existingEntity = 1;
        var atm = _atmRepository.GetById(existingEntity);
        var viewModel = new AtmViewModel(atm, atmRepository, gateway);

        _dialogService.ShowDialog(viewModel);
    }
}