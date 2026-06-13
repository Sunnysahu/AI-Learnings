using Microsoft.Extensions.AI;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OllamaSharp;
using System.Net.Mime;

var builder = Host.CreateApplicationBuilder();

builder.Services.AddChatClient(
    new OllamaApiClient(
        new Uri("http://localhost:11434"),
        "llava"
    )
);

var app = builder.Build();

var chatClient = app.Services.GetRequiredService<IChatClient>();

var imagePath = Path.Combine("images", "traffic.jpg");

if (!File.Exists(imagePath))
{
    Console.WriteLine($"Image not found: {imagePath}");
    return;
}

foreach (var item in Directory.GetFiles("images", "*.jpg"))
{
    var name = Path.GetFileNameWithoutExtension(item);

    var msg = new ChatMessage(ChatRole.User, "What in the Image");

    msg.Contents.Add(new DataContent
        (
        File.ReadAllBytes(item),
        MediaTypeNames.Image.Jpeg
        )
    );  

    var result = await chatClient.GetResponseAsync(msg);

    Console.WriteLine($"Image: {name} - Response: {result} \n");
}

//var message = new ChatMessage(ChatRole.User, "What's in the Image ? ");
//var message = new ChatMessage(ChatRole.User, "Count all the Humans in the image, even in the background");

//message.Contents.Add(
//    new DataContent(
//        File.ReadAllBytes(imagePath), 
//        MediaTypeNames.Image.Jpeg
//    )
//);

//var response = await chatClient.GetResponseAsync(message);

//Console.WriteLine(response);

