namespace HookStream.API.Logging;

public static partial class LoggingExtension
{
    #region Information

    [LoggerMessage(LogLevel.Information, "Push recebido de {Pusher} no repositório {Repo}")]
    public static partial void PushReceived(
        this ILogger logger,
        string pusher,
        string repo);

    #endregion

    #region Warning

    [LoggerMessage(LogLevel.Warning, "Assinatura inválida")]
    public static partial void InvalidSignature(
        this ILogger logger);

    #endregion

    #region Error

    [LoggerMessage(LogLevel.Error, "Erro ao processar WebHook")]
    public static partial void ProcessingError(
        this ILogger logger,
        Exception exception);

    #endregion
}
