using Microsoft.Extensions.AI;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OllamaSharp;
using System.Text;


// Implementation of the Ollama API client in C#

var ollama = new OllamaApiClient(
    new Uri("http://localhost:11434"),
    "qwen3");

Console.Write("Prompt: ");
var prompt = Console.ReadLine();

await foreach (var chunk in OllamaSharp.OllamaApiClientExtensions.GenerateAsync(
    ollama,
    prompt!))
{
    Console.Write(chunk?.Response);
}

Console.WriteLine();

/*
OllamaSharp
OllamaSharp.OllamaApiClientExtensions.GenerateAsync(...)
Microsoft.Extensions.AI
Microsoft.Extensions.AI.EmbeddingGeneratorExtensions.GenerateAsync(...)

So when you call:

ollama.GenerateAsync(...)

👉 C# says: “Which GenerateAsync do you mean?”

*/


// Implementation of the Ollama API client in C# with collecting the entire response into a string
// If you want to collect the entire response into a string:

//var sb = new StringBuilder();

//await foreach (var chunk in ollama.GenerateAsync(prompt!))
//{
//    sb.Append(chunk?.Response);
//}

//var response = sb.ToString();

//Console.WriteLine(response);


// Builder

//var builder = Host.CreateApplicationBuilder(args);

//builder.Services.AddChatClient(new OllamaApiClient(new Uri("http://localhost:11434"), "qwen3"));

//var app = builder.Build();

//Console.WriteLine("Hello There!!!");

//var chatClient = app.Services.GetRequiredService<IChatClient>();

//Console.WriteLine("Hello There 2!!!");

//var chatCompletion = await chatClient.GetResponseAsync("What is .NET ?");

//Console.WriteLine("Answer : " + chatCompletion);