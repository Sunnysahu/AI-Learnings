using AIAgent.Microsoft.Api.Models;
using AIAgent.Microsoft.Api.Services;
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


builder.Services.Configure<AIOptions>(builder.Configuration.GetSection(AIOptions.SectionName));

builder.Services.AddSingleton<IChatClient>(sp =>
{
    AIOptions options = sp.GetRequiredService<Microsoft.Extensions.Options.IOptions<AIOptions>>().Value;

    ChatClient client = new(
        model: options.Model,
        credential: new ApiKeyCredential(options.ApiKey),
        options: new OpenAIClientOptions
        {
            Endpoint = new Uri(options.Endpoint)
        }
    );

    return client.AsIChatClient();
});

builder.Services.AddScoped<IChatService, ChatService>();

builder.Services.AddScoped<IAgentService, AgentService>();

builder.Services.AddSingleton<IAgentFactory, AgentFactory>();

builder.Services.AddSingleton<IConversationManager, ConversationManager>();

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
