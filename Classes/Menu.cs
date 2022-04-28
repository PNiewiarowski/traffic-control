using TrafficController.Interfaces;

namespace TrafficController.Classes;

public class Menu : List<string>, IUiElement
{
    public Menu(IEnumerable<string> options)
    {
        foreach (var option in options)
            Add(option);
    }

    public void Print()
    {
        foreach (var option in this)
            Logger.Log(option);
    }
}