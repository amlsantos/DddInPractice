using UI.Common;

namespace UI.Services;

public interface IDialogService
{
    public bool? ShowDialog(ViewModel viewModel);
}