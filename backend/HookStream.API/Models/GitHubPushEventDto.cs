namespace HookStream.API.Models;

public class GitHubPushEventDto
{
    public string RepositoryName { get; set; } = string.Empty;
    public string PusherName { get; set; } = string.Empty;
    public string CommitMessage { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; }
    public string CommitUrl { get; set; } = string.Empty;
}
