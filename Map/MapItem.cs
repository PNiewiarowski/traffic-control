namespace TrafficController.Map;

public abstract class MapItem
{
    public int X;
    public int Y;

    protected MapItem(int x, int y)
    {
        X = x;
        Y = y;
    }

    public abstract void Update();
}