#region

using System.ComponentModel;
using System.Runtime.CompilerServices;

#endregion

namespace UI.Common;

public abstract class ViewModel : INotifyPropertyChanged
{
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
    public event PropertyChangedEventHandler? PropertyChanged;

    protected void Notify([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}