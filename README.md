# Barber App - Gympass para Barbearias

Uma plataforma de assinatura para barbearias, permitindo que clientes encontrem e marquem cortes em barbearias participantes através de um mapa interativo.

## 🎯 Funcionalidades

- **Autenticação Multi-Papel**: Cliente, Comerciante (Barbearia), Admin (TI)
- **Mapa Interativo**: Google Maps com localização de barbearias próximas
- **Agendamento**: Marcar cortes em qualquer barbearia
- **Planos de Assinatura**: Diferentes modalidades de pagamento
- **Dashboard do Cliente**: Visualizar agendamentos e plano ativo
- **Painel Comerciante**: Gerenciar horários e agendamentos
- **Painel Admin**: Gerenciar usuários, barbearias e planos
- **Responsivo**: Totalmente otimizado para mobile e desktop

## 🛠️ Stack Tecnológico

### Backend
- Node.js com TypeScript
- Express.js
- PostgreSQL (banco de dados)
- JWT para autenticação
- Stripe/PagSeguro para pagamentos

### Frontend
- React 18+ com TypeScript
- Vite (build tool)
- TailwindCSS (styling)
- Google Maps API
- React Router (navegação)
- Axios (HTTP client)

## 📁 Estrutura do Projeto

```
barber-app/
├── backend/
│   ├── src/
│   │   ├── controllers/
│   │   ├── routes/
│   │   ├── middleware/
│   │   ├── services/
│   │   ├── models/
│   │   ├── config/
│   │   └── server.ts
│   ├── package.json
│   ├── tsconfig.json
│   └── .env.example
├── frontend/
│   ├── src/
│   │   ├── components/
│   │   ├── pages/
│   │   ├── services/
│   │   ├── hooks/
│   │   ├── types/
│   │   ├── App.tsx
│   │   └── main.tsx
│   ├── package.json
│   ├── tsconfig.json
│   ├── vite.config.ts
│   └── tailwind.config.js
└── README.md
```

## 🚀 Como Começar

```bash
# Backend
cd backend
pnpm install
pnpm dev

# Frontend (em outro terminal)
cd frontend
pnpm install
pnpm dev
```

## 📝 Variáveis de Ambiente

Veja `.env.example` em cada pasta para as variáveis necessárias.

## 👨‍💻 Desenvolvedor

João Vamorim
