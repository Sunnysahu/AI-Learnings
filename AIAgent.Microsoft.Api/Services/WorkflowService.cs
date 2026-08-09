using AIAgent.Microsoft.Api.Models;
using AIAgent.Microsoft.Api.Repositories;
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
    private readonly IChatHistoryRepository _historyRepository;

    public WorkflowService
    (
        TranslationWorkflow translationWorkflow,
        CodeReviewWorkflow codeReviewWorkflow,
        ChatWorkflow chatWorkflow,
        ConversationSessionManager sessionManager,
        IChatHistoryRepository historyRepository
    )
    {
        _translationWorkflow = translationWorkflow;
        _codeReviewWorkflow = codeReviewWorkflow;
        _chatWorkflow = chatWorkflow;
        _sessionManager = sessionManager;
        _historyRepository = historyRepository;
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

        // Checks if Guid Exists In-Memory
        ConversationSession? session = _sessionManager.GetAll().FirstOrDefault(x => x.SessionId == id); 

        // Enters on if no In-Memory Conversation Exists
        if (session is null)
        {
            // Generate new Conversation Session if No In-Memory Conversation Exists
            session = new ConversationSession(id);

            // Check and Loads if that Guid Exists in DB
            List<ChatHistory> history = await _historyRepository.GetBySessionAsync(id);

            foreach (ChatHistory item in history)
            {
                ChatRole role = item.Role == "User" ? ChatRole.User : ChatRole.Assistant;

                // Add History to Newly Generated Conversation Session from Guid Exists in DB
                session.Messages.Add(new ChatMessage(role, item.Message)); 
            }

            // Add Conversation to In-Memory As Well
            _sessionManager.Add(session);
        }

        // If Guid Exists in In-Memory then Directly stores / Append the Message to In_memory Again
        session.Messages.Add(new ChatMessage(ChatRole.User, message));


        // Saving User MSG in DB
        await _historyRepository.AddAsync(new ChatHistory
        {
            SessionId = id,
            Role = "User",
            Message = message
        });

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
        
        // Appends Assistant Message in In-Memory too
        session.Messages.Add(new ChatMessage(ChatRole.Assistant, response));
        
        // Saving Assistant Message in DB
        await _historyRepository.AddAsync(new ChatHistory
        {
            SessionId = id,
            Role = "Assistant",
            Message = response
        });

        return new ChatResponse(id, response);
    }
}