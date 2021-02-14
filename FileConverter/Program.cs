using Autofac;
using FileConverter.FileConversionServices;
using FileConverter.Services;
using System;

namespace FileConverter
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = GetBuildContainer();
            var fileConverter = container.Resolve<ICsvToJsonFileConverterService>();

            var fileToConvertPath = @"C:\Users\JackTyler\source\repos\FileConverter\FileConverter\Files\people.csv";

            try
            {
                var result = fileConverter.Convert(fileToConvertPath);

                Console.WriteLine(result);
            }
            catch (NotImplementedException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception)
            {
                Console.WriteLine("An unexpected error has occurred.");
            }

            Console.ReadLine();
        }

        private static IContainer GetBuildContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<CsvToJsonFileConverterService>().As<ICsvToJsonFileConverterService>();
            builder.RegisterType<HeadingService>().As<IHeadingService>();
            return builder.Build();
        }
    }
}
