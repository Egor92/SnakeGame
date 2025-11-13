namespace Client;

public class GameStatistics
{
    private readonly List<(int Points, int Steps)> _results = new();

    public void AddResult(int points, int steps) => _results.Add((points, steps));

    public IReadOnlyList<(int Points, int Steps)> GetAllResults() => _results.ToList();

    public (int Points, int Steps)? GetBestGame()
    {
        if (_results.Count == 0) return null;
        var best = _results[0];
        foreach (var r in _results)
        {
            if (r.Points > best.Points)
                best = r;
        }

        return best;
    }
}