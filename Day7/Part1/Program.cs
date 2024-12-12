using System.Text.RegularExpressions;
using Utilities;

const string Url = "2024/day/7/input";
const string CookieFilePath = "../../session-cookie.txt";

string contents = await DownloadHelper.DownloadInput(CookieFilePath, Url);

string[] lines = contents.Split("\n").Where(l => !string.IsNullOrWhiteSpace(l)).ToArray();

Regex regex = new Regex("(\\d+): (.+)");

long runningSumOfValidEquations = 0;
int numberOfValidEquations = 0;
foreach (string line in lines)
{
    Match match = regex.Match(line);
    if (!match.Success)
        throw new Exception($"Failed to parse line: {line}");

    long testValue = long.Parse(match.Groups[1].Value);
    long[] operands = match.Groups[2].Value.Split(" ").Select(long.Parse).ToArray();
    
    if (CanEquationBeValid(testValue, operands))
    {
        numberOfValidEquations++;
        runningSumOfValidEquations += testValue;
    }
}

Console.WriteLine($"The total sum of the {numberOfValidEquations} out of {lines.Length} valid equations (the total calibration result) is {runningSumOfValidEquations}");

bool CanEquationBeValid(long testValue, long[] operands)
{
    if (operands.Length == 1)
        return operands[0] == testValue;

    if (operands[0] > testValue)
        return false;

    long headOperand = operands[0];
    long[] tailOperandsMultiplied = operands.Skip(1).ToArray();
    tailOperandsMultiplied[0] *= headOperand;

    long[] tailOperandsAdded = operands.Skip(1).ToArray();
    tailOperandsAdded[0] += headOperand;
    
    return CanEquationBeValid(testValue, tailOperandsMultiplied) || CanEquationBeValid(testValue, tailOperandsAdded);
}