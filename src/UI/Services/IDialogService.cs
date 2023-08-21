using UI.ViewModels;

namespace UI.Services;

public interface IDialogService
{
    public bool? ShowDialog(ViewModel viewModel);
}