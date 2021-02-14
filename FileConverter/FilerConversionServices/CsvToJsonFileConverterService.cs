using FileConverter.Services;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FileConverter.FileConversionServices
{
    public class CsvToJsonFileConverterService : FileConverterService, ICsvToJsonFileConverterService
    {
        public CsvToJsonFileConverterService(IHeadingService headingService)
            : base(headingService)
        {

        }
        public override string Convert(string path)
        {
            var rows = new List<string[]>();
            var doument = File.ReadAllLines(path);

            var headings = GenerateHeadings(doument[0].Split(','));

            doument = doument.Skip(1).ToArray();

            foreach (var line in doument)
            {
                rows.Add(line.Split(','));
            }

            var csvAsObj = ExpandoHelper.CreateExpandoList(headings, rows);

            string json = JsonConvert.SerializeObject(csvAsObj);

            return json;
        }
    }

    public interface ICsvToJsonFileConverterService
    {
        string Convert(string path);
    }
}
