using TrafficController.Interfaces;

namespace TrafficController.Classes;

public class Menu : List<string>, IUiElement
{
    public Menu(IEnumerable<string> options)
    {
        foreach (string option in options)
            Add(option);
    }

    public void Print()
    {
        foreach (string option in this)
            Logger.Log(option);
    }
}