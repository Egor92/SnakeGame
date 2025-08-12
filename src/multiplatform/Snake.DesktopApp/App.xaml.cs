using Snake.DesktopApp.View;
using Snake.DesktopApp.ViewModels;
using Snake.Logic;
using System.Windows;

namespace Snake.DesktopApp;

public partial class App
{
    protected override void OnStartup(StartupEventArgs e)
    {
        int timeBetweenSteps = 200;

        var window = new MainWindow();

        var gameData = GameDataBuilder.Create()
                                      .SetPlayingFieldSize(35, 35)
                                      .CreateWallAroundPlayingField(35, 35)
                                      .AddSnake(9, 9, Direction.Right, 5)
                                      .AddFood()
                                      .Build();

        var gameLogic = new GameLogic(gameData);
        var snakeGameView = new SnakeGameView();
        var snakeGameViewModel = new GameViewModel(gameData, gameLogic, timeBetweenSteps);

        snakeGameView.DataContext = snakeGameViewModel;
        window.Content = snakeGameView;
        window.Show();
    }
}