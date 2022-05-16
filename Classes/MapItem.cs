namespace TrafficController.Classes;

public abstract class MapItem
{
    protected int X;
    protected int Y;

    public int getX()
    {
        return X;
    }

    public int getY()
    {
        return Y;
    }

    protected MapItem(int x, int y)
    {
        X = x;
        Y = y;
    }

    public abstract void Update();
}