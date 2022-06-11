namespace TrafficController.Plane;

public class Plane : Aircraft
{
    public Plane(int x, int y, Queue<char> path) : base(x, y, path) =>
        Symbol = (char) AircraftSymbol.Plane;
}