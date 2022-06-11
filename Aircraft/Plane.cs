namespace TrafficController.Aircraft;

public class Plane : Aircraft
{
    public Plane(int x, int y, Path.Path path) : base(x, y, path) =>
        Symbol = (char) AircraftSymbol.Plane;
}