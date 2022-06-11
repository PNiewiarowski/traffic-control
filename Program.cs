using TrafficController.Aircraft;
using TrafficController.Map;
using TrafficController.Path;
using TrafficController.Ui;
using TrafficController.Utils;
using Path = TrafficController.Path.Path;

void Main()
{
    var map = GetMap();
    var menu = GetMainMenu();

    var run = true;
    var error = "";
    var counter = 0;
    while (run)
    {
        counter++;
        Logger.Reset();
        Logger.Log($"{error}{Environment.NewLine}");
        error = "";

        Logger.LogYellow($"{Environment.NewLine}RADAR {counter:0000}{Environment.NewLine}");
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
            case "r":
            case "random":
                AddRandomAircraft(map);
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

void AddRandomAircraft(Map map)
{
    var rand = new Random();
    var x = rand.Next(map.GetMapWidth());
    var y = rand.Next(map.GetMapHeight());
    var plane = rand.Next(4);

    switch (plane)
    {
        case 0:
            map.AddItemToRender(new Plane(x, y, new Path(
                        (int) AircraftVelocity.Plane,
                        (int) AircraftHeight.Plane,
                        PathBuilder.Build(20)
                    )
                )
            );
            break;
        case 1:
            map.AddItemToRender(new Glider(x, y, new Path(
                        (int) AircraftVelocity.Glider,
                        (int) AircraftHeight.Helicopter,
                        PathBuilder.Build(20)
                    )
                )
            );
            break;
        case 2:
            map.AddItemToRender(new HotAirBalloon(x, y, new Path(
                        (int) AircraftVelocity.HotAirBalloon,
                        (int) AircraftHeight.HotAirBalloon,
                        PathBuilder.Build(20)
                    )
                )
            );
            break;
        default:
            map.AddItemToRender(new Helicopter(x, y, new Path(
                        (int) AircraftVelocity.Helicopter,
                        (int) AircraftHeight.Helicopter,
                        PathBuilder.Build(20)
                    )
                )
            );
            break;
    }
}

Map GetMap() =>
    args.Length switch
    {
        0 => MapLoader.LoadMapFromString(new MapBuilder(100, 30, .98f).Build()),
        1 => MapLoader.LoadMapFromTextFile(args[0]),
        3 => MapLoader.LoadMapFromString(
            new MapBuilder(
                int.Parse(args[0]),
                int.Parse(args[1]),
                float.Parse(args[2]) / 100
            ).Build()),
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
        "1" => new HotAirBalloon(newX, newY, new Path(
                (int) AircraftVelocity.HotAirBalloon,
                (int) AircraftHeight.HotAirBalloon,
                newPath
            )
        ),
        "2" => new Helicopter(newX, newY, new Path(
                (int) AircraftVelocity.Helicopter,
                (int) AircraftHeight.Helicopter,
                newPath
            )
        ),
        "3" => new Plane(newX, newY, new Path(
                (int) AircraftVelocity.Plane,
                (int) AircraftHeight.Plane,
                newPath
            )
        ),
        "4" => new Glider(newX, newY, new Path(
                (int) AircraftVelocity.Glider,
                (int) AircraftHeight.Glider,
                newPath
            )
        ),
        _ => new Aircraft(newX, newY, new Path(100, 100, newPath))
    };
}

Menu GetMainMenu() =>
    new(new[]
        {
            $"[e] exit{Environment.NewLine}",
            $"[n] next round{Environment.NewLine}",
            $"[c] add new aircraft manually{Environment.NewLine}",
            $"[u] update aircraft path{Environment.NewLine}",
            $"[d] delete aircraft{Environment.NewLine}",
            $"[r] add random aircraft{Environment.NewLine}"
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

if (!new[] {0, 1, 3}.Contains(args.Length))
{
    Logger.LogRed($"Invalid arguments!{Environment.NewLine}");
    Logger.LogRed($"Load map from file: <Program> <path_to_file>{Environment.NewLine}");
    Logger.LogRed($"Generate map with default parameters: <Program>{Environment.NewLine}");
}
else Main();