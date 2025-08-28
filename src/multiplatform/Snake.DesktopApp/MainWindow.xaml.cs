using System.Windows;
using System.Windows.Input;
using Snake.DesktopApp.ViewModels;
using Snake.Logic;

namespace Snake.DesktopApp;

public partial class MainWindow
{
    private readonly GameLogic _gameLogic;

    public MainWindow(GameLogic gameLogic)
    {
        _gameLogic = gameLogic;
        InitializeComponent();
        PreviewKeyDown += OnPreviewKeyDown;
    }

    private void OnPreviewKeyDown(object sender, KeyEventArgs e)
    {
        Direction? requestedDirection = e.Key switch
        {
            Key.Up => Direction.Up,
            Key.Down => Direction.Down,
            Key.Left => Direction.Left,
            Key.Right => Direction.Right,
            _ => null
        };

        if (requestedDirection != null)
        {
            _gameLogic.ChangeDirection(requestedDirection.Value);
        }
    }
}