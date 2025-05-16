import { defineStore } from 'pinia';
import type { GitHubPushEvent } from '../models/GitHubPushEvent';

export const useEventStore = defineStore('eventStore', {
  state: () => ({
    events: [] as GitHubPushEvent[]
  }),

  actions: {
    addEvent(event: GitHubPushEvent) {
      this.events.unshift(event);
    },

    clearEvents() {
      this.events = [];
    }
  }
});
