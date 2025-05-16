import { onBeforeUnmount, onMounted, ref } from 'vue';
import type { GitHubPushEvent } from '../models/GitHubPushEvent';

export function useWebSocket(onMessage: (event: GitHubPushEvent) => void) {
  const socket = ref<WebSocket | null>(null);

  const connect = () => {
    socket.value = new WebSocket(import.meta.env.VITE_WS_URL);

    socket.value.onopen = () => {
      console.log('[WebSocket] Conectado com sucesso');
    };

    socket.value.onmessage = (event) => {
      try {
        const data: GitHubPushEvent = JSON.parse(event.data);
        console.log(event.data);
        onMessage(data);
      } catch (error) {
        console.error('[WebSocket] Erro ao processar mensagem:', error);
      }
    };

    socket.value.onclose = () => {
      console.warn('[WebSocket] Conexão encerrada. Tentando reconectar em 3s...');
      setTimeout(connect, 3000);
    };

    socket.value.onerror = (error) => {
      console.error('[WebSocket] Erro:', error);
      socket.value?.close();
    };
  };

  onMounted(() => {
    connect();
  });

  onBeforeUnmount(() => {
    socket.value?.close();
  });
}
