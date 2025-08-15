using System.Windows.Media;
using Snake.Logic;

namespace Snake.DesktopApp.ViewModels;

public class GameRendererViewModel(GameSceneCreatorViewModel sceneCreatorViewModel)
{
    private CellObjectViewModel[,]? _previousBuffer;

    public void DrawImages(GameData gameData, CellViewModel[][] cellVMs)
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
            CellObjectViewModel.Wall => GameRenderImagesViewModel.Wall,
            CellObjectViewModel.Food => GameRenderImagesViewModel.Food,
            CellObjectViewModel.SnakeHeadLooksUp => GameRenderImagesViewModel.Snake.HeadLooksUp,
            CellObjectViewModel.SnakeHeadLooksDown => GameRenderImagesViewModel.Snake.HeadLooksDown,
            CellObjectViewModel.SnakeHeadLooksLeft => GameRenderImagesViewModel.Snake.HeadLooksLeft,
            CellObjectViewModel.SnakeHeadLooksRight => GameRenderImagesViewModel.Snake.HeadLooksRight,
            CellObjectViewModel.SnakeTailLooksUp => GameRenderImagesViewModel.Snake.TailLooksUp,
            CellObjectViewModel.SnakeTailLooksDown => GameRenderImagesViewModel.Snake.TailLooksDown,
            CellObjectViewModel.SnakeTailLooksLeft => GameRenderImagesViewModel.Snake.TailLooksLeft,
            CellObjectViewModel.SnakeTailLooksRight => GameRenderImagesViewModel.Snake.TailLooksRight,
            CellObjectViewModel.SnakeBodyHorizontal => GameRenderImagesViewModel.Snake.HorizontalBody,
            CellObjectViewModel.SnakeBodyVertical => GameRenderImagesViewModel.Snake.VerticalBody,
            CellObjectViewModel.SnakeBodyDownOrLeft => GameRenderImagesViewModel.Snake.TurnDownOrLeft,
            CellObjectViewModel.SnakeBodyDownOrRight => GameRenderImagesViewModel.Snake.TurnDownOrRight,
            CellObjectViewModel.SnakeBodyUpOrLeft => GameRenderImagesViewModel.Snake.TurnUpOrLeft,
            CellObjectViewModel.SnakeBodyUpOrRight => GameRenderImagesViewModel.Snake.TurnUpOrRight,
            _ => null
        };
    }
}