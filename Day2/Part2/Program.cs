using Utilities;

const string Url = "2024/day/2/input";
const string CookieFilePath = "../../session-cookie.txt";

string contents = await DownloadHelper.DownloadInput(CookieFilePath, Url);

int reportCount = 0;
int safeReportCount = 0;
foreach (var line in contents.Split("\n"))
{
    if (string.IsNullOrWhiteSpace(line))
        continue;

    reportCount++;
    string[] items = line.Split();

    List<int> numbers = items.Select(int.Parse).ToList();

    if (IsReportSafe(numbers))
    {
        safeReportCount++;
    }
    else
    {
        int oldSafeReportCount = safeReportCount;
        for (int i=0; i<numbers.Count; i++)
        {
            if (IsReportSafe(ListExceptIndex(numbers, i)))
            {
                safeReportCount++;
                break;
            }
        }
    }
}

Console.WriteLine($"Number of safe reports (with Problem Dampner applied) is {safeReportCount} from {reportCount}");

static bool IsReportSafe(List<int> numbers)
{
    bool? ascending = null;
    for (int i=0; i<numbers.Count - 1; i++)
    {
        int difference = numbers[i] - numbers[i+1];
        if (Math.Abs(difference) > 3 || difference == 0)
            return false;

        if (ascending == null)
        {
            ascending = difference < 0;
        }
        else
        {
            if (ascending != difference < 0)
                return false;
        }
    }
    return true;
}

static List<int> ListExceptIndex(List<int> numbers, int indexToRemove)
{
    return numbers.Where((v, i) => i != indexToRemove).ToList();
}