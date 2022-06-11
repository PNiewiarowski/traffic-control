using TrafficController.Map;
using TrafficController.Path;
using TrafficController.Plane;
using TrafficController.Ui;
using TrafficController.Utils;

void Main()
{
    var map = GetMap();
    var menu = GetMainMenu();

    var run = true;
    var error = "";
    while (run)
    {
        Logger.Reset();
        Logger.Log($"{error}{Environment.NewLine}");
        error = "";

        Logger.LogYellow($"{Environment.NewLine}RADAR{Environment.NewLine}");
        map.Print();

        var uuidMessage = map.CountAllItems() == 0
            ? $"{Environment.NewLine}No items on map!{Environment.NewLine}"
            : $"{Environment.NewLine}Existing map items UUID:{Environment.NewLine}";
        Logger.LogYellow(uuidMessage);
        map.PrintAllAircraft();

        Logger.LogYellow($"{Environment.NewLine}MENU{Environment.NewLine}");
        menu.Print();

        Logger.LogYellow("Your choice: ");
        switch (Console.ReadLine()?.ToLower())
        {
            case "exit":
            case "e":
                run = false;
                break;
            case "u":
            case "update":
                UpdatePath(map);
                break;
            case "next":
            case "n":
                map.UpdateItems();
                break;
            case "c":
            case "create":
                try
                {
                    map.AddItemToRender(GetPlaneFromUser());
                }
                catch (FormatException)
                {
                    error += "Error: Next time enter number, ok?";
                }
                break;
            case "d":
            case "delete":
                DeleteItem(map);
                break;
        }
    }
}

void UpdatePath(Map map)
{
    Logger.LogYellow("Enter aircraft UUID: ");
    var uuid = Console.ReadLine();

    Logger.LogYellow("Enter new path: ");
    var path = new Queue<char>(Console.ReadLine()?.ToCharArray() ?? Array.Empty<char>());

    map.UpdateItemPath(uuid, path);
}

void DeleteItem(Map map)
{
    Logger.LogYellow("Enter aircraft UUID: ");
    var uuid = Console.ReadLine();

    map.DeleteItemByUuid(uuid);
}

Map GetMap() =>
    args.Length switch
    {
        0 => MapLoader.LoadMapFromString(new MapBuilder(100, 30, .98f).Build()),
        1 => MapLoader.LoadMapFromTextFile(args[0]),
        _ => throw new Exception("Invalid usage...")
    };

Aircraft GetPlaneFromUser()
{
    Logger.LogYellow("Enter new plane X: ");
    var newX = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());

    Logger.LogYellow("Enter new plane Y: ");
    var newY = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());

    Logger.LogYellow("Do you want insert path[Y/N]?: ");
    Queue<char> newPath;
    if (Console.ReadLine()?.ToLower() == "y")
    {
        Logger.LogYellow(
            $"{(char) PathDirection.North} -> {PathDirection.North}{Environment.NewLine}" +
            $"{(char) PathDirection.South} -> {PathDirection.South}{Environment.NewLine}" +
            $"{(char) PathDirection.East} -> {PathDirection.East}{Environment.NewLine}" +
            $"{(char) PathDirection.West} -> {PathDirection.West}{Environment.NewLine}"
        );
        Logger.LogYellow("Enter path plane: ");
        newPath = new Queue<char>(Console.ReadLine()?.ToUpper().ToCharArray() ?? Array.Empty<char>());
    }
    else
        newPath = PathBuilder.Build(20);


    GetAircraftSubmenu().Print();
    Logger.LogYellow("Enter type of Aircraft: ");
    return Console.ReadLine() switch
    {
        "1" => new HotAirBalloon(newX, newY, newPath),
        "2" => new Helicopter(newX, newY, newPath),
        "3" => new Plane(newX, newY, newPath),
        "4" => new Glider(newX, newY, newPath),
        _ => new Aircraft(newX, newY, newPath)
    };
}

Menu GetMainMenu() =>
    new(new[]
        {
            $"[e] exit from menu{Environment.NewLine}",
            $"[n] go next{Environment.NewLine}",
            $"[c] set new plane{Environment.NewLine}",
            $"[u] update plane path{Environment.NewLine}",
            $"[d] delete plane{Environment.NewLine}",
        }
    );

Menu GetAircraftSubmenu() =>
    new(new[]
        {
            $"[1] Hot air balloon{Environment.NewLine}",
            $"[2] Helicopter{Environment.NewLine}",
            $"[3] Plane{Environment.NewLine}",
            $"[4] Glider{Environment.NewLine}",
            $"[default] Is it a Plane? Is it a Bird? No it's dsafxcvzxcvz[?]{Environment.NewLine}"
        }
    );

if (!new[] {0, 1}.Contains(args.Length))
{
    Logger.LogRed($"Invalid arguments!{Environment.NewLine}");
    Logger.LogRed($"Load map from file: <Program> <path_to_file>{Environment.NewLine}");
    Logger.LogRed($"Generate map with default parameters: <Program>{Environment.NewLine}");
}
else Main();