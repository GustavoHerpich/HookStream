<template>
  <div class="min-h-screen bg-gradient-to-br from-slate-100 to-blue-50 p-8">
    <div class="max-w-4xl mx-auto">
      <h1 class="text-4xl font-bold mb-6 flex items-center gap-3 text-blue-900">
        <span class="text-5xl">🚀</span>
        <span>GitHub Webhook Stream</span>
      </h1>

      <div class="space-y-4">
        <GitHubEventCard
          v-for="(event, index) in eventStore.events"
          :key="index"
          :event="event"
        />
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { useEventStore } from '../stores/eventStore';
import { useWebSocket } from '../composables/useWebSocket';
import GitHubEventCard from '../components/GitHubEventCard.vue';

const eventStore = useEventStore();

useWebSocket((event) => {
  eventStore.addEvent(event);
});
</script>
