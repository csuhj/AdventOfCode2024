using System.Text.RegularExpressions;
using Utilities;

public struct ClawMachine
{
    public Vector ButtonAVector { get; }
    public Vector ButtonBVector { get; }
    public Point PrizeLocation { get; }

    public ClawMachine(Vector buttonAVector, Vector buttonBVector, Point prizeLocation)
    {
        ButtonAVector = buttonAVector;
        ButtonBVector  = buttonBVector;
        PrizeLocation = prizeLocation;
    }

    public ClawMachine(string buttonAVectorString, string buttonBVectorString, string prizeLocationString)
    {
        Regex buttonRegex = new Regex("Button (A|B): X\\+(\\d+), Y\\+(\\d+)");
        Regex prizeLocationRegex = new Regex("Prize: X=(\\d+), Y=(\\d+)");

        Match buttonAMatch = buttonRegex.Match(buttonAVectorString);
        if (!buttonAMatch.Success)
            throw new ArgumentException($"{buttonAVectorString} doesn't match a button vector string pattern", nameof(buttonAVectorString));

        Match buttonBMatch = buttonRegex.Match(buttonBVectorString);
        if (!buttonBMatch.Success)
            throw new ArgumentException($"{buttonBVectorString} doesn't match a button vector string pattern", nameof(buttonBVectorString));

        Match prizeLocationMatch = prizeLocationRegex.Match(prizeLocationString);
        if (!prizeLocationMatch.Success)
            throw new ArgumentException($"{prizeLocationString} doesn't match a prize location string pattern", nameof(prizeLocationString));
        
        ButtonAVector = new Vector(int.Parse(buttonAMatch.Groups[2].Value), int.Parse(buttonAMatch.Groups[3].Value));
        ButtonBVector = new Vector(int.Parse(buttonBMatch.Groups[2].Value), int.Parse(buttonBMatch.Groups[3].Value));
        PrizeLocation = new Point(int.Parse(prizeLocationMatch.Groups[1].Value), int.Parse(prizeLocationMatch.Groups[2].Value));
    }
}