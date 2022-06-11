namespace TrafficController.Path;

public struct Path
{
    public readonly Queue<char> Route;
    public readonly int Height;
    public readonly int Velocity;

    public Path(int velocity, int height, Queue<char> route)
    {
        Velocity = velocity;
        Height = height;
        Route = route;
    }
}