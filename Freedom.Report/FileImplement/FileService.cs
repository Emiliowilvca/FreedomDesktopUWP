using Freedom.Report.FileInterface;
using System.IO;

namespace Freedom.Report.FileImplement
{
    public class FileService : IFileService
    {
        public void CreateDirectory(string filePath)
        {
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);

                File.SetAttributes(filePath, FileAttributes.Normal);
            }
        }

        public void DeleteFile(string fullpath)
        {
            if (File.Exists(fullpath))
            {
                File.Delete(fullpath);
            }
        }

        public string GetFullPath(string fileName)
        {
            return Path.Combine(Program.FilePath, fileName);
        }

        public void ResetFilePath(string fileName)
        {
            string fullPath = GetFullPath(fileName);

            CreateDirectory(Program.FilePath);

            DeleteFile(fullPath);
        }
    }
}