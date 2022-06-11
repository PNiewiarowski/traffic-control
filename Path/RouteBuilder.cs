namespace TrafficController.Path;

public static class PathBuilder
{
    private static char MapNumToDirection(int val)
    {
        return val switch
        {
            0 => (char) PathDirection.North,
            1 => (char) PathDirection.East,
            2 => (char) PathDirection.West,
            _ => (char) PathDirection.South
        };
    }

    public static Queue<char> Build(int size)
    {
        var randPath = new Queue<char>();
        var rand = new Random();

        for (var i = 0; i < size; i++)
            randPath.Enqueue(MapNumToDirection(rand.Next(0, 4)));

        return randPath;
    }
}