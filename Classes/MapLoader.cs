namespace TrafficController.Classes;

public static class MapLoader
{
    public static Map LoadMapFromTextFile(string path) => new(File.ReadAllText(path), new List<MapItem>());
    public static Map LoadMapFromString(string data) => new(data, new List<MapItem>());
}