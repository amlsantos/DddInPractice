using UI.Common;

namespace UI.Services;

public class DialogService : IDialogService
{
    public bool? ShowDialog(ViewModel viewModel)
    {
        //CustomWindow window = new CustomWindow(viewModel);
        //return window.ShowDialog();

        return true;
    }
}