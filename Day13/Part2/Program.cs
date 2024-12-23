using Utilities;

const string Url = "2024/day/13/input";
const string CookieFilePath = "../../session-cookie.txt";

string exampleContents =
    "Button A: X+94, Y+34\n"+
    "Button B: X+22, Y+67\n"+
    "Prize: X=8400, Y=5400\n"+
    "\n"+
    "Button A: X+26, Y+66\n"+
    "Button B: X+67, Y+21\n"+
    "Prize: X=12748, Y=12176\n"+
    "\n"+
    "Button A: X+17, Y+86\n"+
    "Button B: X+84, Y+37\n"+
    "Prize: X=7870, Y=6450\n"+
    "\n"+
    "Button A: X+69, Y+23\n"+
    "Button B: X+27, Y+71\n"+
    "Prize: X=18641, Y=10279\n";

string contents = await DownloadHelper.DownloadInput(CookieFilePath, Url);

Dictionary<string, string> nameToContentsMap = new Dictionary<string, string>() {
    {"Example", exampleContents},
    {"Real Data", contents},
};

foreach (var name in nameToContentsMap.Keys)
{
    string[] lines = nameToContentsMap[name].Split("\n");

    List<ClawMachine> machines = new List<ClawMachine>();
    for (int i = 0; i < lines.Length; i++)
    {
        string line1 = lines[i];
        string line2 = lines[++i];
        string line3 = lines[++i];

        var machine = new ClawMachine(line1, line2, line3);
        machine = new ClawMachine(machine.ButtonAVector, machine.ButtonBVector, new PointL(machine.PrizeLocation.X + 10000000000000, machine.PrizeLocation.Y + 10000000000000)); 
        machines.Add(machine);
        i++;
    }

    int numberOfSolvableMachines = 0;
    long totalNumberOfTokensNeeded = 0;
    foreach (var machine in machines)
    {
        if (machine.TrySolve(out (long, long)solution))
        {
            numberOfSolvableMachines++;
            totalNumberOfTokensNeeded += (solution.Item1 * 3);
            totalNumberOfTokensNeeded += solution.Item2;
        }
    }

    Console.WriteLine($"For {name}, there are {machines.Count} claw machines, of which {numberOfSolvableMachines} are solvable and a total number of button presses of {totalNumberOfTokensNeeded}");

//87573046684976 is too low
}