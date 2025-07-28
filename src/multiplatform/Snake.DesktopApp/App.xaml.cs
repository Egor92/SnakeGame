using System.Windows;
using Snake.DesktopApp.View;
using Snake.DesktopApp.ViewModels;
using Snake.Logic;

namespace Snake.DesktopApp;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        GameData gameData = GameDataBuilder.Create()
                                           .SetPlayingFieldSize(15, 45)
                                           .CreateWallAroundPlayingField(15, 45)
                                           .AddSnake(5, 5, Direction.Right, 3)
                                           .AddFood()
                                           .Build();
        var mainWindow = new MainWindow();
        // Создать Game и запустить
        var mainWindow = new MainWindow();
        var snakeGameView = new SnakeGameView();
        var snakeGameViewModel = new SnakeGameViewModel(gameData);

        snakeGameView.DataContext = snakeGameViewModel;
        mainWindow.Content = snakeGameView;
        mainWindow.Show();
    }
}