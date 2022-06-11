namespace TrafficController.Map;

public static class MapLoader
{
    public static Map LoadMapFromTextFile(string path) => new(File.ReadAllText(path), new List<Aircraft.Aircraft>());
    public static Map LoadMapFromString(string data) => new(data, new List<Aircraft.Aircraft>());
}