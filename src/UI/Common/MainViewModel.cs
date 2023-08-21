using Logic;
using UI.Services;

namespace UI.Common;

public class MainViewModel : ViewModel
{
    private readonly SnackMachine _snackMachine;
    private readonly IDialogService _dialogService;

    public MainViewModel(SnackMachine snackMachine, IDialogService dialogService)
    {
        _snackMachine = snackMachine;
        _dialogService = dialogService;

        var transaction = _snackMachine.MoneyInTransaction;

        // _dialogService.ShowDialog();

        /*
         * SnackMachine snackMachine;
            using (ISession session = SessionFactory.OpenSession())
            {
                snackMachine = session.Get<SnackMachine>(1L);
            }
            var viewModel = new SnackMachineViewModel(snackMachine);
            _dialogService.ShowDialog(viewModel); 
         */
    }
}