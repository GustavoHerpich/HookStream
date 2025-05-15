using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Options;
using HookStream.API.Configurations;
using HookStream.API.Services.Impl;
using HookStream.API.Logging;

namespace HookStream.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GitHubWebhookController(
    BroadcastService _broadcast,
    IOptions<GitHubOptions> _options,
    ILogger<GitHubWebhookController> _logger) : ControllerBase
{
    private const string GITHUBSIGNATUREHEADER = "X-Hub-Signature-256";
    private readonly string webhookSecret = _options.Value.Secret;

    [HttpPost]
    public async Task<IActionResult> ReceiveWebhook()
    {
        if (!Request.Headers.TryGetValue(GITHUBSIGNATUREHEADER, out var signatureHeader))
            return Unauthorized("Missing signature");

        using var reader = new StreamReader(Request.Body);
        var body = await reader.ReadToEndAsync();

        if (!ValidateSignature(body, signatureHeader, webhookSecret))
        {
            _logger.InvalidSignature();
            return Unauthorized("Invalid signature");
        }

        try
        {
            _logger.PushReceived("GitHub", "https://github.com/GustavoHerpich/HookStream");
            await _broadcast.BroadcastAsync(body);
        }
        catch (Exception exception)
        {
            _logger.ProcessingError(exception);
            return StatusCode(500, "Erro interno");
        }

        return Ok();
    }

    private static bool ValidateSignature(string body, string signatureHeader, string secret)
    {
        var key = Encoding.UTF8.GetBytes(secret);
        using var hmac = new HMACSHA256(key);
        var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(body));
        var hashString = "sha256=" + Convert.ToHexString(hash).ToLowerInvariant();

        return hashString == signatureHeader;
    }
}
