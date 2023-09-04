using UI.Common;

namespace UI.Utils;

public interface IDialogService
{
    public bool? ShowDialog(ViewModel viewModel);
}