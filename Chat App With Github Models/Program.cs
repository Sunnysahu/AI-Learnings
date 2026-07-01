using Microsoft.Extensions.AI;
using OpenAI;
using OpenAI.Chat;
using System.ClientModel;
using ChatMessage = Microsoft.Extensions.AI.ChatMessage;

IChatClient chatClient = new ChatClient(
    "gpt-4.1-mini",
    new ApiKeyCredential("Your Github Model Token"),
    new OpenAIClientOptions { Endpoint = new Uri("https://models.github.ai/inference"), }
).AsChatClient();

Console.ForegroundColor = ConsoleColor.Yellow;
Console.WriteLine("GPT 4.1 Mini Chat - Type 'exit' to quit");
Console.WriteLine();

List<ChatMessage> chatHistroy = new();

while (true)
{
    Console.ForegroundColor = ConsoleColor.White;
    Console.Write("You: ");
    string userinput = Console.ReadLine();

    if (string.IsNullOrEmpty(userinput)) continue;

    if (userinput.Equals("exit", StringComparison.OrdinalIgnoreCase)) break;

    chatHistroy.Add(new ChatMessage(ChatRole.User, userinput));

    Console.ForegroundColor = ConsoleColor.Green;
    Console.Write("Assistant : ");

    string assistantResponse = string.Empty;

    await foreach (var response in chatClient.CompleteStreamingAsync(chatHistroy))
    {
        Console.Write(response.Text);
        assistantResponse += response.Text;
    }

    chatHistroy.Add(new ChatMessage(ChatRole.Assistant, assistantResponse));
}