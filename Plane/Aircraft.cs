using TrafficController.Map;

namespace TrafficController.Plane;

public class Aircraft : MapItem
{
    private readonly Queue<char> _path;

    public Aircraft(int x, int y, Queue<char> path) : base(x, y)
    {
        _path = path;
        Symbol = (char) AircraftSymbol.Undefined;
    }

    public override void Update()
    {
        switch (_path.Count != 1 ? _path.Dequeue() : _path.Last())
        {
            case 'N':
                Y--;
                break;
            case 'S':
                Y++;
                break;
            case 'E':
                X++;
                break;
            case 'W':
                X--;
                break;
        }
    }
}