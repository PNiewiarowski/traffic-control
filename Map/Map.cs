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

    public void AddItemToRender(Aircraft item) =>
        _aircraft.Add(item);

    public int CountAllItems() =>
        _aircraft.Count;

    public void PrintAllAircraft()
    {
        foreach (var item in _aircraft)
        {
            var warning = item.Warned ? "!WARNING!" : "";
            Logger.Log($"{item.Uuid} : [ {item.GetPath()} ] {warning} {Environment.NewLine}");
        }
    }

    private bool CheckCollision(int x, int y)
    {
        try
        {
            var collision = _map[y][x] == 'X' || _aircraft.Where(item => item.X == x && item.Y == y).ToList().Count > 1;
            return collision;
        }
        catch (IndexOutOfRangeException)
        {
            return true;
        }
    }

    private void Collision(Aircraft item)
    {
        if (CheckCollision(item.X, item.Y))
            DeleteItemByPosition(item.X, item.Y);
    }

    private bool CheckWarning(int x, int y) =>
        CheckCollision(x + 1, y) ||
        CheckCollision(x - 1, y) ||
        CheckCollision(x, y - 1) ||
        CheckCollision(x, y + 1) ||
        CheckCollision(x - 1, y - 1) ||
        CheckCollision(x - 1, y + 1) ||
        CheckCollision(x + 1, y - 1) ||
        CheckCollision(x + 1, y + 1);

    public void Print()
    {
        for (var y = 0; y < _map.Length - 1; y++)
        {
            Logger.LogGreen($"{y:00}");
            for (var x = 0; x < _map[y].Length; x++)
            {
                var cell = _map[y][x];
                var item = _aircraft.FirstOrDefault(i => i.X == x && i.Y == y);

                if (item != null && cell != 'X')
                {
                    if (CheckWarning(x, y))
                    {
                        Logger.LogMapItemWarning(item);
                        item.Warned = true;
                        continue;
                    }

                    Logger.LogMapItem(item);
                    item.Warned = false;
                    continue;
                }

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

    public void UpdateItems() =>
        _aircraft.ForEach(item =>
        {
            item.Update();
            Collision(item);
        });

    public void UpdateItemPath(string? uuid, Queue<char> path) =>
        _aircraft.First(aircraft => aircraft.Uuid == uuid).ChangePath(path);

    public void DeleteItemByUuid(string? uuid) =>
        _aircraft = _aircraft.Where(item => item.Uuid != uuid).ToList();

    public void DeleteItemByPosition(int x, int y) =>
        _aircraft = _aircraft.Where(item => item.X != x && item.Y != y).ToList();
}