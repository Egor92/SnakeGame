
namespace Snake
{
    public class Snake
    {
        private Queue<Pixel> _body;
        public Direction CurrentDirection;
        public Pixel _head;

        public Snake(Pixel head, int count)
        {
            _body = new Queue<Pixel>();
            _body.Enqueue(head);

            for (int i = 1; i < count; i++)
            {
                _body.Enqueue(new Pixel(head.X - i, head.Y));
            }

            CurrentDirection = Direction.Right;
            _head = head;
        }

        public Queue<Pixel> GetBody()
        {
            return _body;
        }

        public Pixel GetHead()
        {
            return _body.Peek();
        }
    }
}
