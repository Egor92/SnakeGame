using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Snake.DesktopApp.ViewModels;

public class CellViewModel : INotifyPropertyChanged
{
    private char _symbol;
    public char Symbol
    {
        get => _symbol;

        set
        {
            if (_symbol != value)
            {
                _symbol = value;
                OnPropertyChanged(Symbol);
            }
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged(char symbol, [CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}