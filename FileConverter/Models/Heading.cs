using System.Collections.Generic;

namespace FileConverter.Models
{
    public class Heading
    {
        public string Name { get; set; }

        public List<Heading> SubHeadings { get; set; }
    }
}
