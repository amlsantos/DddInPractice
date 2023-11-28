using UI.Common;

namespace UI.Utils;

public class DialogService : IDialogService
{
    public bool? ShowDialog(ViewModel viewModel)
    {
        var window = new CustomWindow(viewModel);
        return window.ShowDialog();
    }
}