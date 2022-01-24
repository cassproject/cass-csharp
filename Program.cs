using System;

namespace cass_csharp
{
    class Program
    {
        static void Main(string[] args)
        {
            //Make sure to run npm install in a terminal before executing this.
            var library = new CassLibrary(
                options =>
                {
                    // Puts node_modules in directory scope.
                    options.ProjectPath = AppContext.BaseDirectory;
                },
                // and script files
                "js"
            );

            //Also ensure the go.js file is set to copy to output folder always.
            var result = library.Func("go.js","goGetSomeData").Result;

            Console.WriteLine(result);
        }
    }
}
