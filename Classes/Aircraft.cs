namespace TrafficController.Classes;

public class Plane : Aircraft
{
    private new char _symbol = 'P';
    public Plane(int x, int y, Queue<char> path) : base(x, y, path) { }
}

public class Aircraft : MapItem
{
    private Queue<char> _path;
    protected char _symbol;

    public Aircraft(int x, int y, Queue<char> path) : base(x, y)
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
            move = _path.Last(); //** jeśli wskazana trasa się kończy - przesuwa tak samo jak ostatni wskaźnik trasy. Zmienić, tak jak chcemy żeby działało!//
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