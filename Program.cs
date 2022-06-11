using TrafficController.Classes;
using TrafficController.Map;
using TrafficController.UI;
using TrafficController.Utils;

void Main()
{
    var map = GetMap();
    var menu = GetMenu();
    var run = true;

    while (run)
    {
        Logger.Reset();
        Logger.LogYellow($"{Environment.NewLine}R_A_D_A_R{Environment.NewLine}");
        map.Print();
        Logger.LogYellow($"{Environment.NewLine}M_E_N_U{Environment.NewLine}");
        menu.Print();

        Logger.LogYellow("Your choice: ");
        switch (Console.ReadLine()?.ToLower())
        {
            case "exit":
            case "e":
                run = false;
                break;
            case "next":
            case "n":
                map.UpdateItems();
                break;
            case "c":
            case "create":
                var plane = GetPlaneFromUser();
                map.AddItemToRender(plane);
                break;
        }
    }
}


Map GetMap() =>
    args.Length switch
    {
        0 => MapLoader.LoadMapFromString(new MapBuilder(100, 30, .98f).Build()),
        1 => MapLoader.LoadMapFromTextFile(args[0]),
        _ => throw new Exception("Invalid usage...")
    };

Menu GetMenu() =>
    new(new[]
        {
            $"[e] exit from menu{Environment.NewLine}",
            $"[n] go next{Environment.NewLine}",
            $"[c] set new plane{Environment.NewLine}",
        }
    );

Plane GetPlaneFromUser()
{
    Logger.LogYellow("Enter new plane X: ");
    var newX = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());

    Logger.LogYellow("Enter new plane Y: ");
    var newY = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());

    Logger.LogYellow("Enter path plane[N -> UP, S -> DOWN, E -> RIGHT, W -> LEFT]: ");
    var newPath = new Queue<char>(Console.ReadLine()?.ToCharArray() ?? Array.Empty<char>());

    return new Plane(newX, newY, newPath);
}

if (!new[] {0, 1}.Contains(args.Length))
{
    Logger.LogRed($"Invalid arguments!{Environment.NewLine}");
    Logger.LogRed($"Load map from file: <Program> <path_to_file>{Environment.NewLine}");
    Logger.LogRed($"Generate map with default parameters: <Program>{Environment.NewLine}");
}
else Main();