using Utilities;

const string Url = "2024/day/13/input";
const string CookieFilePath = "../../session-cookie.txt";

string contents = await DownloadHelper.DownloadInput(CookieFilePath, Url);
string[] lines = contents.Split("\n");

List<ClawMachine> machines = new List<ClawMachine>();
for (int i = 0; i < lines.Length; i++)
{
    string line1 = lines[i];
    string line2 = lines[++i];
    string line3 = lines[++i];

    machines.Add(new ClawMachine(line1, line2, line3));
    i++;
}

Console.WriteLine($"There are {machines.Count} claw machines");
