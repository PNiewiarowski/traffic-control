namespace TrafficController.Aircraft;

public class Glider : Aircraft
{
    public Glider(int x, int y, Path.Path path) : base(x, y, path) =>
        Symbol = (char) AircraftSymbol.Glider;
}