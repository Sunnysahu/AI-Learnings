using Microsoft.Extensions.AI;
using Microsoft.Extensions.Options;
using OpenAI;
using OpenAI.Chat;
using System.ClientModel;

namespace AI_Agent_Foundation.AI;

public static class AIServiceExtensions
{
    public static IServiceCollection AddAI(this IServiceCollection services, IConfiguration configuration)
    {

        services
        .AddOptions<AIOptions>()
        .Bind(configuration.GetSection(AIOptions.SectionName))
        .ValidateOnStart();

        services.AddSingleton<IChatClient>(sp =>
        {

            var options = sp.GetRequiredService<IOptions<AIOptions>>().Value;

            ChatClient client = new(
                model: configuration["AzureOpenAI:Deployment"]!,
                credential: new ApiKeyCredential(configuration["AzureOpenAI:ApiKey"]!),
                options: new OpenAIClientOptions
                {
                    Endpoint = new Uri(configuration["AI:Endpoint"]!)
                });

            return client.AsIChatClient();
        });

        return services;
    }
}