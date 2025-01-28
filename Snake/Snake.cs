
namespace Snake
{
    public class Snake
    {
        public Queue<Pixel> Body;
        public Direction Direction;
        public Pixel Head;

        public Snake(Queue<Pixel> body, Direction direction, Pixel head)
        {
            Body = body;
            Direction = direction;
            Head = head;
        }
    }
}
