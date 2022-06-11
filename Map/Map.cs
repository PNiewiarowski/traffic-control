using TrafficController.Plane;
using TrafficController.Utils;
using TrafficController.Ui;

namespace TrafficController.Map;

public class Map
{
    private readonly char[][] _map;
    private List<Aircraft> _aircraft;

    public Map(string map, List<Aircraft> aircraft)
    {
        _aircraft = aircraft;
        _map = map
            .Split(Environment.NewLine)
            .Select(line => line.ToCharArray()).ToArray();
    }

    public void AddItemToRender(Aircraft item) => _aircraft.Add(item);

    public int CountAllItems() => _aircraft.Count;

    public void PrintAllAircraft()
    {
        foreach (var item in _aircraft)
            Logger.Log($"{item.Uuid}{Environment.NewLine}");
    }

    public void Print()
    {
        for (var y = 0; y < _map.Length - 1; y++)
        {
            Logger.LogGreen($"{y:00}");
            for (var x = 0; x < _map[y].Length; x++)
            {
                var item = _aircraft.FirstOrDefault(i => i.X == x && i.Y == y);
                if (item != null)
                {
                    Logger.LogMapItem(item);
                    continue;
                }

                var cell = _map[y][x];
                switch (cell)
                {
                    case 'O':
                        Logger.Log((ConsoleColor) Color.BackgroundSky, cell.ToString());
                        break;
                    case 'X':
                        Logger.Log((ConsoleColor) Color.BackgroundObstacle, cell.ToString());
                        break;
                    default:
                        Logger.Log((ConsoleColor) Color.BackgroundSky, cell.ToString());
                        break;
                }
            }

            Logger.Log(Environment.NewLine);
        }
    }

    public void UpdateItems() => _aircraft.ForEach(i => i.Update());

    public void UpdateItemPath(string? uuid, Queue<char> path) =>
        _aircraft.First(aircraft => aircraft.Uuid == uuid).ChangePath(path);

    public void DeleteItemByUuid(string? uuid) => _aircraft = _aircraft.Where(item => item.Uuid != uuid).ToList();
}