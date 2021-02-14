using FileConverter.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FileConverter.Services
{
    public class HeadingService : IHeadingService
    {
        //Public but not included in the interface
        //This is so it can be tested without being exposed
        public List<Heading> GenerateHeadings(IEnumerable<string> headings)
        {
            List<Heading> existingHeadings = new List<Heading>();

            foreach (var heading in headings)
            {
                GenerateHeading(heading, existingHeadings);
            }

            return existingHeadings;
        }

        public List<Heading> GenerateHeading(string heading)
        {
            List<Heading> existingHeadings = new List<Heading>();

            GenerateHeading(heading, existingHeadings);

            return existingHeadings;
        }

        public List<Heading> GenerateHeading(string heading, List<Heading> existingHeadings)
        {
            if (!heading.Contains('_'))
            {
                existingHeadings.Add(CreateHeading(heading));
                return existingHeadings;
            }

            var headingName = GetHeadingName(heading);
            var headingRemainder = GetHeadingRemainder(heading);
            var existingHeading = existingHeadings.SingleOrDefault(x => x.Name == headingName);

            if (existingHeading == null)
            {
                CreateHeading(existingHeadings, headingName, headingRemainder);
            }
            else
            {
                existingHeading.SubHeadings.AddRange(GenerateHeading(headingRemainder));
            }
            return existingHeadings;
        }

        public void CreateHeading(List<Heading> existingHeadings, string headingName, string headingRemainder)
        {
            var newHeading = new Heading();
            newHeading.Name = headingName;
            newHeading.SubHeadings = new List<Heading>();

            newHeading.SubHeadings.AddRange(GenerateHeading(headingRemainder));
            existingHeadings.Add(newHeading);
        }

        public Heading CreateHeading(string headingName)
        {
            var heading = new Heading();
            heading.Name = headingName;
            heading.SubHeadings = new List<Heading>();
            return heading;
        }

        public string GetHeadingName(string heading)
        {
            return heading.Split('_')[0];
        }

        public string GetHeadingRemainder(string heading)
        {
            return heading.Replace($"{heading.Split('_')[0]}_", string.Empty);
        }
    }

    public interface IHeadingService
    {
        List<Heading> GenerateHeadings(IEnumerable<string> headings);
    }
}
