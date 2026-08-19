using AI_Agent_Foundation.Agents.Chat;
using AI_Agent_Foundation.Agents.Research;
using AI_Agent_Foundation.Agents.Reviewer;
using AI_Agent_Foundation.AI;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.Options;
using OpenAI;
using OpenAI.Chat;
using Scalar.AspNetCore;
using System.ClientModel;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();


builder.Services.AddOptions<AIOptions>()
    .Bind(builder.Configuration.GetSection(AIOptions.SectionName))
    .ValidateOnStart();

builder.Services.AddSingleton<IChatClient>(sp =>
{
    var options = sp.GetRequiredService<IOptions<AIOptions>>().Value;

    ChatClient client = new(
    model: options.Model,
    credential: new ApiKeyCredential(options.ApiKey),
    options: new OpenAIClientOptions
    {
        Endpoint = new Uri(options.Endpoint)
    });

    return client.AsIChatClient();
});

builder.Services.AddSingleton<AgentFactory>();

builder.Services.AddSingleton<ChatAgent>();
builder.Services.AddSingleton<ResearchAgent>();
builder.Services.AddSingleton<ReviewerAgent>();

builder.Services.AddSingleton<ResearchReviewService>();

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
