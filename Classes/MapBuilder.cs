namespace TrafficController.Classes;

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
        string map = "";
        Random rand = new Random();
        float chance = _chance * 100;

        for (int i = 0; i < _height; i++)
        {
            for (int j = 0; j < _width; j++)
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