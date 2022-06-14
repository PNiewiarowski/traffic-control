namespace TrafficController.Path;

public class InvalidRouteError: Exception {
    public InvalidRouteError(): base("Invalid route provided!") {}
}

public struct Path
{
    public readonly Queue<char> Route;
    public readonly int Height;
    public readonly int Velocity;

    public Path(int velocity, int height, Queue<char> route)
    {
        if (route.Count( (char c) => { return "NSWE".Contains(c.ToString().ToUpper()); }) != route.Count) throw new InvalidRouteError();
        Velocity = velocity;
        Height = height;
        Route = route;
    }
}