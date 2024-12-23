using System.Text.RegularExpressions;
using Utilities;
using DotNetMath.SimEq;

public class ClawMachine
{
    public VectorL ButtonAVector { get; }
    public VectorL ButtonBVector { get; }
    public PointL PrizeLocation { get; }

    public ClawMachine(VectorL buttonAVector, VectorL buttonBVector, PointL prizeLocation)
    {
        ButtonAVector = buttonAVector;
        ButtonBVector = buttonBVector;
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
        
        ButtonAVector = new VectorL(int.Parse(buttonAMatch.Groups[2].Value), int.Parse(buttonAMatch.Groups[3].Value));
        ButtonBVector = new VectorL(int.Parse(buttonBMatch.Groups[2].Value), int.Parse(buttonBMatch.Groups[3].Value));
        PrizeLocation = new PointL(int.Parse(prizeLocationMatch.Groups[1].Value), int.Parse(prizeLocationMatch.Groups[2].Value));
    }

    public bool TrySolve(out (long, long) solution)
    {
        SimEqSolver solver = new SimEqSolver(2);
        solver.AddEquation(new double[] { ButtonAVector.X, ButtonBVector.X }, PrizeLocation.X );
        solver.AddEquation(new double[] { ButtonAVector.Y, ButtonBVector.Y }, PrizeLocation.Y );
        double[] solutions = solver.Solve();

        double solution1 = Math.Round(solutions[0], 2);
        double solution2 = Math.Round(solutions[1], 2);
        if (solution1 % 1 != 0 || solution2 % 1 != 0)
        {
            solution = (0, 0);
            return false;
        }

        solution = ((long)solution1, (long)solution2);
        return true;
    }
}