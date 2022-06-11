namespace TrafficController.Map;

public class MapBuilder
{
    private readonly int _width;
    private readonly int _height;
    private readonly float _chance;

    public MapBuilder(int width, int height, float chance)
    {
        _width = width;
        _height = height;
        _chance = chance;
    }

    public string Build()
    {
        var map = "";
        var rand = new Random();
        var chance = _chance * 100;

        for (var i = 0; i < _height; i++)
        {
            for (var j = 0; j < _width; j++)
            {
                if (chance <= rand.Next(100))
                {
                    map += "X";
                    continue;
                }

                map += "O";
            }

            map += Environment.NewLine;
        }

        return map;
    }
}