using Utilities;

const string Url = "2024/day/9/input";
const string CookieFilePath = "../../session-cookie.txt";

string contents = await DownloadHelper.DownloadInput(CookieFilePath, Url);
contents = contents.Substring(0, contents.Length - 1);

List<int> fileBlocks = new List<int>();
int totalNumberOfFreeBlocks = 0;
for (int i = 0; i < contents.Length; i++)
{
    int blockSize = int.Parse($"{contents[i]}");
    int blockId;
    if (i % 2 == 0)
    {
        blockId = i / 2;
    }
    else
    {
        blockId = -1;
        totalNumberOfFreeBlocks += blockSize;
    }

    for (int b = 0; b < blockSize; b++)
        fileBlocks.Add(blockId);
}

int lastFilledBlockIndex = fileBlocks.Count - 1;
int firstFreeBlockIndex = 0;
for (int i = 0; i <= totalNumberOfFreeBlocks; i++)
{
    lastFilledBlockIndex = fileBlocks.FindLastIndex(x => x != -1);
    firstFreeBlockIndex = fileBlocks.FindIndex(x => x == -1);

    fileBlocks[firstFreeBlockIndex] = fileBlocks[lastFilledBlockIndex];
    fileBlocks[lastFilledBlockIndex] = -1;
}

long sum = 0;
for (int i=0; i<lastFilledBlockIndex; i++)
{
    sum += i * fileBlocks[i];
}

Console.WriteLine($"The sum of {lastFilledBlockIndex} file blocks is {sum}");
