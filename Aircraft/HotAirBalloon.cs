namespace TrafficController.Aircraft;

public class HotAirBalloon : Aircraft
{
    public HotAirBalloon(int x, int y, Path.Path path) : base(x, y, path) =>
        Symbol = (char) AircraftSymbol.HotAirBalloon;
}