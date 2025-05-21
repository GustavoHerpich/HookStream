using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Options;
using HookStream.API.Configurations;
using HookStream.API.Services.Impl;
using HookStream.API.Logging;
using System.Text.Json;
using HookStream.API.Utils.SignatureValidator;
using HookStream.API.Utils.Converters;

namespace HookStream.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GitHubWebhookController(
    IBroadcastService _broadcast,
    IOptions<GitHubOptions> _options,
    ILogger<GitHubWebhookController> _logger,
    ISignatureValidator _signatureValidator,
    GitHubPayloadParser _parser) : ControllerBase
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

         if (!_signatureValidator.Validate(body, signatureHeader, webhookSecret))
        {
            _logger.InvalidSignature();
            return Unauthorized("Invalid signature");
        }

        try
        {
            var dto = _parser.ParsePushEvent(body);

            var messageToSend = JsonSerializer.Serialize(dto);

            _logger.PushReceived("GitHub", "https://github.com/GustavoHerpich/HookStream");
            await _broadcast.BroadcastAsync(messageToSend);
        }
        catch (Exception exception)
        {
            _logger.ProcessingError(exception);
            return StatusCode(500, "Erro interno");
        }

        return Ok();
    }
}
