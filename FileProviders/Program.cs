using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Primitives;
using System;
using System.IO;
using System.Threading.Tasks;

namespace FileProviders
{
    class Program
    {
        private static PhysicalFileProvider _fileProvider =
    new PhysicalFileProvider(Directory.GetCurrentDirectory());
        static void Main(string[] args) {
            // getting the current base path in 2 different ways 
            // https://stackoverflow.com/questions/837488/how-can-i-get-the-applications-path-in-a-net-console-application
            var root = System.IO.Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]);
            var appRoot = Directory.GetCurrentDirectory();

            var provider = new PhysicalFileProvider(appRoot);
            var contents = provider.GetDirectoryContents(string.Empty);

            foreach (var content in contents) {
                Console.WriteLine(content.Name);
            }
            Console.WriteLine(root);
            Console.WriteLine(appRoot);
            Console.WriteLine("Hello World!");

            while (true) {
                MainAsync().GetAwaiter().GetResult();
            }
        }


        private static async Task MainAsync() {

            Console.WriteLine(_fileProvider.GetFileInfo("quotes.txt").Exists);
            IChangeToken token = _fileProvider.Watch("quotes.txt");
            var tcs = new TaskCompletionSource<object>();

            token.RegisterChangeCallback(state =>
                ((TaskCompletionSource<object>)state).TrySetResult(null), tcs);

            await tcs.Task.ConfigureAwait(false);

            Console.WriteLine("quotes.txt changed");
        }
    }
}
