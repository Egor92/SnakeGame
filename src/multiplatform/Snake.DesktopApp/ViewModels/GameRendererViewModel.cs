using Snake.Logic;

namespace Snake.DesktopApp.ViewModels;

public class GameRendererViewModel(GameSceneCreatorViewModel sceneCreatorViewModel)
{
    private CellObjectViewModel[,]? _previousBuffer;

    public void DrawSymbols(GameData gameData, CellViewModel[][] cellVMs)
    {
        var currentBuffer = sceneCreatorViewModel.GetSceneCellObjects(gameData);

        int height = gameData.BoardHeight;
        int width = gameData.BoardWidth;

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (_previousBuffer == null || currentBuffer[x, y] != _previousBuffer[x, y])
                {
                    char symbol = GetSymbol(currentBuffer[x, y]);
                    cellVMs[y][x].Symbol = symbol;
                }
            }
        }

        _previousBuffer = currentBuffer;
    }

    private char GetSymbol(CellObjectViewModel cellObject)
    {
        return cellObject switch
        {
            CellObjectViewModel.Empty => ' ',
            CellObjectViewModel.Wall => GameRenderSymbolsViewModel.Wall,
            CellObjectViewModel.Food => GameRenderSymbolsViewModel.Food,
            CellObjectViewModel.SnakeHeadLooksUp => GameRenderSymbolsViewModel.Snake.HeadLooksUp,
            CellObjectViewModel.SnakeHeadLooksDown => GameRenderSymbolsViewModel.Snake.HeadLooksDown,
            CellObjectViewModel.SnakeHeadLooksLeft => GameRenderSymbolsViewModel.Snake.HeadLooksLeft,
            CellObjectViewModel.SnakeHeadLooksRight => GameRenderSymbolsViewModel.Snake.HeadLooksRight,
            CellObjectViewModel.SnakeTailLooksUp => GameRenderSymbolsViewModel.Snake.TailLooksUp,
            CellObjectViewModel.SnakeTailLooksDown => GameRenderSymbolsViewModel.Snake.TailLooksDown,
            CellObjectViewModel.SnakeTailLooksLeft => GameRenderSymbolsViewModel.Snake.TailLooksLeft,
            CellObjectViewModel.SnakeTailLooksRight => GameRenderSymbolsViewModel.Snake.TailLooksRight,
            CellObjectViewModel.SnakeBodyHorizontal => GameRenderSymbolsViewModel.Snake.HorizontalBody,
            CellObjectViewModel.SnakeBodyVertical => GameRenderSymbolsViewModel.Snake.VerticalBody,
            CellObjectViewModel.SnakeBodyDownOrLeft => GameRenderSymbolsViewModel.Snake.TurnDownOrLeft,
            CellObjectViewModel.SnakeBodyDownOrRight => GameRenderSymbolsViewModel.Snake.TurnDownOrRight,
            CellObjectViewModel.SnakeBodyUpOrLeft => GameRenderSymbolsViewModel.Snake.TurnUpOrLeft,
            CellObjectViewModel.SnakeBodyUpOrRight => GameRenderSymbolsViewModel.Snake.TurnUpOrRight,
            _ => ' '
        };
    }
}