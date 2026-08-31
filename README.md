# Saas Scheduler - Fase 1 (Fundação)

Objetivo: criar a fundação do SaaS (solution + projetos, Docker Compose, EF Core, Serilog, HealthChecks, OpenAPI).

Requisitos principais (neste esqueleto):
- .NET 10 / ASP.NET Core 10 (TargetFramework: net10.0)
- EF Core 10 (Npgsql provider)
- Serilog
- Swagger / HealthChecks
- Multi-tenant preparado (Tenant entity + ITenantProvider)
- Docker Compose de desenvolvimento com Postgres (+ Redis, RabbitMQ opcionais)

Como rodar localmente (dev):
1. Copie `.env.example` para `.env` e ajuste valores.
2. docker-compose up -d
3. Configurar connection string (DefaultConnection) em appsettings.Development.json ou variáveis de ambiente.
4. Rodar migrations:
   dotnet ef migrations add InitialCreate -p src/Saas.Scheduler.Infrastructure -s src/Saas.Scheduler.Api
   dotnet ef database update -p src/Saas.Scheduler.Infrastructure -s src/Saas.Scheduler.Api
5. Rodar API:
   dotnet run --project src/Saas.Scheduler.Api

Notas:
- Multi-tenant: TenantId será resolvido por middleware/serviço (implementação planeada na FASE 2).
- Não versione secrets; utilize secret manager em produção.
- Próxima entrega: criação de migrations iniciais, seed de desenvolvimento (Barbearia Demo), implementação do TenantProvider middleware, e configuração do ASP.NET Identity (Fase 3).
