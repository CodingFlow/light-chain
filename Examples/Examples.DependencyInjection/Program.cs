// See https://aka.ms/new-console-template for more information
using Examples.DependencyInjection;

Console.WriteLine("Starting App...");

var result = new Startup().Run();

Console.WriteLine($"Result is: {result}");
