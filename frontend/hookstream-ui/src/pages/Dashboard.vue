<template>
  <v-app>
    <v-main class="dashboard-main">
      <v-container class="dashboard-container">
        <v-card class="header-card">
          <div class="header-content">
            <div class="title">
              <v-icon icon="mdi-access-point-network" class="icon" />
              GitHub Webhook Stream
            </div>
            <v-chip
              :color="eventStore.events.length > 0 ? 'green' : 'grey'"
              variant="elevated"
              size="small"
            >
              {{ eventStore.events.length > 0 ? 'Conectado' : 'Aguardando Webhook...' }}
            </v-chip>
          </div>
          <p class="subtitle">Receba commits em tempo real do GitHub via WebHook.</p>
        </v-card>

        <v-slide-y-transition group>
          <div v-if="eventStore.events.length" class="events-grid">
            <GitHubEventCard
              v-for="(event, index) in eventStore.events"
              :key="index"
              :event="event"
            />
          </div>
          <v-alert
            v-else
            type="info"
            variant="outlined"
            class="no-events-alert"
            text="Nenhum evento recebido ainda. Aguarde commits para este repositório."
          />
        </v-slide-y-transition>
      </v-container>
    </v-main>
  </v-app>
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

<style scoped>
.dashboard-main {
  background: linear-gradient(to bottom right, #0f172a, #1e293b, #0f172a);
  min-height: 100vh;
  color: white;
}

.dashboard-container {
  padding-top: 2.5rem;
  padding-bottom: 2.5rem;
  padding-left: 1rem;
  padding-right: 1rem;
}

.header-card {
  background-color: rgba(255 255 255 / 0.05);
  backdrop-filter: blur(12px);
  border-radius: 1rem;
  padding: 1.5rem 1.5rem 1.5rem 1.5rem;
  margin-bottom: 1.5rem;
}

.header-content {
  display: flex;
  justify-content: space-between;
  align-items: center;
  flex-wrap: wrap;
  gap: 0.5rem;
}

.title {
  display: flex;
  align-items: center;
  gap: 0.75rem;
  font-weight: 600;
  font-size: 1.25rem;
  color: #60a5fa; /* azul claro */
  min-width: 0;
  flex: 1 1 auto;
  padding-left: 0.5rem;
}

.icon {
  color: #3b82f6; /* azul padrão */
}

.subtitle {
  margin-top: 0.25rem;
  font-size: 0.875rem;
  color: #cbd5e1; /* cinza claro */
  padding-left: 0.5rem;
}

.events-grid {
  display: grid;
  grid-template-columns: repeat(1, 1fr);
  gap: 1rem;
}

@media (min-width: 640px) {
  .events-grid {
    grid-template-columns: repeat(2, 1fr);
  }
}

.no-events-alert {
  margin-top: 2.5rem;
}
</style>
