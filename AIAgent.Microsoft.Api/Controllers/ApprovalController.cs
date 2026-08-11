using AIAgent.Microsoft.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AIAgent.Microsoft.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public sealed class ApprovalController : ControllerBase
{
    private readonly ApprovalService _approvalService;

    public ApprovalController(ApprovalService approvalService) => _approvalService = approvalService;

    [HttpPost("{id:guid}/approve")]
    public IActionResult Approve(Guid id)
    {
        _approvalService.Approve(id);

        return Ok(new
        {
            approvalId = id,
            approved = true
        });
    }

    [HttpPost("{id:guid}/reject")]
    public IActionResult Reject(Guid id)
    {
        _approvalService.Reject(id);

        return Ok(new
        {
            approvalId = id,
            approved = false
        });
    }

    [HttpGet("{id:guid}")]
    public IActionResult GetDecision(Guid id)
    {
        bool? decision = _approvalService.GetDecision(id);

        return Ok(new
        {
            approvalId = id,
            decision
        });
    }
}
