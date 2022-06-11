using TrafficController.Plane;

namespace TrafficController.Map;

public static class MapLoader
{
    public static Map LoadMapFromTextFile(string path) => new(File.ReadAllText(path), new List<Aircraft>());
    public static Map LoadMapFromString(string data) => new(data, new List<Aircraft>());
}