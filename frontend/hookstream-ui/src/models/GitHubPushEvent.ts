export interface GitHubPushEvent {
  RepositoryName: string;
  PusherName: string;
  CommitMessage: string;
  Timestamp: string;
}