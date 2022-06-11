namespace TrafficController.Plane;

public class HotAirBalloon : Aircraft
{
    public HotAirBalloon(int x, int y, Queue<char> path) : base(x, y, path) =>
        Symbol = (char) AircraftSymbol.HotAirBalloon;
}