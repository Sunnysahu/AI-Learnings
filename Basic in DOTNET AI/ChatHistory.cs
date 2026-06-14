using OllamaSharp;
using OllamaSharp.Models.Chat;

namespace Basic_in_DOTNET_AI
{
    public class ChatHistory
    {
        private readonly OllamaApiClient _ollama;
        private readonly List<Message> _messages = new();


        public ChatHistory()
        {
            _ollama = new OllamaApiClient(
                new Uri("http://localhost:11434"),
                "qwen3"
            );
        }

        public async Task RunAsync(string prompt)
        {

            _messages.Add(new Message
            {
                Role = "user",
                Content = prompt
            });

            var request = new ChatRequest
            {
                Model = "qwen3",
                Messages = _messages
            };

            string assistantResponse = "";

            await foreach (var chunk in _ollama.ChatAsync(request))
            {
                if (chunk?.Message?.Content is not null)
                {
                    Console.Write(chunk.Message.Content);
                    assistantResponse += chunk.Message.Content;
                }
            }


            _messages.Add(new Message
            {
                Role = "assistant",
                Content = assistantResponse
            });

            Console.WriteLine();
        }
    }
}
