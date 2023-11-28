using Logic.Atms;
using Logic.Management;
using Logic.SnackMachines;
using UI.Managements;
using UI.Utils;

namespace UI.Common;

public class MainViewModel : ViewModel
{
    private readonly AtmRepository _atmRepository;
    private readonly IDialogService _dialogService;
    private readonly IPaymentGateway _gateway;
    private readonly HeadOfficeRepository _headOfficeRepository;
    private readonly SnackMachineRepository _snackMachineRepository;

    public MainViewModel(AtmRepository atmRepository,
        IDialogService dialogService,
        IPaymentGateway gateway,
        HeadOfficeRepository headOfficeRepository,
        SnackMachineRepository snackMachineRepository)
    {
        _atmRepository = atmRepository;
        _dialogService = dialogService;
        _gateway = gateway;
        _snackMachineRepository = snackMachineRepository;
        _headOfficeRepository = headOfficeRepository;

        // var snackMachine = _snackMachineRepository.GetById(existingEntity);
        // var viewModel = new SnackMachineViewModel(snackMachine, snackMachineRepository);

        // const int existingEntity = 1;
        // var atm = _atmRepository.GetById(existingEntity);
        // // var viewModel = new AtmViewModel(atm, atmRepository, gateway);
        var viewModel =
            new DashboardViewModel(atmRepository, dialogService, headOfficeRepository, snackMachineRepository,
                _gateway);

        _dialogService.ShowDialog(viewModel);
    }
}