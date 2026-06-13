using OllamaSharp;
using System.Text;


namespace Basic_in_DOTNET_AI
{
    public class EntireResponse
    {

        private readonly OllamaApiClient _ollama;

        public EntireResponse()
        {
            _ollama = new OllamaApiClient(
                new Uri("http://localhost:11434"),
                "qwen3"
            );
        }


        public async Task GenerateAsync(string prompt)
        {

            var responseBuilder = new StringBuilder();

            await foreach (var chunk in _ollama.GenerateAsync(prompt))
            {
                responseBuilder.Append(chunk?.Response);
            }
            
            Console.WriteLine(responseBuilder.ToString());
        }
    }
}
