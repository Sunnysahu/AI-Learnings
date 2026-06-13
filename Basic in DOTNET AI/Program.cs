using Basic_in_DOTNET_AI;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OllamaSharp;
using System.Text;




internal class Program
{
    static async Task Main(string[] args)
    {

        Console.Write("Prompt: ");
        var prompt = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(prompt))
        {
            Console.WriteLine("Prompt cannot be empty.");
            return;
        }

        // Implementation of the Ollama API client in C#

        GenerateResponse app = new GenerateResponse();
        await app.RunAsync(prompt);

        // Implementation of the Ollama API client in C# with collecting the entire response into a string
        // If you want to collect the entire response into a string:

        //EntireResponse entireResponse = new EntireResponse();
        //await entireResponse.GenerateAsync(prompt);



    }
}