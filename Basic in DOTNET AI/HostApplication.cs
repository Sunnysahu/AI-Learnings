using Microsoft.Extensions.AI;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OllamaSharp;
using System;

namespace Basic_in_DOTNET_AI
{
    public class HostApplication
    {
        public static async Task RunAsync(string[] args, string prompt)
        {
            var builder = Host.CreateApplicationBuilder(args);

            builder.Services.AddChatClient(
                new OllamaApiClient(
                    new Uri("http://localhost:11434"),
                    "qwen3"
                )
            );

            var app = builder.Build();

            var chatClient = app.Services.GetRequiredService<IChatClient>();

            var chatCompletion = await chatClient.GetResponseAsync(prompt);

            Console.WriteLine("Answer: " + chatCompletion);
        }
    }
}
