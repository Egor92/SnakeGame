using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media;

namespace Snake.DesktopApp.ViewModels;

public class CellViewModel(ImageSource image) : INotifyPropertyChanged
{
    private ImageSource? _image = image;

    public ImageSource? Image
    {
        get => _image;

        set
        {
            if (_image != value)
            {
                _image = value;
                OnPropertyChanged();
            }
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}