using TrafficController.Utils;

namespace TrafficController.UI;

public class Menu : List<string>
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