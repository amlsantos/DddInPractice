using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace UI.ViewModels;

public abstract class ViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    private bool? _dialogResult;

    public bool? DialogResult
    {
        get => _dialogResult;
        protected set
        {
            _dialogResult = value;
            Notify();
        }
    }

    public virtual string Caption => string.Empty;

    protected void Notify([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}