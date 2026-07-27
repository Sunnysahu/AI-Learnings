using AIAgent.Microsoft.Api.Services;
using AIAgent.Microsoft.Api.Tools;
using AIAgent.Microsoft.Api.Workflows;
using Microsoft.Extensions.AI;
using OpenAI;
using OpenAI.Chat;
using Scalar.AspNetCore;
using System.ClientModel;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddSingleton<IChatClient>(sp =>
{
    IConfiguration config = sp.GetRequiredService<IConfiguration>();

    ChatClient client = new(
        model: config["AI:Model"]!,
        credential: new ApiKeyCredential(config["AI:ApiKey"]!),
        options: new OpenAIClientOptions
        {
            Endpoint = new Uri(config["AI:Endpoint"]!)
        });

    return client.AsIChatClient();
});


builder.Services.AddSingleton<IAgentFactory, AgentFactory>();   

builder.Services.AddSingleton<TranslationWorkflow>();

builder.Services.AddSingleton<WorkflowService>();

builder.Services.AddSingleton<CodeReviewWorkflow>();

builder.Services.AddSingleton<ChatWorkflow>();

builder.Services.AddSingleton<WeatherTool>();

builder.Services.AddSingleton<ConversationMemory>();

builder.Services.AddHttpClient<WeatherService>();

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
