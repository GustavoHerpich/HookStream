<template>
  <transition name="fade-slide" appear>
    <div
      class="bg-white border border-gray-200 shadow-md rounded-2xl p-6 hover:shadow-lg transition duration-300 ease-in-out"
    >
      <div class="flex items-center gap-2 text-blue-700 font-semibold text-lg mb-1">
        <i class="i-lucide-git-branch w-5 h-5 text-blue-500" />
        {{ event.RepositoryName }}
      </div>

      <div class="text-sm text-gray-600">
        <strong>Por:</strong> {{ event.PusherName }}
      </div>

      <div class="mt-2 italic text-gray-700 text-sm">
        "{{ event.CommitMessage }}"
      </div>

      <div class="text-xs text-gray-400 mt-4">{{ formattedDate }}</div>
    </div>
  </transition>
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
.fade-slide-enter-active {
  transition: all 0.4s ease;
}
.fade-slide-enter-from {
  opacity: 0;
  transform: translateY(8px);
}
</style>
