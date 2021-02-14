using FileConverter.Models;
using FileConverter.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace FileConverterTests
{
    [TestClass]
    public class HeadingServiceTests
    {
        private HeadingService fileConverterService;

        [TestInitialize]
        public void Init()
        {
            fileConverterService = new HeadingService();
        }

        [TestMethod]
        public void GenerateHeadings_WhenCalledWithTwoGroupedFields_ReturnsListOfTwo()
        {
            var fields = new List<string>()
            {
                "name",
                "address_line1",
                "address_line2"
            };
            var result = fileConverterService.GenerateHeadings(fields);
            var expected = getExpectedHeadings();

            Assert.AreEqual(expected[0].Name, result[0].Name);
            Assert.AreEqual(expected[1].Name, result[1].Name);
            Assert.AreEqual(expected[1].SubHeadings[0].Name, result[1].SubHeadings[0].Name);
            Assert.AreEqual(expected[1].SubHeadings[1].Name, result[1].SubHeadings[1].Name);
        }

        [TestMethod]
        public void GenerateHeadings_WhenCalledWithMultiLeveledGrouping_ReturnsListOfHeadingsWithMultipleMatchingSubHeadings()
        {
            var fields = new List<string>()
            {
                "multi_level_test"
            };
            var result = fileConverterService.GenerateHeadings(fields);
            var expected = getMultiLevelExpectedHeadings();

            Assert.AreEqual(expected[0].Name, result[0].Name);
            Assert.AreEqual(expected[0].SubHeadings[0].Name, result[0].SubHeadings[0].Name);
            Assert.AreEqual(expected[0].SubHeadings[0].SubHeadings[0].Name, result[0].SubHeadings[0].SubHeadings[0].Name);
        }

        [TestMethod]
        public void GenerateHeadings_WhenCalledWithFieldsWithSameNameAtDifferentLevels_ReturnsListOfHeadingsWithCorrectNesting()
        {
            var fields = new List<string>()
            {
                "level_test",
                "test_data"
            };
            var result = fileConverterService.GenerateHeadings(fields);
            var expected = getSameFieldDifferentLevelsHeadings();

            Assert.AreEqual(expected[0].Name, result[0].Name);
            Assert.AreEqual(expected[0].SubHeadings[0].Name, result[0].SubHeadings[0].Name);
            Assert.AreEqual(expected[1].Name, result[1].Name);
            Assert.AreEqual(expected[1].SubHeadings[0].Name, result[1].SubHeadings[0].Name);
        }


        private List<Heading> getExpectedHeadings()
        {
            return new List<Heading>()
            {
                new Heading()
                {
                    Name = "name"
                },
                new Heading()
                {
                    Name = "address",
                    SubHeadings = new List<Heading>()
                    {
                        new Heading()
                        {
                            Name = "line1"
                        },
                        new Heading()
                        {
                            Name = "line2"
                        }
                    }
                }
            };
        }

        private List<Heading> getSameFieldDifferentLevelsHeadings()
        {
            return new List<Heading>()
            {
                new Heading()
                {
                    Name = "level",
                    SubHeadings = new List<Heading>()
                    {
                        new Heading()
                        {
                            Name = "test"
                        }
                    }
                },
                new Heading()
                {
                    Name = "test",
                    SubHeadings = new List<Heading>()
                    {
                        new Heading()
                        {
                            Name = "data"
                        }
                    }
                }
            };
        }

        private List<Heading> getMultiLevelExpectedHeadings()
        {
            return new List<Heading>()
            {
                new Heading()
                {
                    Name = "multi",
                    SubHeadings = new List<Heading>()
                    {
                        new Heading()
                        {
                            Name = "level",
                            SubHeadings = new List<Heading>()
                            {
                                new Heading()
                                {
                                    Name = "test"
                                }
                            }
                        }
                    }
                }
            };
        }

        [TestCleanup]
        public void Dispose()
        {
            fileConverterService = null;
        }
    }
}
