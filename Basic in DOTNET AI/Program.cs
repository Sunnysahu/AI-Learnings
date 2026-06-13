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



    }
}





//var builder = Host.CreateApplicationBuilder(args);

//builder.Services.AddChatClient(new OllamaApiClient(new Uri("http://localhost:11434"), "qwen3"));

//var app = builder.Build();

//Console.WriteLine("Hello There!!!");

//var chatClient = app.Services.GetRequiredService<IChatClient>();

//Console.WriteLine("Hello There 2!!!");

//var chatCompletion = await chatClient.GetResponseAsync("What is .NET ?");

//Console.WriteLine("Answer : " + chatCompletion);