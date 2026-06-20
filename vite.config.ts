import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'
import path from 'path'

export default defineConfig({
  plugins: [react()],
  resolve: {
    alias: {
      '@': path.resolve(__dirname, './src/client'),
      '@components': path.resolve(__dirname, './src/client/components'),
      '@pages': path.resolve(__dirname, './src/client/pages'),
      '@hooks': path.resolve(__dirname, './src/client/hooks'),
      '@store': path.resolve(__dirname, './src/client/store'),
      '@utils': path.resolve(__dirname, './src/client/utils'),
      '@types': path.resolve(__dirname, './src/client/types'),
      '@api': path.resolve(__dirname, './src/client/api'),
    },
  },
  server: {
    port: 5173,
    proxy: {
      '/api': {
        target: 'http://localhost:3000',
        changeOrigin: true,
      },
    },
  },
  build: {
    outDir: 'dist/client',
    sourcemap: true,
  },
})