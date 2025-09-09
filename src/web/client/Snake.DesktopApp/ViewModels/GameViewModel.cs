using System.Windows;
using System.Windows.Threading;
using Snake.Logic;
using System.Windows.Input;
using Snake.DesktopApp.Infrastructure;
using Snake.DesktopApp.Logic;

namespace Snake.DesktopApp.ViewModels;

public class GameViewModel
{
    private readonly GameData _gameData;
    private readonly GameLogic _gameLogic;
    private readonly GameRenderer _renderer;
    private readonly DispatcherTimer _gameTimer;

    public CellViewModel[][] CellVMs { get; }
    public ICommand StartCommand { get; }

    public GameViewModel(GameData gameData, GameLogic gameLogic, int timeBetweenSteps)
    {
        _gameData = gameData;
        _gameLogic = gameLogic;

        var sceneCreator = new GameSceneCreator();
        _renderer = new GameRenderer(sceneCreator);

        int width = gameData.BoardWidth;
        int height = gameData.BoardHeight;

        CellVMs = new CellViewModel[height][];
        for (int y = 0; y < height; y++)
        {
            CellVMs[y] = new CellViewModel[width];
            for (int x = 0; x < width; x++)
            {
                CellVMs[y][x] = new CellViewModel(GameRenderImages.Wall);
            }
        }

        _renderer.DrawImages(_gameData, CellVMs);
        _gameTimer = new DispatcherTimer
        {
            Interval = TimeSpan.FromMilliseconds(timeBetweenSteps)
        };

        _gameTimer.Tick += StartMove;
        StartCommand = new Command(StartGame);
    }

    private void StartGame()
    {
        _gameTimer.Start();
    }
    private void StartMove(object? sender, EventArgs e)
    {
        if (_gameData.IsGameOver)
        {
            _gameTimer.Stop();
            MessageBox.Show("Игра закончена!");
            return;
        }

        _gameLogic.DoStep();
        _renderer.DrawImages(_gameData, CellVMs);
    }
}