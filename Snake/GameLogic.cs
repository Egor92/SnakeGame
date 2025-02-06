namespace Snake;

public class GameLogic
{
    private readonly GameData _gameData;

    public GameLogic(GameData gameData)
    {
        _gameData = gameData;
    }

    public void DoStep()
    {
        var snakeBody = _gameData.Snake.Body;
        var head = _gameData.Snake.Head;

        int newX = head.X;
        int newY = head.Y;

        switch (_gameData.Snake.Direction)
        {
            case Direction.Up:
                newY--;
                break;
            case Direction.Down:
                newY++;
                break;
            case Direction.Left:
                newX--;
                break;
            case Direction.Right:
                newX++;
                break;
        }

        var newHead = new Pixel(newX, newY);
        snakeBody.Enqueue(newHead);
        snakeBody.Dequeue();
        _gameData.Snake.Head = newHead;
    }
}