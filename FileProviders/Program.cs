using Microsoft.Extensions.FileProviders;
using System;
using System.IO;

namespace FileProviders
{
    class Program
    {
        static void Main(string[] args) {
            // getting the current base path in 2 different ways 
            // https://stackoverflow.com/questions/837488/how-can-i-get-the-applications-path-in-a-net-console-application
            var root = System.IO.Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]);
            var appRoot = Directory.GetCurrentDirectory();

            var provider = new PhysicalFileProvider(appRoot);
            var contents = provider.GetDirectoryContents(string.Empty);
            var fileInfo = provider.GetFileInfo("wwwroot/js/site.js");

            foreach (var content in contents) {
                Console.WriteLine(content.Name);
            }
            Console.WriteLine(root);
            Console.WriteLine(appRoot);
            Console.WriteLine("Hello World!");
        }
    }
}
