namespace TrafficController.Plane;

public class Glider : Aircraft
{
    public Glider(int x, int y, Queue<char> path) : base(x, y, path) =>
        Symbol = (char) AircraftSymbol.Glider;
}