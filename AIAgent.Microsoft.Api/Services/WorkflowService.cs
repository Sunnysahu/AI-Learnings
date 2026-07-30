using AIAgent.Microsoft.Api.Workflows;
using Microsoft.Agents.AI.Workflows;
using Microsoft.Extensions.AI;

using ChatResponse = AIAgent.Microsoft.Api.Models.ChatResponse;

namespace AIAgent.Microsoft.Api.Services;

public sealed class WorkflowService
{
    private readonly TranslationWorkflow _translationWorkflow;
    private readonly CodeReviewWorkflow _codeReviewWorkflow;
    private readonly ChatWorkflow _chatWorkflow;
    private readonly ConversationSessionManager _sessionManager;

    public WorkflowService
    (
        TranslationWorkflow translationWorkflow,
        CodeReviewWorkflow codeReviewWorkflow,
        ChatWorkflow chatWorkflow,
        ConversationSessionManager sessionManager
    )
    {
        _translationWorkflow = translationWorkflow;
        _codeReviewWorkflow = codeReviewWorkflow;
        _chatWorkflow = chatWorkflow;
        _sessionManager = sessionManager; ;
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

    public async Task<ChatResponse> ExecuteChatAsync(Guid? sessionId, string message)
    {
        Guid id = sessionId ?? Guid.NewGuid();

        ConversationSession session = _sessionManager.GetSession(id);

        session.Messages.Add(new ChatMessage(ChatRole.User, message));

        Workflow workflow = _chatWorkflow.Build();

        List<ChatMessage> messages = [.. session.Messages];

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
        session.Messages.Add(new ChatMessage(ChatRole.Assistant, response));

        return new ChatResponse(id, response);
    }
}