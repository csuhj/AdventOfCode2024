public struct File
{
    public int FileId { get; set; }
    public int NumberOfBlocks { get; set; }

    public bool IsFreeSpace => FileId == -1;

    public File(int fileId, int numberOfBlocks)
    {
        FileId = fileId;
        NumberOfBlocks = numberOfBlocks;
    }

    public override bool Equals(object? obj)
    {
        return obj is File file &&
            FileId.Equals(file.FileId) &&
            NumberOfBlocks == file.NumberOfBlocks;
    }

    public override int GetHashCode()
    {
        return FileId.GetHashCode() * NumberOfBlocks.GetHashCode();
    }

    public override string ToString()
    {
        return $"File {FileId} - size {NumberOfBlocks}";
    }
}