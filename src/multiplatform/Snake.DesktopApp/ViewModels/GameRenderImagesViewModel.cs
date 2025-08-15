using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Snake.DesktopApp.ViewModels;

public static class GameRenderImagesViewModel
{
    public static readonly ImageSource Food = new BitmapImage(new Uri("/Image/other/apple.png", UriKind.Relative));
    public static readonly ImageSource Wall = new BitmapImage(new Uri("/Image/other/Wall.png", UriKind.Relative));
    public static readonly ImageSource Empty = new BitmapImage(new Uri("/Image/other/Empty.png", UriKind.Relative));

    public static class Snake
    {
        public static readonly ImageSource HeadLooksUp = new BitmapImage(new Uri("/Image/head/head_up.png", UriKind.Relative));
        public static readonly ImageSource HeadLooksDown = new BitmapImage(new Uri("/Image/head/head_down.png", UriKind.Relative));
        public static readonly ImageSource HeadLooksLeft = new BitmapImage(new Uri("/Image/head/head_left.png", UriKind.Relative));
        public static readonly ImageSource HeadLooksRight = new BitmapImage(new Uri("/Image/head/head_right.png", UriKind.Relative));

        public static readonly ImageSource TailLooksUp = new BitmapImage(new Uri("/Image/tail/tail_up.png", UriKind.Relative));
        public static readonly ImageSource TailLooksDown = new BitmapImage(new Uri("/Image/tail/tail_down.png", UriKind.Relative));
        public static readonly ImageSource TailLooksLeft = new BitmapImage(new Uri("/Image/tail/tail_left.png", UriKind.Relative));
        public static readonly ImageSource TailLooksRight = new BitmapImage(new Uri("/Image/tail/tail_right.png", UriKind.Relative));

        public static readonly ImageSource HorizontalBody = new BitmapImage(new Uri("/Image/body/body_horizontal.png", UriKind.Relative));
        public static readonly ImageSource VerticalBody = new BitmapImage(new Uri("/Image/body/body_vertical.png", UriKind.Relative));
        public static readonly ImageSource TurnDownOrLeft = new BitmapImage(new Uri("/Image/turn/body_bottomleft.png", UriKind.Relative));
        public static readonly ImageSource TurnDownOrRight = new BitmapImage(new Uri("/Image/turn/body_bottomright.png", UriKind.Relative));
        public static readonly ImageSource TurnUpOrLeft = new BitmapImage(new Uri("/Image/turn/body_topleft.png", UriKind.Relative));
        public static readonly ImageSource TurnUpOrRight = new BitmapImage(new Uri("/Image/turn/body_topright.png", UriKind.Relative));
    }
}