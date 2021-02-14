using FileConverter.Models;
using FileConverter.Services;
using System.Collections.Generic;

namespace FileConverter.FileConversionServices
{
    public abstract class FileConverterService : IFileConverterService
    {
        private readonly IHeadingService _headingService;
        public FileConverterService(IHeadingService headingService)
        {
            _headingService = headingService;
        }
        public abstract string Convert(string path);

        public List<Heading> GenerateHeadings(IEnumerable<string> headings)
        {
            return _headingService.GenerateHeadings(headings);
        }
    }

    public interface IFileConverterService
    {
        string Convert(string path);
    }
}
