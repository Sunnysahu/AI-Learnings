using AI_Agent_Basic.Agents;
using AI_Agent_Basic.Configuration;
using Microsoft.Extensions.AI;
using OpenAI;
using Scalar.AspNetCore;
using System.ClientModel;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var aiOptions = builder.Configuration
    .GetSection("AI")
    .Get<AIOptions>() ?? throw new InvalidOperationException("AI configuration is missing.");


builder.Services.AddSingleton<IChatClient>(_ =>
{
    var client = new OpenAIClient(
        new ApiKeyCredential(aiOptions.ApiKey),
        new OpenAIClientOptions
        {
            Endpoint = new Uri(aiOptions.Endpoint)
        });

    return client.GetChatClient(aiOptions.Model).AsIChatClient();
});

builder.Services.AddSingleton<AgentFactory>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options.DarkMode = true;
        //options.Theme = ScalarTheme.Mars;
        options.WithTheme(ScalarTheme.DeepSpace);
        options.WithTitle("My AI Agent");
        options.DefaultHttpClient = new();
        options.WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient);
        options.WithSearchHotKey("k");
    });
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
