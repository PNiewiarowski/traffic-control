namespace TrafficController.Map;

public abstract class MapItem
{
    public int X;
    public int Y;
    public string Uuid;

    protected MapItem(int x, int y)
    {
        X = x;
        Y = y;
        Uuid = Guid.NewGuid().ToString();
    }

    public abstract void Update();
}