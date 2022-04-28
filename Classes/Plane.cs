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
        var move = _path.Dequeue();
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
        }
    }
}