public struct File
{
    public int FileId { get; set; }
    public int NumbeOfBlocks { get; set; }

    public bool IsFreeSpace => FileId == -1;

    public File(int fileId, int numberOfBlocks)
    {
        FileId = fileId;
        NumbeOfBlocks = numberOfBlocks;
    }

    public override bool Equals(object? obj)
    {
        return obj is File file &&
            FileId.Equals(file.FileId) &&
            NumbeOfBlocks == file.NumbeOfBlocks;
    }

    public override int GetHashCode()
    {
        return FileId.GetHashCode() * NumbeOfBlocks.GetHashCode();
    }

    public override string ToString()
    {
        return $"File {FileId} - size {NumbeOfBlocks}";
    }
}