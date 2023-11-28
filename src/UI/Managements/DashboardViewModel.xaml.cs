using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using DevExpress.Mvvm;
using Logic.Atms;
using Logic.Management;
using Logic.SnackMachines;
using UI.Atms;
using UI.Common;
using UI.SnackMachines;
using IDialogService = UI.Utils.IDialogService;

namespace UI.Managements;

public class DashboardViewModel : ViewModel
{
    private readonly AtmRepository _atmRepository;
    private readonly IDialogService _dialogService;
    private readonly IPaymentGateway _gateway;
    private readonly HeadOfficeRepository _headOfficeRepository;
    private readonly SnackMachineRepository _snackMachineRepository;

    public DashboardViewModel(
        AtmRepository atmRepository,
        IDialogService dialogService,
        HeadOfficeRepository headOfficeRepository,
        SnackMachineRepository snackMachineRepository, IPaymentGateway gateway)
    {
        _atmRepository = atmRepository;
        _headOfficeRepository = headOfficeRepository;
        _snackMachineRepository = snackMachineRepository;
        _gateway = gateway;
        _dialogService = dialogService;

        const int existingHeadOffice = 1;
        HeadOffice = headOfficeRepository.GetById(existingHeadOffice) ?? new HeadOffice();

        RefreshAll();

        ShowSnackMachineCommand = new DelegateCommand<SnackMachineDto>(ShowSnackMachine);
        UnloadCashCommand = new AsyncCommand<SnackMachineDto>(UnloadCash);
        ShowAtmCommand = new DelegateCommand<AtmDto>(ShowAtm);
        LoadCashToAtmCommand = new AsyncCommand<AtmDto>(LoadCashToAtm);
    }

    public HeadOffice HeadOffice { get; }
    public IReadOnlyList<SnackMachineDto> SnackMachines { get; private set; }
    public IReadOnlyList<AtmDto> Atms { get; private set; }
    public DelegateCommand<SnackMachineDto> ShowSnackMachineCommand { get; }
    public AsyncCommand<SnackMachineDto> UnloadCashCommand { get; }
    public DelegateCommand<AtmDto> ShowAtmCommand { get; }
    public AsyncCommand<AtmDto> LoadCashToAtmCommand { get; }

    private void ShowSnackMachine(SnackMachineDto? snackMachineDto)
    {
        if (snackMachineDto is null)
        {
            MessageBox.Show("Please select a Snack Machine first!!!", "Error", MessageBoxButton.OK,
                MessageBoxImage.Error);
            return;
        }

        var snackMachine = _snackMachineRepository.GetById(snackMachineDto.Id);
        if (snackMachine is null)
        {
            MessageBox.Show("Snack machine was not found", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        _dialogService.ShowDialog(new SnackMachineViewModel(snackMachine, _snackMachineRepository));
        RefreshAll();
    }

    private async Task UnloadCash(SnackMachineDto snackMachineDto)
    {
        if (!CanUnloadCash(snackMachineDto))
            return;

        var snackMachine = _snackMachineRepository.GetById(snackMachineDto.Id);
        if (snackMachine is null)
            return;

        HeadOffice.UnloadCashFromSnackMachine(snackMachine);
        await _snackMachineRepository.SaveChangesAsync();
        await _headOfficeRepository.SaveChangesAsync();

        RefreshAll();
    }

    private bool CanUnloadCash(SnackMachineDto snackMachineDto)
    {
        return snackMachineDto is { MoneyInside: > 0 };
    }

    private void ShowAtm(AtmDto? atmDto)
    {
        if (atmDto is null)
        {
            MessageBox.Show("Please select an ATM Machine first!!!", "Error", MessageBoxButton.OK,
                MessageBoxImage.Error);
            return;
        }

        var atm = _atmRepository.GetById(atmDto.Id);
        if (atm is null)
            return;

        _dialogService.ShowDialog(new AtmViewModel(atm, _atmRepository, _gateway));
        RefreshAll();
    }

    private async Task LoadCashToAtm(AtmDto atmDto)
    {
        if (!CanLoadCashToAtm(atmDto))
            return;

        var atm = _atmRepository.GetById(atmDto.Id);
        if (atm is null)
            return;

        HeadOffice.LoadCashToAtm(atm);
        await _atmRepository.SaveChangesAsync();
        await _headOfficeRepository.SaveChangesAsync();

        RefreshAll();
    }

    private bool CanLoadCashToAtm(AtmDto atmDto)
    {
        return atmDto != null && HeadOffice.Cash.Amount > 0;
    }


    private void RefreshAll()
    {
        SnackMachines = _snackMachineRepository.GetSnackMachineList();
        Atms = _atmRepository.GetAtmList();

        Notify(nameof(Atms));
        Notify(nameof(SnackMachines));
        Notify(nameof(HeadOffice));
    }
}