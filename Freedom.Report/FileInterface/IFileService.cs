namespace Freedom.Report.FileInterface
{
    public interface IFileService
    {
        string GetFullPath(string fileName);

        void CreateDirectory(string filePath);

        void DeleteFile(string fullpath);

        void ResetFilePath(string fileName);
    }
}