<template>
  <v-card
    class="event-card"
    elevation="4"
  >
    <v-card-text class="card-text">
      <div class="header">
        <div class="repo-info">
          <v-icon icon="mdi-git" class="git-icon" />
          {{ event.RepositoryName }}
        </div>
        <span class="timestamp">{{ formattedDate }}</span>
      </div>

      <div class="pusher">
        <strong class="label">Por:</strong> {{ event.PusherName }}
      </div>

      <div class="commit-message">
        "{{ event.CommitMessage }}"
      </div>

      <div class="commit-link-container">
        <a
          :href="event.CommitUrl"
          target="_blank"
          rel="noopener noreferrer"
          class="commit-link"
          >Link para o commit
        </a>
      </div>

    </v-card-text>
  </v-card>
</template>

<script setup lang="ts">
import type { GitHubPushEvent } from '../models/GitHubPushEvent';
import { computed } from 'vue';

const props = defineProps<{ event: GitHubPushEvent }>();

const formattedDate = computed(() =>
  new Date(props.event.Timestamp).toLocaleString()
);
</script>

<style scoped>
.event-card {
  background-color: rgba(255 255 255 / 0.1);
  backdrop-filter: blur(18px);
  border-radius: 1rem;
  border: 1px solid rgba(59, 130, 246, 0.2);
  transition: all 0.25s ease-in-out;
  cursor: default;
}

.event-card:hover {
  transform: scale(1.01);
  box-shadow: 0 10px 15px rgba(59, 130, 246, 0.4);
}

.card-text {
  color: #f0f9ff; /* azul bem claro */
}

.header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-bottom: 0.5rem;
}

.repo-info {
  display: flex;
  align-items: center;
  gap: 0.5rem;
  font-weight: 600;
  color: #93c5fd; /* azul claro */
}

.git-icon {
  color: #60a5fa;
}

.timestamp {
  font-size: 0.75rem;
  color: #94a3b8; /* cinza azulado */
}

.pusher {
  font-size: 0.875rem;
  color: #bfdbfe; /* azul claro */
  margin-bottom: 0.5rem;
}

.label {
  color: #60a5fa;
  font-weight: 600;
}

.commit-message {
  font-style: italic;
  font-size: 0.875rem;
  color: #bfdbfe;
  border-left: 4px solid rgba(59, 130, 246, 0.5);
  padding-left: 0.75rem;
  margin-top: 0.5rem;
}

.commit-link-container {
  margin-top: 1.5rem;
}

.commit-link {
  display: block;
  width: 32%;
  padding: 0.5rem 1rem;
  background-color: rgba(59, 130, 246, 0.1);
  border-radius: 0.75rem;
  border: 1px solid rgba(59, 130, 246, 0.3);
  color: #60a5fa;
  text-decoration: underline;
  cursor: pointer;
  font-weight: 600;
  font-size: 0.9rem;
  transition: background-color 0.25s ease;
}

.commit-link:hover {
  background-color: rgba(59, 130, 246, 0.25);
  color: #93c5fd;
}

</style>
