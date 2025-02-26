namespace Snake;

public class Snake
{
    public Queue<Pixel> Body { get; set; }

    public Direction Direction { get; set; }

    public Pixel Head { get; set; }

    public Snake(Queue<Pixel> body, Direction direction, Pixel head)
    {
        Body = body;
        Direction = direction;
        Head = head;
    }
}