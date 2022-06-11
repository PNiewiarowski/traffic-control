namespace TrafficController.Plane;

public class Helicopter : Aircraft
{
    public Helicopter(int x, int y, Queue<char> path) : base(x, y, path) =>
        Symbol = (char) AircraftSymbol.Helicopter;
}