using System.Windows;
using System.Windows.Threading;
using Snake.Logic;

namespace Snake.DesktopApp.ViewModels;

public class GameViewModel
{
    private readonly GameData _gameData;
    private readonly GameLogic _gameLogic;
    private readonly GameRendererViewModel _renderer;
    private readonly DispatcherTimer _gameTimer;

    public CellViewModel[][] CellVMs { get; }

    public GameViewModel(GameData gameData, GameLogic gameLogic, int timeBetweenSteps)
    {
        _gameData = gameData;
        _gameLogic = gameLogic;

        var sceneCreator = new GameSceneCreatorViewModel();
        _renderer = new GameRendererViewModel(sceneCreator);

        int width = gameData.BoardWidth;
        int height = gameData.BoardHeight;

        CellVMs = new CellViewModel[height][];
        for (int y = 0; y < height; y++)
        {
            CellVMs[y] = new CellViewModel[width];
            for (int x = 0; x < width; x++)
            {
                CellVMs[y][x] = new CellViewModel { Symbol = ' ' };
            }
        }

        _renderer.DrawSymbols(_gameData, CellVMs);

        _gameTimer = new DispatcherTimer
        {
            Interval = TimeSpan.FromMilliseconds(timeBetweenSteps)
        };
        _gameTimer.Tick += Start;
        _gameTimer.Start();
    }

    private void Start(object? sender, EventArgs e)
    {
        if (_gameData.IsGameOver)
        {
            _gameTimer.Stop();
            MessageBox.Show("Игра закончена!");
            return;
        }

        _gameLogic.DoStep();
        _renderer.DrawSymbols(_gameData, CellVMs);
    }
}