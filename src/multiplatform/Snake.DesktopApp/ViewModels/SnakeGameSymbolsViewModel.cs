namespace Snake.DesktopApp.ViewModels;

public class SnakeGameSymbolsViewModel
{
    public const char Food = 'ó';
    public const char Wall = '#';

    public static class Snake
    {
        public const char HeadLooksUp = (char)708;
        public const char HeadLooksDown = (char)709;
        public const char HeadLooksLeft = (char)706;
        public const char HeadLooksRight = (char)707;

        public const char TailLooksUp = '\u2191';
        public const char TailLooksDown = '\u2193';
        public const char TailLooksLeft = '\u2190';
        public const char TailLooksRight = '\u2192';

        public const char HorizontalBody = '-';
        public const char VerticalBody = '|';
        public const char TurnDownOrLeft = '┐';
        public const char TurnDownOrRight = '┌';
        public const char TurnUpOrLeft = '┘';
        public const char TurnUpOrRight = '└';
    }
}