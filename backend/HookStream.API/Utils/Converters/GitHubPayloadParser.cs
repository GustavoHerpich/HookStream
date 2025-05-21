using System.Text.Json;
using HookStream.API.Models;

namespace HookStream.API.Utils.Converters;

public class GitHubPayloadParser
{
    public GitHubPushEventDto ParsePushEvent(string json)
    {
        using var jsonDoc = JsonDocument.Parse(json);
        var root = jsonDoc.RootElement;

        string repositoryName = "";
        if (root.TryGetProperty("repository", out var repoProp))
            repositoryName = repoProp.GetProperty("name").GetString() ?? "";

        string pusherName = "";
        if (root.TryGetProperty("pusher", out var pusherProp))
            pusherName = pusherProp.GetProperty("name").GetString() ?? "";

        string commitMessage = "";
        string commitUrl = "";
        DateTime timestamp = DateTime.UtcNow;

        if (root.TryGetProperty("commits", out var commitsProp))
        {
            var commits = commitsProp.EnumerateArray();
            JsonElement? lastCommit = null;
            foreach (var commit in commits)
                lastCommit = commit;

            if (lastCommit.HasValue)
            {
                commitMessage = lastCommit.Value.GetProperty("message").GetString() ?? "";
                timestamp = lastCommit.Value.GetProperty("timestamp").GetDateTime();
                commitUrl = lastCommit.Value.GetProperty("url").GetString() ?? "";
            }
        }

        return new GitHubPushEventDto
        {
            RepositoryName = repositoryName,
            PusherName = pusherName,
            CommitMessage = commitMessage,
            Timestamp = timestamp,
            CommitUrl = commitUrl
        };
    }
}
