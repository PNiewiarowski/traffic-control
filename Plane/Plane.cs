using TrafficController.Map;

namespace TrafficController.Plane;

public class Plane : MapItem
{
    private readonly Queue<char> _path;
    private readonly string _uud;

    public Plane(int x, int y, Queue<char> path) : base(x, y)
    {
        _path = path;
        _uuid = Guid.NewGuid().ToString();
    }

    public override string ToString() => _uuid;

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