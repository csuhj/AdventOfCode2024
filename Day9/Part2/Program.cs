using Utilities;

const string Url = "2024/day/9/input";
const string CookieFilePath = "../../session-cookie.txt";

string contents = await DownloadHelper.DownloadInput(CookieFilePath, Url);
contents = contents.Substring(0, contents.Length - 1);

List<File> files = new List<File>();
for (int i = 0; i < contents.Length; i++)
{
    int blockSize = int.Parse($"{contents[i]}");
    int fileId;
    if (i % 2 == 0)
    {
        fileId = i / 2;
    }
    else
    {
        fileId = -1;
    }

    files.Add(new File(fileId, blockSize));
}

Console.WriteLine($"From file map of length {contents.Length} found {files.Count} files - of which {files.Count(f => !f.IsFreeSpace)} are not free space");

for (int i=files.Count - 1; i >= 0; i--)
{
    int indexOfSuitableFreeSpace = files.FindIndex(x => x.IsFreeSpace && x.NumbeOfBlocks >= files[i].NumbeOfBlocks);
    if (indexOfSuitableFreeSpace != -1)
    {
        if (files[indexOfSuitableFreeSpace].NumbeOfBlocks > files[i].NumbeOfBlocks)
        {
            File remainingFreeSpace = new File(-1, files[indexOfSuitableFreeSpace].NumbeOfBlocks - files[i].NumbeOfBlocks);
            files.Insert(indexOfSuitableFreeSpace + 1, remainingFreeSpace);
            if (indexOfSuitableFreeSpace < i)
                i++;
        }
        files[indexOfSuitableFreeSpace] = files[i];
        files.RemoveAt(i);
    }
}

Console.WriteLine($"After packing without fragmentation now have {files.Count} files - of which {files.Count(f => !f.IsFreeSpace)} are not free space");

long sum = 0;
int blockPointer = 0;
foreach (File file in files)
{
    if (!file.IsFreeSpace)
    {
        for (int i=blockPointer; i<blockPointer + file.NumbeOfBlocks; i++)
        {
            sum += i * file.FileId;
        }
    }
    blockPointer += file.NumbeOfBlocks;
}

Console.WriteLine($"The sum of {files.Where(f => !f.IsFreeSpace).Sum(f => f.NumbeOfBlocks)} file blocks is {sum}");
//Between [4486081281] 6,276,589,684,123 and 27,554,557,063,531