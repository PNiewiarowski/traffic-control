namespace TrafficController.Aircraft;

public class Helicopter : Aircraft
{
    public Helicopter(int x, int y, Path.Path path) : base(x, y, path) =>
        Symbol = (char) AircraftSymbol.Helicopter;
}