# BarberApp - Plataforma de Agendamento de Barbearia com Assinatura

## 📋 Visão Geral

BarberApp é uma plataforma web que funciona como um Gympass/WelHub para barbearias. Permite que clientes encontrem e marquem cortes em barbearias próximas através de um mapa interativo do Google Maps, com sistema de pagamento por assinatura.

## 🎯 Funcionalidades

### Para Clientes
- 📍 Mapa com barbearias próximas (Google Maps)
- 📅 Agendamento de cortes
- 💳 Planos de assinatura mensal

### Para Administradores de Barbearias
- ⚙️ Gerenciar horários de funcionamento
- ✂️ Gerenciar serviços oferecidos
- 📅 Visualizar agendamentos

### Para Admin TI
- 👥 Gerenciar usuários
- 🏪 Gerenciar barbearias
- 📊 Relatórios e analytics

## 🛠️ Stack Tecnológico

### Frontend
- React 18 com TypeScript
- Tailwind CSS
- Vite
- React Router
- Zustand (State Management)
- React Hook Form
- Google Maps API

### Backend
- Node.js com Express
- TypeScript
- MongoDB
- JWT para autenticação
- Stripe para pagamentos

## 📦 Instalação

```bash
git clone https://github.com/joaovamorim/barber-app.git
cd barber-app
pnpm install
cp .env.example .env
pnpm dev
```

## 📁 Estrutura do Projeto

```
barber-app/
├── src/
│   ├── client/
│   │   ├── components/
│   │   ├── pages/
│   │   ├── api/
│   │   ├── store/
│   │   ├── types/
│   │   └── main.tsx
│   └── server/
│       └── index.ts
├── package.json
└── README.md
```

## 🚀 Scripts Disponíveis

```bash
pnpm dev              # Inicia servidor e cliente
pnpm dev:server       # Inicia apenas o servidor
pnpm dev:client       # Inicia apenas o cliente
pnpm build            # Build para produção
```
