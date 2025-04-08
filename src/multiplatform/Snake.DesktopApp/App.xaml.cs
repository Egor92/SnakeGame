using System.Configuration;
using System.Data;
using System.Windows;
using Snake.DesktopApp.View;
using Snake.DesktopApp.Views;

namespace Snake.DesktopApp;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        var window = new MainWindow();
        var snakeGameView = new SnakeGameView();
        var snakeGameViewModel = new SnakeGameViewModel();

        snakeGameView.DataContext = snakeGameViewModel;
        window.Content = snakeGameView;
        window.Show();
    }
}