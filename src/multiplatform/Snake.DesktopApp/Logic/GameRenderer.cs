using System.Windows.Media;
using Snake.DesktopApp.ViewModels;
using Snake.Logic;

namespace Snake.DesktopApp.Logic;

public class GameRenderer(GameSceneCreator sceneCreator)
{
    private CellObjectViewModel[,]? _previousBuffer;

    public void DrawImages(GameData gameData, CellViewModel[][] cellVMs)
    {
        var currentBuffer = sceneCreator.GetSceneCellObjects(gameData);

        int height = gameData.BoardHeight;
        int width = gameData.BoardWidth;

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (_previousBuffer == null || currentBuffer[x, y] != _previousBuffer[x, y])
                {
                    ImageSource? symbol = GetImage(currentBuffer[x, y]);
                    cellVMs[y][x].Image = symbol;
                }
            }
        }

        _previousBuffer = currentBuffer;
    }

    private ImageSource? GetImage(CellObjectViewModel cellObject)
    {
        return cellObject switch
        {
            CellObjectViewModel.Empty => null,
            CellObjectViewModel.Wall => GameRenderImages.Wall,
            CellObjectViewModel.Food => GameRenderImages.Food,
            CellObjectViewModel.SnakeHeadLooksUp => GameRenderImages.Snake.HeadLooksUp,
            CellObjectViewModel.SnakeHeadLooksDown => GameRenderImages.Snake.HeadLooksDown,
            CellObjectViewModel.SnakeHeadLooksLeft => GameRenderImages.Snake.HeadLooksLeft,
            CellObjectViewModel.SnakeHeadLooksRight => GameRenderImages.Snake.HeadLooksRight,
            CellObjectViewModel.SnakeTailLooksUp => GameRenderImages.Snake.TailLooksUp,
            CellObjectViewModel.SnakeTailLooksDown => GameRenderImages.Snake.TailLooksDown,
            CellObjectViewModel.SnakeTailLooksLeft => GameRenderImages.Snake.TailLooksLeft,
            CellObjectViewModel.SnakeTailLooksRight => GameRenderImages.Snake.TailLooksRight,
            CellObjectViewModel.SnakeBodyHorizontal => GameRenderImages.Snake.HorizontalBody,
            CellObjectViewModel.SnakeBodyVertical => GameRenderImages.Snake.VerticalBody,
            CellObjectViewModel.SnakeBodyDownOrLeft => GameRenderImages.Snake.TurnDownOrLeft,
            CellObjectViewModel.SnakeBodyDownOrRight => GameRenderImages.Snake.TurnDownOrRight,
            CellObjectViewModel.SnakeBodyUpOrLeft => GameRenderImages.Snake.TurnUpOrLeft,
            CellObjectViewModel.SnakeBodyUpOrRight => GameRenderImages.Snake.TurnUpOrRight,
            _ => null
        };
    }
}