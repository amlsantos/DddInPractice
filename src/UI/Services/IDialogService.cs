using UI.Common;
using UI.ViewModels.Common;

namespace UI.Services;

public interface IDialogService
{
    public bool? ShowDialog(ViewModel viewModel);
}