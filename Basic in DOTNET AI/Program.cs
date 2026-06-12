using OllamaSharp;
using System.Text;

var ollama = new OllamaApiClient(new Uri("http://localhost:11434"),"qwen3");

Console.Write("Prompt: ");

var prompt = Console.ReadLine();

await foreach (var chunk in ollama.GenerateAsync(prompt!))
{
    Console.Write(chunk?.Response);
}

Console.WriteLine();


// If you want to collect the entire response into a string:

//var sb = new StringBuilder();

//await foreach (var chunk in ollama.GenerateAsync(prompt!))
//{
//    sb.Append(chunk?.Response);
//}

//var response = sb.ToString();

//Console.WriteLine(response);

