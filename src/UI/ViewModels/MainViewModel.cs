using System.Linq;
using Logic.Persistence;
using UI.Services;

namespace UI.ViewModels;

public class MainViewModel : ViewModel
{
    private readonly IDialogService _dialogService;
    private readonly ApplicationDbContext _context;

    public MainViewModel(IDialogService dialogService, ApplicationDbContext context)
    {
        _dialogService = dialogService;
        _context = context;

        var snackMachine = _context.SnackMachines.FirstOrDefault();
        var viewModel = new SnackMachineViewModel(snackMachine, context);

        _dialogService.ShowDialog(viewModel);
    }
}