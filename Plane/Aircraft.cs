using TrafficController.Map;
using TrafficController.Route;

namespace TrafficController.Plane;

public class Aircraft : MapItem
{
    private Queue<char> _path;

    public Aircraft(int x, int y, Queue<char> path) : base(x, y)
    {
        _path = path;
        Symbol = (char) AircraftSymbol.Undefined;
    }

    public void ChangePath(Queue<char> path) => _path = path;

    public override void Update()
    {
        switch (_path.Count != 1 ? _path.Dequeue() : _path.Last())
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
}