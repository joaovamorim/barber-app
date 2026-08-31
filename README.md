# Saas Scheduler - Fase 1–3 (Fundação e Autenticação)

Objetivo: criar a fundação do SaaS (solution + projetos, Docker Compose, EF Core, Serilog, HealthChecks, OpenAPI) e implementar autenticação multi-tenant básica com refresh tokens para desenvolvimento.

Aviso: este repositório contém dados e credenciais de exemplo para desenvolvimento (fake data). Não use essas credenciais em produção.

Versões alvo (ver VERSIONS.md) e dependências estão especificadas nos csproj.

Como rodar localmente (desenvolvimento)

1. Pré-requisitos
- .NET 10 SDK instalado
- Docker & Docker Compose
- (Opcional) dotnet-ef global tool para gerar migrations localmente

2. Configurar variáveis de ambiente
- Copie `.env.example` para `.env` e ajuste valores:
  - ConnectionStrings__DefaultConnection (ex.: Host=localhost;Port=5432;Database=saas_dev;Username=saas;Password=saas_password)
  - JWT__Secret = definir secret forte para desenvolvimento (ex.: dev_secret_very_strong)

3. Iniciar infraestrutura em Docker

```bash
docker-compose up -d
```

Observação: Redis e RabbitMQ estão comentados por padrão neste compose. Habilite apenas quando necessário.

4. Gerar e aplicar migrations

Este repositório chama `Database.Migrate()` no startup para aplicar migrations automaticamente em desenvolvimento. No entanto, as migrations precisam existir no projeto. Você pode:

A) Gerar migrations localmente (recomendado se quiser controlar o arquivo):

```bash
# instalar dotnet-ef se necessário
dotnet tool install --global dotnet-ef

dotnet restore

# gerar migrations
dotnet ef migrations add InitialCreate -p src/Barber.App.Infrastructure -s src/Barber.App.Api

dotnet ef database update -p src/Barber.App.Infrastructure -s src/Barber.App.Api
```

B) Ou permitir que o startup aplique as migrations (apenas se as migrations já estiverem commited no repositório):

```bash
dotnet run --project src/Barber.App.Api
```

5. Executar a API

```bash
dotnet run --project src/Barber.App.Api
```

6. Dados fakes (seed de desenvolvimento)

Ao iniciar, o seed criará (em Development):
- Tenant: `barbearia-demo` (slug)
- Roles: Owner, Admin, Manager, Receptionist, Professional
- Owner user: `owner@barbearia.demo` / `DevPass123!` (atributo TenantId apontando para `barbearia-demo`)

Use estas credenciais apenas em ambiente de desenvolvimento.

7. Endpoints úteis (dev)
- Health: GET /health  (use header `X-Tenant-Slug: barbearia-demo` para testar tenant middleware)
- Swagger (dev): /swagger/index.html

Autenticação - exemplos

Register (necessário header X-Tenant-Slug):

POST /api/auth/register
Headers: Content-Type: application/json, X-Tenant-Slug: barbearia-demo
Body:
{
  "email": "user@example.com",
  "password": "YourPass123!",
  "displayName": "User Demo"
}

Login:
POST /api/auth/login
Body:
{
  "email": "owner@barbearia.demo",
  "password": "DevPass123!"
}

Refresh:
POST /api/auth/refresh
Body:
{ "refreshToken": "<refresh-token-here>" }

Revoke:
POST /api/auth/revoke
Body:
{ "refreshToken": "<refresh-token-here>" }

8. Testes
- Unit tests:
  dotnet test tests/UnitTests

- Integration tests (se implementados):
  dotnet test tests/IntegrationTests

9. Observações de segurança (desenvolvimento -> produção)
- Nunca armazene secrets no repositório (.env included) — use Secret Manager / Vault in production.
- JWT__Secret deve ser forte e armazenado em ambiente seguro.
- Refresh tokens são armazenados como hash (SHA-256) in the DB in this branch — do not store plaintext tokens in production.
- Implementar rate limiting, captcha, monitoring, and MFA for production readiness.


Contato
- Repositório: https://github.com/joaovamorim/barber-app
