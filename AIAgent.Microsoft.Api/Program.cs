using AIAgent.Microsoft.Api.Data;
using AIAgent.Microsoft.Api.Repositories;
using AIAgent.Microsoft.Api.Services;
using AIAgent.Microsoft.Api.Tools;
using AIAgent.Microsoft.Api.Workflows;
using Microsoft.EntityFrameworkCore;
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

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddSingleton<IChatClient>(sp =>
{
    IConfiguration config = sp.GetRequiredService<IConfiguration>();

    ChatClient client = new(
        model: config["AzureOpenAI:Deployment"]!,
        credential: new ApiKeyCredential(config["AzureOpenAI:ApiKey"]!),
        options: new OpenAIClientOptions
        {
            Endpoint = new Uri(config["AzureOpenAI:Endpoint"]!)
        }
    );

    return client.AsIChatClient();
});

builder.Services.AddScoped<IChatHistoryRepository, ChatHistoryRepository>();

builder.Services.AddSingleton<IAgentFactory, AgentFactory>();

builder.Services.AddScoped<WorkflowService>();

builder.Services.AddScoped<TranslationWorkflow>();

builder.Services.AddScoped<CodeReviewWorkflow>();

builder.Services.AddScoped<ChatWorkflow>();

builder.Services.AddSingleton<WeatherTool>();

builder.Services.AddSingleton<ConversationSessionManager>();

builder.Services.AddSingleton<ApprovalService>();

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
