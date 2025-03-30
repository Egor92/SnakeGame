namespace Snake;

public class Snake
{
    public Queue<Pixel> Body { get; set; }

    public Direction LastStepDirection { get; set; }

    public Direction? RequestedDirection { get; set; }

    public Direction NextStepDirection => RequestedDirection ?? LastStepDirection;

    public Pixel Head { get; set; }

    public Snake(Queue<Pixel> body, Direction direction, Pixel head)
    {
        Body = body;
        LastStepDirection = direction;
        Head = head;
    }
}