using FileConverter.Models;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

namespace FileConverter
{
    public static class ExpandoHelper
    {
        public static IEnumerable<ExpandoObject> CreateExpandoList(List<Heading> headings, List<string[]> data)
        {
            return data.Select(row => CreateExpando(headings, row));
        }
        public static ExpandoObject CreateExpando(List<Heading> headings, string[] cells, int cellCount = 0)
        {
            dynamic expando = new ExpandoObject();

            foreach (var heading in headings)
            {
                if (heading.SubHeadings != null && heading.SubHeadings.Any())
                {
                    AddProperty(expando, heading.Name, CreateExpando(heading.SubHeadings, cells, cellCount));
                }
                else
                {
                    AddProperty(expando, heading.Name, cells[cellCount]);
                    cellCount++;
                }
            }
            return expando;
        }

        public static void AddProperty(ExpandoObject expando, string propertyName, object value)
        {
            var expandoDict = expando as IDictionary<string, object>;
            if (expandoDict.ContainsKey(propertyName))
                expandoDict[propertyName] = value;
            else
                expandoDict.Add(propertyName, value);
        }
    }
}
