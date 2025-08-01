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
        var window = new MainWindow();
        GameData gameData = GameDataBuilder.Create()
                                           .SetPlayingFieldSize(35, 35)
                                           .CreateWallAroundPlayingField(35, 35)
                                           .AddSnake(9, 9, Direction.Right, 5)
                                           .AddFood()
                                           .Build();
        var snakeGameView = new SnakeGameView();
        var snakeGameViewModel = new SnakeGameViewModel(gameData);

        snakeGameView.DataContext = snakeGameViewModel;
        window.Content = snakeGameView;
        window.Show();
    }
}