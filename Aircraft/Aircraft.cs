using TrafficController.Map;
using TrafficController.Path;

namespace TrafficController.Aircraft;

public class Aircraft : MapItem
{
    public Path.Path Path;
    public bool Warned;

    public Aircraft(int x, int y, Path.Path path) : base(x, y)
    {
        Path = path;
        Symbol = (char) AircraftSymbol.Undefined;
    }

    public void ChangePath(Path.Path path) => Path = path;

    public override void Update()
    {
        switch (Path.Route.Count > 0 ? Path.Route.Dequeue() : char.MaxValue)
        {
            case (char) PathDirection.North:
                Y--;
                break;
            case (char) PathDirection.South:
                Y++;
                break;
            case (char) PathDirection.East:
                X++;
                break;
            case (char) PathDirection.West:
                X--;
                break;
        }
    }

    public string GetPath() => Path.Route.Aggregate("", (current, item) => current + item);
}