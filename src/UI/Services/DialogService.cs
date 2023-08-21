using UI.Common;
using UI.ViewModels.Common;

namespace UI.Services;

public class DialogService : IDialogService
{
    public bool? ShowDialog(ViewModel viewModel)
    {
        var window = new CustomWindow(viewModel);
        return window.ShowDialog();
    }
}