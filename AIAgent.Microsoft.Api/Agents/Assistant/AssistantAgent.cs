using AIAgent.Microsoft.Api.Tools;
using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;

namespace AIAgent.Microsoft.Api.Agents.Assistant;

public static class AssistantAgent
{
    public static ChatClientAgent Create(IChatClient chatClient, IServiceProvider serviceProvider)
    {
        WeatherTool weatherTool = serviceProvider.GetRequiredService<WeatherTool>();
        return new ChatClientAgent(
            chatClient,
            instructions:
            """
            You are a helpful AI assistant.

            When the user asks about:
                - calculations → use calculator tools.
                - date or time → use date/time tools.
                - weather → use the weather tool.

            Never invent tool results.
            Always call the appropriate tool.
            """,

            tools: [
                AIFunctionFactory.Create(CalculatorTool.Add),
                AIFunctionFactory.Create(CalculatorTool.Subtract),
                AIFunctionFactory.Create(CalculatorTool.Multiply),
                AIFunctionFactory.Create(CalculatorTool.Divide),

                AIFunctionFactory.Create(DateTimeTool.CurrentDate),
                AIFunctionFactory.Create(DateTimeTool.CurrentTime),
                AIFunctionFactory.Create(weatherTool.GetWeather)
            ]
        );
    }
}