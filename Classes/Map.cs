using System.Drawing;
using TrafficController.Interfaces;
using Color = TrafficController.Enums.Color;

namespace TrafficController.Classes;

public class Map : IUiElement
{
    private readonly char[][] _map;
    private readonly List<MapItem> _itemsToRender;

    public Map(string map, List<MapItem> itemsToRender)
    {
        _itemsToRender = itemsToRender;
        _map = map
            .Split(Environment.NewLine)
            .Select(line => line.ToCharArray()).ToArray();
    }

    public void AddItemToRender(MapItem item)
    {
        _itemsToRender.Add(item);
    }

    public void Print()
    {
        for (var y = 0; y < _map.Length - 1; y++)
        {
            Logger.LogColor(ConsoleColor.Green, $"{y:00}");
            for (var x = 0; x < _map[y].Length; x++)
            {
                var item = _itemsToRender.FirstOrDefault(i => i.X == x && i.Y == y);
                if (item != null)
                {
                    Logger.LogPlane();
                    continue;
                }

                var cell = _map[y][x];
                switch (cell)
                {
                    case 'O':
                        Logger.LogBGColor((ConsoleColor) Color.BackgroundSky, cell.ToString());
                        break;
                    case 'X':
                        Logger.LogBGColor((ConsoleColor) Color.BackgroundNotSky, cell.ToString());
                        break;
                    default:
                        Logger.LogBGColor((ConsoleColor) Color.BackgroundSky, cell.ToString());
                        break;
                }
            }

            Logger.Log(Environment.NewLine);
        }
    }

    public void UpdateItems()
    {
        _itemsToRender.ForEach(i => i.Update());
    }
}