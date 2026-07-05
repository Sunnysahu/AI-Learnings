using AI_Translation_Agent.Agents;
using AI_Translation_Agent.Agents.Definitions;
using AI_Translation_Agent.Configuration;
using AI_Translation_Agent.Orchestrators;
using AI_Translation_Agent.Services;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.Options;
using OpenAI;
using OpenAI.Chat;
using Scalar.AspNetCore;
using System.ClientModel;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddOpenApi();

builder.Services.Configure<GitHubModelsOptions>(builder.Configuration.GetSection("GitHubModels"));

builder.Services.AddSingleton<IChatClient>(serviceProvider =>
{
    var options = serviceProvider
        .GetRequiredService<IOptions<GitHubModelsOptions>>()
        .Value;

    ChatClient chatClient = new(
        model: options.Model,
        credential: new ApiKeyCredential(options.ApiKey),
        options: new OpenAIClientOptions
        {
            Endpoint = new Uri(options.Endpoint)
        });

    return chatClient.AsIChatClient();
});

builder.Services.AddScoped<IAIChatService, AIChatService>();

builder.Services.AddScoped<IAIAgentFactory, AIAgentFactory>();

builder.Services.AddScoped<IAgentDefinitionProvider, AgentDefinitionProvider>();

builder.Services.AddScoped<IAIAgentFactory, AIAgentFactory>();

builder.Services.AddScoped<IAgentOrchestrator, AgentOrchestrator>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();