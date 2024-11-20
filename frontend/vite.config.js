import { defineConfig } from 'vite';
import vue from '@vitejs/plugin-vue';

export default defineConfig({
  plugins: [vue()],
  base: '/', // Укажите базовый путь, если приложение размещается в корне
  server: {
    port: 61125, // Порт для Frontend
    proxy: {
      '/api': {
        target: 'https://localhost:7128', // Адрес вашего Backend
        changeOrigin: true,
        secure: false,
      },
    },
  },
});
