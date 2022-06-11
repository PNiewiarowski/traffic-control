using TrafficController.Map;
using TrafficController.Path;

namespace TrafficController.Plane;

public class Aircraft : MapItem
{
    private Queue<char> _path;
    public bool Warned;

    public Aircraft(int x, int y, Queue<char> path) : base(x, y)
    {
        _path = path;
        Symbol = (char) AircraftSymbol.Undefined;
    }

    public void ChangePath(Queue<char> path) => _path = path;

    public override void Update()
    {
        switch (_path.Count > 0 ? _path.Dequeue() : char.MaxValue)
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

    public string GetPath() => _path.Aggregate("", (current, item) => current + item);
}