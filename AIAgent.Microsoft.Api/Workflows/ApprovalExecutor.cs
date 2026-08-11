using AIAgent.Microsoft.Api.Models;
using Microsoft.Agents.AI.Workflows;

namespace AIAgent.Microsoft.Api.Workflows;

public sealed class ApprovalExecutor : Executor<object, ApprovalResult>
{
    public ApprovalExecutor() : base("HumanApproval")
    {
    }

    protected override ProtocolBuilder ConfigureProtocol(ProtocolBuilder protocolBuilder)
    {
        return protocolBuilder.SendsMessage<ApprovalRequestedMessage>();
    }

    public override async ValueTask<ApprovalResult> HandleAsync
    (
        object message,
        IWorkflowContext context,
        CancellationToken cancellationToken = default
    )
    {
        Guid approvalId = Guid.NewGuid();

        await context.SendMessageAsync(
            new ApprovalRequestedMessage(approvalId, message?.ToString() ?? string.Empty), 
            null, 
            cancellationToken
        );

        return new ApprovalResult(approvalId, true);
    }
}