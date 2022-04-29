namespace TrafficController.Classes;

public class Plane : MapItem
{
    private Queue<char> _path;

    public Plane(int x, int y, Queue<char> path) : base(x, y)
    {
        _path = path;
    }

    public override void Update()
    {
        char move = (char)0;
        if (_path.Count() != 1) {
            move = _path.Dequeue();
        }
        else {
            move = _path.Last();
        }
        switch (move)
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
            default:
                break;
        }
    }
}