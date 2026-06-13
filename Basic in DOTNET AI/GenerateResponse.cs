using OllamaSharp;
using System;


namespace Basic_in_DOTNET_AI
{
    public class GenerateResponse
    {
        private readonly OllamaApiClient _ollama;

        public GenerateResponse()
        {
            _ollama = new OllamaApiClient(
                new Uri("http://localhost:11434"),
                "qwen3"
            );
        }

        public async Task RunAsync(string prompt)
        {

            await foreach (var chunk in _ollama.GenerateAsync(prompt))
            {
                Console.Write(chunk?.Response);
            }

            Console.WriteLine();
        }
    }
}
