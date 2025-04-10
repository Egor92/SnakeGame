namespace Snake.DesktopApp.Views;

public class SnakeGameViewModel
{
    public CellViewModel[][] CellVMs { get; }

    public SnakeGameViewModel()
    {
        int rows = 10;
        int columns = 10;
        CellVMs = new CellViewModel[rows][];

        for (int i = 0; i < rows; i++)
        {
            CellVMs[i] = new CellViewModel[columns];
            for (int j = 0; j < columns; j++)
            {
                char element = '*';
                CellVMs[i][j] = new CellViewModel { Symbol = element };
            }
        }
    }
}