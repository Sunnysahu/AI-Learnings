using AIAgent.Microsoft.Api.Workflows;
using Microsoft.Agents.AI.Workflows;
using Microsoft.Extensions.AI;

namespace AIAgent.Microsoft.Api.Services;

public sealed class WorkflowService
{
    private readonly TranslationWorkflow _translationWorkflow;
    private readonly CodeReviewWorkflow _codeReviewWorkflow;
    private readonly ChatWorkflow _chatWorkflow;
    private readonly ConversationMemory _memory;

    public WorkflowService
    (
        TranslationWorkflow translationWorkflow,
        CodeReviewWorkflow codeReviewWorkflow,
        ChatWorkflow chatWorkflow,
        ConversationMemory memory
    )
    {
        _translationWorkflow = translationWorkflow;
        _codeReviewWorkflow = codeReviewWorkflow;
        _chatWorkflow = chatWorkflow;
        _memory = memory;
    }

    public async Task<string> ExecuteAsync(string input)
    {
        Workflow workflow = _translationWorkflow.Build();

        //Run run = await InProcessExecution.RunAsync(workflow, input);

        List<ChatMessage> messages = [new(ChatRole.User, input)];

        await using StreamingRun run = await InProcessExecution.RunStreamingAsync(workflow, messages);

        await run.TrySendMessageAsync(new TurnToken(emitEvents: true));

        List<ChatMessage> result = [];

        await foreach (WorkflowEvent evt in run.WatchStreamAsync())
        {
            Console.WriteLine(evt.GetType().Name);

            if (evt is AgentResponseUpdateEvent update)
            {
                Console.Write(update.Update.Text);
            }
            else if (evt is WorkflowOutputEvent output)
            {
                result = output.As<List<ChatMessage>>()!;
                break;
            }
        }

        return result.LastOrDefault()?.Text ?? "No output";

        //return "Workflow completed without output.";
    }

    public async Task<string> ExecuteCodeReviewAsync(string code)
    {
        Workflow workflow = _codeReviewWorkflow.Build();

        List<ChatMessage> messages =
        [
            new(ChatRole.User, code)
        ];

        await using StreamingRun run =
            await InProcessExecution.RunStreamingAsync(
                workflow,
                messages);

        await run.TrySendMessageAsync(new TurnToken(emitEvents: true));

        string output = "";

        await foreach (WorkflowEvent evt in run.WatchStreamAsync())
        {
            if (evt is AgentResponseUpdateEvent update)
            {
                output += update.Update.Text;
            }
        }

        return output;
    }

    public async Task<string> ExecuteChatAsync(string message)
    {
        Workflow workflow = _chatWorkflow.Build();

        _memory.AddUserMessage(message);

        List<ChatMessage> messages = [.. _memory.Messages];

        await using StreamingRun run = await InProcessExecution.RunStreamingAsync(workflow, messages);

        await run.TrySendMessageAsync(new TurnToken(emitEvents: true));

        string response = "";

        await foreach (WorkflowEvent evt in run.WatchStreamAsync())
        {
            if (evt is AgentResponseUpdateEvent update)
            {
                response += update.Update.Text;
            }
        }

        _memory.AddAssistantMessage(response);

        return response;
    }
}