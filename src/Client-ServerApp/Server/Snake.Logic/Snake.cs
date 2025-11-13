namespace client_serverApp.Snake.Logic;

public class Snake
{
    public Queue<Cell> Body { get; set; }

    public Direction LastStepDirection { get; set; }

    public Direction? RequestedDirection { get; set; }

    public Direction NextStepDirection => RequestedDirection ?? LastStepDirection;

    public Cell Head { get; set; }

    public Snake(Queue<Cell> body, Direction direction, Cell head)
    {
        Body = body;
        LastStepDirection = direction;
        Head = head;
    }
}