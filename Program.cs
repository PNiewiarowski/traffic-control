using TrafficController.Map;
using TrafficController.Plane;
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

        var uuidMessage = map.CountAllItems() == 0
            ? $"{Environment.NewLine}No items on map!{Environment.NewLine}"
            : $"{Environment.NewLine}Existing map items UUID:{Environment.NewLine}";
        Logger.LogYellow(uuidMessage);
        map.PrintAllUuid();

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

Aircraft GetPlaneFromUser()
{
    Logger.LogYellow("Enter new plane X: ");
    var newX = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());

    Logger.LogYellow("Enter new plane Y: ");
    var newY = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());

    Logger.LogYellow("Enter path plane[N -> UP, S -> DOWN, E -> RIGHT, W -> LEFT]: ");
    var newPath = new Queue<char>(Console.ReadLine()?.ToCharArray() ?? Array.Empty<char>());

    Logger.Log($"[1] Hot air balloon{Environment.NewLine}");
    Logger.Log($"[2] Helicopter{Environment.NewLine}");
    Logger.Log($"[3] Plane{Environment.NewLine}");
    Logger.Log($"[4] Glider{Environment.NewLine}");
    Logger.Log($"[default] Is it a Plane? Is it a Bird? No it's dsafxcvzxcvz[?]{Environment.NewLine}");
    Logger.LogYellow("Enter type of Aircraft: ");
    var type = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());
    return type switch
    {
        1 => new HotAirBalloon(newX, newY, newPath),
        2 => new Helicopter(newX, newY, newPath),
        3 => new Plane(newX, newY, newPath),
        4 => new Glider(newX, newY, newPath),
        _ => new Aircraft(newX, newY, newPath)
    };
}

if (!new[] {0, 1}.Contains(args.Length))
{
    Logger.LogRed($"Invalid arguments!{Environment.NewLine}");
    Logger.LogRed($"Load map from file: <Program> <path_to_file>{Environment.NewLine}");
    Logger.LogRed($"Generate map with default parameters: <Program>{Environment.NewLine}");
}
else Main();