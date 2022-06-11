namespace TrafficController.Map;

public abstract class MapItem
{
    public int X;
    public int Y;
    protected char Symbol;
    public readonly string Uuid;

    protected MapItem(int x, int y)
    {
        X = x;
        Y = y;
        Uuid = Guid.NewGuid().ToString();
    }
    
    public override string ToString() => Symbol.ToString();

    public abstract void Update();
}