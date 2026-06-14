using OllamaSharp;
using System.Text;
using Microsoft.Extensions.AI;

namespace Basic_in_DOTNET_AI
{
    public class ChatHistroyCustomImplementation
    {
        private readonly OllamaApiClient _ollama;
        private readonly List<Microsoft.Extensions.AI.ChatMessage> _messages = new  List<Microsoft.Extensions.AI.ChatMessage>();

        public ChatHistroyCustomImplementation()
        {
            _ollama = new OllamaApiClient(
                new Uri("http://localhost:11434"),
                "qwen3"
            );
        }

        public async Task RunAsync(string prompt)
        {
            _messages.Add(
                new Microsoft.Extensions.AI.ChatMessage(Microsoft.Extensions.AI.ChatRole.User, prompt)
            );

            var historyPrompt = BuildConversationPrompt();

            var assistantResponse = new StringBuilder();

            await foreach (var chunk in ((IOllamaApiClient) _ollama).GenerateAsync(historyPrompt))
            {
                if (!string.IsNullOrEmpty(chunk?.Response))
                {
                    Console.Write(chunk.Response);
                    assistantResponse.Append(chunk.Response);
                }
            }

            _messages.Add(
                new Microsoft.Extensions.AI.ChatMessage(
                    Microsoft.Extensions.AI.ChatRole.Assistant,
                    assistantResponse.ToString()
                )
            );

            Console.WriteLine();
        }

        private string BuildConversationPrompt()
        {
            var sb = new StringBuilder();

            sb.AppendLine("You are a helpful assistant.");

            foreach (var message in _messages)
            {
                var role =
                    message.Role == Microsoft.Extensions.AI.ChatRole.User ? "User" :
                    message.Role == Microsoft.Extensions.AI.ChatRole.Assistant ? "Assistant" :
                    message.Role == Microsoft.Extensions.AI.ChatRole.System ? "System" :
                    "User";

                sb.AppendLine($"{role}: {message.Text}");
            }

            sb.AppendLine("Assistant:");

            return sb.ToString();
        }

    }
}
