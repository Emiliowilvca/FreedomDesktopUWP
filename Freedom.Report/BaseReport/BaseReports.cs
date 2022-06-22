using Freedom.Report.FileInterface;
using Microsoft.Extensions.DependencyInjection;

namespace Freedom.Report.BaseReport
{
    public class BaseReports
    {
        public readonly IFileService _fileService;

        public BaseReports()
        {
            _fileService = Program.ServiceProvider.GetRequiredService<IFileService>();
        }
    }
}